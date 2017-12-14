using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM2UNET
{
    public class SIMService : IMicroService
    {

      //  udpbinding
        public void Start()
        {
            Console.WriteLine("I started");
        }

        public void Stop()
        {
            Console.WriteLine("I stopped");
        }
    }
}
