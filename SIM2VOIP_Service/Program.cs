using System;
using System.Collections;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace XPUEESSaver
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*  static void Main()
           {
               ServiceBase[] ServicesToRun;
               ServicesToRun = new ServiceBase[] 
               { 
                   new ESSaverPUE() 
               };
               ServiceBase.Run(ServicesToRun);
            }*/
        /// <summary>
        ///  the install functionality is based on: http://stackoverflow.com/questions/1449994/inno-setup-for-windows-service
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // if (System.Environment.UserInteractive)
            //  {
            if (args.Length > 0)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
                        break;
                    case "--runservice":
                        RunService();
                        break;
                }
                //    }
            }
            else
            {
              //  ServiceBase[] ServicesToRun;
              //  ServicesToRun = new ServiceBase[]
                    //                {
                     //                   new  ESSaverPUE()
                     //               };
                ServiceBase.Run(new ESSaverPUE()); //ServicesToRun);
            }

            //onderstaand start de service direct na installatie
            // gebaseerd op: http://stackoverflow.com/questions/1195478/how-to-make-a-net-windows-service-start-right-after-the-installation
         /*   if (args.Length == 0)
            {
                // Run your service normally.
                ServiceBase[] ServicesToRun = new ServiceBase[] { new ESSaverPUE()  };
                ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-install":
                        InstallService();
                        StartService();
                        break;
                    case "-uninstall":
                        StopService();
                        UninstallService();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }*/

        }

        /// <summary>
        /// deze functie runt de service, net zoals dat normaal gesproken gebeurd als er geen parameter wordt meegegeven
        /// </summary>
       private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                                    {
                                        new ESSaverPUE()
                                    };
            ServiceBase.Run(ServicesToRun);
        }
    }
}