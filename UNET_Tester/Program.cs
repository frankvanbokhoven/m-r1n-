using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace UNET_Tester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new frmUNETTester_Main());
        //}


        // The mutex prevents an application of starting twice on one system
        static Mutex _m;

        static bool IsSingleInstance()
        {
            try
            {
                // Try to open existing mutex.
                Mutex.OpenExisting("UNET_Tester");
            }
            catch
            {
                // If exception occurred, there is no such mutex.
                Program._m = new Mutex(true, "UNET_Tester");

                // Only one instance.
                return true;
            }
            // More than one instance.
            return false;
        }

        [STAThread]
        static void Main()
        {
            if (!Program.IsSingleInstance())
            {
                Console.WriteLine("More than one instance of UNET_Tester"); // Exit program.
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmUNETTester_Main());
            }
        }
    }
}
