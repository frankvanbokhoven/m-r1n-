using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNET_Theming;

namespace PJSUA2Implementation.Logging
{
    public static class LogAppender
    {

        #region AppendToLog
        private static readonly string clogfile = RegistryAccess.GetStringRegistryValue(@"UNET", @"logdir", @"c:\log");// ConfigurationManager.AppSettings["LogFile"];
        private static string filename;

        //     private static readonly bool clogactive = RegistryAccess.GetStringRegistryValue(@"UNET", @"logactive", "true") == true ? true : false;//Convert.ToBoolean(ConfigurationManager.AppSettings["LogActive"]);

        /// <summary>
        /// supersimple method to add a logging row to a log file
        /// </summary>
        /// <param name="_rowToBeAppended"></param>
        public static void AppendToLog(string _rowToBeAppended)
        {
            try
            {
                // if (clogactive) //only add to the log when this flag is active
                //  {
                ///check the size of the existing log and if bigger than 5 MB, create a new one
                string path = Path.GetFullPath(clogfile);
                filename = string.Format("UNET_{0}.log", DateTime.Now.ToString("yyyyMMdd"));
                string fullfilename = Path.Combine(path, string.Format("{0}{1}.log", filename, DateTime.Now.ToString("yyyyMMdd")));
                if (File.Exists(fullfilename))
                {
                 //   Int64 fileSizeInBytes = new FileInfo(clogfile).Length;
                 //   if (fileSizeInBytes > 1000000)
                 //   {
                 //       System.IO.File.Move(clogfile, Path.Combine(path, string.Format("{0}{1}.log", filename, DateTime.Now.ToString("yyyyMMdd"))));
                 //   }
                }
                else
                {
                    File.Create(fullfilename).Dispose();

                }
                ///
                using (StreamWriter w = File.AppendText(fullfilename))
                {
                    w.WriteLine(string.Format("Pjsua2: {0} - {1}", DateTime.Now.ToString("u"), _rowToBeAppended));
                    w.Flush();
                    w.Close();
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
        }

        #endregion
    }
}
