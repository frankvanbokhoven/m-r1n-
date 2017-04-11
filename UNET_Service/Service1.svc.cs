using System;
using System.Configuration;
using System.Diagnostics;

namespace UNET_Service
{
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel.Activation;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [AspNetCompatibilityRequirements
        (RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service1 : IService1
    {
        private readonly string cUploadFileDirectory = ConfigurationManager.AppSettings["UploadFileDirectory"];
        private readonly string cFileDownloadDirectory = ConfigurationManager.AppSettings["FileDownloadDirectory"];
        private readonly string cFileSourceDirectory = ConfigurationManager.AppSettings["FileSourceDirectory"];
        private readonly string clogfile = ConfigurationManager.AppSettings["LogFile"];
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region Getters
        /// <summary>
        /// Get the exercises from the inline memory
        /// </summary>
        /// <returns></returns>
        public List<UNET_Service.Classes.Exercise> GetExercises()
        {
            List<UNET_Service.Classes.Exercise> result = new List<UNET_Service.Classes.Exercise>();
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Exercise>(singleton.Exercises);
            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available exercises: ", ex);

                throw;
            }
            return result;
        }


        public List<UNET_Service.Classes.Role> GetRoles()
        {
            List<UNET_Service.Classes.Role> result = new List<UNET_Service.Classes.Role>();
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Role>(singleton.Roles);
            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available exercises: ", ex);
             //   result.Add("Error retrieving exercices");
                throw;
            }
            return result;
        }

        public List<UNET_Service.Classes.Radio> GetRadios()
        {
            List<UNET_Service.Classes.Radio > result = new List<UNET_Service.Classes.Radio>();
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Radio>(singleton.Radios);

            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available Radios: ", ex);
                 throw;
            }
            return result;
        }

        public List<UNET_Service.Classes.Instructor> GetInstructors()
        {
            List<UNET_Service.Classes.Instructor> result = new List<UNET_Service.Classes.Instructor>();
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Instructor>(singleton.Instructors);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available Instructors: " + ex.Message);

                throw;
            }
            return result;
        }

        public List<UNET_Service.Classes.Trainee> GetTrainees()
        {
            List<UNET_Service.Classes.Trainee> result = new List<UNET_Service.Classes.Trainee>();
            try
            {

                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Trainee>(singleton.Trainees);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available trainees: " + ex.Message);

                throw;
            }
            return result;
        }


        public List<UNET_Service.Classes.Platform> GetPlatforms()
        {
            List<UNET_Service.Classes.Platform> result = new List<UNET_Service.Classes.Platform>();
            try
            {

                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Platform>(singleton.Platforms);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available platforms: " + ex.Message);

                throw;
            }
            return result;
        }

        #endregion

        #region Setters
        public bool SetExerciseCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Exercises.Clear();

                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_count); i++)
                {
                    Classes.Exercise exercise = new Classes.Exercise();
                    exercise.Number = i;
                    exercise.SpecificationName = string.Format("Exercise_{0}", i);
                    singleton.Exercises.Add(exercise);
                }


                // result = new List<UNET_Service.Classes.Platform>(singleton.Platforms);


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting exersisecount", ex);
                result = false;

            }
            return result;
        }

        /// <summary>
        /// Overwrite the list of exercises
        /// </summary>
        /// <param name="_exercises"></param>
        /// <returns></returns>
        public bool SetExercises(List<UNET_Service.Classes.Exercise> _exercises)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                singleton.Exercises = _exercises;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting exercises.", ex);
                result = false;
                // log.Fatal("Set Exercises: ", ex);
            }
            return result;
        }

        public bool SetRoles(List<Classes.Role>  _role)
        {
            return true;
        }

        public bool SetRadios(List<Classes.Radio>  _radio)
        {
            return true;
        }


        public bool SetInstructors(List<Classes.Instructor> _instructor)
        {
            return true;
        }


        public bool SetTrainees(List<Classes.Trainee>  _trainee)
        {
            return true;
        }


        public bool SetPlatforms(List<Classes.Platform>  _platform)
        {
            return true;
        }
        #endregion 

        #region AppendToLog

        /// <summary>
        /// supersimple method to add a logging row to a log file
        /// </summary>
        /// <param name="_rowToBeAppended"></param>
        private void AppendToLog(string _rowToBeAppended)
        {
            using (StreamWriter w = File.AppendText(clogfile))
            {
                w.WriteLine(string.Format("Servicelog: {0} - {1}", DateTime.Now.ToString("u"), _rowToBeAppended));
                w.Flush();
                w.Close();
            }
        }

        #endregion

    }
}

