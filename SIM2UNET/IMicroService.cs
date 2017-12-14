using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM2UNET
{
    public interface IMicroService
    {
        void Start();
        void Stop();
    }

    public class ExampleService : IMicroService
    {
        public void Start()
        {
          //  this.StartBase();
            Timer.Start("Poller", 1000, () =>
            {
                Console.WriteLine("Polling at {0}\n", DateTime.Now.ToString("o"));
            },
            (e) =>
            {
                Console.WriteLine("Exception while polling: {0}\n", e.ToString());
            });
            Console.WriteLine("I started");
        }

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("I stopped");
        }
    }
}
