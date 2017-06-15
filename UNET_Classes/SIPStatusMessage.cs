using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNET_Classes
{
    /// <summary>
    /// the SIPStatusMessage holds a message that is sent via WCF by a sipclient.
    /// The the sipstatusmessage is used to avoid thread problems.
    /// In UNET_Service_Singleton there is a list of these sipstatusmessages
    /// </summary>
    public class SIPStatusMessage
    {
        public string ID { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }

        public SIPStatusMessage(string _id, string _message)
        {
            ID = _id;
            MessageDate = DateTime.Now;
            Message = _message;
        }
    }
}