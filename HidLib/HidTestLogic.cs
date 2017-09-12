using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using GenericHid;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace HidTest
{
    public delegate void dlgBoolean(bool b);

    public class HidTestLogic
    {
        #region Properties
        private string _vid;
        public string Vid
        {
            get { return _vid; }
            set { _vid = value; }
        }

        private string _pid;
        public string Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private bool running = false;
        public bool Running
        {
            get { return running = false; }
        }

        private bool headphonePlugged = false;
        public bool HeadphonePlugged
        {
            get { return headphonePlugged; }
            set { headphonePlugged = value; if (OnHeadsetPluggedChanged != null) OnHeadsetPluggedChanged(headphonePlugged); }
        }

        private bool pttPushed;
        public bool PttPushed
        {
            get { return pttPushed; }
            set { pttPushed = value; if (OnPttPushedChanged != null) OnPttPushedChanged(pttPushed); }
        }

        private bool showMessages;
        public bool ShowMessages
        {
            get { return showMessages; }
            set { showMessages = value; }
        }
        #endregion

        public event dlgBoolean OnHeadsetPluggedChanged, OnPttPushedChanged;

        enum sourceFlags { VUP = 0, VDN = 1, MUTE = 2, GPIOC = 3, HEADPH = 4, REGIN = 5, EEIN = 6, MCUIN = 7 }
        enum Reg1Flags { Gpio1_signal = 5 }
        private const bool USE_CONTROL_TRANSFER = true;

        private Boolean myDeviceDetected;
        private Boolean transferInProgress = false;
        private String myDevicePathName;
        private FileStream fileStreamDeviceData;
        private SafeFileHandle hidHandle;
        private DeviceManagement MyDeviceManagement = new DeviceManagement();
        private Hid MyHid = new Hid();
        private String hidUsage;
        private System.Timers.Timer tmrSample;
        private static System.Timers.Timer tmrReadTimeout;

        public bool Init(string vid, string pid)
        {
            Vid = vid;
            Pid = pid;

            tmrReadTimeout = new System.Timers.Timer(5000);
            tmrReadTimeout.Elapsed += new ElapsedEventHandler(OnReadTimeout);
            tmrReadTimeout.Stop();

            bool result = FindTheHid(Vid, Pid);

            return result;
        }

        #region Start/Stop Sampling
        public void StartSampling(int interval)
        {
            if (tmrSample == null)
            {
                tmrSample = new System.Timers.Timer(interval);
                tmrSample.AutoReset = true;
                tmrSample.Elapsed += new System.Timers.ElapsedEventHandler(tmrSample_Elapsed);
                tmrSample.Start();
                running = true;

                WriteDeviceRegisters();
            }
            else
            {
                //timer allready initialized, just update interval
                tmrSample.Interval = interval;
                tmrSample.Start();
                running = true;
            }
        }

        public void StopSampling()
        {
            if (tmrSample != null)
            {
                tmrSample.Stop();
                tmrSample = null;
            }

            running = false;
        }

        void tmrSample_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ReadAndWriteToDevice(new byte[] { 0x30, 0x00, 0x00, 0x01 });    //Request the value of REG1...
        }

        /// <summary>
        /// Initialize this Device by modifing his registers
        /// </summary>
        public void WriteDeviceRegisters()
        {
            if (showMessages)
            {
                //                Console.WriteLine("Write REG2 (don't mute headset AND front-out as headset source)");
            }
            ReadAndWriteToDevice(new byte[] { 0x20, 0x04, 0xE0, 0x02 });    //Write REG2 (don't mute automaticly when headset pluggedin AND define front-out as headset source)
        }
        #endregion

        ///  <summary>
        ///  Uses a series of API calls to locate a HID-class device
        ///  by its Vendor ID and Product ID.
        ///  </summary>
        ///          
        ///  <returns>
        ///   True if the device is detected, False if not detected.
        ///  </returns>
        private Boolean FindTheHid(string vid, string pid)
        {
            Boolean deviceFound = false;
            String[] devicePathName = new String[128];
            String functionName = "";
            Guid hidGuid = Guid.Empty;
            Int32 memberIndex = 0;
            Int32 myProductID = 0;
            Int32 myVendorID = 0;
            Boolean success = false;

            try
            {
                myDeviceDetected = false;
                CloseCommunications();

                //  Get the device's Vendor ID and Product ID from the form's text boxes.

                myVendorID = Int32.Parse(vid, NumberStyles.AllowHexSpecifier);
                myProductID = Int32.Parse(pid, NumberStyles.AllowHexSpecifier);

                //  ***
                //  API function: 'HidD_GetHidGuid

                //  Purpose: Retrieves the interface class GUID for the HID class.

                //  Accepts: 'A System.Guid object for storing the GUID.
                //  ***

                Hid.HidD_GetHidGuid(ref hidGuid);
                Debug.WriteLine("  GUID for system HIDs: " + hidGuid.ToString());

                //  Fill an array with the device path names of all attached HIDs.

                deviceFound = MyDeviceManagement.FindDeviceFromGuid(hidGuid, ref devicePathName);

                //  If there is at least one HID, attempt to read the Vendor ID and Product ID
                //  of each device until there is a match or all devices have been examined.

                if (deviceFound)
                {
                    memberIndex = 0;

                    do
                    {
                        //  ***
                        //  API function:
                        //  CreateFile

                        //  Purpose:
                        //  Retrieves a handle to a device.

                        //  Accepts:
                        //  A device path name returned by SetupDiGetDeviceInterfaceDetail
                        //  The type of access requested (read/write).
                        //  FILE_SHARE attributes to allow other processes to access the device while this handle is open.
                        //  A Security structure or IntPtr.Zero. 
                        //  A creation disposition value. Use OPEN_EXISTING for devices.
                        //  Flags and attributes for files. Not used for devices.
                        //  Handle to a template file. Not used.

                        //  Returns: a handle without read or write access.
                        //  This enables obtaining information about all HIDs, even system
                        //  keyboards and mice. 
                        //  Separate handles are used for reading and writing.
                        //  ***

                        // Open the handle without read/write access to enable getting information about any HID, even system keyboards and mice.

                        hidHandle = FileIO.CreateFile(devicePathName[memberIndex], 0, FileIO.FILE_SHARE_READ | FileIO.FILE_SHARE_WRITE, IntPtr.Zero, FileIO.OPEN_EXISTING, 0, 0);

                        functionName = "CreateFile";
                        Debug.WriteLine("  Returned handle: " + hidHandle.ToString());

                        if (!hidHandle.IsInvalid)
                        {
                            //  The returned handle is valid, 
                            //  so find out if this is the device we're looking for.

                            //  Set the Size property of DeviceAttributes to the number of bytes in the structure.

                            MyHid.DeviceAttributes.Size = Marshal.SizeOf(MyHid.DeviceAttributes);

                            //  ***
                            //  API function:
                            //  HidD_GetAttributes

                            //  Purpose:
                            //  Retrieves a HIDD_ATTRIBUTES structure containing the Vendor ID, 
                            //  Product ID, and Product Version Number for a device.

                            //  Accepts:
                            //  A handle returned by CreateFile.
                            //  A pointer to receive a HIDD_ATTRIBUTES structure.

                            //  Returns:
                            //  True on success, False on failure.
                            //  ***                            

                            success = Hid.HidD_GetAttributes(hidHandle, ref MyHid.DeviceAttributes);

                            if (success)
                            {
                                Debug.WriteLine("HIDD_ATTRIBUTES structure filled without error.");
                                Debug.WriteLine("Structure size: " + MyHid.DeviceAttributes.Size);
                                Debug.WriteLine("Vendor ID: " + Convert.ToString(MyHid.DeviceAttributes.VendorID, 16));
                                Debug.WriteLine("Product ID: " + Convert.ToString(MyHid.DeviceAttributes.ProductID, 16));
                                Debug.WriteLine("Version Number: " + Convert.ToString(MyHid.DeviceAttributes.VersionNumber, 16));

                                //  Find out if the device matches the one we're looking for.

                                if ((MyHid.DeviceAttributes.VendorID == myVendorID) && (MyHid.DeviceAttributes.ProductID == myProductID))
                                {
                                    Debug.WriteLine("  My device detected");

                                    //  Display the information in form's list box.

                                    Debug.WriteLine("Device detected:");
                                    Debug.WriteLine("Vendor ID= " + Convert.ToString(MyHid.DeviceAttributes.VendorID, 16));
                                    Debug.WriteLine("Product ID = " + Convert.ToString(MyHid.DeviceAttributes.ProductID, 16));

                                    myDeviceDetected = true;

                                    //  Save the DevicePathName for OnDeviceChange().

                                    myDevicePathName = devicePathName[memberIndex];
                                }
                                else
                                {
                                    //  It's not a match, so close the handle.

                                    myDeviceDetected = false;
                                    hidHandle.Close();
                                }
                            }
                            else
                            {
                                //  There was a problem in retrieving the information.

                                Debug.WriteLine("  Error in filling HIDD_ATTRIBUTES structure.");
                                myDeviceDetected = false;
                                hidHandle.Close();
                            }
                        }

                        //  Keep looking until we find the device or there are no devices left to examine.

                        memberIndex = memberIndex + 1;
                    }
                    while (!((myDeviceDetected || (memberIndex == devicePathName.Length))));
                }

                if (myDeviceDetected)
                {
                    //  The device was detected.
                    //  Register to receive notifications if the device is removed or attached.

                    //success = MyDeviceManagement.RegisterForDeviceNotifications(myDevicePathName, FrmMy.Handle, hidGuid, ref deviceNotificationHandle);

                    //Debug.WriteLine("RegisterForDeviceNotifications = " + success);

                    //  Learn the capabilities of the device.

                    MyHid.Capabilities = MyHid.GetDeviceCapabilities(hidHandle);

                    if (success)
                    {
                        //  Find out if the device is a system mouse or keyboard.

                        hidUsage = MyHid.GetHidUsage(MyHid.Capabilities);

                        //  Get the Input report buffer size.

                        GetInputReportBufferSize();

                        //Close the handle and reopen it with read/write access.

                        hidHandle.Close();
                        hidHandle = FileIO.CreateFile(myDevicePathName, FileIO.GENERIC_READ | FileIO.GENERIC_WRITE, FileIO.FILE_SHARE_READ | FileIO.FILE_SHARE_WRITE, IntPtr.Zero, FileIO.OPEN_EXISTING, 0, 0);

                        if (hidHandle.IsInvalid)
                        {
                            //exclusiveAccess = true;
                            Debug.WriteLine("The device is a system " + hidUsage + ".");
                            Debug.WriteLine("Windows 2000 and Windows XP obtain exclusive access to Input and Output reports for this devices.");
                            Debug.WriteLine("Applications can access Feature reports only.");
                        }

                        else
                        {
                            if (MyHid.Capabilities.InputReportByteLength > 0)
                            {
                                //  Set the size of the Input report buffer. 

                                Byte[] inputReportBuffer = null;

                                inputReportBuffer = new Byte[MyHid.Capabilities.InputReportByteLength];

                                fileStreamDeviceData = new FileStream(hidHandle, FileAccess.Read | FileAccess.Write, inputReportBuffer.Length, false);
                            }

                            if (MyHid.Capabilities.OutputReportByteLength > 0)
                            {
                                Byte[] outputReportBuffer = null;
                                outputReportBuffer = new Byte[MyHid.Capabilities.OutputReportByteLength];
                            }

                            //  Flush any waiting reports in the input buffer. (optional)

                            MyHid.FlushQueue(hidHandle);
                        }
                    }
                }
                else
                {
                    //  The device wasn't detected.
                    Debug.WriteLine("Device not found.");
                }
                return myDeviceDetected;
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        ///  <summary>
        ///  Finds and displays the number of Input buffers
        ///  (the number of Input reports the host will store). 
        ///  </summary>

        private void GetInputReportBufferSize()
        {
            Int32 numberOfInputBuffers = 0;
            Boolean success;

            try
            {
                //  Get the number of input buffers.
                success = MyHid.GetNumberOfInputBuffers(hidHandle, ref numberOfInputBuffers);
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        ///  <summary>
        ///  Set the number of Input buffers (the number of Input reports 
        ///  the host will store) from the value in the text box.
        ///  </summary>
        private void SetInputReportBufferSize(int numInputBuffers)
        {
            try
            {
                //  Set the number of buffers.
                MyHid.SetNumberOfInputBuffers(hidHandle, numInputBuffers);

                //  Verify and display the result.
                GetInputReportBufferSize();
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        ///  <summary>
        ///  Initiates exchanging reports. 
        ///  The application sends a report and requests to read a report.
        ///  </summary>
        private void ReadAndWriteToDevice(byte[] dataToWrite)
        {
            // Report header for the debug display:

            //Debug.WriteLine("");
            //Debug.WriteLine("***** HID Test Report *****");
            //Debug.WriteLine(DateTime.Today + ": " + DateTime.Now.TimeOfDay);

            try
            {
                //  If the device hasn't been detected, was removed, or timed out on a previous attempt
                //  to access it, look for the device.
                if ((myDeviceDetected == false))
                {
                    myDeviceDetected = FindTheHid(Vid, Pid);
                }

                if ((myDeviceDetected == true))
                {
                    //  Exchange Input and Output reports
                    ExchangeInputAndOutputReports(dataToWrite);
                }
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        ///  <summary>
        ///  Sends an Output report, then retrieves an Input report.
        ///  Assumes report ID = 0 for both reports.
        ///  </summary>
        private void ExchangeInputAndOutputReports(byte[] dataToSend)
        {
            String byteValue = null;
            Int32 count = 0;
            Byte[] inputReportBuffer = null;
            Byte[] outputReportBuffer = null;
            Boolean success = false;

            try
            {
                success = false;

                //  Don't attempt to exchange reports if valid handles aren't available
                //  (as for a mouse or keyboard under Windows 2000/XP.)

                if (!hidHandle.IsInvalid)
                {
                    //  Don't attempt to send an Output report if the HID has no Output report.

                    if (MyHid.Capabilities.OutputReportByteLength > 0)
                    {
                        //  Set the size of the Output report buffer.   
                        outputReportBuffer = new Byte[MyHid.Capabilities.OutputReportByteLength];

                        //  Store the report ID in the first byte of the buffer:
                        outputReportBuffer[0] = 0;

                        //  Store the report data following the report ID.
                        Array.Copy(dataToSend, 0, outputReportBuffer, 1, outputReportBuffer.Length - 1);


                        if (USE_CONTROL_TRANSFER)
                        {
                            //  Write a report.
                            //  Use a control transfer to send the report,
                            //  even if the HID has an interrupt OUT endpoint.
                            success = MyHid.SendOutputReportViaControlTransfer(hidHandle, outputReportBuffer);
                        }
                        else
                        {
                            // If the HID has an interrupt OUT endpoint, the host uses an 
                            // interrupt transfer to send the report. 
                            // If not, the host uses a control transfer.

                            if (fileStreamDeviceData.CanWrite)
                            {
                                fileStreamDeviceData.Write(outputReportBuffer, 0, outputReportBuffer.Length);
                                success = true;
                            }
                        }

                        if (success)
                        {
                            //Debug.Print("An Output report has been written.");
                            //Debug.Print(" Output Report ID: " + String.Format("{0:X2} ", outputReportBuffer[0]));
                            //Debug.Print(" Output Report Data:");

                            for (count = 0; count <= outputReportBuffer.Length - 1; count++)
                            {
                                //  Display bytes as 2-character hex strings.
                                // byteValue = String.Format("{0:X2} ", outputReportBuffer[count]);
                                // Debug.Print(" " + byteValue);
                            }
                        }
                        else
                        {
                            CloseCommunications();
                            Debug.Print("The attempt to write an Output report failed.");
                        }
                    }
                    else
                    {
                        Debug.Print("The HID doesn't have an Output report.");
                    }

                    //  Read an Input report.
                    success = false;

                    //  Don't attempt to send an Input report if the HID has no Input report.
                    //  (The HID spec requires all HIDs to have an interrupt IN endpoint,
                    //  which suggests that all HIDs must support Input reports.)

                    if (MyHid.Capabilities.InputReportByteLength > 0)
                    {
                        //  Set the size of the Input report buffer. 
                        inputReportBuffer = new Byte[MyHid.Capabilities.InputReportByteLength];

                        if (USE_CONTROL_TRANSFER)
                        {
                            //  Read a report using a control transfer.
                            success = MyHid.GetInputReportViaControlTransfer(hidHandle, ref inputReportBuffer);

                            if (success)
                            {
                                //Debug.Print("An Input report has been read.");

                                //  Display the report data received in the form's list box.

                                //Debug.Print(" Input Report ID: " + String.Format("{0:X2} ", inputReportBuffer[0]));
                                //Debug.Print(" Input Report Data:");

                                for (count = 0; count <= inputReportBuffer.Length - 1; count++)
                                {
                                    //  Display bytes as 2-character Hex strings.

                                    //byteValue = String.Format("{0:X2} ", inputReportBuffer[count]);

                                    //Debug.Print(" " + byteValue);
                                }

                                ProcessInputReport(inputReportBuffer);
                            }
                            else
                            {
                                CloseCommunications();
                                Debug.Print("The attempt to read an Input report has failed.");
                            }
                        }
                        else
                        {
                            //  Read a report using interrupt transfers.                
                            //  To enable reading a report without blocking the main thread, this
                            //  application uses an asynchronous delegate.

                            IAsyncResult ar = null;
                            transferInProgress = true;

                            // Timeout if no report is available.

                            tmrReadTimeout.Start();

                            if (fileStreamDeviceData.CanRead)
                            {
                                fileStreamDeviceData.BeginRead(inputReportBuffer, 0, inputReportBuffer.Length, new AsyncCallback(GetInputReportData), inputReportBuffer);
                            }
                            else
                            {
                                CloseCommunications();
                                Debug.Print("The attempt to read an Input report has failed.");
                            }
                        }

                    }
                    else
                    {
                        Debug.Print("No attempt to read an Input report was made.");
                        Debug.Print("The HID doesn't have an Input report.");
                    }
                }
                else
                {
                    Debug.Print("Invalid handle. The device is probably a system mouse or keyboard.");
                    Debug.Print("No attempt to write an Output report or read an Input report was made.");
                }
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        ///  <summary>
        ///  Retrieves Input report data and status information.
        ///  This routine is called automatically when myInputReport.Read
        ///  returns. Calls several marshaling routines to access the main form.
        ///  </summary>
        ///  
        ///  <param name="ar"> an object containing status information about 
        ///  the asynchronous operation. </param>
        private void GetInputReportData(IAsyncResult ar)
        {
            String byteValue = null;
            Int32 count = 0;
            Byte[] inputReportBuffer = null;
            Boolean success = false;

            try
            {
                inputReportBuffer = (byte[])ar.AsyncState;

                fileStreamDeviceData.EndRead(ar);

                tmrReadTimeout.Stop();

                if ((ar.IsCompleted))
                {
                    /*Debug.Print("An Input report has been read.");
                    Debug.Print(" Input Report ID: " + String.Format("{0:X2} ", inputReportBuffer[0]));
                    Debug.Print(" Input Report Data:");

                    for (count = 0; count <= inputReportBuffer.Length - 1; count++)
                    {
                        //  Display bytes as 2-character Hex strings.

                        byteValue = String.Format("{0:X2} ", inputReportBuffer[count]);
                        Debug.Print(" " + byteValue);
                    }*/

                    ProcessInputReport(inputReportBuffer);
                }
                else
                {
                    Debug.Print("The attempt to read an Input report has failed.");
                }

                transferInProgress = false;
            }
            catch (Exception ex)
            {
                //DisplayException(this.Name, ex);
                throw;
            }
        }

        private void ProcessInputReport(byte[] inputReport)
        {
            if (inputReport.Length == 4)
            {
                byte reportId = inputReport[0];
                byte source = inputReport[1];
                byte dataL = inputReport[2];
                byte dataH = inputReport[3];

                /* Source Byte:
                  
                   ----7-------6------5-------4--------3------2------1-----0---
                   | MCUIN | EEIN | REGIN | HEADPH | GPIOC | MUTE | VDN | VUP |
                   ------------------------------------------------------------ 
                 * 
                 * Now check if the status of one or more byte are changed
                 */

                //     Console.WriteLine("{0:X2} {1:X2}", dataH, dataL);

                #region Hardware buttons
                if ((source & (0x01 << (byte)sourceFlags.VUP)) > 0)
                {
                    Debug.Print("VolumeUP pressed!");
                }
                if ((source & (0x01 << (byte)sourceFlags.VDN)) > 0)
                {
                    Debug.Print("VolumeDOWN pressed!");
                }
                if ((source & (0x01 << (byte)sourceFlags.MUTE)) > 0)
                {
                    Debug.Print("MUTE pressed!");
                }
                #endregion

                #region Headphone Changed
                if ((source & (0x01 << (byte)sourceFlags.HEADPH)) > 0)
                {
                    if (HeadphonePlugged)
                    {
                        HeadphonePlugged = false;
                        //Debug.Print("Headphone plugged");
                    }
                }
                else
                {
                    if (!HeadphonePlugged)
                    {
                        HeadphonePlugged = true;
                        //Debug.Print("Headphone unplugged");
                    }
                }
                #endregion

                #region PTT Changed
                if ((dataL & (0x01 << (byte)Reg1Flags.Gpio1_signal)) > 0)
                {
                    if (!PttPushed)
                    {
                        PttPushed = true;
                        //Debug.Print("PTT pushed");
                    }
                }
                else
                {
                    if (PttPushed)
                    {
                        PttPushed = false;
                        //Debug.Print("PTT released");
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// Close the handle and FileStreams for a device.
        /// </summary>
        /// 
        private void CloseCommunications()
        {
            if (fileStreamDeviceData != null)
            {
                fileStreamDeviceData.Close();
            }

            if ((hidHandle != null) && (!(hidHandle.IsInvalid)))
            {
                hidHandle.Close();
            }

            // The next attempt to communicate will get new handles and FileStreams.

            myDeviceDetected = false;
        }

        /// <summary>
        /// ystem timer timeout if read via interrupt transfer doesn't return.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnReadTimeout(object source, ElapsedEventArgs e)
        {
            Debug.Print("The attempt to read a report timed out.");
            CloseCommunications();

            tmrReadTimeout.Stop();
        }
    }
}
