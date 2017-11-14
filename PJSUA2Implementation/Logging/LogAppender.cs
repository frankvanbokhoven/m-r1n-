using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSUA2Implementation.Logging
{
    public static class LogAppender
    {

        #region AppendToLog
        private static readonly string clogfile = ConfigurationManager.AppSettings["LogFile"];
        private static readonly bool clogactive = Convert.ToBoolean(ConfigurationManager.AppSettings["LogActive"]);

        /// <summary>
        /// supersimple method to add a logging row to a log file
        /// </summary>
        /// <param name="_rowToBeAppended"></param>
        public static void AppendToLog(string _rowToBeAppended)
        {
            if (clogactive) //only add to the log when this flag is active
            {
                ///check the size of the existing log and if bigger than 5 MB, create a new one
                if (File.Exists(clogfile))
                {
                    Int64 fileSizeInBytes = new FileInfo(clogfile).Length;
                    if (fileSizeInBytes > 1000000)
                    {
                        string path = Path.GetFullPath(clogfile);
                        string filename = Path.GetFileNameWithoutExtension(clogfile);
                        System.IO.File.Move(clogfile, Path.Combine(path, string.Format("{0}{1}.log", filename, DateTime.Now.ToString("yyyyMMdd_HHmmss"))));
                    }
                }
                ///
                using (StreamWriter w = File.AppendText(clogfile))
                {
                    w.WriteLine(string.Format("Pjsua2: {0} - {1}", DateTime.Now.ToString("u"), _rowToBeAppended));
                    w.Flush();
                    w.Close();
                }
            }
        }

        #endregion
    }
}
