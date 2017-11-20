using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{
    public class TraineeStatus
    {
        public int TraineeID { get; set; }
        public List<Role> RoleList { get; set; }
        public List<Radio> RadioList { get; set; }
        public Exercise CurrentExercise { get; set; }


        public TraineeStatus()
        {
            //empty constructor
        }

        public TraineeStatus(int _traineeID, List<Role> _roleList, List<Radio> _radioList, Exercise _currentExercise)
        {
            TraineeID = _traineeID;
            RoleList = _roleList;
            RadioList = _radioList;
            CurrentExercise = _currentExercise;
        }
    }
}
