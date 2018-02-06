using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net.Mime;
using System.Reflection;
using System.Resources;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using ESSaverBase;
using ESSaverProcess;
using Timer = System.Timers.Timer;
using WMIProcessCounters = ESSaverProcess.WMIProcessCounters;
using PushData;

namespace XPUEESSaver
{
    public partial class ESSaverPUE : ServiceBase
    {
        private static ResourceManager rm;
        private EventLog eventLog1; //hier stond readonly bij
        public ESSaverSingleton Essaversingleton;
        private ArrayList ProcessList = new ArrayList();
        private DateTime StartDateTime;
        private UsagesStruct TodaysUsages;
        private bool UseWebServices;
        public EnergySchemeMonitor energyscheme;
        private string machineGUID = string.Empty;
        private bool isVirtual = false;
        public OccuranceLog occurrancelog;
        private Timer timerProcessUsagesPUE;
        public Usages usages;
        private WMIEventPUE wmiEvent;
        public WMIProcessCounters wpc;
        private Timer timerstall;

        public ESSaverPUE()
        {
            InitializeComponent();
        }

        public double CurrentWattUsage { get; set; }

        public static ResourceManager RM
        {
            get { return rm; }
        }

        /// <summary>
        /// this function uploads the figures for today to the webserver
        /// </summary>
        private void SaveDailyUsage()
        {
            try
            {
                var webservicecaller = new WebServiceCallerPUE();
                if (webservicecaller.IsAbleToCommunicate())
                {
                    TimeSpan Duration = DateTime.Now - StartDateTime;
                    var erf = new ESSaverReportFields();
                    erf.UserName = Essaversingleton.UserName;
                    erf.Organisation = Essaversingleton.Organisation;
                    erf.ESSaverSerialNumber = Essaversingleton.ESSaverSerialNumber;
                    erf.MachineID = machineGUID;
                    erf.Date = DateTime.Now.Date;
                    erf.MonitorTime = Convert.ToInt32(Duration.TotalSeconds);
                    //todo: in principe is deze conversie gevaarlijk
                    erf.SavingsTime = 0; // useridletime.SavingsTime;
                    erf.IdleTime = 0; // useridletime.TotaalIdle;
                    erf.KWH = usages.KiloWattUsage + energyscheme.TotalkWhUsage;
                    erf.Carbon = usages.CarbonUsage;
                    erf.Euro = usages.EuroSaved;
                    erf.KWHSaved = usages.KiloWattSaved;
                    erf.CarbonSaved = usages.CarbonHardwareSaved + usages.CarbonProcessesSaved;
                    //todo: moet er daar niet nóg een bij???
                    erf.EuroSaved = usages.EuroSaved;
                    erf.SaveSetting = Essaversingleton.SaveValuesAmount;
                    erf.KWHHardware = usages.TotalUsageHardware;
                    erf.KWHProcess = usages.TotalUsageProcess;
                    erf.KWHUserIdle = usages.TotalUsageUsers;
                    erf.KWHHardwareSaved = usages.TotalSavehardwarekwh;
                    erf.KWHProcessSaved = usages.TotalSaveprocessorkwh;
                    erf.KWHUserIdleSaved = usages.TotalSaveusertijd;

                    PUEProcessUsages puepu = PUEProcessUsages.Instance;
                    puepu.SaveDailyUsages(erf, machineGUID);
                    //   webservicecaller.UploadStatistics(erf, machineGUID);

                    ResetCounters();

                    usages.SetUsagesSend(DateTime.Now.Date);
                }
            }
            catch (Exception ex) //General exception
            {
                eventLog1.WriteEntry("ESSaver errored (save daily usage): " + ex.Message);
            }
        }

        private void ResetCounters()
        {
            try
            {
                //'reset alle counters
                StartDateTime = DateTime.Now;
                usages.KiloWattUsage = 0;
                energyscheme.TotalkWhUsage = 0;
                usages.CarbonUsage = 0;
                usages.EuroSaved = 0;
                usages.KiloWattSaved = 0;
                usages.CarbonHardwareSaved = 0;
                usages.CarbonProcessesSaved = 0;
                usages.EuroSaved = 0;
                usages.TotalUsageHardware = 0;
                usages.TotalUsageProcess = 0;
                usages.TotalUsageUsers = 0;
                usages.TotalSavehardwarekwh = 0;
                usages.TotalSaveprocessorkwh = 0;
                usages.TotalSaveusertijd = 0;
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ESSaver errored (reset counters): " + ex.Message);
            }
        }

