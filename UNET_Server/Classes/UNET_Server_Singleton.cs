﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNET_Server.Classes
{

    public sealed class UNET_Server_Singleton
    {
        private static UNET_Server_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        private UNET_Server_Singleton() { }

        public Exercise[] Exercises;
        public Role[] Roles;
        public Radio[] Radios;
        public Instructor[] Instructors;
        public Trainee[] Trainees;
        public Platform[] Platforms;

        public static UNET_Server_Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UNET_Server_Singleton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}