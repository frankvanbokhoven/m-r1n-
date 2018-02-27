using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using UNET_Classes;

namespace UNET_Service
{
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;

    // NOTE:we only want one single instance of the wcf service to run all UNET instructor and trainee clients, thatswhy the concurrencymode is set to single
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service1 : IService1
    {

        //     private readonly string clogfile = ConfigurationManager.AppSettings["LogFile"];
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static object locker = new object(); //tbv wcf broadcasting

        public Service1()
        {

            log4net.Config.BasicConfigurator.Configure();
            log.Info("Successfully started UNET_Service");

        }

        #region PTT

        /// <summary>
        /// add a ptt call to the queue
        /// </summary>
        /// <param name="_traineeInstructorID"></param>
        /// <returns></returns>
 
        public bool AddPTT(int _traineeInstructorID, PTTuser _pttUser)
        {
            bool result = false;

            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                //enqueue this call to the PTTQueue
                PTTcaller pt = new PTTcaller();
                pt.User = _pttUser;
                pt.ID = _traineeInstructorID;
                pt.PTTDateTime = DateTime.Now;
                singleton.PTTQueue.Enqueue(pt);
                
            }
            catch (Exception ex)
            {
                log.Error("Error setting the ptt:" + ex.Message);
                result = false;
            }
            return result;

        }

