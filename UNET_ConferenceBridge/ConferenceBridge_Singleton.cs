using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNET_Classes;
using PJSUA2Implementation;
using UNET_Theming;

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

        //needed to keep track of weather the headphone is attached or the ptt is pressed
        public bool PTTPressed = false;
        public bool HeadphoneAttached = false;

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

                            try
                            {
                                ///haal de settings op uit de registry. Dit mislukt de allereerste keer
                                instance.LeftShadow = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"LeftShadow", "5"));
                                instance.RightShadow = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"RightShadow", "5"));
                                instance.LeftESM = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"LeftESM", "5"));
                                instance.RightESM = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"RightESM", "5"));
                                instance.MicGain = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"MicGain", "5"));
                                instance.LeftVolume = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"LeftVolume", "5"));
                                instance.RightVolume = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"RightVolume", "5"));
                            }
                            catch(Exception ex)
                            {
                                string messages = ex.Message;
                            }
                        }
                    }
                }
                return instance;
            }
        }


     }
}
