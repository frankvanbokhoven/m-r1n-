using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer.Classes
{
 
    /// <summary>
    /// this single-instance class takes care of the in-memory-database.
    /// </summary>
    public sealed class UNET_Singleton
    {
        private static UNET_Singleton instance = null;
        // adding locking object
        private static readonly object syncRoot = new object();
        private UNET_Singleton() { }

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
