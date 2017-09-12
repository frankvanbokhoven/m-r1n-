using System;
using System.Threading;
using HardwareInterface.FTDI;

namespace HardwareInterface
{
    public class FtdiInterface : IHardwareInterface
    {
        const int SAMPLERATE = 100;
        public static D2xxAccess m_USB_Port;    //make instance
        protected UInt32 m_hPort = 0;           //the local port that is referenced by the functions
        protected Thread UsbReadThread;
        public bool Enabled = true;
        public event EventHandler<PttChangedEventArgs> PttChangedEvent;
        public event EventHandler<HeadsetPluggedChangedEventArgs> HeadsetPluggedChangedEvent;

        public FtdiInterface()   //constructor
        {
            m_USB_Port = new D2xxAccess();
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

        #region Properties
        private bool hsPlugged;
        public bool HeadSetPlugged
        {
            get { return hsPlugged; }
        }

        private bool pttActive;
        public bool PttActive
        {
            get { return pttActive; }
        }

        private bool devAvailable = false;
        public bool DeviceAvailable
        {
            get { return devAvailable; }
        }

        #endregion

        public void Initialize()
        {
            int iResult = m_USB_Port.FT_Open_USB(0, ref m_hPort); //opening...

            if (iResult == 0)
            {
                devAvailable = true;
                m_USB_Port.FT_Purge_USB(ref m_hPort);

                m_USB_Port.FT_SetBitMode_USB(ref m_hPort, 0x00, 0x01);  //configuring...
                m_USB_Port.FT_SetBaudRate_USB(ref m_hPort, 300);

                UsbReadThread = new Thread(new ThreadStart(ReadThread));    //start the thread...
                UsbReadThread.Priority = ThreadPriority.Lowest;
            }
        }

        public void Start()
        {
            if (UsbReadThread != null)
            {
                UsbReadThread.Start();
            }
        }

        public void Stop()
        {
            StopReadThread();
        }

        private void StopReadThread()
        {
            if (UsbReadThread != null)
            {
                Enabled = false;                        //stop the thread
                Thread ThrTemp = UsbReadThread;
                UsbReadThread = null;
                ThrTemp.Join(500);

                m_USB_Port.FT_Close_USB(ref m_hPort);   //close the USB device
            }
        }

        private void ReadThread()
        {
            byte sample = 0;
            sample = GetBitsFromUSB();
            OnUsbInputPttChanged(GetPttState(sample));
            OnUsbInputHeadsetChanged(GetHsPluggedState(sample));

            while (Enabled)
            {
                sample = GetBitsFromUSB();

                if (GetPttState(sample) != PttActive) // Ptt changed  --> bit1=PTT
                {
                    pttActive = GetPttState(sample);
                    OnUsbInputPttChanged(PttActive);
                }
                if (GetHsPluggedState(sample) != HeadSetPlugged) //Headset (un)plugged  --> bit2=HDST
                {
                    hsPlugged = GetHsPluggedState(sample);
                    OnUsbInputHeadsetChanged(HeadSetPlugged);
                }

                Thread.Sleep(SAMPLERATE);
            }
        }

        private byte GetBitsFromUSB()
        {
            byte resultByte = m_USB_Port.FT_GetBitMode_USB(ref m_hPort);
            return resultByte;
        }

        private bool GetPttState(byte input)
        {
            return (input & 0x01) == 0;
        }

        private bool GetHsPluggedState(byte input)
        {
            return (input & 0x02) == 0;
        }
    }
}
