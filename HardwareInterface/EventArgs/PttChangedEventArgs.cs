using System;

namespace HardwareInterface
{
    public class PttChangedEventArgs : EventArgs
    {
        public bool PttActive
        {
            get; set;
        }

    }
}