        /// <summary>
        /// Let the server know, this ptt is handled by removing it from the queue
        /// </summary>
        /// <param name="_traineeInstructorID"></param>
        /// <returns></returns>
        public  bool AcknowledgePTT(int _traineeInstructorID)
        {
            bool result = false;

            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                foreach(PTTcaller pttc in singleton.PTTQueue)
                {
                    if (((PTTcaller)pttc).ID == _traineeInstructorID)
                    {

                       //remove it from the queue
                       PTTcaller local = (PTTcaller)singleton.PTTQueue.Dequeue();
                       break;
                    }
                }
    
            }
            catch (Exception ex)
            {
                log.Error("Error acknowledgeing the ptt:" + ex.Message);
                result = false;
            }
            return result;
        }


        /// <summary>
        /// retrieve the PTT queue
        /// speciaal tbv UNET_ServiceStatus
        /// </summary>
        /// <returns></returns>
        public Queue<PTTcaller> GetPTTQueue()
        {
            Queue<PTTcaller> result = new Queue<PTTcaller>();
            try
            {

                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
             //   result = (Queue) singleton.PTTQueue;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the PTT Queue: " + ex.Message);

                throw;
            }
            return result;
        }
        #endregion

        #region SIM
        public bool DisconnectVCS()
        {
            return true;
        }


        /// <summary>
        /// deze methode gooit het hele singleton object leeg.
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            bool result = true;
            try
            {

                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                singleton.Instructors.Clear();
                singleton.Exercises.Clear();
                singleton.Trainees.Clear();
                singleton.Radios.Clear();
                singleton.Roles.Clear();
                singleton.PTTQueue.Clear();
                singleton.Platforms.Clear();
                singleton.SIPStatusMessageList.Clear();
             

                singleton.PendingChanges = DateTime.Now;                                                 

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception resetting: " + ex.Message);

                //throw;
            }
            return result;
        }

        
        public bool Login()
        {
            //we may always log in
            return true;
        }

        public bool Start()
        {
            return true;
        }

        /// <summary>
        /// Stop all exercises
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {


            return true;
        }
        #endregion

        #region Getters

        //public bool StartService()
        //{
        //    log4net.Config.BasicConfigurator.Configure();
        //    log.Info("Successfully started UNET_Service");
        //    //  AppendToLog(string.Format("Successfully started UNET_Service: {0}", DateTime.Now.ToString("G")));

        //    return true;
        //}
        /// <summary>
        /// Get the exercises from the inline memory
        /// </summary>
        /// <returns></returns>
        public List<UNET_Classes.Exercise> GetExercises()
        {
            //todo!!! deze staat in een methode die toevallig snel wordt aangeroepen na de start, maar moet op een betere plaats
            //constructor werkt niet

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
        //public string GetSIPStatusMessage(string _id)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        UNET_Singleton singleton = UNET_Singleton.Instance;

        //        //use lync to select the messages for given sipclient
        //        List<SIPStatusMessage> list = singleton.SIPStatusMessageList.Where(x => x.ID.ToLower() == _id.ToLower()).ToList<SIPStatusMessage>();

        //        //then build the pipe separated result string
        //        foreach (SIPStatusMessage ssm in list)
        //        {
        //            result += ssm.Message + "|";
        //        }
        //        //remove last pipe, if it exists in the string
        //        if (result.Length > 0) //if the string is NOT empty
        //        {
        //            if (result[result.Length - 1] == '|') //if the last character is a pipe
        //            {
        //                result = result.Remove(result.Length - 1);//remove the last pipe
        //            }
        //        }
        //        //now remove all these items using lync
        //        singleton.SIPStatusMessageList.RemoveAll(x => x.ID.ToLower() == _id.ToLower());
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error adding to the SIPStatusMessage list: ", ex);
        //        //throw;
        //        result = ex.Message;
        //    }
        //    return result; //return the pipe separated messages string
        //}

        //public bool ClearStatusMessages(string _id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        UNET_Singleton singleton = UNET_Singleton.Instance;

        //        //now remove all these items using lync
        //        singleton.SIPStatusMessageList.RemoveAll(x => x.ID.ToLower() == _id.ToLower());
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error clearing the SIPStatusMessage list: ", ex);
        //        //throw;
        //        result = false;
        //    }
        //    return result;
        //}
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
            List<UNET_Classes.Radio> result = new List<UNET_Classes.Radio>();
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

        /// <summary>
        /// this function deliveres all data about an instructor.
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <returns></returns>
        public Instructor GetAllInstructorData(int _instructorID)
        {
            UNET_Classes.Instructor result;// = new List<UNET_Classes.Instructor>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = singleton.Instructors.FirstOrDefault(x => x.ID == _instructorID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the Instructors: " + ex.Message);

                throw;
            }
            return result;
        }


        /// <summary>
        /// this function returns the current data for the given trainee
        /// </summary>
        /// <param name="_traineeID"></param>
        /// <returns></returns>
        public TraineeStatus GetAllTraineeData(int _traineeID)
        {
            try
            {
                UNET_Classes.TraineeStatus result = null;// = new TraineeStatus();
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object

                //first, try to find the assigned trainee. We have to look in all the instructor/exercises
                foreach (Instructor inst in singleton.Instructors)
                {
                    foreach (Exercise exe in inst.Exercises)
                    {
                        foreach (Trainee trn in exe.TraineesAssigned)
                        {
                            if (trn.ID == _traineeID) //we found the trainee
                            {
                                result = new TraineeStatus(trn.ID, exe.RolesAssigned, trn.Radios, exe);
                                break;
                            }
                        }
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the current Trainee data: " + ex.Message);
                return null;
                throw;
            }
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

        public List<UNET_Classes.Trainee> GetTraineesAssigned(int _instructor, int _exercise)
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

                foreach (Radio radio in singleton.Radios)
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
                for (int i = 0; i <= Convert.ToInt16(_count - 1); i++)
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

                //first, find out if any exercise is selected, if so, store the exerciseid
                int exercisenumber = -1;
                foreach (UNET_Classes.Exercise excercise in _exercises)
                {
                    if(excercise.Selected)
                    {
                        exercisenumber = excercise.Number;
                        break;
                    }
                }
                //first clear the array
                singleton.Exercises.Clear();

                foreach (UNET_Classes.Exercise exercise in _exercises)
                {
                    if(exercise.Number ==exercisenumber)
                    {
                        exercise.Selected = true;//if the exercise was selected previously, reselect it again in the new list
                    }

                    singleton.Exercises.Add(exercise);

                }
                singleton.PendingChanges = DateTime.Now;

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

                log.Info(string.Format("SetRoleAssignedStatus: Instructor: {0}  Exercise: {1} Role: {2}  Add: {3}", _instructorID, _exersiseID, _traineeID, _add));
                foreach (Instructor inst in singleton.Instructors)  //find the given instructor
                {
                    if (inst.ID == _instructorID)
                    {
                        foreach (Exercise exe in inst.Exercises) //find the given exercise
                        {
                            if (exe.Number == _exersiseID)
                            {
                                bool found = false;
                                foreach (Trainee trn in exe.TraineesAssigned) //find the given role
                                {
                                    if (trn.ID == _traineeID)
                                    {
                                        found = true;
                                        if (!_add)
                                        {
                                            exe.TraineesAssigned.Remove(trn);
                                        }
                                        break;
                                    }


                                }
                                if (!found && _add) //if the trainee is not found in the list, but should be added (true) then ADD this trainee
                                {
                                    exe.TraineesAssigned.Add(new Trainee(_traineeID, "trainee name"));
                                }

                                singleton.PendingChanges = DateTime.Now;
                                break;
                            }
                            break;
                        }
                        break;
                    }
                }
                singleton.PendingChanges = DateTime.Now;

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
        /// This method assigns a role to an exercise and instructor, OR removes it
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <param name="_exersiseID"></param>
        /// <param name="_traineeID"></param>
        /// <param name="_add"></param>
        /// <returns></returns>
        public bool SetRoleAssignedStatus(int _instructorID, int _exersiseID, int _role, bool _add)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                                                                   //find the richt instructor, exercise and trainee

                log.Info(string.Format("SetRoleAssignedStatus: Instructor: {0}  Exercise: {1} Role: {2}  Add: {3}", _instructorID, _exersiseID, _role, _add));
                foreach (Instructor inst in singleton.Instructors)  //find the given instructor
                {
                    if (inst.ID == _instructorID)
                    {
                        foreach (Exercise exe in inst.Exercises) //find the given exercise
                        {
                            if (exe.Number == _exersiseID)
                            {
                                bool found = false;
                                foreach (Role rol in exe.RolesAssigned) //find the given role
                                {
                                    if (rol.ID == _role)
                                    {
                                        found = true;
                                        if (!_add)
                                        {
                                            exe.RolesAssigned.Remove(rol);
                                        }
                                        break;
                                    }


                                }
                                if (!found && _add) //if the trainee is not found in the list, but should be added (true) then ADD this trainee
                                {
                                    exe.RolesAssigned.Add(new Role(_role, "rolename"));
                                }

                                singleton.PendingChanges = DateTime.Now;

                                break;
                            }
                            break;
                        }
                        break;
                    }
                }
                singleton.PendingChanges = DateTime.Now;

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
        /// This method assigns a radio to an exercise OR removes it
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <param name="_exersiseID"></param>
        /// <param name="_traineeID"></param>
        /// <param name="_add"></param>
        /// <returns></returns>
        public bool SetRadioAssignedStatus(int _instructorID, int _exersiseID, int _radio, bool _add)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                                                                   //find the right instructor and exercise

                log.Info(string.Format("SetRadioAssignedStatus: Instructor: {0}  Exercise: {1} Radio: {2}  Add: {3}", _instructorID, _exersiseID, _radio, _add));
                foreach (Instructor inst in singleton.Instructors)  //find the given instructor
                {
                    if (inst.ID == _instructorID)
                    {
                        foreach (Exercise exe in inst.Exercises) //find the given exercise
                        {
                            if (exe.Number == _exersiseID)
                            {
                                bool found = false;
                                foreach (Radio rad in exe.RadiosAssigned) //find the given role
                                {
                                    if (rad.ID == _radio)
                                    {
                                        found = true;
                                        if (!_add)
                                        {
                                            exe.RadiosAssigned.Remove(rad);
                                        }
                                        break;
                                    }


                                }
                                if (!found && _add) //if the trainee is not found in the list, but should be added (true) then ADD this trainee
                                {
                                    exe.RadiosAssigned.Add(new Radio(_radio, "ID: " + _radio, KeyGenerator.GetUniqueKey(4)));
                                }

                                singleton.PendingChanges = DateTime.Now;

                                break;
                            }
                            break;
                        }
                        break;
                    }
                }
                singleton.PendingChanges = DateTime.Now;

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
                    Trainee trainee = new Trainee();
                    trainee.ID = i;
                    trainee.Name = string.Format("Trainee{0}", i);
                    singleton.Trainees.Add(trainee);
                }

                singleton.PendingChanges = DateTime.Now;


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
                    radio.Frequency = KeyGenerator.GetUniqueKey(4);
                    singleton.Radios.Add(radio);
                }

                singleton.PendingChanges = DateTime.Now;

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

                singleton.PendingChanges = DateTime.Now;

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

                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting roles", ex);
                result = false;

            }
            return result;
        }

        /// <summary>
        /// this function sets the selected parameter of an exercise. This way, we know which exercise is selected by the instructor
        /// </summary>
        /// <param name="_exerciseIndex"></param>
        /// <param name="_select"></param>
        /// <returns></returns>
        public bool SetExerciseSelected(int _instructor, int _exerciseNumber, bool _select)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object

                ////zet eerst overal waar deze instructor geselecteerd stond, op false
                foreach (UNET_Classes.Exercise ex in singleton.Instructors.SingleOrDefault(x => x.ID == _instructor).Exercises)
                {
                    ex.Selected = false;
                }

                foreach (UNET_Classes.Exercise ex in singleton.Exercises)
                {
                    ex.Selected = false;
                }


                //now set the new given exercise to selected for the given instructor
                if (singleton.Instructors.Any(x => x.ID == _instructor))
                {
                    if (singleton.Instructors.SingleOrDefault(x => x.ID == _instructor).Exercises.Any(y => y.Number == _exerciseNumber))
                    {
                        //de exercise bestaat, dus we kunnen hem op selected zetten.
                        singleton.Instructors.SingleOrDefault(x => x.ID == _instructor).Exercises.SingleOrDefault(y => y.Number == _exerciseNumber).Selected = _select;

                    }
                    else
                    {
                        //de exercise bestaat NIET, dan voegen we hem toe bij deze instructor en zetten hem op selected
                        Exercise exe = singleton.Exercises.SingleOrDefault(x => x.Number == _exerciseNumber); //find the exercise
                        if (exe != null)
                        {
                            exe.Selected = true;
                            singleton.Instructors.SingleOrDefault(x => x.ID == _instructor).Exercises.Add(exe);
                     //
                            //hierna verwijderen we hem bij de oude lijst


                        }

                    }
                }
                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting selecte exercise", ex);
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


                foreach (Radio radio in singleton.Radios) //because we use the foreacht, we implicitly protect this function from ever setting the state with a non existant id
                {
                    if (radio.ID == _radioNumber)
                    {
                        radio.State = _state;
                        break;
                    }
                }

                log.Error("Finised setting the Radio status. Radionummer: " + _radioNumber + " State: " + _state.ToString());

                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting Radio Status", ex);
                result = false;

            }
            return result;
        }


        public bool SetRadios(List<Radio> _radio)
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


                singleton.PendingChanges = DateTime.Now;

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
                //first, find out if any exercise is online, if so, store the instructorid
                int instructorid = -1;
                foreach (UNET_Classes.Instructor instructor in _instructor)
                {
                    if (instructor.Online)
                    {
                        instructorid = instructor.ID;
                        break;
                    }
                }

                //then clear the array
                singleton.Instructors.Clear();


                foreach (UNET_Classes.Instructor instructor in _instructor)
                {
                    if (instructor.ID  == instructorid)
                    {
                        instructor.Online =  true;//if the instructor was online previously, make it online again in the new list
                    }

                    singleton.Instructors.Add(instructor);
                }
                singleton.PendingChanges = DateTime.Now;

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

                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting trainee", ex);
                result = false;

            }
            return result;
        }


        public bool SetTrainees(List<Trainee> _trainee)
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


                singleton.PendingChanges = DateTime.Now;

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
        public bool SetPlatforms(List<Platform> _platform)
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
        /// set the online status of a client to false
        /// </summary>
        /// <param name="_clientID"></param>
        /// <param name="_isTrainee"></param>
        /// <returns></returns>
        public bool UnRegisterClient(int _clientID, bool _isTrainee)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object

                if (_isTrainee) //in case of a TRAINEE
                {
                    //now remove the trainees or instructor from the list
                    if ((singleton.Trainees.SingleOrDefault(x => x.ID == _clientID) != null))
                    {
                        singleton.Trainees.SingleOrDefault(x => x.ID == _clientID).Online = false;
                    }
                }
                else  //in case of an INSTRUCTOR
                {
                    if ((singleton.Instructors.SingleOrDefault(x => x.ID == _clientID) != null))
                    {
                        singleton.Instructors.SingleOrDefault(x => x.ID == _clientID).Online = false;
                    }
                }

                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error unregistering a Trainee or Instructor", ex);

                result = false;

            }
            return result;
        }

        /// <summary>
        /// with this method, a trainee or instructor can 'register'
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool RegisterClient(int _clientID, string _displayName, bool _isTrainee)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                string clientName = Convert.ToString(_clientID);
                if (clientName != null && clientName != "")
                {
                    try
                    {
                        IBroadcastorCallBack callback = OperationContext.Current.GetCallbackChannel<IBroadcastorCallBack>();

                        lock (locker)
                        {
                            //remove the old client
                            if (singleton.clients.Keys.Contains(clientName))
                                singleton.clients.Remove(clientName);
                            singleton.clients.Add(clientName, callback);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error registering client: " + ex.Message);
                    }
                }

                if (_isTrainee)
                {
                    ////now add it to the trainees list
                    if ((singleton.Trainees.SingleOrDefault(x => x.ID == _clientID) == null))
                    {
                        //in case of a TRAINEE

                        Trainee trn = new Trainee(_clientID, _displayName);
                        trn.Online = true;
                        singleton.Trainees.Add(trn);
                    }
                    else
                    {
                        singleton.Trainees.SingleOrDefault(x => x.ID == _clientID).Online = true;
                    }
                }
                else  //in case of an INSTRUCTOR
                {
                    if ((singleton.Instructors.SingleOrDefault(x => x.ID == _clientID) == null))
                    {
                        Instructor inst = new Instructor(_clientID, _displayName);
                        inst.Online = true;
                        singleton.Instructors.Add(inst);
                    }
                    else
                    {
                        singleton.Instructors.SingleOrDefault(x => x.ID == _clientID).Online = true;
                    }
                }
                singleton.PendingChanges = DateTime.Now;

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting Registering Trainee or Instructor", ex);

                result = false;

            }
            return result;
        }


        /// <summary>
        /// verwijder client
        /// </summary>
        /// <param name="eventData"></param>
        public void NotifyServer(EventDataType eventData)
        {
            UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object

            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in singleton.clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToClient(eventData);
                        }
                        catch (Exception ex)
                        {
                            inactiveClients.Add(client.Key);
                        }
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        singleton.clients.Remove(client);
                    }
                }
                singleton.PendingChanges = DateTime.Now;

            }
        }

        /// <summary>
        /// Given the id of the trainee, retrieve the current assigned exercise for this trainee
        /// </summary>
        /// <param name="_traineeID"></param>
        /// <returns></returns>
        public CurrentInfo GetExerciseInfo(int _traineeID)
        {
            CurrentInfo result = null;
            try

            {
                UNET_Singleton singleton = UNET_Singleton.Instance;

                //Loop thrue the exercises and try to find the trainee in the exercises. If so, return the info
                foreach (Instructor instr in singleton.Instructors)
                {

                    foreach (Exercise ex in instr.Exercises)
                    {
                        //loop thrue the list of assigned trainees
                        foreach (Trainee tr in ex.TraineesAssigned)
                        {
                            if (tr.ID == _traineeID)
                            {
                                result = new CurrentInfo();
                                result.ID = ex.Number;
                                result.ExerciseName = ex.ExerciseName;
                                //   result.ConsoleRole = ex.RolesAssigned[0].Name;
                                result.ExerciseMode = "todo: mode";
                                result.ConsoleRole = "todo: console";
                                result.Platform = "todo: platform";
                                result.InstructorName = instr.Name;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting the exercise info " + ex.Message);
                //    AppendToLog(string.Format("Error getting the exercise info  at: {0}", DateTime.Now.ToString("G")));

                result = null;
            }
            return result;
        }
        #endregion


        #region Assist

        /// <summary>
        /// The create assist adds an assist to the Assits list. Other active assists for this trainee must be canceled
        /// </summary>
        /// <param name="_traineeID"></param>
        /// <param name="_traineeInfo"></param>
        /// <returns></returns>
        public bool CreateAssist(int _traineeID, string _traineeInfo)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                //set all unacknowledge assist to true
                //todo: ooit iets moois Lync van maken
                foreach (Assist assist in singleton.Assists)
                {
                    if (assist.TraineeID == _traineeID && assist.Acknowledged == false)
                    {
                        assist.Acknowledged = true;
                        assist.AcknowledgedBy = "The system";
                        assist.AcknowledgeTime = DateTime.Now;
                    }

                }
                //    singleton.Assists.Select(x => x.TraineeID == _traineeID).ToList().ForEach(c => c = true);


                //create assist
                Assist ass = new Assist(_traineeID, _traineeInfo);
                singleton.Assists.Add(ass);



                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error creating assist for:" + _traineeInfo, ex);
                result = false;

            }
            return result;

        }


        /// <summary>
        /// Set an assist request to acknowledged
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <param name="_traineeID"></param>
        /// <returns></returns>
        public bool AcknowledgeAssist(int _instructorID, int _traineeID)
        {
            bool result = true;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object

                //create assist
                //    Assist ass = new Assist(_traineeID, _traineeInfo);
                //    singleton.Assists.Add(ass);
                //todo: ooit iets moois Lync van maken
                foreach (Assist assist in singleton.Assists)
                {
                    if (assist.TraineeID == _traineeID && assist.Acknowledged == false)
                    {
                        assist.Acknowledged = true;
                        assist.AcknowledgedBy = _instructorID.ToString();
                        assist.AcknowledgeTime = DateTime.Now;
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error creating assist for:" + _traineeID, ex);
                result = false;

            }

            return result;
        }


        /// <summary>
        /// retrieve all unacknowledged assist requests for the given instructor.
        /// </summary>
        /// <param name="_instructorID"></param>
        /// <returns></returns>
        public List<UNET_Classes.Assist> GetAssists(int _instructorID)
        {
            List<Assist> result = new List<Assist>();
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object


                ////first retrieve the list of trainees that are assigned to this instructor
                ////then retrieve the unacknowledged requests for each of these assigned trainees
                //foreach (Instructor inst in singleton.Instructors)
                //{
                //    if (_instructorID != -1)
                //    {
                //        if (inst.ID == _instructorID)
                //        {
                //            foreach (Exercise exe in inst.Exercises)
                //            {
                //                foreach (Trainee trn in exe.TraineesAssigned)  //here we have a list of assigned trainees to this particular instructor
                //                {
                //                    foreach (Assist ass in singleton.Assists) //now we loop thrue the assist to find trainees that requested assist
                //                    {
                //                        if ((ass.TraineeID == trn.ID) && (ass.Acknowledged == false)) //if we found the trainee AND the assist is unacknowledged
                //                        {
                //                            result.Add(ass);
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {// if _instructorid == -1, then we want ALL pending Assists
                //        foreach (Exercise exe in inst.Exercises)
                //        {
                //            foreach (Trainee trn in exe.TraineesAssigned)  //here we have a list of assigned trainees to this particular instructor
                //            {
                //                foreach (Assist ass in singleton.Assists) //now we loop thrue the assist to find trainees that requested assist
                //                {
                //                    if ((ass.TraineeID == trn.ID) && (ass.Acknowledged == false)) //if we found the trainee AND the assist is unacknowledged
                //                    {
                //                        result.Add(ass);
                //                    }
                //                }
                //            }
                //        }


                //    }
                //}

                //just return all unacknowledged assists
                foreach (Assist ass in singleton.Assists) //now we loop thrue the assist to find trainees that requested assist
                {
                    if (ass.Acknowledged == false)
                    {
                        result.Add(ass);
                    }
                }


            }
            catch (Exception ex)
            {
                log.Error("Error retrieving assists for:" + _instructorID, ex);

            }

            return result;
        }
        #endregion

        #region PendingChanges
        /// <summary>
        /// this function returns the datetime of the last time something has changed in the singleton lists
        /// </summary>
        /// <returns></returns>
        public DateTime GetPendingChanges()
        {
            DateTime result;
            try
            {
                UNET_Singleton singleton = UNET_Singleton.Instance;//get the singleton object
                result = singleton.PendingChanges;



                //if there is a pending assist, this must be known on the clients as well!
                if (singleton.Assists.Count(x => x.Acknowledged == false) > 0)
                {
                    result = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error retrieving datetime of pendingchanges", ex);
                result = DateTime.MinValue;

            }

            return result;

        }

        #endregion

    }

    /// <summary>
    /// generate a random string
    /// https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
    /// </summary>
    public class KeyGenerator
    {
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}

