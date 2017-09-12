using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using FT_HANDLE = System.UInt32;

namespace HardwareInterface.FTDI
{
    public class D2xxAccess
    {
        public D2xxAccess()
        {
        }

        private int miUSBFtWriteCount = 0;
        private bool mbD2xxConnectionLost = false;

        protected UInt32 dwListDescFlags;
        //		protected UInt32 m_hPort;
        //		protected Thread pThreadRead;
        //		protected Thread pThreadWrite;
        protected bool fContinue;


        #region "FT_Status Enum";

        public enum FT_STATUS//:Uint32
        {
            FT_OK = 0,
            FT_INVALID_HANDLE,
            FT_DEVICE_NOT_FOUND,
            FT_DEVICE_NOT_OPENED,
            FT_IO_ERROR,
            FT_INSUFFICIENT_RESOURCES,
            FT_INVALID_PARAMETER,
            FT_INVALID_BAUD_RATE,
            FT_DEVICE_NOT_OPENED_FOR_ERASE,
            FT_DEVICE_NOT_OPENED_FOR_WRITE,
            FT_FAILED_TO_WRITE_DEVICE,
            FT_EEPROM_READ_FAILED,
            FT_EEPROM_WRITE_FAILED,
            FT_EEPROM_ERASE_FAILED,
            FT_EEPROM_NOT_PRESENT,
            FT_EEPROM_NOT_PROGRAMMED,
            FT_INVALID_ARGS,
            FT_OTHER_ERROR
        };

        #endregion

        #region "Constants";
        public const UInt32 FT_BAUD_300 = 300;
        public const UInt32 FT_BAUD_600 = 600;
        public const UInt32 FT_BAUD_1200 = 1200;
        public const UInt32 FT_BAUD_2400 = 2400;
        public const UInt32 FT_BAUD_4800 = 4800;
        public const UInt32 FT_BAUD_9600 = 9600;
        public const UInt32 FT_BAUD_14400 = 14400;
        public const UInt32 FT_BAUD_19200 = 19200;
        public const UInt32 FT_BAUD_38400 = 38400;
        public const UInt32 FT_BAUD_57600 = 57600;
        public const UInt32 FT_BAUD_115200 = 115200;
        public const UInt32 FT_BAUD_230400 = 230400;
        public const UInt32 FT_BAUD_460800 = 460800;
        public const UInt32 FT_BAUD_921600 = 921600;

        public const UInt32 FT_LIST_NUMBER_ONLY = 0x80000000;
        public const UInt32 FT_LIST_BY_INDEX = 0x40000000;
        public const UInt32 FT_LIST_ALL = 0x20000000;
        public const UInt32 FT_OPEN_BY_SERIAL_NUMBER = 1;
        public const UInt32 FT_OPEN_BY_DESCRIPTION = 2;

        // Word Lengths
        public const byte FT_BITS_8 = 8;
        public const byte FT_BITS_7 = 7;
        public const byte FT_BITS_6 = 6;
        public const byte FT_BITS_5 = 5;

        // Stop Bits
        public const byte FT_STOP_BITS_1 = 0;
        public const byte FT_STOP_BITS_1_5 = 1;
        public const byte FT_STOP_BITS_2 = 2;

        // Parity
        public const byte FT_PARITY_NONE = 0;
        public const byte FT_PARITY_ODD = 1;
        public const byte FT_PARITY_EVEN = 2;
        public const byte FT_PARITY_MARK = 3;
        public const byte FT_PARITY_SPACE = 4;

        // Flow Control
        public const UInt16 FT_FLOW_NONE = 0;
        public const UInt16 FT_FLOW_RTS_CTS = 0x0100;
        public const UInt16 FT_FLOW_DTR_DSR = 0x0200;
        public const UInt16 FT_FLOW_XON_XOFF = 0x0400;

        // Purge rx and tx buffers
        public const byte FT_PURGE_RX = 1;
        public const byte FT_PURGE_TX = 2;

        // Events
        public const UInt32 FT_EVENT_RXCHAR = 1;
        public const UInt32 FT_EVENT_MODEM_STATUS = 2;
        #endregion

        #region "Properties";

        public int MiFtWriteCount
        {
            get
            {
                return miUSBFtWriteCount;
            }
        }

        public bool MbD2xxConnectionLost
        {
            get
            {
                return mbD2xxConnectionLost;
            }
            set
            {
                mbD2xxConnectionLost = value;
            }
        }

        #endregion

        #region "Win32API";

