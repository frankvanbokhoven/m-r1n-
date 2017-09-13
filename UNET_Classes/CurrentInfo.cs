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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InstructorName;


        /// <summary>
        /// constructor
        /// </summary>
        public CurrentInfo()
        {

        }

        //  public Exercise ExerciseCurrent;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"> current info id</param>
        /// <param name="_exerciseName"> a name someone gives the exercise</param>
        /// <param name="_exerciseMode">exercise mode</param>
        /// <param name="_role">the role assigned by the instructor to the trainee</param>
        /// <param name="_platform">the platform, whatever this is</param>
        /// <param name="_exercisecurrent">the exercise currently in play</param>
        /// <param name="_instructorNmae">name of the instructor so the trainee knows who's the instructor</param>
        public CurrentInfo(int _id, string _exerciseName, string _exerciseMode, string _role, string _platform, string _instructorName)
        {
            ID = _id;
            ExerciseName = _exerciseName;
            ExerciseMode = _exerciseMode;
            ConsoleRole = _role;
            Platform = _platform;
            InstructorName = _instructorName;
        }
    }
}