        private void timerstall_Elapsed(object sender, ElapsedEventArgs e)
        {
            Load();
        }

        private void timerprocessUsagesPUE_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // this starts all the work
                wpc.GetUsages(out ProcessList);
                UpdateProcesses();
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ESSaver GetUsages error: " + ex.Message);

            }
        }

        /// <summary>
        /// this function updates all processes with the contents of the wmi process list
        /// </summary>
        private void UpdateProcesses()
        {
            try
            {
                PUEProcessUsages puepu = PUEProcessUsages.Instance;
                puepu.IsVirtual = isVirtual;
                foreach (WMIProcessData process in ProcessList)
                {
                    if ((process.ProcessName.Trim().ToLower() != "idle.exe") &&
                        (process.ProcessName.Trim().ToLower() != "_total.exe"))
                    {

                        if (process.PercentProcessorTime > 0) // we doen dit om te voorkomen dat alles gelogd wordt.
                            puepu.AddProcessUsagesToEventLog(process);

                    }
                }
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ESSaver Updateprocess error: " + ex.Message);

            }
        }

        private void Load() 
        {
            //     InitializeComponent();
            try
            {
                eventLog1 = new EventLog();
                ((ISupportInitialize) (eventLog1)).BeginInit();


                // TODO: Add any initialization after the InitComponent call
                if (!EventLog.SourceExists("XPUEESSaver"))
                {
                    EventLog.CreateEventSource("XPUEESSaver", "ESSaver");
                }


                eventLog1.Source = "XPUEESSaver";
                eventLog1.Log = "ESSaver";

                //   ServiceName = "ESSaverPUEservice";
                try
                {
                ((ISupportInitialize) (eventLog1)).EndInit();
                isVirtual = DetectVirtualMachine();
                }
                catch (Exception)
                {
              
                }

                    // dit bepaalt of deze pue op een virtuele machine draait. We hoeven dat maar 1 keer te doen.

                //    eventLog1.WriteEntry("ESSaver start " + Essaversingleton.DatabaseLocation);

                //   Visible = false; // Hide form window.  

                // start de singleton zodat deze het ini object laadt

                //   LoadSQLLiteAssembly();

                bool allowToContinue = true;
                //      eventLog1.WriteEntry("ESSaver timerProcessUsagesPUE started ");
                //Check if the regcode is filled in
                /*  if ((Essaversingleton.ESSaverSerialNumber == "") | (Essaversingleton.ESSaverSerialNumber == "0") | (Essaversingleton.UserName.Trim() == "") | (Essaversingleton.Organisation.Trim() == ""))
                {
                    WebServiceCallerPUE wsc = new WebServiceCallerPUE();
                    if (!wsc.IsAbleToCommunicate())
                    {
                     //   allowToContinue = false;
                        //     MessageBox.Show(
                        //         "No connection to the internet. ESSaver requires an active connection during initialization. Try again when there is an active internet connection. ESSaver will now close.",
                        //          "Unable to initialize ESSaver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Stop();
                    }
                }*/

                //   allowToContinue = false;
                if (allowToContinue)
                {
                    try
                    {
                        eventLog1.WriteEntry("ESSaver read registry");

                        UseWebServices = (RegistryAccess.GetStringRegistryValue(@"CONFIGURATION", @"UseWebServices", "0") == "1");
                        StartDateTime = DateTime.Now;
                        // onderstaand object houdt de userinteractie in de gaten.
                        //                   useridletime = UserIdleTime.Instance;
                        //      sch = new SessionChangeHandler(this);


                        //test voor resource laden
                        //     CPUCharacterization cc = new CPUCharacterization();
                        //    cc.SetPowerIndex(Essaversingleton.CPUType);           
                        //        eventLog1.WriteEntry("ESSaver globalize ");

                        //              GlobalizeApp();

                        //check for autostart
                      //  var autoStart = new Autostart();
                     //   autoStart.SetAutoStart();
                        //     eventLog1.WriteEntry("ESSaver check write permissions ");

                        //Check write rights
                        //   if (!Essaversingleton.HaveWritePermissions(Essaversingleton.DatabaseLocation))
                        //   {
                        //       eventLog1.WriteEntry("ESSaver errored: ESSaver does not have the nessecary rights to write data to your system. This is a requirement for running ESSaver.");
                        //   }
                        //    eventLog1.WriteEntry("ESSaver create objects ");
                     

      
                        // sleur de objecten de lucht in
                        usages = Usages.Instance;
                        //    eventLog1.WriteEntry("ESSaver create objects: Usages ");
                        //      usages.CalculateTotals(); todo 16/7/2012 kijken of dit nodig is
                        //   eventLog1.WriteEntry("ESSaver create objects: Usages.calculatetotals ");

                        //    TodaysUsages = usages.GetTodaysUsage(); //dit haalt eventuele verbruiken van eerder deze dag op. todo 16/7/2012 kijken of dit nodig is
                        wmiEvent = new WMIEventPUE();
                        machineGUID = wmiEvent.GetMachineGUID(); // this one we get for later use;
                        //    isVirtual = wmiEvent.DetectVirtualMachine();
                        //    eventLog1.WriteEntry("ESSaver create objects: wmi ");


                        wmiEvent.ShowUSBMessages = false; // Essaversingleton.ShowUSBMessages;

                        //  SetMinMaxSaveSetting();

                        //check if the cpu is known and if different, change it using wmi
                        if (Essaversingleton.CPUType != wmiEvent.GetCPUName())
                        {
                            //set the cpu name
                            Essaversingleton.CPUType = wmiEvent.GetCPUName();
                            Essaversingleton.MemoryType = wmiEvent.GetMemoryDeviceName();
                            Essaversingleton.VideoType = wmiEvent.GetVideoController();
                            CPUCharacterization cc = new CPUCharacterization();
                            Essaversingleton.CPUBenchMark = cc.SetPowerIndex(Essaversingleton.CPUType);


                            Essaversingleton.ComputerTypeDescription = wmiEvent.GetComputerType();
                            //deze methode zet ook het computertype en powerpc
                            //save the ini setting
                            Essaversingleton.SetIniValues();

                            string cpumsg = ("ESSaver has determined that the CPU type of this system = " +
                                             Essaversingleton.CPUType +
                                             ". The appropriate powerusagesettings have been selected for your system.");
                            eventLog1.WriteEntry(cpumsg);
                            string memorymsg = ("ESSaver has determined that the memory type of this system = " +
                                                Essaversingleton.MemoryType +
                                                ". The appropriate powerusage settings for the memory have been selected for your system.");
                            eventLog1.WriteEntry(memorymsg);
                        }
                        UseWebServices = false;
                        if (UseWebServices)
                        {
                            var wsc = new WebServiceCallerPUE();
                            //check if the cpu characteristics are in the database. If not, try to download them from the webserver.
                            // we check this each and every time essaver starts.
                            var characteristics = new Characteristics();
                            // Then insert this into the characteristics
                            if (!characteristics.CheckCharacteristics(Essaversingleton.CPUType))
                            {
                                //Now fire a webservice that tries to download the characteristics from the server
                                string characteristicsDataString =
                                    wsc.GetCharacteristics(Essaversingleton.ESSaverSerialNumber,
                                                           machineGUID, "CPU",
                                                           Essaversingleton.CPUType);
                                characteristics.DeleteOldCharacteristics(Essaversingleton.CPUType);
                                characteristics.GetCharacteristics(Essaversingleton.CPUType, characteristicsDataString);
                            }
                        }
                        //now set the cpu power usage
                        XmlCharacterisations xml = XmlCharacterisations.Instance;
                        //      Essaversingleton.PowerPC = xml.CPUWatt; //todo get this also from the webservice
                        Essaversingleton.PowerMemory = xml.MemoryWatt; //todo: get this also from the webservice

                        wpc = WMIProcessCounters.Instance;
                        wpc.TimerInterval = Convert.ToInt16(Essaversingleton.RefreshProcesses/1000);
                        //nodig voor de kwh berekeningen

                        energyscheme = EnergySchemeMonitor.Instance;
                        //       processmonitor = ProcessMonitor.Instance;
                        //      userprocessmonitor = new UserProcessMonitor(Essaversingleton.Threshold);
                        occurrancelog = OccuranceLog.Instance;
                        if (Essaversingleton.ComputerTypeDescription == "Laptop / Mobile")
                            //essaverSingleton.LaptopMobile) // alléén als het een laptop (== 1) is mag deze methode uitgevoerd worden
                        {
                            int absvalue = Math.Abs(Convert.ToInt16(Essaversingleton.BrightnessBattery) - 100);
                            byte bt = Convert.ToByte(absvalue);
                        }



                        //usb
                        timerProcessUsagesPUE = new Timer();
                        ((ISupportInitialize) (timerProcessUsagesPUE)).BeginInit();
                        timerProcessUsagesPUE.Interval = Essaversingleton.RefreshProcesses; //een minuut
                        timerProcessUsagesPUE.Elapsed += timerprocessUsagesPUE_Elapsed;
                        ((ISupportInitialize) (timerProcessUsagesPUE)).EndInit();
                         timerProcessUsagesPUE.Start(); //  .Enabled = true;

                        Essaversingleton.SetIniValues();

                        // try to add hardware info, but ONLY if this is the first time running
                        string done = RegistryReader.GetStringRegistryValue("CONFIGURATION", "Hardware", string.Empty);
                        if (done.Length == 0)
                        {
                            var hi = new HardwareInfo();
                            //  hi.SaveHardwareInfo();
                            hi.SaveHardwareInfoToEventlog();

                            RegistryReader.SetStringRegistryValue("CONFIGURATION", "Hardware",
                                                                  string.Format("Done: {0}", DateTime.Today.ToString()));
                        }
                    }

                    catch (Exception ex) //General exception
                    {
                        eventLog1.WriteEntry("ESSaver errored: " + ex.Message);

                    }
                }
            }
            catch (Exception ex) //General exception
            {
                eventLog1.WriteEntry("ESSaver errored: " + ex.Message);
            }
        }

        /// <summary>
        /// Detect if this OS runs in a virtual machine
        /// http://blogs.msdn.com/b/virtual_pc_guy/archive/2005/10/27/484479.aspx
        /// Microsoft themselves say you can see that by looking at the motherboard via wmi
        /// </summary>
        /// <returns>false</returns> if it runs on a fysical machine
        public bool DetectVirtualMachine()
        {
            bool result = false;
            const string MICROSOFTCORPORATION = "microsoft corporation";
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    result = queryObj["Manufacturer"].ToString().ToLower() == MICROSOFTCORPORATION.ToLower();
                }
                return result;
            }
            catch (ManagementException ex)
            {
                eventLog1.WriteEntry("ESSaver errored (detect virtual machine): " + ex.Message);

                return result;
            }
        }


        protected override void OnStart(string[] args)
        {
            this.RequestAdditionalTime(15000); 
            // 27072012: deze requestadditionaltime is erbij geplaatst om te voorkomen dat er een timeout optreedt in de onstart op sommige systeen
            Essaversingleton = ESSaverSingleton.Instance;
            //  bgw = new BackgroundWorker();
            //  bgw.DoWork += new DoWorkEventHandler(Load);
            //  bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler
            //                  (bw_RunWorkerCompleted);
            // Kick off the Async thread
            //    bgw.RunWorkerAsync();
            //stall 
           // timerstall = new Timer();
           // ((ISupportInitialize) (timerstall)).BeginInit();
           // timerstall.Interval = 1000;
          //  timerProcessUsagesPUE.Elapsed += timerstall_Elapsed;
         //   ((ISupportInitialize) (timerstall)).EndInit();
            Load();
        }

        protected override void OnStop()
        {
            Essaversingleton.SetIniValues();
            eventLog1.WriteEntry("ESSaver stopped");
            try
            {
                timerProcessUsagesPUE.Enabled = false;
                SaveDailyUsage();
                try
                {
                    if (wmiEvent != null)
                    {
                        wmiEvent.StopWMI(); //dit com object moet worden gestopt, anders volgt er een fout
                    }
                }
                catch (Exception ex)
                {
                    eventLog1.WriteEntry("ESSaver errored (FrmMain.TimerScheduler_Elapsed): " + ex.Message);
                }

            }
            catch (Exception ex) //General exception
            {
                eventLog1.WriteEntry("ESSaver errored (OnExit): " + ex.Message);

            }
        }


    }
}