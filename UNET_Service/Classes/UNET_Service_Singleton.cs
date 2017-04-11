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

        public Exercise[] Exercises;
        public Role[] Roles;
        public Radio[] Radios;
        public Instructor[] Instructors;
        public Trainee[] Trainees;
        public Platform[] Platforms;

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
    }
}