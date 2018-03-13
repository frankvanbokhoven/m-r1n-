using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{
    public class PointToPoint
    {
        public Guid ID { get; set; }

        [Description("TraineeID of the trainee, requesting the P2P")]
        public string TraineeID { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime AcknowledgeTime { get; set; }
        public string AcknowledgedBy { get; set; }
        public Boolean Acknowledged { get; set; }



        public PointToPoint()
        {

        }


        /// <summary>
        /// constructor of PointToPoint
        /// </summary>
        /// <param name="_traineeID"></param>
        public PointToPoint(string _traineeID)
        {
            ID = Guid.NewGuid();
            TraineeID = _traineeID;
            //  ShortDescription = _description.Substring(0, _description.Length > 8 ? 8 : _description.Length);
        }
    }
}
