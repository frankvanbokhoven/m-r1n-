using System;
using Microsoft.Win32;

//using System.Windows.Forms;

namespace UNET_Theming
{
    // custom exception class
    /// <summary>
    /// based on: http://www.java2s.com/Code/CSharp/Language-Basics/Exceptionhandlewithyourownexceptionclass.htm
    /// </summary>
    internal class ESSaverRegistryException : ApplicationException
    {
        public ESSaverRegistryException(string message) :
            base(message) // pass the message up to the base class
        {
        }
    }

    /// <summary>
    /// Summary description for clsRegistry.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        private const string APPLICATION_NAME = "UNET";

        /// <summary>
        /// Method for retrieving a Registry Value.
        /// Within ESSaver, beneath the ESSaver key, there is ALWAYS a subkey, and within the subkey the value is stored.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subkey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetStringRegistryValue(string key, string subkey, string defaultValue)
        {
            string result = defaultValue;
            try
            {
                RegistryKey rk =
                    Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true).OpenSubKey(APPLICATION_NAME, true).OpenSubKey(
                        key, true);
                //     RegistryKey rk = Registry.LocalMachine.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(APPLICATION_NAME, false).OpenSubKey(key, false);
                if (rk != null)
                {
                    foreach (string sKey in rk.GetValueNames())
                    {
                        if (sKey == subkey)
                        {
                            result = (string)rk.GetValue(sKey);
                        }
                    }
                }
                return result;
            }
            catch (Exception) //General exception
            {
                //var err = new ErrorLogger.ErrorLogger();
                //err.WriteToErrorLog(ex.Message, ex.StackTrace,
                //                    ex.InnerException +
                //                    " ESSaver made an error reading the registry. Maybe the key wasn't there??");

                //// create a custom exception instance
                //var e = new ESSaverRegistryException(
                //    "ESSaver made an error reading the registry. Maybe the key wasn't there??");
                //e.HelpLink =
                //    "http://www.essaver.net";
                ////               throw e;
                //var err2 = new ErrorLogger.ErrorLogger();
                //err2.WriteToErrorLog(e.Message, e.StackTrace,
                //                     e.InnerException +
                //                     " ESSaver made an error reading the registry. Maybe the key wasn't there??");

                return defaultValue;
            }
        }

           /// <summary>
        /// write a registry value to currentuser
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stringValue"></param>
        public static void SetStringRegistryValue(string key, string subkey, string stringValue)
        {
            RegistryKey rkWrite;
            RegistryKey rk;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true).OpenSubKey(APPLICATION_NAME, true);
                //    rk = Registry.LocalMachine.OpenSubKey(SOFTWARE_KEY, true).OpenSubKey(APPLICATION_NAME, true);
                if (rk != null)
                {
                    rkWrite = rk.OpenSubKey(key, true);
                    if (rkWrite != null)
                    {
                        rkWrite.SetValue(subkey, stringValue);
                    }
                }
            }
            catch(Exception)
            {
                //geen throw();
            }
        }

    }
}
