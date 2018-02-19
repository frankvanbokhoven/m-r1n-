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
            Console.Write(string.Format("HSO 2018 - UNET Service Status Builddate: {0}", Utils.GetLinkerDateTime(Assembly.GetExecutingAssembly(), null)));
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
