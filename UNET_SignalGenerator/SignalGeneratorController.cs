using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace UNET_SignalGenerator
{
    public class SignalGeneratorController
    {
        private readonly IWavePlayer driverOut;

        private SignalGenerator wg;

        public SignalGeneratorController()
        {
   
            // Init Audio
            driverOut = new WaveOutEvent();
            //driverOut = new AsioOut(0);
            wg = new SignalGenerator();

                 // Init Driver Audio
            driverOut.Init(wg);
         }

        public void Start()
        {
            if (driverOut != null)
            {
                driverOut.Play();
            }
        }

        public void Stop()
        {
            if (driverOut != null)
            {
                driverOut.Stop();             
            }
        }

        // Clean DriverOut
        private void Cleanup()
        {
            if (driverOut != null)
                driverOut.Stop();

            wg = null;

            if (driverOut != null)
            {
                driverOut.Dispose();
            }
        }
    }
}


    

