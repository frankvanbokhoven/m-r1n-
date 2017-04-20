﻿using System;
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
        //private readonly string cUploadFileDirectory = ConfigurationManager.AppSettings["UploadFileDirectory"];
        //private readonly string cFileDownloadDirectory = ConfigurationManager.AppSettings["FileDownloadDirectory"];
        //private readonly string cFileSourceDirectory = ConfigurationManager.AppSettings["FileSourceDirectory"];
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
                //first clear the array
                singleton.Exercises.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_exercises.Count-1); i++)
                {
                    Classes.Exercise exercise = new Classes.Exercise();
                    exercise.Number = i;
                    exercise.SpecificationName = string.Format("Exercise_{0}", i);
                    singleton.Exercises.Add(exercise);
                }

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting exersisecount", ex);
                result = false;

            }
            return result;
        }

        public bool SetTraineesCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Trainees.Clear();


                //and create a number of Trainees
                for (int i = 1; i <= Convert.ToInt16(_count - 1); i++)
                {
                    Classes.Trainee  trainee = new Classes.Trainee();
                    trainee.ID = i;
                    trainee.Name = string.Format("Trainee{0}", i);
                    singleton.Trainees.Add(trainee);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting trainee", ex);
                result = false;

            }
            return result;
        }

        public bool SetRadiosCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Radios.Clear();


                //and create a number of Radios
                for (int i = 1; i <= Convert.ToInt16(_count - 1); i++)
                {
                    Classes.Radio radio = new Classes.Radio();
                    radio.ID = i;
                    radio.Description = string.Format("Radio{0}", i);
                    singleton.Radios.Add(radio);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting radiocount", ex);
                result = false;

            }
            return result;
        }


        public bool SetRolesCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Roles.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_count - 1); i++)
                {
                    Classes.Role role = new Classes.Role();
                    role.ID = i;
                    role.Name = string.Format("Role{0}", i);
                    singleton.Roles.Add(role);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting exersisecount", ex);
                result = false;

            }
            return result;
        }

        public bool SetRoles(List<Classes.Role>  _role)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Roles.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_role.Count - 1); i++)
                {
                    Classes.Role role = new Classes.Role();
                    role.ID = i;
                    role.Name = string.Format("Role{0}", i);
                    singleton.Roles.Add(role);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting roles", ex);
                result = false;

            }
            return result;
        }

        public bool SetRadios(List<Classes.Radio>  _radio)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Radios.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_radio.Count - 1); i++)
                {
                    Classes.Radio radio = new Classes.Radio();
                    radio.ID = i;
                    radio.Description = string.Format("Radio{0}", i);
                    singleton.Radios.Add(radio);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting radios", ex);
                result = false;

            }
            return result;
        }


        public bool SetInstructors(List<Classes.Instructor> _instructor)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Instructors.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_instructor.Count - 1); i++)
                {
                    Classes.Instructor instructor = new Classes.Instructor();
                    instructor.ID = i;
                    instructor.Name = string.Format("Instructor{0}", i);
                    singleton.Instructors.Add(instructor);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting instructor", ex);
                result = false;

            }
            return result;
        }


        public bool SetTrainees(List<Classes.Trainee>  _trainee)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Trainees.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_trainee.Count - 1); i++)
                {
                    Classes.Trainee trainee = new Classes.Trainee();
                    trainee.ID = i;
                    trainee.Name = string.Format("Trainee{0}", i);
                    singleton.Trainees.Add(trainee);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting trainee", ex);
                result = false;

            }
            return result;
        }


        public bool SetPlatforms(List<Classes.Platform>  _platform)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Platforms.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_platform.Count - 1); i++)
                {
                    Classes.Platform platform = new Classes.Platform();
                    platform.ID = i;
                    platform.Description = string.Format("Platform{0}", i);
                    singleton.Platforms.Add(platform);
                }


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting platform", ex);
                result = false;

            }
            return result;
        }

        /// <summary>
        /// with this method, a trainee can 'register' himself as
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool RegisterTrainee(Classes.CurrentInfo _currentInfo)
        {
            bool result = true;
            try
            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;//get the singleton object
                bool existing = false;
                //try to find the given traineeclient in the list. If found, update the information, otherwise add the traineeclient
                for (int i = 1; i <= Convert.ToInt16(singleton.CurrentInfoList.Count - 1); i++)
                {
                    //try to find a currentinfo object in the list with the same id
                    if(singleton.CurrentInfoList[i].ID == _currentInfo.ID)
                    {

                        singleton.CurrentInfoList[i] = _currentInfo; //overwrite the currentinfo object with the one from the client
                        existing = true;
                        break;
                    }
                    //if the currentinfo is not found, then create one and add it to the list
                    if(!existing)
                    {
                        singleton.CurrentInfoList.Add(_currentInfo);
                    }                   
                }

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting Registering Trainees", ex);
                result = false;

            }
            return result;
        }

        /// <summary>
        /// Given the id of the trainee, retrieve the current info for this trainee
        /// </summary>
        /// <param name="_traineeID"></param>
        /// <returns></returns>
        public Classes.CurrentInfo GetExerciseInfo(int _traineeID)
        {
            Classes.CurrentInfo result = null;
            try

            {
                UNET_Service.Classes.UNET_Service_Singleton singleton = UNET_Service.Classes.UNET_Service_Singleton.Instance;
                                                                                                                             
                foreach (Classes.CurrentInfo cu in singleton.CurrentInfoList)
                {
                    if (cu.ID == _traineeID)
                    {
                        result = cu;
                        break;
                    }
                }
                // Classes.CurrentInfo result = new Classes.CurrentInfo();

            }
            catch (Exception ex)
            {
                log.Error("Error getting the exercise info " + ex.Message);
                result = null;
            }


            return result;
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
