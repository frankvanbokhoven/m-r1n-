using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{
   public class Assist
    {
        public Guid ID { get; set; }
        public string TraineeInfo { get; set; }
        public int TraineeID { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime AcknowledgeTime { get; set; }
        public string AcknowledgedBy { get; set; }
        public Boolean Acknowledged { get; set; }

        public Assist()
        {

        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_requestor"></param>
        public Assist(int _traineeID, string _traineeInfo)
        {
            TraineeID = _traineeID;
            TraineeInfo = _traineeInfo;
            RequestTime = DateTime.Now;
            ID = new Guid();
            Acknowledged = false;
        }
    }
}