        // The following functions are the required to 
        // make communication with the USB Port possible.

        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ListDevices(void* pvArg1, void* pvArg2, UInt32 dwFlags);  // FT_ListDevices by number only
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ListDevices(UInt32 pvArg1, void* pvArg2, UInt32 dwFlags); // FT_ListDevcies by serial number or description by index only
        [DllImport("FTD2XX.dll")]
        static extern FT_STATUS FT_Open(UInt32 uiPort, ref FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_OpenEx(void* pvArg1, UInt32 dwFlags, ref FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern FT_STATUS FT_Close(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Read(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Write(FT_HANDLE ftHandle, void* lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesWritten);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBaudRate(FT_HANDLE ftHandle, UInt32 dwBaudRate);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDataCharacteristics(FT_HANDLE ftHandle, byte uWordLength, byte uStopBits, byte uParity);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetFlowControl(FT_HANDLE ftHandle, char usFlowControl, byte uXon, byte uXoff);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDtr(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ClrDtr(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetRts(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ClrRts(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetModemStatus(FT_HANDLE ftHandle, ref UInt32 lpdwModemStatus);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetChars(FT_HANDLE ftHandle, byte uEventCh, byte uEventChEn, byte uErrorCh, byte uErrorChEn);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_Purge(FT_HANDLE ftHandle, UInt32 dwMask);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetTimeouts(FT_HANDLE ftHandle, UInt32 dwReadTimeout, UInt32 dwWriteTimeout);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetQueueStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBreakOn(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBreakOff(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetStatus(FT_HANDLE ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetEventNotification(FT_HANDLE ftHandle, UInt32 dwEventMask, void* pvArg);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_ResetDevice(FT_HANDLE ftHandle);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetDivisor(FT_HANDLE ftHandle, char usDivisor);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetLatencyTimer(FT_HANDLE ftHandle, ref byte pucTimer);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetLatencyTimer(FT_HANDLE ftHandle, byte ucTimer);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_GetBitMode(FT_HANDLE ftHandle, ref byte pucMode);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetBitMode(FT_HANDLE ftHandle, byte ucMask, byte ucEnable);
        [DllImport("FTD2XX.dll")]
        static extern unsafe FT_STATUS FT_SetUSBParameters(FT_HANDLE ftHandle, UInt32 dwInTransferSize, UInt32 dwOutTransferSize);
        #endregion

        #region "Methods";


        //______________________________BitBang functions__________________________________________

        public unsafe int FT_Open_USB(UInt32 iDevice, ref UInt32 FtHandle)
        {
            try
            {
                FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
                ftStatus = FT_Open(iDevice, ref FtHandle);


                if (ftStatus == FT_STATUS.FT_OK)
                {
                    return 0;
                }
                else { return -1; }  //error, not connected
            }
            catch (Exception e)
            { return -1; }
        }

        public unsafe void FT_Close_USB(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_Close(mLocal_hPort);
        }

        public unsafe void FT_SetBitMode_USB(ref UInt32 mLocal_hPort, byte ucMask, byte ucMode)
        {
            //ucMask must be 0x00 te be all input.
            //ucMode must be 0x01 for asynchronous bitbang mode or 0x04 for synchronous bitbang mode
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_SetBitMode(mLocal_hPort, ucMask, ucMode);
        }

        public unsafe byte FT_GetBitMode_USB(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            byte PinResult = 0;
            ftStatus = FT_GetBitMode(mLocal_hPort, ref PinResult);
            if (ftStatus == FT_STATUS.FT_OK)
            {
                return PinResult;
            }
            else
            {
                return 0;
            }
        }

        public unsafe void FT_SetBaudRate_USB(ref UInt32 mLoCal_hport, UInt32 BaudRate)
        {
            //baudrate is normally 9600 baud
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_SetBaudRate(mLoCal_hport, BaudRate);
        }

        public unsafe void FT_Purge_USB(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_Purge(mLocal_hPort, FT_PURGE_TX);
        }

        //________________________________________________________________________________

        #region unused
        public unsafe uint FT_ListDevices_USB()
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            UInt32 numDevs;
            void* p1;

            p1 = (void*)&numDevs;
            ftStatus = FT_ListDevices(p1, null, FT_LIST_NUMBER_ONLY);

            return numDevs;
        }

        public unsafe string FT_ListDevicesSerial_USB(int SelDev)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            byte[] sDevName = new byte[64];
            fixed (byte* pBuf = sDevName)

                ftStatus = FT_ListDevices((uint)SelDev, pBuf, FT_LIST_BY_INDEX | FT_OPEN_BY_SERIAL_NUMBER);
            if (ftStatus == FT_STATUS.FT_OK)
            {
                string str;
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                str = enc.GetString(sDevName, 0, sDevName.Length);
                return str;
            }
            else
            {
                MessageBox.Show("Error list devices " + Convert.ToString(ftStatus), "Error");
                string str = "error";
                return str;
            }
        }

        public unsafe string FT_ListDevicesDescription_USB(int SelDev)
        {

            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            byte[] sDevName = new byte[64];
            fixed (byte* pBuf = sDevName)

                ftStatus = FT_ListDevices((uint)SelDev, pBuf, FT_LIST_BY_INDEX | FT_OPEN_BY_DESCRIPTION);
            if (ftStatus == FT_STATUS.FT_OK)
            {
                string str;
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                str = enc.GetString(sDevName, 0, sDevName.Length);
                return str;
            }
            else
            {
                MessageBox.Show("Error list devices " + Convert.ToString(ftStatus), "Error");
                string str = "error";
                return str;
            }
        }

        public unsafe bool FT_CheckCurrentConnection_USB(int SelDev)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            byte[] sDevName = new byte[64];									// moet 12 bytes zijn
            fixed (byte* pBuf = sDevName)

                ftStatus = FT_ListDevices((uint)SelDev, pBuf, FT_LIST_BY_INDEX | FT_OPEN_BY_DESCRIPTION);
            if (ftStatus == FT_STATUS.FT_OK)
            {
                return true;
            }
            else
            {
                // return wordt niet uitgevoerd als hier een messagebox staat!!!
                //				MessageBox.Show("Error list devices " + Convert.ToString(ftStatus), "Error");
                return false;
            }
        }

        public unsafe UInt32 FT_OpenEx_USB(string SelDevSerial, ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] sDevName = enc.GetBytes(SelDevSerial);
            fixed (byte* pBuf = sDevName)
            {
                ftStatus = FT_OpenEx(pBuf, FT_LIST_BY_INDEX | FT_OPEN_BY_SERIAL_NUMBER, ref mLocal_hPort);
            }
            if (ftStatus == FT_STATUS.FT_OK)
            {
                MbD2xxConnectionLost = false;
                // Set up the port
                ftStatus = FT_SetBaudRate(mLocal_hPort, 4800);
                ftStatus = FT_Purge(mLocal_hPort, FT_PURGE_RX | FT_PURGE_TX);
                ftStatus = FT_SetTimeouts(mLocal_hPort, 500, 500);

                return mLocal_hPort;
            }
            else
            {
                MessageBox.Show("Failed To Open Port" + Convert.ToString(ftStatus));
                return 0;
            }
        }

        public unsafe void FT_Read_USB(ref UInt32 mLocal_hPort, out byte bOutBuf)
        {
            byte[] cBuf = new Byte[1];
            bOutBuf = 0;
            UInt32 dwRet = 0;

            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;

            fixed (byte* pBuf = cBuf)
            {
                ftStatus = FT_Read(mLocal_hPort, pBuf, 1, ref dwRet);
            }

            if (ftStatus != FT_STATUS.FT_OK && !(mbD2xxConnectionLost == true))
            {
                // bij failed  1)stop de read timer 
                //			   2) de request timer 
                //			   3) Geef gebruiker de keuze geven: RETRY or CANCEL
                mbD2xxConnectionLost = true;
                MessageBox.Show("Failed to Read, closing connection, Try again." + Convert.ToString(ftStatus));
            }

            if (dwRet == 1)
            {
                bOutBuf = cBuf[0];
                //return bOutBuf;
                //lbDataBytes.Items.Add(BitConverter.ToString(cBuf, 0, 1));
            }
        }

        public unsafe void FT_Write_USB(ref UInt32 mLocal_hPort, ref byte[] bInBuf)
        {
            UInt32 dwRet = 0;
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            int i;
            uint uBufLength;
            byte[] cBuf = new Byte[12];                                     //moet 12 bytes zijn tbv frame size
                                                                            //			byte bOutBuf;

            for (i = 0; i < bInBuf.Length; i++)
            {
                cBuf[i] = bInBuf[i];
            }
            //	bInBuf = 0;
            uBufLength = Convert.ToUInt32(cBuf.Length);

            fixed (byte* pBuf = cBuf)
            {
                //ftStatus = FT_Write(mLocal_hPort, pBuf, uBufLength, ref dwRet);
                ftStatus = FT_Write(mLocal_hPort, pBuf, uBufLength, ref dwRet);
            }
            miUSBFtWriteCount = miUSBFtWriteCount + 1;
            if (ftStatus != FT_STATUS.FT_OK)
            {
                MessageBox.Show("Failed to Write " + Convert.ToString(ftStatus));
            }
        }

        public unsafe void FT_ResetDevice_USB(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_ResetDevice(mLocal_hPort);
            //			int i;

        }

        public unsafe void FT_SetTimeouts_USB(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            //			FT_SetTimeouts(m_hPort, 3000, 3000);
            ftStatus = FT_SetTimeouts(mLocal_hPort, 3000, 3000);
        }

        public unsafe void FT_SetTimeouts_USB_300(ref UInt32 mLocal_hPort)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            ftStatus = FT_SetTimeouts(mLocal_hPort, 30, 30);
        }
        #endregion

        #endregion

    }
}
