﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using pjsua2;
using UNET_Classes;
using System.Drawing;
using UNET_Theming;
using System.Runtime.InteropServices;
using System.Media;
using System.IO;
//using HardwareInterface;
using System.Reflection;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETMain : Form //FrmUNETbase
    {
        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;
        public int TraineeID = 1;
        public string SIPServer = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128"); //ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
        public string SIPAccountname = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013"); // ConfigurationManager.AppSettings["sipAccount"].ToString().Trim();
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        UNET_ConferenceBridge.ConferenceBridge_Singleton ucb = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer
        private SoundPlayer simpleSound;

        private bool HeadsetPlugged = false;
        private bool SoundPlaying = false;
        private int RoleClicked = -1;


        //      private UsbInterface hardwareInterface;
        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;

        public static FrmUNETMain GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new FrmUNETMain();
                return inst;
            }
        }
        public FrmUNETMain()
        {
            InitializeComponent();
            log4net.Config.BasicConfigurator.Configure();

        }

          /// <summary>
        /// this sets the information about the exercise in the panel right above
        /// </summary>
        /// <param name="_exersicename"></param>
        /// <param name="_exercisemode"></param>
        /// <param name="_consolerole"></param>
        /// <param name="_platform"></param>
        private void SetExersiceInformation(string _exersicename, string _exercisemode, string _consolerole, string _platform)
        {
            lblConsole.Text = _consolerole;
            lblExerciseName.Text = _exersicename;
            lblExerciseMode.Text = _exercisemode;
            lblPlatform.Text = _platform;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //  this.Close();
            Application.Exit();
        }

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                 this.Text = "UNET Trainee";
                log.Info("Started UNET_Instructor");

                //todo: terugzetten   timer1.Enabled = true;

                ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
                TraineeID = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013"));//Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);

                // Set the text displayed in the caption.
                this.BackColor = Color.White;
                // Set the opacity to 75%.
                this.Opacity = 1;
                // Size the form to be 300 pixels in height and width.
                this.Size = new Size(800, 600);
                // Display the form in the center of the screen.
                this.Top = 0;
                this.Left = 0;
                this.Height = 600;
                this.Width = 800;
                Theming the = new Theming();
                the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
                the.InitPanels(panelRadios);
                the.InitPanels(panelRoles);


                the.SetFormSizeAndPosition(this);
             //   InitHardwareInterface();
               ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
                try
                {
                    //the useragent holds everything needed for the sip communication
                    string account = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013");
                    //sipserver
                    string sipserver = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128");
                    //account
                    string domain = RegistryAccess.GetStringRegistryValue(@"UNET", @"domain", "unet");
                    //account
                    UInt16 port = Convert.ToUInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"port", "5060"));
                    string password = RegistryAccess.GetStringRegistryValue(@"UNET", @"password", "1234");

                    //the useragent holds everything needed for the sip communication
                    useragent = new PJSUA2Implementation.SIP.UserAgent(account, sipserver, port, domain, password);
                    useragent.UserAgentStart();
                    lblRegInfo.Text = "Registered: " + TraineeID + " " +  Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                        ex.InnerException + Environment.NewLine +
                        "Contact your system administrator");
                    log.Error("Error creating accounts " + ex.Message);
                    lblRegInfo.Text = "Failed to reg " + TraineeID;
                    this.Close();
                }

                try
                {
                    //close the connection to the wcf service, if it is still opened
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }
                 
                    service.RegisterTrainee(new CurrentInfo(TraineeID, null, null, null, null,null));
                    lblRegInfo.Text += " WCF regOK";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                        ex.InnerException + Environment.NewLine +
                        "Contact your system administrator");
                    log.Error("Error creating accounts " + ex.Message);
                    lblRegInfo.Text += " WCF regFail";
                    this.Close();
                }

                try
                {
                   //this is specially for the COMservice that listens to the PTT and Headset events, generated
                   //by the TCPSocketClient 
                   Task.Factory.StartNew(() => { StartListinging (); });
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Exception starting PTT and Headset monitoring: " + Environment.NewLine + ex.Message);
                }

              

            }
            catch (Exception ex)
            {
                log.Error("Problem starting trainee!" + ex.Message + ex.InnerException);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {

                SetButtonStatus(this);

                try
                {
                    //close the connection to the wcf service, if it is still opened
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }

                    //enable the Exercise buttons
                    UNET_Classes.CurrentInfo currentInfo = service.GetExerciseInfo(TraineeID);
                    if (currentInfo != null)
                    {
                        lblPlatform.Text = currentInfo.Platform;
                        lblConsole.Text = currentInfo.ConsoleRole;
                        lblExerciseMode.Text = currentInfo.ExerciseMode;
                        lblExerciseName.Text = currentInfo.ExerciseName;
                        lblInstructor.Text = currentInfo.InstructorName;
                    }
                    else
                    {
                        //TODO: HIER IETS ZINVOLS VERZINNEN  Console.Write("The service.getexerciseinfo object is empty!!!");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error updating screen controls", ex);
                    // throw;
                }

                SetButtonStatus(panelRoles);
                SetButtonStatus(panelRadios);
            }

        }



        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatus(Control parent)
        {
            // first the trainees, we assume the name of the button component is the key for the function
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("trainee")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }

                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("exersise")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }

                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("role")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }
                Application.DoEvents();
            }

            try
            {
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //enable the Roles buttons
                List<UNET_Classes.Role> rolelist = service.GetRoles().ToList<UNET_Classes.Role>();// service.GetRoles().Cast<Role>();

                UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;




                //enable the Roles buttons
          //      var rolelist = service.GetRoles();
                List<UNET_Classes.Role> lstrole = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelRoles.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }
                foreach (UNET_Classes.Role role in lstrole)
                {
                    panelRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                    panelRoles.Controls["btnRole" + role.ID.ToString("00")].Text = string.Format("Role {0}{1}{2}", role.ID, Environment.NewLine, role.Name);
                }
                UNET_Classes.Helpers.ResizeButtonsVertical(panelRoles, lstrole.Count, "role");


                //enable the Roles buttons
                var radiolist = service.GetRadios();

                List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelRadios.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }
                foreach (UNET_Classes.Radio radio in lstRadio)
                {
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Enabled = true;
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Text = string.Format("Radio {0}{1}{2}{3}Noise:{4}", radio.ID, Environment.NewLine, radio.Description, Environment.NewLine, radio.NoiseLevel);

                }

                UNET_Classes.Helpers.ResizeButtonsVertical(panelRadios, lstRadio.Count, "radio");
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }


        private void PlayBeep()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, @"Sounds\beep-01a.wav")))
                {
                simpleSound = new SoundPlayer(Path.Combine(Application.StartupPath, @"Sounds\beep-01a.wav"));
                simpleSound.PlayLooping();
                //           simpleSound.PlaySync();

                SoundPlaying = true;
            }
        }


        private void btnAudio_Click(object sender, EventArgs e)
        {
            FrmAudio frm = new FrmAudio();
            frm.Show();
        }

        private void FrmUNETMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close the useragent en with that the sip connection
            if (!object.ReferenceEquals(useragent, null))
            {
                 //stop the sip connection in a nice manner before closing
                useragent.ep.hangupAllCalls();
                useragent.UserAgentStop();
            }
            //close the connection to the wcf service, if it is still opened
            if (service.State == System.ServiceModel.CommunicationState.Opened)
            {
                service.Close();
            }


            //try to find and kill the TCPSocketClient process and kill it
            FindAndKillProcess("TCPSocketClient.exe");
            log.Info("Terminated UNET_Instructor");

        }

        /// <summary>
        /// Try to find the process in the processlist and if so, try to kill it.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool FindAndKillProcess(string _name)
        {
            try
            {
                string searchName = Path.GetFileNameWithoutExtension(_name);
                Process[] processes = Process.GetProcessesByName(searchName);

                foreach (Process process in processes)
                {
                    process.Kill();
                    log.Info("Successfully killed TCPSocketClient.exe");

                }
            }

            catch (Exception ex)
            {
              log.Error("Kill process " + _name + ex.Message + ex.StackTrace + ex.InnerException);
            }

            //process not found, return false
            return false;
        }

        private void btnRadio01_Click(object sender, EventArgs e)
        {
            int radioNumber = -1;
            try
            {
                //1 zoek uit welke radio geklikt heeft
                string state = string.Empty;
                radioNumber = Convert.ToInt16(UNET_Classes.Helpers.ExtractNumber(((Button)sender).Name));
                if (((Button)sender).Text.Trim().Length > 8)
                {
                    state = ((Button)sender).Text.Trim().Substring(((Button)sender).Text.Trim().Length - 2);
                }
                else
                {
                    state = "Rx"; //if the state is now 'TX', the next one will be 'OFF' and that is what we initially want
                }
                //2 zoek die op in de radios lijst in de conferencebridge
                switch (state)
                {
                    //3 zet de status   
                    case "Rx":
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsTx;
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Tx");
                            MakeCall(TraineeID, "1015");
                            break;
                        }
                    case "Off":
                    default:
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsRx;//we zetten hem 1 status HOGER dan de huidige status, en zitten dit in de singleton en op de hmi
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Rx");
                            break;
                        }
                    case "Tx":
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsOff;
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "OFF");
                            break;
                        }
                }
                log.Info("Have set the status of the Radio: " + radioNumber + " to: " + state);
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                //set the new status
                service.SetRadioStatus(Convert.ToInt16(radioNumber), ucb.Radios[radioNumber - 1].State);



                MakeCall(Convert.ToInt16(radioNumber), "1015");
            }
            catch (Exception ex)
            {
                log.Error("Error setting the status:" + radioNumber, ex);
                // throw;
            }
        }

        #region CALL
        /// <summary>
        /// Make a call to Freeswich using PJSUA2
        /// </summary>
        /// <param name="traineeid"></param>
        /// <param name="_destination"></param>
        private void MakeCall(int traineeid, string _destination)
        {
            try
            {
                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, TraineeID);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                sc.makeCall(string.Format("sip:{0}@{1}", _destination, SIPServer), cop);
            }
            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
                // throw;
            }
        }

        #endregion

        private void btnClassBroadcast_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ClassBroadcast");
            MakeCall(TraineeID, "");


        }

        private void btnMonitorTrainee_MouseUp(object sender, MouseEventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, @"Sounds\beep-01a.wav")))
            {

                simpleSound.Stop();
            }
        }

        private void btnMonitorTrainee_MouseDown(object sender, MouseEventArgs e)
        {
           

        }

        private void FrmUNETMain_Shown(object sender, EventArgs e)
        {

        }

        private void btnIntercom_Click(object sender, EventArgs e)
        {
            log.Info("Clicked Intercom");
            PlayBeep();
            MakeCall(TraineeID,@"INTERCOM_CUB_X\" + TraineeID);
        }

        private void btnAssist_Click(object sender, EventArgs e)
        {
            MakeCall(TraineeID, @"MIC_Conference_Pos01\" + TraineeID);
            log.Info("Clicked assist by:" + TraineeID);

        }
        #region HID COMserver

        private StreamWriter serverStreamWriter;
        private StreamReader serverStreamReader;
        /// <summary>
        /// Start Server
        /// </summary>
        /// <returns></returns>
        private bool StartServer()
        {
            //1: create server's tcp listener for incoming connection
            TcpListener tcpServerListener = new TcpListener(4444);
            tcpServerListener.Start();      //start server
            Console.WriteLine("Server Started");
            //block tcplistener to accept incoming connection
            Socket serverSocket = tcpServerListener.AcceptSocket();


            //2: start the server that listens to the events
            try
            {
                if (serverSocket.Connected)
                {
                    Console.WriteLine("tcpClient for PTT and headset event capture connected");
                    //open network stream on accepted socket
                    NetworkStream serverSockStream = new NetworkStream(serverSocket);
                    serverStreamWriter = new StreamWriter(serverSockStream);
                    serverStreamReader = new StreamReader(serverSockStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            //3: start the win32 client that captures the PTT and headset events
            try
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "TCPSocketClient.exe")))
                {
                    var pr = new Process();
                    pr.StartInfo.FileName = Path.Combine(Application.StartupPath, "TCPSocketClient.exe");
                    pr.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot find tcpclient " +ex.Message + ex.StackTrace);

            }
            return true;
        }
        //////////////////////////////////////////////////////////////////////////////
        ///Event handlers
        //////////////////////////////////////////////////////////////////////////////
        private void StartListinging()
        {
            //start server
            if (!StartServer())
                Console.WriteLine("Unable to start server");

            //sending n receiving msgs
            while (true)
            {
                // Application.DoEvents();
                string received = serverStreamReader.ReadLine();
                if (received.Length > 0)
                {
                    string[] splitstr = received.Split('|');
                    if (splitstr[0].ToString().Trim().ToLower() == "ptt")
                    {
                        if (splitstr[1].ToString().Trim().ToLower() == "true")
                        {
                            lblPtt.Text = "PTT ";

                        }
                        else
                            lblPtt.Text = "";
                    }
                    else
                    {
                        if (splitstr[1].ToString().Trim().ToLower() == "true")
                        {
                            lblHeadset.Text = "HEADSET";

                        }
                        else
                            lblHeadset.Text = "";
                    }
                }
              //  Application.DoEvents();
            //    Console.WriteLine("CLIENT: " + serverStreamReader.ReadLine());
            //    serverStreamWriter.WriteLine("Hi!");
            //    serverStreamWriter.Flush();
            }//while
        }
        #endregion

        private void btnRole1_Click(object sender, EventArgs e)
        {
            RoleClicked = (int)(Enum.Parse(typeof(Enums.Roles), ((Button)sender).Name.Remove(0, 3)));
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;


        }
    }
}