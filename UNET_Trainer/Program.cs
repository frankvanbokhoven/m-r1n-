using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace UNET_Trainer
{
    static class Program
    {
        static Mutex _m;

        [DebuggerNonUserCode]  //alleen in deze methode willen we de exceptie NIET zien
        static bool IsSingleInstance()
        {
            try
            {
                // Try to open existing mutex.
                Mutex.OpenExisting("UNET");
            }
            catch
            {
                // If exception occurred, there is no such mutex.
                Program._m = new Mutex(true, "UNET");

                // Only one instance.
                return true;
            }
            // More than one instance.
            return false;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]  //alleen in deze methode willen we de exceptie NIET zien
        static void Main()
        {
            if (!Program.IsSingleInstance())
            {
                Console.WriteLine("More than one instance of UNET trainer"); // Exit program.
            }
            else
            {
                Console.WriteLine("UNET One instance"); // Continue with program.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
               //  Application.Run(new FrmUNETMain());
                Application.Run(FrmUNETMain.GetForm); //dit zorgt ervoor dat frmmain direct als singleton wordt geopend
            }
            // Stay open.
            Console.ReadLine();

        }
    }
}
