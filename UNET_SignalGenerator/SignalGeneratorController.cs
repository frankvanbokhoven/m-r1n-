using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace UNET_Sounds
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

        public void DisposeSignalgenerator()
        {
            try
            {
                if (driverOut != null)
                {

                    driverOut.Stop();

                    driverOut.Dispose();

                }
                if (wg != null)
                {
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                //niets
            }
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
            try
            {
                if (driverOut != null)
                {
                    driverOut.Stop();
                }
            }
            catch (Exception ex)
            {
                //niets
            }
        }

        // Clean DriverOut
        public void Cleanup()
        {
            try
            {
                if (driverOut != null)
                    driverOut.Stop();

                wg = null;

                if (driverOut != null)
                {
                    driverOut.Dispose();
                }

            }
            catch (Exception ex)
            {
                //niets
            }
        }
    }
}


    

