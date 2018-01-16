
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UNET_Classes;

namespace SIM2UNET
{
    /// <summary>
    /// singleton class that holds the UDP listener
    /// </summary>
    public sealed class UDPListenerSingleton
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();

        private const int listenPort = 11000;
        [ThreadStatic]
        private static UDPListenerSingleton instance = null;
        private static readonly object syncRoot = new object();

        private UDPListenerSingleton()
        {
            //bool done = false;
            //UdpClient listener = new UdpClient(listenPort);
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            //string received_data;
            //byte[] receive_byte_array;
            //try

            //{
            //    while (!done)
            //    {
            //        Console.WriteLine("Waiting for broadcast");
            //        // this is the line of code that receives the broadcase message.

            //        // It calls the receive function from the object listener (class UdpClient)

            //        // It passes to listener the end point groupEP.

            //        // It puts the data from the broadcast message into the byte array

            //        // named received_byte_array.

            //        // I don't know why this uses the class UdpClient and IPEndPoint like this.

            //        // Contrast this with the talker code. It does not pass by reference.

            //        // Note that this is a synchronous or blocking call.

            //        receive_byte_array = listener.Receive(ref groupEP);
            //        Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
            //        received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);

            //        if(received_data.Length > 0)
            //        {
            //            Send2UNET(received_data);
            //        }
            //        Console.WriteLine("data follows \n{0}\n\n", received_data);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
            //listener.Close();
        }

        public static UDPListenerSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UDPListenerSingleton();
                        }
                    }
                }
                return instance;
            }
        }

        #region SendToUNET

        /// <summary>
        /// Interpreteer de binaire data en send naar UNET_Service
        /// </summary>
        /// <param name="_receiveddata"></param>
        public void Send2UNET(string _receiveddata)
        {
             if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            
             //loop nu door de receiveddata array en trek deze uit elkaar
            for (int i = 0; i < _receiveddata.Length  - 1; i++)
            {
                //hier wordt de binaire array uit elkaar eetrokken


            }

                if (!_receiveddata.ToLower().Contains("frank"))
            {   //Voeg voor iedere trainee-id een trainee object toe
             //   string[] instructorids = tbxInstructorIDs.Text.Split(',');

                List<Instructor> instructorlist = new List<Instructor>();
                Instructor inst = new Instructor(Convert.ToInt16("1020"), "Instructor on spectre 1012");// let op!! alleen de eerste instructor komt aan bod!!
                inst.Exercises.Add(new Exercise(1, "Exercise 1"));
                instructorlist.Add(inst);
                service.SetInstructors(instructorlist.ToArray());
            
            }
        
        }
        #endregion
    }


}
