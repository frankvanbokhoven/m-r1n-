using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using ESSaverBase;

namespace XPUEESSaver
{
    [Description("This class catches all USB HD devices that are added and removed from the system")]
    internal class WMIEventPUE
    {
        #region ChassisTypes enum

        public enum ChassisTypes
        {
            Other = 1,
            Unknown,
            Desktop,
            LowProfileDesktop,
            PizzaBox,
            MiniTower,
            Tower,
            Portable,
            Laptop,
            Notebook,
            Handheld,
            DockingStation,
            AllInOne,
            SubNotebook,
            SpaceSaving,
            LunchBox,
            MainSystemChassis,
            ExpansionChassis,
            SubChassis,
            BusExpansionChassis,
            PeripheralChassis,
            StorageChassis,
            RackMountChassis,
            SealedCasePC
        }

        #endregion

        #region PCSystemTypes enum

        public enum PCSystemTypes
        {
            Unspecified = 0,
            Desktop,
            Mobile,
            Workstation,
            EnterpriseServer,
            SmallOfficeAndHomeOfficeServer,
            AppliancePC,
            PerformanceServer,
            Maximum
        }

        #endregion

        private const int WD250GB = 17;
        private readonly List<string> listDetectedDevices = new List<string>();
        private readonly Usages usages;

        private readonly ManagementEventWatcher w;
        public double CurrentWattUsage;
        protected DateTime LastUpdate;
        public bool ShowUSBMessages;
        public double TotalkWhUsage;


        /// <summary>
        /// Constructor
        /// </summary>
        public WMIEventPUE()
        {
            LastUpdate = DateTime.Now;
            listDetectedDevices.Clear();
            WqlEventQuery q;
            var observer = new ManagementOperationObserver();
            // Bind to local machine
            var scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true; //set required privilege
            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceOperationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = @"TargetInstance ISA 'Win32_DiskDrive' ";
                w = new ManagementEventWatcher(scope, q);

                w.EventArrived += DiskEventArrived;
                w.Start();
                //   Console.ReadLine(); // block main thread for test purposes
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMIEvent->Main");
            }
        }


        /// <summary>
        /// Deze methode is nodig, omdat het COM object expliciet blijkt te moeten worden gestopt. Dat wordt gedaan in ESSaverMonitor.FrmMain
        /// //deze is public, zodat we hem vanuit frmmain kunnen destroyen (stoppen)
        /// </summary>
        public void StopWMI()
        {
            try
            {
                w.Stop();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI StopWMI");
            }
        }

        /// <summary>
        /// Use this class to keep track of the usb devices that get connected and disconnected to this system
        /// 
        /// Als volgt moet het werken:
        /// 1) Bij start, is usb lijst leeg
        /// 2) Zodra een usb apparaat wordt toegevoegd, wordt deze toegevoegd aan de lijst. Twee mogelijkheden
        /// 2a) Het is een bekend apparaat. Haal uit tblUSB de verbruikstotale en wattage van dit apparaat en gebruik die
        /// 2b) Het is een onbekend apparaat. Vuur de webservice 'FindDevice' af met de gegevens van het apparaat. In deze webmethod,
        /// wordt een poging gedaan het apparaat te vinden alsmede het wattage. Indien succes, wordt dat terug gegeven. Dit wordt vervolgens opgeslagen in de local database.
        /// Als op de server-db het apparaat níet wordt gevonden, wordt dit toegeveogd.
        /// </summary>
        protected void DiskEventArrived(object sender, EventArrivedEventArgs e)
        {
            string tempstring = string.Empty;
            if (ShowUSBMessages) //only if the user wánts to see these messages
            {
                string model = string.Empty;
                string usbName = string.Empty;
                string usbDeviceInfo = string.Empty;
                //Get the Event object and display its properties (all)
                foreach (PropertyData pd in e.NewEvent.Properties)
                {
                    ManagementBaseObject mbo = null;
                    if ((mbo = pd.Value as ManagementBaseObject) != null)
                    {
                        foreach (PropertyData prop in mbo.Properties)
                        {
                            usbDeviceInfo = string.Concat(prop.Name, " : ", prop.Value);
                            tempstring = string.Concat(tempstring, " | ", prop.Name, "-", prop.Value);

                            if (prop.Name == "Caption")
                            {
                                usbName = prop.Value.ToString();
                            }

                            if (prop.Name == "Model")
                            {
                                model = prop.Value.ToString().Trim();
                            }
                        }
                    }
                }
                // zoek nu eerst uit of het apparaat al bestaat
                var um = new USBMonitor();
                double usbWatt = um.DeviceIsKnown(model);
                if (usbWatt > 0)
                {
                  //  um.AddUSB(model, -1, 0, model, 1, "DISK");
                    // als het record al bestaat, wordt dit gewoon ge-updated  // WATT is dan niet nodig, dus wordt als -1 doorgegeven
                    CurrentWattUsage += usbWatt;
                }
                    // ReSharper disable RedundantIfElseBlock
                else // als het device nog níet bestaat
                {
                    /*  FrmAddUsbDevice frm = new FrmAddUsbDevice(); // alleen als de gebruiker de meldingen wíl zien.
                         frm.USBDeviceInfo = usbDeviceInfo;
                         frm.USBName = usbName;
                         frm.ShowDialog();

                         if (frm.DialogResul  t == DialogResult.OK)
                         {
                             um.AddUSB(model, frm.USBWatt, 0, model, 1, "DISK");
                                 // als het record al bestaat, wordt dit gewoon ge-updated  // WATT is dan niet nodig, dus wordt als -1 doorgegeven
                         }*/
                }
                // ReSharper restore RedundantIfElseBlock
            }
        }

