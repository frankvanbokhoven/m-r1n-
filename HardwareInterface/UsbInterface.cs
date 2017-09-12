using System;
using HidTest;

namespace HardwareInterface
{
    public class UsbInterface : IHardwareInterface
    {

        #region Constants
        private readonly string _VID = "0d8c";                                          // Vendor ID C-Media
        private readonly string _PID = "0102";                                          // Product ID CM6206
        #endregion

        #region Classmembers
        HidTestLogic _HidLogic;                                                         // Class for polling PTT status
        private bool _IsInitialized;                                                    // Flag to indicate if the initialization of the audiobox was succeffful
        public event EventHandler<PttChangedEventArgs> PttChangedEvent;
        public event EventHandler<HeadsetPluggedChangedEventArgs> HeadsetPluggedChangedEvent;
        #endregion

        public void Initialize()
        {
            _IsInitialized = false;

            _HidLogic = new HidTestLogic();
            _HidLogic.ShowMessages = false;     // Suppress internal messages

            // Initialize USB
            _IsInitialized = _HidLogic.Init(_VID, _PID);

            if (_IsInitialized)
            {
                // Connect events from HIDLIB
                _HidLogic.OnPttPushedChanged += new dlgBoolean(OnUsbInputPttChanged);
                _HidLogic.OnHeadsetPluggedChanged += new dlgBoolean(OnUsbInputHeadsetChanged);
            }
        }

        public void Start()
        {
            if (_IsInitialized)
            {
                _HidLogic.StartSampling(100);
                _IsInitialized = true;
            }
        }

        public void Stop()
        {
            if (_IsInitialized)
            {
                _HidLogic.StopSampling();
                _IsInitialized = false;
            }
        }

        #region Event implementation
        private void OnUsbInputPttChanged(bool pttActive)
        {
            if (PttChangedEvent != null)
                PttChangedEvent(this, new PttChangedEventArgs() { PttActive = pttActive });
        }

        private void OnUsbInputHeadsetChanged(bool headsetPlugged)
        {
            if (HeadsetPluggedChangedEvent != null)
                HeadsetPluggedChangedEvent(this, new HeadsetPluggedChangedEventArgs() { HeadsetPlugged = headsetPlugged });
        }
        #endregion
    }
}
