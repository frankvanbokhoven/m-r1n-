using System;
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