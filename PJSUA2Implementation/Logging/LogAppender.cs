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
        private static readonly string clogfile = ConfigurationManager.AppSettings["LogFile"]; //@"c:\temp\ServiceLog.txt";
        /// <summary>
        /// supersimple method to add a logging row to a log file
        /// </summary>
        /// <param name="_rowToBeAppended"></param>
        public static void AppendToLog(string _rowToBeAppended)
        {
            using (StreamWriter w = File.AppendText(clogfile))
            {
                w.WriteLine(string.Format("Pjsua2: {0} - {1}", DateTime.Now.ToString("u"), _rowToBeAppended));
                w.Flush();
                w.Close();
            }
        }

        #endregion
    }
}
