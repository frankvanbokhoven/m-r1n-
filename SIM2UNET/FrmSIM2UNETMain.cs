using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace SIM2VOIP
{
    public partial class FrmSIM2VOIPMain : Form
    {
        private UdpClient listener;
        private IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 10000);
        private string received_data;
        private byte[] receive_byte_array;
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        bool done = false;
        private UDPListenerSingleton singleton = UDPListenerSingleton.Instance;
        public FrmSIM2VOIPMain()
        {
            InitializeComponent();
            log4net.Config.BasicConfigurator.Configure();
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {


            if (btnStartListening.ImageIndex == 0)
            {
                btnStartListening.ImageIndex = 1;
                listener = new UdpClient(Convert.ToInt16(tbxPort.Text.Trim()));
                Thread thread = new Thread(new ThreadStart(Listen));
                done = false;
            thread.Start();
         }
            else
            {
                done = true;
                btnStartListening.ImageIndex = 0;
            }
         }


        private void Listen()
        {
            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    // this is the line of code that receives the broadcase message.
                    // It calls the receive function from the object listener (class UdpClient)
                    // It passes to listener the end point groupEP.
                    // It puts the data from the broadcast message into the byte array
                    // named received_byte_array.
                    // I don't know why this uses the class UdpClient and IPEndPoint like this.
                    // Contrast this with the talker code. It does not pass by reference.
                    // Note that this is a synchronous or blocking call.
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    Console.WriteLine("data follows \n{0}\n\n", received_data);

                    if (received_data.Length > 0)
                    {
                     singleton.Send2UNET(received_data);
                    }

                }
                if (done == true)
                {
                    Console.WriteLine("UDP Listening cancelled");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while listening: " + e.ToString());
            }
            listener.Close();
        }
    }
}
