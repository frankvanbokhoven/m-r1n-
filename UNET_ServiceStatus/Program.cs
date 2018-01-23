using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Console.Write(string.Format("Yggdra Solutions 2017 - UNET Service Status Builddate: {0}", Utils.GetLinkerDateTime(Assembly.GetExecutingAssembly(), null)));
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

        ///// <summary>
        ///// Get the assemblys build date
        ///// see: https://stackoverflow.com/questions/1600962/displaying-the-build-date
        ///// </summary>
        ///// <param name="assembly"></param>
        ///// <param name="target"></param>
        ///// <returns></returns>
        //public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        //{
        //    var filePath = assembly.Location;
        //    const int c_PeHeaderOffset = 60;
        //    const int c_LinkerTimestampOffset = 8;

        //    var buffer = new byte[2048];

        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //        stream.Read(buffer, 0, 2048);

        //    var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
        //    var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
        //    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //    var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

        //    var tz = target ?? TimeZoneInfo.Local;
        //    var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

        //    return localTime;
        //}

        private static void Timerhart_Elapsed(object sender, ElapsedEventArgs e)
        {
            //  getData getData = new getData();
            getData gd = new getData();
            gd.GetAndReportStatus();

        }
    }
}
