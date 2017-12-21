using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StubSIM2UNET
{
    public class UDPPacketSender
    {
        private IPAddress serverAddress;
        private int Port;

        public UDPPacketSender()
        {
            serverAddress = IPAddress.Parse("192.168.2.255");
            Port = 11000;
        }

        /// <summary>
        /// send a packect via UDP
        /// see: https://stackoverflow.com/questions/2637697/sending-udp-packet-in-c-sharp
        /// maar ook: https://social.msdn.microsoft.com/Forums/en-US/92846ccb-fad3-469a-baf7-bb153ce2d82b/simple-udp-example-code?forum=netfxnetcom
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        public bool SendPacket(string _message)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //   IPAddress serverAddr = IPAddress.Parse("192.168.2.255");

                IPEndPoint endPoint = new IPEndPoint(serverAddress, Port);

                byte[] send_buffer = Encoding.ASCII.GetBytes(_message);

                sock.SendTo(send_buffer, endPoint);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception while sending packet: " + ex.Message);
                return false;
            }
        }

    }
}