        /// <summary>n
        /// Update the usage figures for the usb devices
        /// </summary>
        public void UpdateUSB()
        {
            try
            {
                // update the usage figures
                TimeSpan duration = DateTime.Now - LastUpdate;
                usages.CalculateUsages(duration.TotalSeconds, WD250GB);
                TotalkWhUsage += usages.KiloWattUsage;
                LastUpdate = DateTime.Now;
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "Update usb");
            }
        }

        /// <summary>
        /// deze functie vraagt via WMI de naam van de cpu uit
        /// code hiervoor is gecreerd met microsoft's WMI Code creator
        /// </summary>
        /// <returns></returns>
        public string GetCPUName()
        {
            string cpuname = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    cpuname += queryObj["Name"];
                }
                searcher.Dispose();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetCPUName");
            }
            return cpuname;
        }


        /// <summary>
        /// With ths routine, we check wat kind of computer this is. This method is more reliable that 'GetChassisType'
        /// see: //http://msdn.microsoft.com/en-us/library/aa394102%28VS.85%29.aspx
        /// </summary>
        /// <returns></returns>
        public string GetComputerType()
        {
            string strComputer = string.Empty;
            string result = "Desktop";
            ESSaverSingleton ess = ESSaverSingleton.Instance;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    strComputer = queryObj["PCSystemType"].ToString();
                }

                //the result is a number, with which we can select in the enum 'pcsystemtypes' what kind of pc it is
                //This also enables us to set some of the standard power usages
                if (strComputer.Length > 0)
                {
                    int typenr = Convert.ToInt16(strComputer);
                    //  ess.ComputerType = typenr;

                    switch (typenr) //TODO: this switch can probably be made more effective
                    {
                        case 0:
                            {
                                result = "Unspecified";
                                ess.PowerPC = 40;
                                break;
                            }
                        case 1:
                            {
                                result = "Desktop";
                                ess.PowerPC = 60;
                                break;
                            }
                        case 2:
                            {
                                result = "Laptop / Mobile"; // ess.LaptopMobile; //"Laptop / Mobile";
                                ess.PowerPC = 40;
                                break;
                            }
                        case 3:
                            {
                                result = "Workstation";
                                ess.PowerPC = 25;

                                break;
                            }
                        case 4:
                            {
                                result = "Enterprise Server";
                                ess.PowerPC = 130;

                                break;
                            }
                        case 5:
                            {
                                result = "Small Office and Home Office Server";
                                ess.PowerPC = 120;

                                break;
                            }
                        case 6:
                            {
                                result = "Applicance PC";
                                ess.PowerPC = 60;

                                break;
                            }
                        case 7:
                            {
                                result = "Performance Server";
                                ess.PowerPC = 1500;

                                break;
                            }
                        case 8:
                            {
                                result = "Maximum";
                                ess.PowerPC = 150;

                                break;
                            }
                        default:
                            {
                                result = "Desktop";
                                ess.PowerPC = 60;

                                break;
                            }
                    }
                    ess.IniWriteValue("CHARACTERIZATION", "POWERPC", ess.PowerPC.ToString());
                    ess.IniWriteValue("CHARACTERIZATION", "COMPUTERTYPEDESCRIPTION", result);
                    ess.IniWriteValue("CHARACTERIZATION", "COMPUTERTYPE", typenr.ToString());

                    ess.PowerPC = ess.PowerPC;
                    ess.ComputerTypeDescription = result;
                }
                searcher.Dispose();
            }

            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetMachineGUID");
            }

            return result;
        }


        //http://msdn.microsoft.com/en-us/library/aa394102%28VS.85%29.aspx

        /// <summary>
        /// deze functie vraagt via WMI de naam van de cpu uit
        /// code hiervoor is gecreerd met microsoft's WMI Code creator
        /// </summary>
        /// <returns></returns>
        public string GetMemoryName()
        {
            string cpuname = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_MemoryDevice");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    cpuname += queryObj["Description"];
                }
                searcher.Dispose();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetMemoryName");
            }
            return cpuname;
        }

        /// <summary>
        /// deze functie vraagt via WMI het type memory uit
        /// Gebaseerd op: http://www.eggheadcafe.com/community/aspnet/2/10159728/get-ram-typeddr3ddr2ddr-sdr-memory-usage-and-cpu-usage.aspx
        /// </summary>
        /// <returns></returns>
        public string GetMemoryDeviceName()
        {
            string result = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_PhysicalMemory");

                int memoryType = 0;
                foreach (ManagementObject obj in searcher.Get())
                {
                    memoryType = Convert.ToInt16(obj.GetPropertyValue("MemoryType"));
                }

                //Use this memory type to search in the list below
                switch (memoryType)
                {
                    case 0:
                        {
                            result = "Unknown";
                            break;
                        }
                    case 1:
                        {
                            result = "Other";
                            break;
                        }

                    case 2:
                        {
                            result = "DRAM";
                            break;
                        }

                    case 3:
                        {
                            result = "Synchronous DRAM";
                            break;
                        }

                    case 4:
                        {
                            result = "Cache DRAM";
                            break;
                        }

                    case 5:
                        {
                            result = "EDO";
                            break;
                        }

                    case 6:
                        {
                            result = "EDRAM";
                            break;
                        }

                    case 7:
                        {
                            result = "VRAM";
                            break;
                        }

                    case 8:
                        {
                            result = "SRAM";
                            break;
                        }

                    case 9:
                        {
                            result = "RAM";
                            break;
                        }

                    case 10:
                        {
                            result = "ROM";
                            break;
                        }

                    case 11:
                        {
                            result = "Flash";
                            break;
                        }

                    case 12:
                        {
                            result = "EEPROM";
                            break;
                        }

                    case 13:
                        {
                            result = "FEPROM";
                            break;
                        }

                    case 14:
                        {
                            result = "EPROM";
                            break;
                        }

                    case 15:
                        {
                            result = "CDRAM";
                            break;
                        }

                    case 16:
                        {
                            result = "3DRAM";
                            break;
                        }

                    case 17:
                        {
                            result = "SDRAM";
                            break;
                        }

                    case 18:
                        {
                            result = "SGRAM";
                            break;
                        }

                    case 19:
                        {
                            result = "RDRAM";
                            break;
                        }

                    case 20:
                        {
                            result = "DDR";
                            break;
                        }

                    case 21:
                        {
                            result = "DDR-2";
                            break;
                        }
                    default:
                        {
                            result = "Unknown";
                            break;
                        }
                }
                searcher.Dispose();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetMemoryType");
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, "GetMemoryDeviceName");
            }

            return result;
        }

        /// <summary>
        /// Get the videocontroller from the system
        /// </summary>
        /// <returns></returns>
        public string GetVideoController()
        {
            string result = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_VideoController");

                foreach (ManagementObject obj in searcher.Get())
                {
                    result = obj.GetPropertyValue("Description").ToString();
                }
                searcher.Dispose();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetVideoController");
            }
            catch (Exception ex) //General exception
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, "GetVideoController");
            }

            return result;
        }

        /// <summary>
        /// This function returns the machine's unique machine GUID using WMI
        /// </summary>
        /// <returns></returns>
        public string GetMachineGUID()
        {
            string strComputer = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_ComputerSystemProduct");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    strComputer = queryObj["UUID"].ToString();
                }
                searcher.Dispose();
            }

            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetMachineGUID");
            }

            return strComputer;
        }

        /// <summary>
        /// This function returns the machine's unique machine GUID using WMI
        /// </summary>
        /// <returns></returns>
        public string GetDisplayMonitor()
        {
            string strComputer = string.Empty;
            try
            {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                                                 "SELECT * FROM Win32_DesktopMonitor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    strComputer = queryObj["UUID"].ToString();
                }
                searcher.Dispose();
            }
            catch (ManagementException ex)
            {
                var err = new ErrorLogger.ErrorLogger();
                err.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException + "WMI GetDisplayMonitor");
            }

            return strComputer;
        }

    }
}