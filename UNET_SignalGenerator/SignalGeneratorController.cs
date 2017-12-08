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
             wg = new SignalGenerator();

                 // Init Driver Audio
            driverOut.Init(wg);
         }

        /// <summary>
        /// noiselevel controlse the volume of the signal;
        /// </summary>
        private int _noiselevel;
        public int NoiseLevel
        {
            get { return _noiselevel; }
            set
            {
                wg.Gain = value;
                _noiselevel = value;
                Stop(); //restart the noise
                Start();
            }
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


    

