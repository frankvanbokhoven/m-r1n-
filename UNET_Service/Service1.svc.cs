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
        private readonly string clogfile = ConfigurationManager.AppSettings["LogFile"]; //@"c:\temp\ServiceLog.txt";
        private byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

      


      

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
                UNET_Service.Classes.UNET_Server_Singleton singleton = UNET_Service.Classes.UNET_Server_Singleton.Instance;//get the singleton object
                result = new List<UNET_Service.Classes.Exercise>(singleton.Exercises);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);

                throw;
            }
            return result;
        }


        public List<string> GetRoles()
        {
            List<string> result = new List<string>();
            try
            {
                result.AddRange(Directory.GetFiles(@"c:\temp"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);
                result.Add("Error retrieving exercices");
                throw;
            }
            return result;
        }

        public List<string> GetRadios()
        {
            List<string> result = new List<string>();
            try
            {

                // for
                result.AddRange(Directory.GetFiles(@"c:\temp"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);
                result.Add("Error retrieving exercices");
                throw;
            }
            return result;
        }

        public List<string> GetInstructors()
        {
            List<string> result = new List<string>();
            try
            {

                // for
                result.AddRange(Directory.GetFiles(@"c:\temp"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);
                result.Add("Error retrieving exercices");
                throw;
            }
            return result;
        }

        public List<string> GetTrainees()
        {
            List<string> result = new List<string>();
            try
            {

                // for
                result.AddRange(Directory.GetFiles(@"c:\temp"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);
                result.Add("Error retrieving exercices");
                throw;
            }
            return result;
        }


        public List<string> GetPlatforms()
        {
            List<string> result = new List<string>();
            try
            {
                // for
                result.AddRange(Directory.GetFiles(@"c:\temp"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving the available exercises: " + ex.Message);
                result.Add("Error retrieving exercices");
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
                //UNET_Server_Singleton.
                result = true;
            }
            catch (Exception ex)
            {
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
                UNET_Service.Classes.UNET_Server_Singleton singleton = UNET_Service.Classes.UNET_Server_Singleton.Instance;//get the singleton object
                singleton.Exercises = _exercises.ToArray();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
               // log.Fatal("Set Exercises: ", ex);
            }
            return result;
        }

        public bool SetRoles(string _role)
        {
            return true;
        }

        public bool SetRadios(string _radio)
        {
            return true;
        }


        public bool SetInstructors(string _instructor)
        {
            return true;
        }


        public bool SetTrainees(string _trainee)
        {
            return true;
        }


        public bool SetPlatforms(string _platform)
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
