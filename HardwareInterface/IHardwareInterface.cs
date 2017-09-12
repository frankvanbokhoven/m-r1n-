using System;

namespace HardwareInterface
{
    public interface IHardwareInterface
    {
        event EventHandler<PttChangedEventArgs> PttChangedEvent;
        event EventHandler<HeadsetPluggedChangedEventArgs> HeadsetPluggedChangedEvent;

        void Initialize();
        void Start();
        void Stop();

    }
}
