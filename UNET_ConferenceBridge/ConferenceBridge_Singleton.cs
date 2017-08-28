using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNET_Classes;
using PJSUA2Implementation;

namespace UNET_ConferenceBridge
{
    public sealed class ConferenceBridge_Singleton
    {
        private static ConferenceBridge_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        private ConferenceBridge_Singleton() { }

        public List<Exercise> Exercises = new List<Exercise>();
        public List<Role> Roles = new List<Role>();
        public List<UNET_Classes.Radio> Radios = new List<UNET_Classes.Radio>();
        public List<PJSUA2Implementation.SIP.SIPCall> ActiveCalls = new List<PJSUA2Implementation.SIP.SIPCall>();
  
        public bool TraineeStatusChanged = false;
        public bool NoiseLevelChanged = false;

        //Local sound settings
        public int LeftShadow = 5;
        public int RightShadow = 5;
        public int LeftVolume = 5;
        public int RightVolume = 5;
        public int LeftESM = 5;
        public int RightESM = 5;
        public int MicGain = 5;

        public static ConferenceBridge_Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ConferenceBridge_Singleton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
