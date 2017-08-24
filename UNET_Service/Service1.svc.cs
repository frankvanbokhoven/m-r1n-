using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using UNET_Classes;

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

        public Service1()
        {
         }
        #region Getters
        /// <summary>
        /// Get the exercises from the inline memory
        /// </summary>
        /// <returns></returns>
        public List<UNET_Classes.Exercise> GetExercises()
        {
            //todo!!! deze staat in een methode die toevallig snel wordt aangeroepen na de start, maar moet op een betere plaats
            //constructor werkt niet
            log4net.Config.BasicConfigurator.Configure();
            log.Info("Successfully started UNET_Service");

            List<UNET_Classes.Exercise> result = new List<UNET_Classes.Exercise>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Exercise>(singleton.Exercises);
            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available exercises: ", ex);

                throw;
            }
            return result;
        }

        #region Status messages
        /// <summary>
        /// To avoid thread problems, a client can call this method to report information
        /// This method adds the sip status message to the stack and this stack is read by method GetSIPStatusMessage
        /// </summary>
        /// <param name="_message"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool SetSIPStatusMessage(string _message, string _id)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;
                singleton.SIPStatusMessageList.Add(new SIPStatusMessage(_id, _message));
            }
            catch (Exception ex)
            {
                log.Error("Error adding to the SIPStatusMessage list: ", ex);
                //throw;
                result = false;
            }
            return result;
        }


        /// <summary>
        /// Return the statusmessages for the given sip client (identified by an id) and clear the message stack
        /// After returning the statusmessages, they are cleare from the list
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetSIPStatusMessage(string _id)
        {
            string result = string.Empty;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                //use lync to select the messages for given sipclient
                List<SIPStatusMessage> list =  singleton.SIPStatusMessageList.Where(x => x.ID.ToLower() == _id.ToLower()).ToList<SIPStatusMessage>();

                //then build the pipe separated result string
                foreach(SIPStatusMessage ssm in list)
                {
                    result += ssm.Message + "|";
                }
                //remove last pipe, if it exists in the string
                if(result.Length > 0) //if the string is NOT empty
                {
                    if(result[result.Length-1] == '|') //if the last character is a pipe
                    {
                        result = result.Remove(result.Length - 1);//remove the last pipe
                    }
                }
                //now remove all these items using lync
                singleton.SIPStatusMessageList.RemoveAll(x => x.ID.ToLower() == _id.ToLower());
            }
            catch (Exception ex)
            {
                log.Error("Error adding to the SIPStatusMessage list: ", ex);
                //throw;
                result = ex.Message;
            }
            return result; //return the pipe separated messages string
        }

        public bool ClearStatusMessages(string _id)
        {
            bool result = false;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                //now remove all these items using lync
                singleton.SIPStatusMessageList.RemoveAll(x => x.ID.ToLower() == _id.ToLower());
                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error clearing the SIPStatusMessage list: ", ex);
                //throw;
                result = false;
            }
            return result; 
        }
        #endregion

        /// <summary>
        /// Only when this statuschanged is true, the clients have to  bother updating the other statusses
        /// </summary>
        /// <returns></returns>
        public bool GetTraineeStatusChanged()
        {
            UNET_Singleton singleton = UNET_Singleton.Instance;

            return singleton.TraineeStatusChanged;
        }

       public bool GetNoiseLevelChanged()
        {
            UNET_Singleton singleton = UNET_Singleton.Instance;
            return singleton.NoiseLevelChanged;
        }

        /// <summary>
        /// Set the changed status for every individual trainee
        /// </summary>
        /// <param name="_traineeId"></param>
        /// <param name="_changed"></param>
        /// <returns></returns>
        public bool SetTraineeStatusChanged(int _traineeId, bool _changed)
        {
            UNET_Singleton singleton = UNET_Singleton.Instance;
            singleton.TraineeStatusChanged = _changed;

            return true;
        }


        /// <summary>
        /// </summary>
        /// <param name="_radioid"></param>
        /// <param name="_changed"></param>
        /// <returns></returns>
        public bool SetNoiseLevelChanged(int _radioid, bool _changed)
        {
            UNET_Singleton singleton = UNET_Singleton.Instance;
            singleton.NoiseLevelChanged = _changed;
            return true;
        }

        public List<UNET_Classes.Role> GetRoles()
        {
            List<UNET_Classes.Role> result = new List<UNET_Classes.Role>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Role>(singleton.Roles);
            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available exercises: ", ex);
             //   result.Add("Error retrieving exercices");
              //  throw;
            }
            return result;
        }

        public List<UNET_Classes.Radio> GetRadios()
        {
            List<UNET_Classes.Radio > result = new List<UNET_Classes.Radio>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Radio>(singleton.Radios);

            }
            catch (Exception ex)
            {
                log.Error("Exception retrieving the available Radios: ", ex);
                 throw;
            }
            return result;
        }

        public List<UNET_Classes.Instructor> GetInstructors()
        {
            List<UNET_Classes.Instructor> result = new List<UNET_Classes.Instructor>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Instructor>(singleton.Instructors);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available Instructors: " + ex.Message);

                throw;
            }
            return result;
        }

        public List<UNET_Classes.Trainee> GetTrainees()
        {
            List<UNET_Classes.Trainee> result = new List<UNET_Classes.Trainee>();
            try
            {

                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Trainee>(singleton.Trainees);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available trainees: " + ex.Message);

                throw;
            }
            return result;
        }

        public List<UNET_Classes.Trainee>GetTraineesAssigned(int _instructor, int _exercise)
        {
            List<UNET_Classes.Trainee> result = new List<UNET_Classes.Trainee>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;
              //  result = new List<Trainee>(singleton.Instructors.Where(x => x.ID == _instructor).  .All<).Where(t => t.ID == _exercise);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available trainees: " + ex.Message);

                throw;
            }
            return result;
        }


        public List<UNET_Classes.Platform> GetPlatforms()
        {
            List<UNET_Classes.Platform> result = new List<UNET_Classes.Platform>();
            try
            {

                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = new List<UNET_Classes.Platform>(singleton.Platforms);


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

        /// <summary>
        /// use case: 3.1.3.8.3
        /// We try to find the radio, given the id in parameter _radioID
        /// once we found it, we set the noiselevel
        /// </summary>
        /// <param name="_radioID"></param>
        /// <param name="_noiselevel"></param>
        /// <returns></returns>
        public bool SetNoiseLevel(int _radioID, int _noiselevel)
        {
            bool result = false;

            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                foreach (Radio radio in singleton.Radios)
                {
                    if (radio.ID == _radioID)
                    {
                        radio.NoiseLevel = _noiselevel;
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error setting the noiselevel for id: " + _radioID + ex.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// use case: 3.1.3.8.3
        /// We try to find the radio, given the id in parameter _radioID
        /// once we found it, we return the noiselevel
        /// </summary>
        /// <param name="_radioID"></param>
        /// <returns></returns>
        public int GetNoiseLevel(int _radioID)
        {
            int result = 0;

            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                foreach (Radio  radio in singleton.Radios)
                {
                    if (radio.ID == _radioID)
                    {
                        result = radio.NoiseLevel;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting the noiselevel for id: " + _radioID + ex.Message);
                result = 0;
            }
            return result;
        }


 

        /// <summary>
        /// add mock exercises to the singleton
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool SetExerciseCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Exercises.Clear();

                //and create a number of exercises
                for (int i = 0; i <= Convert.ToInt16(_count-1); i++)
                {
                    Exercise exercise = new Exercise();
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
        /// Retrieve an list of
        /// </summary>
        /// <returns></returns>
        public bool[] GetTraineeStatus()
        {
            bool[] traineestatus = new bool[8];
            traineestatus[0] = true;
            traineestatus[1] = false;
            return traineestatus;
        }


        /// <summary>
        /// Overwrite the list of exercises
        /// </summary>
        /// <param name="_exercises"></param>
        /// <returns></returns>
        public bool SetExercises(List<UNET_Classes.Exercise> _exercises)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Exercises.Clear();

                foreach(UNET_Classes.Exercise exercise in _exercises)
                {
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
        /// This method assigns a trainee to an exercise and instructor, OR removes it
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <param name="_exersiseID"></param>
        /// <param name="_traineeID"></param>
        /// <param name="_add"></param>
        /// <returns></returns>
        public bool SetTraineeAssignedStatus(int _instructorID, int _exersiseID, int _traineeID, bool _add)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                                                                   //find the richt instructor, exercise and trainee


                foreach (Instructor inst in singleton.Instructors)  //find the given instructor
                {
                    if (inst.ID == _instructorID)
                    {
                        foreach (Exercise exe in inst.Exercises) //find the given exercise
                        {
                            if(exe.Number == _exersiseID)
                            {
                                bool found = false;
                                foreach(Trainee  tr in exe.TraineesAssigned) //find the given trainee
                                {
                                    if(tr.ID == _traineeID)
                                    {
                                        found = true;
                                        if(!_add)
                                        {
                                            exe.TraineesAssigned.Remove(tr);
                                        }
                                       break;
                                    }

                                   
                                }
                                if(!found && _add) //if the trainee is not found in the list, but should be added (true) then ADD this trainee
                                {
                                    exe.TraineesAssigned.Add(new Trainee(_traineeID, "traineename_nogverzinnen"));
                                }
                                break; 
                            }
                            break;
                        }
                        break;
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error assigning/deassigning trainee", ex);
                result = false;

            }
            return result;
        }

        /// <summary>
        /// add mock trainees to the singleton
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool SetTraineesCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Trainees.Clear();


                //and create a number of Trainees
                for (int i = 1; i <= Convert.ToInt16(_count); i++)
                {
                    Trainee  trainee = new Trainee();
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

        /// <summary>
        /// add mock radios to the singleton
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool SetRadiosCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Radios.Clear();


                //and create a number of Radios
                for (int i = 1; i <= Convert.ToInt16(_count); i++)
                {
                    Radio radio = new Radio();
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

        /// <summary>
        /// add mock roles to the singleton
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool SetRolesCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Roles.Clear();


                //and create a number of exercises
                for (int i = 1; i <= Convert.ToInt16(_count); i++)
                {
                    Role role = new Role();
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

        public bool SetRoles(List<Role> _role)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Roles.Clear();

                foreach (UNET_Classes.Role role in _role)
                {
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

        public bool SetRadioStatus(int _radioNumber, UNET_Classes.UNETRadioState _state)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
            

                foreach(Radio radio in singleton.Radios) //because we use the foreacht, we implicitly protect this function from ever setting the state with a non existant id
                {
                    if(radio.ID == _radioNumber)
                    {
                        radio.State = _state;
                        break;
                    }
                }

                log.Error("Finised setting the Radio status. Radionummer: " + _radioNumber + " State: " + _state.ToString());


                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting Radio Status", ex);
                result = false;

            }
            return result;
        }


        public bool SetRadios(List<Radio>  _radio)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Radios.Clear();



                foreach (UNET_Classes.Radio radio in _radio)
                {
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


        public bool SetInstructors(List<Instructor> _instructor)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Instructors.Clear();


                foreach (UNET_Classes.Instructor instructor in _instructor)
                {
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

        /// <summary>
        /// this function adds mock instructors to the singleton list
        /// </summary>
        /// <param name="_count"></param>
        /// <returns></returns>
        public bool SetInstructorsCount(int _count)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Instructors.Clear();


                //and create a number of Trainees
                for (int i = 1; i <= Convert.ToInt16(_count); i++)
                {
                    Instructor instructor = new Instructor();
                    instructor.ID = i;
                    instructor.Name = string.Format("Instructor{0}", i);
                    singleton.Instructors.Add(instructor);
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


        public bool SetTrainees(List<Trainee>  _trainee)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Trainees.Clear();


                foreach (UNET_Classes.Trainee trainee in _trainee)
                {
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

        /// <summary>
        /// add mock platforms to the singleton
        /// </summary>
        /// <param name="_platform"></param>
        /// <returns></returns>
        public bool SetPlatforms(List<Platform>  _platform)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //first clear the array
                singleton.Platforms.Clear();


                //and create a number of exercises
                for (int i = 0; i <= Convert.ToInt16(_platform.Count - 1); i++)
                {
                    Platform platform = new Platform();
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
        public bool RegisterTrainee(CurrentInfo _currentInfo)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                bool existing = false;
                //try to find the given traineeclient in the list. If found, update the information, otherwise add the traineeclient
                for (int i = 0; i <= Convert.ToInt16(singleton.CurrentInfoList.Count - 1); i++)
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
        public CurrentInfo GetExerciseInfo(int _traineeID)
        {
            CurrentInfo result = null;
            try

            {
                UNET_Singleton singleton = UNET_Singleton.Instance;
                                                                                                                             
                foreach (CurrentInfo cu in singleton.CurrentInfoList)
                {
                    if (cu.ID == _traineeID)
                    {
                        result = cu;
                        break;
                    }
                }
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

