using HardwareInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInterfaceTestApp
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testapplication for HardwareInterface.dll");
            Console.WriteLine("Author: Frank van Bokhoven => frankvanbokhoven@gmail.com");
            Console.WriteLine(".........................................................");
            //IHardwareInterface hardware = new FtdiInterface();
            IHardwareInterface hardware = new UsbInterface();
            hardware.Initialize();
            hardware.HeadsetPluggedChangedEvent += Hardware_HeadsetPluggedChangedEvent;
            hardware.PttChangedEvent += Hardware_PttChangedEvent;
            hardware.Start();
            Console.ReadLine();
            hardware.Stop();
        }

        private static void Hardware_PttChangedEvent(object sender, PttChangedEventArgs e)
        {
            Console.WriteLine("PTT value: {0}", e.PttActive);
        }

        private static void Hardware_HeadsetPluggedChangedEvent(object sender, HeadsetPluggedChangedEventArgs e)
        {
            Console.WriteLine("Headset value: {0}", e.HeadsetPlugged);
        }
    }
}

