using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UNET_ServiceStatus
{
    class Program
    {
       static void Main(string[] args)
        {
          getData gd = new getData();
        Timer timerhart = new System.Timers.Timer(3000);
          Console.Write("Yggdra Solutions 2017 - UNET Service Status Version 2 oktober 2017");
            Console.Write(Environment.NewLine);
            Console.Write("UNET Status weergever van de UNET_Service");
            Console.Write(Environment.NewLine);
            timerhart.Enabled = false;
            timerhart.Elapsed += Timerhart_Elapsed;
            if (args.Length > 0)
            {
                Console.Write("Init...");
                gd.InitSettings();

                Console.Write("Connectie gemaakt!");
                Console.Write(Environment.NewLine);
                Console.Write("Exact importer downloading Artikelen");
                Console.Write(Environment.NewLine);
                gd.GetAndReportStatus();
                Console.Write(Environment.NewLine);

                Console.Write(Environment.NewLine);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("not enough arguments specified! (expected the name of an eventlog)");
            }
            timerhart.Enabled = true;
            //Console.Write("Upload to ExactOnline: Finished work. This Importer will exit in 30 seconds:")
            //For i As Integer = 0 To 30
            //    If i = 10 Or i = 20 Then
            //        Console.Write(i)
            //    Else
            //        Console.Write("*")
            //    End If
            //    System.Threading.Thread.Sleep(1000)
            //Next
            Console.ReadLine();
            timerhart.Enabled = false;

        }

        private static void Timerhart_Elapsed(object sender, ElapsedEventArgs e)
        {
            //  getData getData = new getData();
            getData gd = new getData();
            gd.GetAndReportStatus();

        }
    }
}
