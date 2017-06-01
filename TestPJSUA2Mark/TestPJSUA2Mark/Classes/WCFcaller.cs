using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPJSUA2Mark.Classes
{
    /// <summary>
    /// this static class reports messages to the wcf service
    /// </summary>
    public static class WCFcaller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Report to the wcf service
        /// </summary>
        /// <param name="_message"></param>
        public static void SetSIPStatusMessage(string _message)
        {
            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    var success = service.SetSIPStatusMessage(_message, ConfigurationManager.AppSettings["TraineeID"].ToString());

                    service.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error setsipstatusmessage", ex);
                // throw;
            }

        }

        /// <summary>
        /// get the messages from the stack
        /// </summary>
        /// <returns></returns>
        public static string GetSIPStatusMessages()
        {
            string result = string.Empty;
            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    result = service.GetSIPStatusMessage(ConfigurationManager.AppSettings["TraineeID"].ToString());

                    service.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getsipstatusmessage", ex);
                // throw;
                result = "Error getsipstatusmessage" + ex.Message;
            }
            return result;
        }
    }
}
