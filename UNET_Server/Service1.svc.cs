using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;

namespace UNET_Server
{
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel.Activation;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements
        (RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service1 : IService1
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string cUploadFileDirectory = ConfigurationManager.AppSettings["UploadFileDirectory"];
        private readonly string cFileSourceDirectory = ConfigurationManager.AppSettings["FileSourceDirectory"];
        private readonly string clogfile = ConfigurationManager.AppSettings["LogFile"];
        private UNET_Server.Classes.UNET_Server_Singleton UNET_Singleton;
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

        public void IService1()
        {
            log4net.Config.BasicConfigurator.Configure();

            // Register callbacks from callcontrol
            log.Info("Started UNet Server");
        }


        //public Stream GetLargeObject(string _filename)
        //{
        //    #region performance testing

        //    //in this test, the filename and location is given from the client, but that is not the real situation
        //    string filePath = (@"C:\Testfiles\test2.mp4");// Path.Combine(cFileSourceDirectory, _filename);
        //    Stopwatch sw = new Stopwatch(); //exlusively for measuring the performance
        //    sw.Start();
        //    //file info about  the given file
        //    //   FileInfo oFileInfo = new FileInfo(filePath);
        //    //   long filekilobytes = oFileInfo.Length;
        //    AppendToLog(string.Format("Start GetLargeObject at: {0}", DateTime.Now.ToString("G")));

        //    int filekilobytes = 0; //todo: fileinfo toevoegen
        //    #endregion

        //    try
        //    {
        //        //extract the path from the filename
        //        string filename = Path.GetFileName(_filename);
        //        Console.WriteLine(filePath);
        //        //  FileStream imageFile = File.OpenRead(Path.Combine(cFileSourceDirectory, filename));
        //        // return imageFile;
        //        FileStream stream = new FileStream(Path.Combine(cFileSourceDirectory, filename), FileMode.Open, FileAccess.Read);

        //        return stream;
        //    }

        //    catch (IOException ex)
        //    {
        //        AppendToLog(String.Format("An exception was thrown while trying to open file {0} Exception: {1}", filePath, ex.Message));

        //        throw;
        //    }
        //    finally
        //    {
        //        AppendToLog(String.Format("File sent: GetLargeObject Duration: {0} Filesize (mb): {1} Filename: {2}", sw.Elapsed.TotalSeconds.ToString("#0.###"), (filekilobytes * 0.000001).ToString("#0.###"), filePath));
        //    }
        //}

        #region Getters
        /// <summary>
        /// Get the exercises from the inline memory
        /// </summary>
        /// <returns></returns>
        public List<string> GetExercises()
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


        public List<string> GetRoles()
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
        public bool SetExercises(string _exercise)
        {
            bool result = true;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
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

        /// <summary>
        /// Create a listing of the files that exist in the FileSource directory and return this list to the client
        /// </summary>
        /// <returns></returns>
        //public List<string> GetAvailableFiles()
        //{
        //    List<string> result = new List<string>();
        //    try
        //    {

        //        result.AddRange(Directory.GetFiles(@"c:\temp"));

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception retrieving the available file list: " + ex.Message);
        //        result.Add("Error retrieving list");
        //        throw;
        //    }
        //    return result;
        //}




        ///// <summary>
        ///// UNET test get roles
        ///// </summary>
        ///// <returns></returns>
        //public List<string> GetTestRoles()
        //{
        //    List<string> result = new List<string>();
        //    try
        //    {

        //        result.Add("rol1");
        //        result.Add("rol2");
        //        result.Add("rol3");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception retrieving the Test roles: " + ex.Message);
        //        result.Add("Error retrieving list");
        //        throw;
        //    }
        //    return result;
        //}


        ///// <summary>
        ///// store the given file at the server on the designated path..
        ///// This methods return type is void. That is because otherwise a wcf error 
        ///// </summary>
        ///// <param name="_request"></param>
        ///// <returns></returns>
        //public void SaveLargeObject(SaveFileInfo _request)
        //{
        //    //in this test, the filename and location is given from the client, but that is not the real situation
        //    string filePath = Path.Combine(cUploadFileDirectory, _request.FileName);
        //    string fileName = Path.GetFileName(_request.FileName);

        //    #region performance testing

        //    Stopwatch sw = new Stopwatch(); //exlusively for measuring the performance
        //    sw.Start();
        //    //file info about the given file
        //    FileInfo oFileInfo = new FileInfo(filePath);
        //    long filekilobytes = oFileInfo.Length;
        //    AppendToLog(string.Format("Start GetLargeObject at: {0}", DateTime.Now.ToString("G")));

        //    #endregion

        //    try
        //    {
        //        SaveStream(_request.FileByteStream, Path.Combine(cUploadFileDirectory, fileName));
        //        AppendToLog("End: SaveLargeObject Duration: " + sw.Elapsed.TotalSeconds.ToString("#0.###") +
        //                    " Filesize (mb): " + (filekilobytes * 0.000001).ToString("#0.###") + " Filename: " + filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        AppendToLog(String.Format("An exception was thrown while trying to open file {0} Exception: {1}", filePath, ex.Message));

        //    }
        //}

        /// <summary>
        /// this method actually saves the stream to the given file location
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="destPath"></param>
        //private void SaveStream(Stream stream, string destPath)
        //{
        //    using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        //    {
        //        stream.CopyTo(fileStream);
        //    }
        //}

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

