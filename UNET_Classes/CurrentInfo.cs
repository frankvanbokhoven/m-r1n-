using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{
    /// <summary>
    /// this class holds the information about the current training
    /// </summary>
   public class CurrentInfo
   {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ID;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ExerciseName;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ExerciseMode;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConsoleRole;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Platform;
      //  public Exercise ExerciseCurrent;


       // /// <summary>
       // /// constructor
       // /// </summary>
       // /// <param name="_id"></param>
       // /// <param name="_exerciseName"></param>
       // /// <param name="_exerciseMode"></param>
       // /// <param name="_role"></param>
       // /// <param name="_platform"></param>
       // /// <param name="_exercisecurrent"></param>
       // public CurrentInfo(int _id, string _exerciseName, string _exerciseMode, string _role, string _platform)
       // {
       //     ID = _id;
       //     ExerciseName = _exerciseName;
       //     ExerciseMode = _exerciseMode;
       //     ConsoleRole = _role;
       //     Platform = _platform;
       //}
   }
}
