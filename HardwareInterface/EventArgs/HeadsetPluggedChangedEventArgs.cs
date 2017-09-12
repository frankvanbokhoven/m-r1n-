using System;

namespace HardwareInterface
{
    public class HeadsetPluggedChangedEventArgs : EventArgs
    {
        public bool HeadsetPlugged
        {
            get; set;
        }

    }
}