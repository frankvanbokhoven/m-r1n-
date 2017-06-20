using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNET_Classes;

namespace UNET_Service
{

    public sealed class UNET_Singleton
    {
        private static UNET_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        private UNET_Singleton() { }

        public List<Exercise> Exercises = new List<Exercise>();
        public List<Role> Roles = new List<Role>();
        public List<Radio> Radios = new List<Radio>();
        public List<Instructor> Instructors = new List<Instructor>();
        public List<Trainee> Trainees = new List<Trainee>();
        public List<Platform> Platforms = new List<Platform>();
        public List<CurrentInfo> CurrentInfoList = new List<CurrentInfo>();
        public List<SIPStatusMessage> SIPStatusMessageList = new List<SIPStatusMessage>();

        public bool TraineeStatusChanged = false;


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