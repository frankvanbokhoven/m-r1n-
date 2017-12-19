using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSIM2UNET.EasyPCap
{
    /// <summary>
    /// This class holds the PCap info
    /// </summary>
    public class PcapPacket
    {
        private int secs;
        private int usecs;
        private byte[] data;

        public PcapPacket(int secs, int usecs, byte[] data)
        {
            this.secs = secs;
            this.usecs = usecs;
            this.data = data;
        }

        public int Seconds
        {
            get
            {
                return secs;
            }
        }

        public int Microseconds
        {
            get
            {
                return usecs;
            }
        }

        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// Number of bytes in the data array
        /// </summary>
        public int DataLength
            {
            get
            {
                return data.Length;
            }
            }

        public override string ToString()
        {
            return String.Format("{0}.{1}: {2} bytes of data", secs, usecs, data.Length);
        }
    }
}
