using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNET_Classes;
using System.Collections.ObjectModel;
using System.Collections;

namespace UNET_Service
{
    public enum PTTuser { puTrainee, puInstructor}
    //this struct holds the PTT info
    public class PTTcaller
    {
        public int ID;
        public PTTuser User;
        public DateTime PTTDateTime;
    }

    public sealed class UNET_Singleton
    {
        private static UNET_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        //all the ObservableCollections below are changed from lists to observablecollections. this way, 
        //we can attach an event to them and know when something has changed in them.
        public ObservableCollection<Exercise> Exercises = new ObservableCollection<Exercise>();
        public ObservableCollection<Role> Roles = new ObservableCollection<Role>();
        public ObservableCollection<Radio> Radios = new ObservableCollection<Radio>();
        public ObservableCollection<Instructor> Instructors = new ObservableCollection<Instructor>();
        public ObservableCollection<Trainee> Trainees = new ObservableCollection<Trainee>();
        public ObservableCollection<Platform> Platforms = new ObservableCollection<Platform>();
        public ObservableCollection<CurrentInfo> CurrentInfoList = new ObservableCollection<CurrentInfo>();
        public ObservableCollection<SIPStatusMessage> SIPStatusMessageList = new ObservableCollection<SIPStatusMessage>();
        public ObservableCollection<Assist> Assists = new ObservableCollection<Assist>();


        public DateTime PendingChanges; //property is set whenever something anywhere in the singleton model is changed
        public Dictionary<string, IBroadcastorCallBack> clients = new Dictionary<string, IBroadcastorCallBack>();


        /// <summary>
        /// when a trainee or instructor does PTT, enqueue this PTT and handle it
        /// </summary>
        public Queue PTTQueue = new Queue();

        public bool TraineeStatusChanged = false;
        public bool NoiseLevelChanged = false;

        private UNET_Singleton()
        {
            //We attach the collectionchangedevent to the lists, to keep track of any changes
            //we only need one change event for all lists
            Exercises.CollectionChanged += Exercises_CollectionChanged;
            Roles.CollectionChanged += Exercises_CollectionChanged;
            Radios.CollectionChanged += Exercises_CollectionChanged;
            Instructors.CollectionChanged += Exercises_CollectionChanged;
            Trainees.CollectionChanged += Exercises_CollectionChanged;
            Platforms.CollectionChanged += Exercises_CollectionChanged;
            CurrentInfoList.CollectionChanged += Exercises_CollectionChanged;
            SIPStatusMessageList.CollectionChanged += Exercises_CollectionChanged;
            Assists.CollectionChanged += Exercises_CollectionChanged;

        }

        #region Singleton Change Events
        /// <summary>
        /// whenever this event is fired, then appearantly something has changed in one of the lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exercises_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PendingChanges = DateTime.Now;
        }

        #endregion

        public static UNET_Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UNET_Singleton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}