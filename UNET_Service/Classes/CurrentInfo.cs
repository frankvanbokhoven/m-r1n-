using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNET_Service.Classes
{
    /// <summary>
    /// this class holds the information about the current training
    /// </summary>
    public class CurrentInfo
    {
        public int ID;
        public string ExerciseName;
        public string ExerciseMode;
        public string ConsoleRole;
        public string Platform;



        //public CurrentInfo(int _id, string _exerciseName, string _exerciseMode, string _consoleRole, string _platform)
        //{
        //    //  ID = new Guid(); //using the guid, we always have a unique ID
        //    ID = _id;
        //    ExerciseName = _exerciseName;
        //    ExerciseMode = _exerciseMode;
        //    ConsoleRole = _consoleRole;
        //    Platform = _platform;
        //}
    }
}