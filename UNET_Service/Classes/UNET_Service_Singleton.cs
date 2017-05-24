using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNET_Service.Classes
{

    public sealed class UNET_Service_Singleton
    {
        private static UNET_Service_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        private UNET_Service_Singleton() { }

        public List<Exercise> Exercises = new List<Exercise>();
        public List<Role> Roles = new List<Role>();
        public List<Radio> Radios = new List<Radio>();
        public List<Instructor> Instructors = new List<Instructor>();
        public List<Trainee> Trainees = new List<Trainee>();
        public List<Platform> Platforms = new List<Platform>();
        public List<Classes.CurrentInfo> CurrentInfoList = new List<Classes.CurrentInfo>();

        public bool TraineeStatusChanged = false;

        public static UNET_Service_Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UNET_Service_Singleton();                         

                        }
                    }
                }
                return instance;
            }
        }

        //private UNET_Service_Singleton()
        //{
        //    Exercises = new List<Exercise>();
        //}
    }
}