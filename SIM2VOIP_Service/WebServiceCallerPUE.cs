using System;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using ESSaverBase;
using XPUEESSaver.Computerfuel;
//using ESSaverPUE.ESSaverWebService;

namespace XPUEESSaver
{



    /// <summary>
    /// This class contains all methods for calling webservices
    /// </summary>
    internal class WebServiceCallerPUE
    {
        #region ESSaverRights enum

        public enum ESSaverRights
        {
            User,
            Manager,
            Administrator
        }

        #endregion

        public string RetrievedVersionString; //deze property wordt gevuld NA aanroepen van checkversion webservice

        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }



        public string GetCharacteristics(string regcode, string machineGUID, string hardwareType, string name)
        {
            string charstring = string.Empty;
            try
            {
                //  ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                var essws = new ServiceESSaver();
                //     IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                // Use the default credentials of the logged on user.
                //     proxyObject.Credentials = CredentialCache.DefaultCredentials;
                //     essws.Proxy = proxyObject;

                charstring = essws.GetCharacteristics(regcode, machineGUID, hardwareType, name);
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WebserviceCaller>GetCharacteristics");
            }
            return charstring;
        }


        /// <summary>
        /// Download the latest version of the msi, especially for the particular organisation
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public string GetESSaverDownloadLink(string machineCode)
        {
            string result = string.Empty;
            try
            {
                var essws = new ServiceESSaver();

                result = essws.GetESSaverDownloadLink(machineCode);
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace,
                                    ex.InnerException + "WebserviceCaller>GetESSaverDownloadLink");
            }
            return result;
        }


        /// <summary>
        /// Retrieve the rights from the server.
        /// If there is no connection, use the rights in the registry
        /// </summary>
        /// <param name="machineGUID"></param>
        /// <returns></returns>
        public string GetRights(string machineGUID)
        {
            string result = "ADMINISTRATOR";
            try
            {
                if (IsAbleToCommunicate())
                {
                    //  ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                    var essws = new ServiceESSaver();
                    //     IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                    // Use the default credentials of the logged on user.
                    //     proxyObject.Credentials = CredentialCache.DefaultCredentials;
                    //     essws.Proxy = proxyObject;

                    string rights = essws.GetRights(machineGUID);


                    if (rights == "ADMINISTRATOR")
                    {
                        result = "ADMINISTRATOR"; //ESSaverRights.Administrator; 
                    }
                    else
                    {
                        if (rights == "MANAGER")
                        {
                            result = "MANAGER"; // ESSaverRights.Manager;
                        }
                        else
                        {
                            result = "USER"; // ESSaverRights.User;
                        }
                    }
                }
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WebserviceCaller>GetRights");
            }
            return result;
        }



        /// <summary>
        /// this function returns all the known Organisations
        /// </summary>
        /// <returns></returns>
        public string GetESSaverOrganisations(string key)
        {
            string resultstring = string.Empty;
            try
            {
                //   ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                var essws = new ServiceESSaver();
                //      IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                // Use the default credentials of the logged on user.
                ////     proxyObject.Credentials = CredentialCache.DefaultCredentials;
                //   essws.Proxy = proxyObject;
                // IWebProxy proxyObject = new WebProxy("http://proxyserver:80", true);
                //       IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));

                //    essws.Proxy = WebProxy.GetDefaultProxy();//FetchURL(essws.Url);

                //      resultstring = essws.GetOrganisationfs("12345678945345");//todo: hier moet het échte serienummer van de klant staan
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WebserviceCaller>GetOrganisations");
                resultstring = string.Empty;
            }
            return resultstring;
        }

        /// <summary>
        /// An organisation can set its min - max values. This function retrieves them.
        /// A user can only move its slider between these min and max values
        /// </summary>
        /// <param name="regcode"></param>
        /// <param name="machineGUID"></param>
        /// <returns></returns>
        public string GetMinMaxSaveValue(string organisationName)
        {
            string result = string.Empty;
            try
            {
                //  ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                var essws = new ServiceESSaver();
                //       IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                // Use the default credentials of the logged on user.
                //      proxyObject.Credentials = CredentialCache.DefaultCredentials;
                //       essws.Proxy = proxyObject;
                result = essws.GetMinMaxSaveValue(organisationName);
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WebserviceCaller>GetMinMaxSaveValue");
            }
            return result;
        }


        /// <summary>
        /// Fetch given url and stores it to the file
        /// </summary>
        /// based on: http://bansky.net/blog/2007/10/fetching-url-via-default-proxy-in-c-sharp/
        /// <returns>True if operation was successful</returns>
        private IWebProxy FetchURL(string url)
        {
            IWebProxy result = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                // Use default system proxy
                request.Proxy = WebRequest.DefaultWebProxy;
                var response = (HttpWebResponse) request.GetResponse();

                // Check for status code
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = WebRequest.DefaultWebProxy;
                }
                else
                {
                    result = null;
                }
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "FetchProxy");
            }

            // return result of the operation
            return result;
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool InternetGetConnectedState(ref InternetConnectionState_e lpdwFlags, int dwReserved);


        // Return true or false if connecting through a proxy server 
        public bool connectingThroughProxy()
        {
            InternetConnectionState_e flags = 0;
            InternetGetConnectedState(ref flags, 0);
            bool hasProxy = false;

            if ((flags & InternetConnectionState_e.INTERNET_CONNECTION_PROXY) != 0)
            {
                hasProxy = true;
            }
            else
            {
                hasProxy = false;
            }

            return hasProxy;
        }


        /// <summary>
        ///  Deze functie test of ESSaver in staat eis te communiceren
        /// http://stackoverflow.com/questions/520347/c-how-do-i-check-for-a-network-connection        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsAbleToCommunicate()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();

                //     Ping ping = new Ping();
                //    PingReply pingStatus = ping.Send(IPAddress.Parse("http://www.essaver.net"));

                //   http://stackoverflow.com/questions/801075/c-testing-active-internet-connection-pinging-google-com
                //  return (pingStatus.Status == IPStatus.Success); 
                //  ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                /*         ESSaverMonitor.Computerfuel.ServiceESSaver essws = new Computerfuel.ServiceESSaver();
                  //       IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                         // Use the default credentials of the logged on user.
                   //      proxyObject.Credentials = CredentialCache.DefaultCredentials;
                         WebRequest request = WebRequest.Create(essws.Url);

                        // Use default system proxy
                  //       request.Proxy = proxyObject;  // WebRequest.DefaultWebProxy;//todo: deze is er dus tussen geplaatst om de proxy te bypassen
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        */
                // Check for status code
                //       result = true;//(response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace,
                                    ex.InnerException + "WebserviceCaller.IsAbleToCommunicate");
                return false;
            }

            // return result of the operation
            //   return result;
        }

        /// <summary>
        /// This function sends the machineid to the webserver directly after registration
        /// </summary>
        /// <param name="regcode"></param>
        /// <param name="machineGUID"></param>
        /// <returns></returns>
        public string RegisterMachineAtWebserver(string regcode, string machineGUID, string userName)
        {
            string resultstring = string.Empty;
            try
            {
                // ESSaverMonitor.ESSaverWebService.ServiceESSaver essws = new ServiceESSaver(); // dit is de locale versie
                var essws = new ServiceESSaver();
                //        IWebProxy proxyObject = new WebProxy(new Uri(essws.Url));
                // Use the default credentials of the logged on user.
                //       proxyObject.Credentials = CredentialCache.DefaultCredentials;
                //     essws.Proxy = proxyObject;
                //       resultstring = essws.RegisterMachineAtWebserver(regcode, machineGUID, userName);
                resultstring = essws.Register(regcode, machineGUID, userName);
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace,
                                    ex.InnerException + "WebserviceCaller>RegisterMachineAtWebserver");
                resultstring = string.Empty;
            }
            return resultstring;
        }

        #region Nested type: InternetConnectionState_e

        [Flags]
        private enum InternetConnectionState_e
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        #endregion
    }
}