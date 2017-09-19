using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using HardwareInterface;
using System.Runtime.InteropServices; //tbv verbergen console window

namespace TCPSocketClient
{

    /// <summary>
    /// Summary description for Client.
    /// </summary>
    public class Client 
    {
        #region hide console window
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        #endregion
        private static string HeadsetPlugged;
        private static string PttActive;

        /////////////////////////////////////////////////////////////////////////////
        ///Variables & Properties
        /////////////////////////////////////////////////////////////////////////////
        private Button btnConnectToServer;
        private Button btnSendMessage;
        private static StreamReader clientStreamReader;
        private static StreamWriter clientStreamWriter;

        /////////////////////////////////////////////////////////////////////////////
        ///Constructor
        public Client()
        {
            ////create ConnectToServer button, set its properties & event handlers
            //this.btnConnectToServer = new Button();
            //this.btnConnectToServer.Text = "Connect";
            //this.btnConnectToServer.Click += new System.EventHandler(btnConnectToServer_Click);
            ////create SendMessage button, set its properties & event handlers
            //this.btnSendMessage = new Button();
            //this.btnSendMessage.Text = "Send Message";
            //this.btnSendMessage.Top += 30;
            //this.btnSendMessage.Width += 20;
            //this.btnSendMessage.Click += new System.EventHandler(btnSendMessage_Click);

            ////add controls to windows form
            //this.Controls.Add(this.btnConnectToServer);
            //this.Controls.Add(this.btnSendMessage);
        }

        /////////////////////////////////////////////////////////////////////////////
        ///Main method
        public static void Main(string[] args)
        {


         

            //connect to server
            if (!ConnectToServer())
                Console.WriteLine("Unable to connect to server");

            //create n display windows form
          //  Client tcpSockClient = new Client();
           // Application.Run(tcpSockClient);

            Console.WriteLine("Testapplication for HardwareInterface.dll");
            Console.WriteLine("Author: Frank van Bokhoven => frankvanbokhoven@gmail.com");
            Console.WriteLine(".........................................................");
            //IHardwareInterface hardware = new FtdiInterface();
            IHardwareInterface hardware = new UsbInterface();
            hardware.Initialize();
            hardware.HeadsetPluggedChangedEvent += Hardware_HeadsetPluggedChangedEvent;
            hardware.PttChangedEvent += Hardware_PttChangedEvent;
            hardware.Start();


            var handle = GetConsoleWindow();

            // Hide this window (NOTE: AFTER THIS, THE CONSOLE WINDOW IS HIDDEN!!
            ShowWindow(handle, SW_HIDE);
            Console.ReadLine();

            hardware.Stop();
        }

        private static void Hardware_PttChangedEvent(object sender, PttChangedEventArgs e)
        {
            //       Console.WriteLine("PTT value: {0}", e.PttActive);
            try
            {
                if (e.PttActive)
                    PttActive = "true";
                else
                    PttActive = "false";
                //send message to server
                clientStreamWriter.WriteLine("ptt|" + PttActive);
                clientStreamWriter.Flush();
                Console.WriteLine(DateTime.Now.ToString("D") + " ptt: " + PttActive);
            }
            catch (Exception se)
            {
                Console.WriteLine(se.StackTrace);
            }

        }

        private static void Hardware_HeadsetPluggedChangedEvent(object sender, HeadsetPluggedChangedEventArgs e)
        {
            // Console.WriteLine("Headset value: {0}", e.HeadsetPlugged);
            try
            {
                if (e.HeadsetPlugged)
                    HeadsetPlugged = "true";
                else
                    HeadsetPlugged = "false";
                //send message to server
                clientStreamWriter.WriteLine("headset|" + HeadsetPlugged);
                clientStreamWriter.Flush();
                Console.WriteLine(DateTime.Now.ToString("D") +" headset: " + HeadsetPlugged);
            }
            catch (Exception se)
            {
                Console.WriteLine(se.StackTrace);
            }


        }

        /////////////////////////////////////////////////////////////////////////////
        ///Connect to server
        private static bool ConnectToServer()
        {
            //connect to server at given port
            try
            {
                TcpClient tcpClient = new TcpClient("localhost", 4444);
                Console.WriteLine("Connected to Server");
                //get a network stream from server
                NetworkStream clientSockStream = tcpClient.GetStream();
                clientStreamReader = new StreamReader(clientSockStream);
                clientStreamWriter = new StreamWriter(clientSockStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        /////////////////////////////////////////////////////////////////////////////
        ///Event Handlers
        /////////////////////////////////////////////////////////////////////////////
        //private void btnConnectToServer_Click(object sender, System.EventArgs e)
        //{
        //    //connect to server
        //    if (!ConnectToServer())
        //        Console.WriteLine("Unable to connect to server");
        //}

        //private void btnSendMessage_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        //send message to server
        //        clientStreamWriter.WriteLine("Hello!");
        //        clientStreamWriter.Flush();
        //        Console.WriteLine("SERVER: " + clientStreamReader.ReadLine());
        //    }
        //    catch (Exception se)
        //    {
        //        Console.WriteLine(se.StackTrace);
        //    }
        //}
    }
}
