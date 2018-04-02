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
using System.Reflection;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Diagnostics;
using PJSUA2Implementation.SIP;
using UNET_Sounds;
using System.Threading;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETMain : Form// FrmUNETbase
    {
        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;
        public string TraineeID = string.Empty;
        public string DisplayName;
        public string SIPServer = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128");
        public string SIPAccountname = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013");
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        private UNET_ConferenceBridge.ConferenceBridge_Singleton ucb = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer
        private SoundPlayer simpleSound;
        private String[] RadioStateArray = new String[20]; //used for keeping track of the radio status
        private bool AssistRequested = false;
        private UNET_Classes.CurrentInfo currentInfo;
        private List<UNET_Classes.Radio> lstRadio = new List<Radio>();
    //    private PJSUA2Implementation.SIP.SIPCall sc;
        private CallOpParam cop;
     //   private bool HeadsetPlugged = false;
        private bool SoundPlaying = false;
        private int RoleClicked = -1;

        private bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        private bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        private bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;
        //token, needed to be able to cancel the TCP listener Task
        // zie: https://www.c-sharpcorner.com/UploadFile/80ae1e/canceling-a-running-task/
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken  token = tokenSource.Token;


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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Dit event gaat af zodra in de Sip>sipaccount>onIncomingCall een call binnenkomt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trigger_CallAlert(object sender, AlertEventArgs e)
        {
            try
            {
                log.Info("called by: " + e.Caller_AccountName);
                this.Invoke((MethodInvoker)(() => lblPtt.Text = "Ringing!!"));

                this.Invoke((MethodInvoker)(() => MessageBox.Show("Het is " + e.Caller_AccountName + " die belt!")));


                //als een Radio call binnenkomt, moet op basis van de accountnaam, de gui geupdate worden
                if (e.Caller_AccountName.Contains("12345")) //intercom
                {
                    this.Invoke((MethodInvoker)(() => btnIntercom.BackColor = Color.Red));
                }

                if (e.Caller_AccountName.Contains("RADIO")) //TYPE 1 VERBINDING
                {
                    string[] exeinfo = e.Caller_AccountName.Split('_');
                    string exercisenumber = Helpers.ExtractNumber(exeinfo[1]);
                    string radnr = Helpers.ExtractNumber(exeinfo[2]);
                    this.Invoke((MethodInvoker)(() => panelRadios.Controls["btnRadio" + radnr].ForeColor = Color.Red));
                }

                //Class broadcast
                if (e.Caller_AccountName.Contains(Constants.cClassBroadcastAll) || e.Caller_AccountName.Contains(Constants.cClassBroadcastAllInstructors) || e.Caller_AccountName.Contains(Constants.cClassBroadcastAllTrainees)) //TYPE 3 VERBINDING BROADCAST
                {
                    this.Invoke((MethodInvoker)(() => this.BackColor = Color.Red));
                }

                //point to point: Als een call voor een rol binnenkomt, moet het bolletje rechtsboven gaan knipperen
              //  if (e.Caller_AccountName.Contains("P2P")) //TYPE 1 VERBINDING
              //  {
              //i//f it is not radio, broadcast or intercom, it must be a p2p
                 //   string[] roleinfo = Helpers.ExtractNumber(e.Caller_AccountName);
               //     string exercisenumber = Helpers.ExtractNumber(roleinfo[1]);
                    string radnr2 =( Helpers.ExtractNumber(e.Caller_AccountName)).Substring(0,4);
                foreach(Control control in panelRoles.Controls)
                {
                    if (control.GetType() == typeof(UNET_Button.UNET_Button))
                    {
                      if ( ((UNET_Button.UNET_Button)control).ID.ToString() == radnr2)
                        {
                            this.Invoke((MethodInvoker)(() => control.ForeColor = Color.Red));
                            this.Invoke((MethodInvoker)(() => ((UNET_Button.UNET_Button)control).P2PCallState = UNET_Button.P2PState.psP2PCallPending));
                            break;
                        }
                    }
                    
             
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                log.Error("Error trigger_call_alert: " + ex.Message);
                MessageBox.Show("trigger call alert + " + ex.Message);
            }

        }

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                this.Text = "UNET Trainee";
                log.Info("Started UNET_Trainee");

                //todo: terugzetten   timer1.Enabled = true;

                ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
                TraineeID = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013");//Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);
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
                    DisplayName = RegistryAccess.GetStringRegistryValue(@"UNET", @"displayname", "1013 trainee");
                    string sipserver = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128");
                    //account
                    string domain = RegistryAccess.GetStringRegistryValue(@"UNET", @"domain", "unet");
                    //account
                    UInt16 port = Convert.ToUInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"port", "5060"));
                    string password = RegistryAccess.GetStringRegistryValue(@"UNET", @"password", "1234");

                    bool debug = (RegistryAccess.GetStringRegistryValue(@"UNET", @"debug", "1").Trim() == "1") ? true : false;


                    ///first check if freeswich is reach-able, if not, cancel the whole startup
                    //Initializes a new instance of ESLconnection, and connects to the host $host on the port $port, and supplies $password to freeswitch
                    try
                    {
                        var ping = new System.Net.NetworkInformation.Ping();

                        var result = ping.Send(sipserver);

                        if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                        {
                            MessageBox.Show("Unable to ping Freeswitch server. Cannot continue");
                            Application.Exit();
                        }
                           

                    }
                    catch (Exception ex)
                    {

                    }

                    //als NIET in debug mode, verberg een aantal knoppen
                    button1.Visible = debug;
                    button2.Visible = debug;
                    button3.Visible = debug;
                    button4.Visible = debug;

                    //the useragent holds everything needed for the sip communication
                    useragent = new PJSUA2Implementation.SIP.UserAgent(account, sipserver, port, domain, password, DisplayName);
                    useragent.UserAgentStart("UNETTrainee");


                    //koppel het onIncomingCall event aan de frmmain schemupdate
                    useragent.acc.CallAlert += new SipAccount.AlertEventHandler(trigger_CallAlert);

                    //  sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
                    cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    lblRegInfo.Text = "Registered: " + TraineeID + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();


                    //initial fill for the radio state array
                    for (int i = 0; i <= 19; i++)
                    {
                        RadioStateArray[i] = "Off";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                        ex.InnerException + Environment.NewLine + ex.StackTrace.ToString() +
                        "Contact your system administrator");
                    log.Error("Error creating accounts " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                    lblRegInfo.Text = "Failed to reg " + TraineeID;
                    this.Close();
                    
                }

                try
                {
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }

                    //Register the trainee to the wcf service
                    service.RegisterClient(TraineeID, DisplayName, true); //'true' means: this is a trainee
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
                    ////by the TCPSocketClient 
                    Task.Factory.StartNew(() => { StartListinging(); });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception starting PTT and Headset monitoring: " + Environment.NewLine + ex.Message);
                }


                Helpers.HideButtons(this);


            }
            catch (Exception ex)
            {
                log.Error("Problem starting trainee!" + ex.Message + ex.InnerException);
            }

        }

        /// <summary>
        /// gets the pending P2P (type3(1) calls for this instructor and sets the status indicators accordingly
        /// </summary>
        /// <param name="parent"></param>
        private void GetP2P(Control parent)
        {
            try
            {
                foreach (Control c in parent.Controls)
                {
                    //reset everything
                    if (c.GetType() == typeof(UNET_Button.UNET_Button) && (c.Name.ToLower().Contains("role")))
                    {
                        ((UNET_Button.UNET_Button)c).ImageIndex = 0;

                    }
                }

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                //retrieve the pending PointToPoints for this instructor
                var resultP2P = service.GetP2P(TraineeID);
                List<PointToPoint> pendingP2P = resultP2P.ToList<PointToPoint>();

                //loop thrue the list of pending assists
                foreach (PointToPoint pending in pendingP2P)
                {
                    //then loop thrue the list of btnTrainees
                    foreach (Control ctrl in panelRoles.Controls)
                    {
                        if (ctrl.GetType() == typeof(UNET_Button.UNET_Button))
                        {
                            string[] p2plabel = ((UNET_Button.UNET_Button)ctrl).Text.Split(' ');
                            if (((UNET_Button.UNET_Button)ctrl).P2PCallState == UNET_Button.P2PState.psP2PCallPending)
                            {//als de button een unetbutton is EN de role is pending, dan laat het ledje knipperen

                                if (((UNET_Button.UNET_Button)ctrl).ImageIndex == 1)
                                {
                                    ((UNET_Button.UNET_Button)ctrl).ImageIndex = 2;
                                }
                                else
                                {
                                    ((UNET_Button.UNET_Button)ctrl).ImageIndex = 1;
                                }

                                break;
                            }
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting P2P", ex);
                // throw;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                try
                {
                    //close the connection to the wcf service, if it is still opened
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }


                    if (service.GetPendingChanges() > ucb.LastUpdate) //only if the last-changed-datetime on the server is more recent than on the client, we need to update
                    {


                        //enable the Exercise buttons
                        currentInfo = service.GetExerciseInfo(TraineeID);


                        if (currentInfo != null)
                        {
                            lblPlatform.Text = currentInfo.Platform;
                            lblConsole.Text = currentInfo.ConsoleRole;
                            lblExerciseMode.Text = currentInfo.ExerciseMode;
                            lblExerciseName.Text = currentInfo.ExerciseName;
                            lblInstructor.Text = currentInfo.InstructorName;

                            lblPlatform.ForeColor = Color.Yellow;
                            lblConsole.ForeColor = Color.Yellow;
                            lblExerciseMode.ForeColor = Color.Yellow;
                            lblExerciseName.ForeColor = Color.Yellow;
                            lblInstructor.ForeColor = Color.Yellow;
                            // SetButtonStatus(this);
                            //  SetButtonStatus(panelRoles);
                            //  SetButtonStatus(panelRadios);
                            ucb.LastUpdate = DateTime.Now; //todo: eigenlijk moet hier het resultaat van GetPendingchanges in komen        
                        }
                        else
                        {
                            //TODO: HIER IETS ZINVOLS VERZINNEN  Console.Write("The service.getexerciseinfo object is empty!!!");
                        }
                    }


                }
                catch (Exception ex)
                {
                    log.Error("Error updating screen controls", ex);
                    // throw;
                }
                SetButtonStatusRoles(panelRoles);
                SetButtonStatusRadios(panelRadios);

                GetP2P(this);
            }

        }


        #region buttonstatus
        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatusRoles(Control parent)
        {
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

                //     var rolelist = service.GetRoles();
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
                    panelRoles.Controls["btnRole" + role.ID.ToString("00")].Tag = "enabled";
                    ((UNET_Button.UNET_Button)panelRoles.Controls["btnRole" + role.ID.ToString("00")]).ID = role.ID;

                    panelRoles.Controls["btnRole" + role.ID.ToString("00")].Text = string.Format("Role {0}{1}{2}", role.ID, Environment.NewLine, role.Name);
                    ((UNET_Button.UNET_Button)panelRoles.Controls["btnRole" + role.ID.ToString("00")]).ImageIndex = -1;

                    //voor iedere role button (P2P) Moet er een apart account geregistreerd worden, zodat de p2p elkaar kan bellen
                    //de addaccount zorgt zelf dat een account niet dubbel geregistreerd wordt.
                    //if (currentInfo != null)
                    //{
                    //string domain = RegistryAccess.GetStringRegistryValue(@"UNET", @"domain", "unet");
                    //    useragent.AddAccount("P2P_EXERCISE_" + currentInfo.ExerciseNumber + "_PLATFORM_" + currentInfo.Platform + "_ROLE_" + role.Name, domain);
                    //}
                    //kleur de button navenant de p2pcallstatus
                    switch (((UNET_Button.UNET_Button)panelRoles.Controls["btnRole" + role.ID.ToString("00")]).P2PCallState)
                    {
                        case UNET_Button.P2PState.psCalledByInstructor: //when you get called by an trainee
                        case UNET_Button.P2PState.psNoP2PCall:
                        case UNET_Button.P2PState.psCalledByTrainee: //on the trainee, this should never happen
                        default:
                            {
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].BackColor = Theming.RoleSelectedButton;
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].ForeColor = Theming.ButtonSelectedText;
                                break;
                            }
                        case UNET_Button.P2PState.psP2PCallPending:
                            {
                                //just wait..
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].BackColor = Color.Black;
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].ForeColor = Color.White;

                                break;
                            }
                        case UNET_Button.P2PState.psP2PInProgress:
                            {
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].BackColor = Color.Green;
                                panelRoles.Controls["btnRole" + role.ID.ToString("00")].ForeColor = Color.White;

                                break;
                            }

                    }
                    Application.DoEvents();
                }
                UNET_Classes.Helpers.ResizeButtonsVertical(panelRoles, lstrole.Count, "role");
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus. tranee mainform", ex);
                // throw;
            }

            try
            {
                if (AssistRequested & btnAssist.BackColor == Theming.AssistRequestedColor)
                {
                    btnAssist.BackColor = Theming.AssistAcknowledgedColor;
                }
                else
                {
                    btnAssist.BackColor = Theming.AssistRequestedColor;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception making assist: " + ex.Message);
            }
        }



        /// <summary>
        /// enable en set colors for the radio buttons
        /// </summary>
        /// <param name="parent"></param>
        private void SetButtonStatusRadios(Control parent)
        {
            try
            {
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }


                //enable the Radio buttons
                var radiolist = service.GetRadios();
                lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
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

                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Tag = "enable";
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Text = string.Format("Radio {0}{1}{2}{3}Noise:{4}{5}{6}", radio.ID, Environment.NewLine, radio.State.ToString().Substring(2, radio.State.ToString().Length - 2), Environment.NewLine, radio.NoiseLevel, Environment.NewLine, radio.SpecialEffect == UNETSpecialEffect.seVHF ? "VHF" : "UHF");
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].BackColor = Theming.RadioSelectedButton;
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].ForeColor = Theming.ButtonSelectedText;
                   ((UNET_Button.UNET_Button)panelRadios.Controls["btnRadio" + radio.ID.ToString("00")]).ID = radio.ID;
                }

                UNET_Classes.Helpers.ResizeButtonsVertical(panelRadios, lstRadio.Count, "radio");
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus. Instructor mainform", ex);
                // throw;
            }

            try
            {
                if (AssistRequested & btnAssist.BackColor == Theming.AssistRequestedColor)
                {
                    btnAssist.BackColor = Theming.AssistAcknowledgedColor;
                }
                else
                {
                    btnAssist.BackColor = Theming.AssistRequestedColor;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception making assist: " + ex.Message);
            }
        }

        #endregion

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

            try
            {
                //close the useragent en with that the sip connection
                if (!object.ReferenceEquals(useragent, null))
                {
                    //stop the sip connection in a nice manner before closing
                    HangupAllCalls();
                    useragent.UserAgentStop();
                }

                //cancel the TCP listener
                tokenSource.Cancel();
                //close the connection to the wcf service, if it is still opened
                if (service.State == System.ServiceModel.CommunicationState.Opened)
                {
                    service.UnRegisterClient(TraineeID, false);

                    service.Close();
                }


                //try to find and kill the TCPSocketClient process and kill it
                FindAndKillProcess("TCPSocketClient.exe");

                //ruim de signalgenerator netjes op
                signal.Stop();
                signal.Cleanup();
                signal.DisposeSignalgenerator();


                log.Info("Terminated UNET_Trainee");

            }
            catch (Exception ex)
            {
                log.Error("Error shutting down: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace.ToString());
            }

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

        /// <summary>
        /// determine the destination account when a radio call is started
        /// </summary>
        /// <param name="_radioNumber"></param>
        /// <returns></returns>
        private string GetDestination(int _radioNumber)
        {
            string result = string.Empty;

            try
            {
                //   return string.Format("RADIO_EXERCISE{0}_{1}", currentInfo.ExerciseName, _radioNumber);
                return "RADIO_" + _radioNumber + "_EXERCISE" + currentInfo.ExerciseName;// "1010";

            }

            catch (Exception ex)
            {
                log.Error("Error GetDestination " + ex.Message + ex.StackTrace + ex.InnerException);
            }

            //process not found, return false
            return result;
        }


        private void btnRadio01_Click(object sender, EventArgs e)
        {


            int radioNumber = -1;
            try
            {
                StopNoise();
                //1 zoek uit welke radio geklikt heeft
                string state = string.Empty;
                radioNumber = Convert.ToInt16(UNET_Classes.Helpers.ExtractNumber(((Button)sender).Name));
                Radio radioinfocus = new Radio(-1, "unreal radio", "xx");
                //try to find the radioobject
                foreach (Radio rad in lstRadio)
                {
                    if (rad.ID == Convert.ToInt16(((Button)sender).Tag))
                    {
                        radioinfocus = rad;
                    }
                }


                if (((Button)sender).Text.Trim().Length > 8)
                {
                    if (((Button)sender).Text.Trim().ToLower().Contains("off"))
                        state = "Off";
                    else if (((Button)sender).Text.Trim().ToLower().Contains("rx"))
                        state = "Rx";
                    else if (((Button)sender).Text.Trim().ToLower().Contains("tx"))
                        state = "Tx";

                    //   state = ((Button)sender).Text.Trim().Substring(((Button)sender).Text.Trim().Length - 2);
                }
                else
                {
                    state = "Rx"; //if the state is now 'TX', the next one will be 'OFF' and that is what we initially want
                }
                //2 zoek die op in de radios lijst in de conferencebridge
                //  string btnstate = ((Button)sender).Tag.ToString();
                switch (state) //todo: weer werkend maken
                {
                    //3 zet de status   
                    case "Rx":
                        {
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Tx");
                            //     ((Button)sender).Text = string.Format("Radio {0}{1}{2}{3}Noise:{4}{5}{6}", radioNumber, Environment.NewLine,  radio.State.ToString().Substring(2, radio.State.ToString().Length - 2), Environment.NewLine, radio.NoiseLevel, Environment.NewLine, radio.SpecialEffect == UNETSpecialEffect.seVHF ? "VHF" : "UHF");
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}{3}Noise:{4}{5}{6}", radioNumber, Environment.NewLine, RadioStateArray[radioNumber], Environment.NewLine, radioinfocus.NoiseLevel, Environment.NewLine, radioinfocus.SpecialEffect == UNETSpecialEffect.seVHF ? "VHF" : "UHF");

                            ((Button)sender).Tag = "Tx";
                            RadioStateArray[radioNumber] = "Tx";
                            //mix noise into the conversation
                            InitNoiseLevel(radioNumber);
                            service.SetRadioStatus(Convert.ToInt16(radioNumber), UNETRadioState.rsTx);

                            //bel in op de conferentie
                            MakeCall(GetDestination(radioNumber), true, true, false, true, true, false);
                            //  ucb.Radios[radioNumber].State = UNETRadioState.rsTx;
                            break;
                        }
                    case "Off":
                    default:
                        {
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Rx");
                            ((Button)sender).Tag = "Rx";
                            RadioStateArray[radioNumber] = "Rx";
                            service.SetRadioStatus(Convert.ToInt16(radioNumber), UNETRadioState.rsRx);

                            //  ucb.Radios[radioNumber].State = UNETRadioState.rsRx;//we zetten hem 1 status HOGER dan de huidige status, en zitten dit in de singleton en op de hmi
                            break;
                        }
                    case "Tx":
                        {
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "OFF");
                            ((Button)sender).Tag = "Off";
                            RadioStateArray[radioNumber] = "Off";
                            service.SetRadioStatus(Convert.ToInt16(radioNumber), UNETRadioState.rsOff);


                            // ucb.Radios[radioNumber].State = UNETRadioState.rsOff;
                            break;
                        }
                }
                log.Info("Have set the status of the Radio: " + radioNumber + " to: " + state);
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //    MakeCall("1010" +(Convert.ToInt16(radioNumber)), true, true, false, true, true, false);
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
        private void MakeCall(string _destination, bool _inLeft, bool _inRight, bool _inSpeaker, bool _outLeft, bool _outRight, bool _outSpeaker)
        {
            try
            {
                //hier worden de channels gekoppeld aan de call die wordt opgezet
                List<InputChannels> lstinputchannels = new List<InputChannels>();
                if (_inLeft)
                    lstinputchannels.Add(InputChannels.ichMic);
                if (_inRight)
                    lstinputchannels.Add(InputChannels.ichSecondMic);
                if (_inSpeaker)
                    lstinputchannels.Add(InputChannels.ichThirdMic);


                List<OutputChannels> lstoutputchannels = new List<OutputChannels>();
                if (_outLeft)
                    lstoutputchannels.Add(OutputChannels.ochLeft);
                if (_outRight)
                    lstoutputchannels.Add(OutputChannels.ochRight);
                if (_outSpeaker)
                    lstoutputchannels.Add(OutputChannels.ochSpeaker);

                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, ref lstinputchannels, ref lstoutputchannels, -1);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                //    cop.reason = "test van frank";

                sc.makeCall(string.Format("sip:{0}@{1}", _destination, SIPServer), cop);
                useragent.acc.Calls.Add(sc); //kennelijk worden die calls niet vanzelf in deze list geplaatst.
            }
            catch (Exception ex)
            {
                log.Error("Error making call to: " + _destination, ex);
                // throw;
            }
        }

        /// <summary>
        /// hang up a given call
        /// </summary>
        /// <param name="_traineeID"></param>
        /// <param name="_destination"></param>
        private void HangupCall(string _destination)
        {
            try
            {



                foreach (Call call in useragent.acc.Calls)
                {
                    CallInfo ci = call.getInfo();

                    if (ci.localUri.Contains(_destination))
                    {
                        CallOpParam cop = new CallOpParam();
                        cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                        call.hangup(cop);
                        Console.Write("Successfully hanged up call: " + ci.id + " Totalduration: " + ci.totalDuration);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.Write("Exception hanging up calls: " + ex.Message);
            }

        }

        #endregion

        private void btnClassBroadcast_Click(object sender, EventArgs e)
        {
            log.Info("Clicked ClassBroadcast");
            MakeCall("20000", true, true, false, false, true, false); //20000 is de code voor de class broadcast conferentie, en gaat van stereo IN naar Right OUT


        }

        private void btnMonitorTrainee_MouseUp(object sender, MouseEventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, @"Sounds\beep-01a.wav")))
            {

                simpleSound.Stop();
            }
        }


        private void btnIntercom_Click(object sender, EventArgs e)
        {
            if (btnIntercom.BackColor == Theming.IntercomPressed)
                btnIntercom.BackColor = Theming.IntercomPressed;
            else
                btnIntercom.BackColor = Theming.IntercomNotPressed;


            log.Info("Clicked Intercom");
            MakeCall("12345", true, true, false, false, true, false); //12345 is de code voor de intercom
            PlayBeep();
        }

        private void btnAssist_Click(object sender, EventArgs e)
        {
            try
            {
                  if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }   
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                // visible/invisible and also set the statusled
                //  {
                if (!AssistRequested)
                {
             

                    service.CreateAssist(TraineeID.ToString(), DisplayName);
                    AssistRequested = true;
                  log.Info("Assist requested by:" + TraineeID);
       }
                else
                {
                    //if the assist is pending then assistrequested is true and we want to cancel it
                    service.AcknowledgeAssist("cancelassist", TraineeID);
                    AssistRequested = false;
                    log.Info("Assist cancelled by:" + TraineeID);

                }


                //  MakeCall(@"MIC_Conference_Pos01\" + TraineeID, true, true, false, true, false, false);

            }
            catch (Exception ex)
            {
                log.Error("Error requesting assist: " + ex.Message);
            }

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
                Console.WriteLine("Cannot find tcpclient " + ex.Message + ex.StackTrace);
            }
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
            return true;
        }
        #region noise
        private UNET_Sounds.SignalGeneratorController signal = new SignalGeneratorController();

        /// <summary>
        /// this starts the noise and at a particular level
        /// </summary>
        private void InitNoiseLevel(int _radiobutton)
        {
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }

            // int SelectedRadioButtonIndex = 1;// Convert.ToInt16(Regex.Replace(_btn.Name, "[^0-9.]", "")); //haal het indexnummer op van de button
            int noiselevel = service.GetNoiseLevel(_radiobutton);
            if (noiselevel > 0)
            {
                signal.NoiseLevel = noiselevel;
                signal.Start();
            }
            else
            {
                signal.Stop();
            }
        }

        /// <summary>
        /// this makes the noise stop
        /// </summary>
        private void StopNoise()
        {
            signal.Stop();
        }

        #endregion


        /// <summary>
        /// Add or remove the ptt to or from the queue
        /// </summary>
        /// <param name="_pressed"></param>
        private void SetPTTPressed(bool _pressed)
        {
            try
            {
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                // visible/invisible and also set the statusled
                //  {
                if (!AssistRequested)
                {
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }

                    if (_pressed)
                    {
                        service.AddPTT(TraineeID.ToString().Trim(), UNET_Service.PTTuser.puTrainee);
                    }
                    else
                    {
                        service.AcknowledgePTT(TraineeID.ToString().Trim());
                    }
                 }


                //  MakeCall(@"MIC_Conference_Pos01\" + TraineeID, true, true, false, true, false, false);
                log.Info("Set ptt event:" + TraineeID + " Value: "  + _pressed);

            }
            catch (Exception ex)
            {
                log.Error("Error requesting assist: " + ex.Message);
            }

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
            while (!token.IsCancellationRequested)
            {
                // Application.DoEvents();
                if (serverStreamReader != null)
                {
                    string received = serverStreamReader.ReadLine();
                    if (received.Length > 0)
                    {
                        string[] splitstr = received.Split('|');
                        if (splitstr[0].ToString().Trim().ToLower() == "ptt")
                        {
                            if (splitstr[1].ToString().Trim().ToLower() == "true")
                            {
                                lblPtt.Text = "PTT ";
                                SetPTTPressed(true);
                            }
                            else
                            {
                                lblPtt.Text = "";
                                SetPTTPressed(false);
                            }

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
                }
              //  Application.DoEvents();
            //    Console.WriteLine("CLIENT: " + serverStreamReader.ReadLine());
            //    serverStreamWriter.WriteLine("Hi!");
            //    serverStreamWriter.Flush();
            }//while
        }
        #endregion

        private void btnRole_Click(object sender, EventArgs e)
        {
            RoleClicked = (int)(Enum.Parse(typeof(Enums.Roles), ((UNET_Button.UNET_Button)sender).Name.Remove(0, 3)));
       
            try
            {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                switch (((UNET_Button.UNET_Button)sender).P2PCallState)
                {
                    case UNET_Button.P2PState.psNoP2PCall:
                        {
                            //start a p2p call (by instructor)
                            service.RequestPointToPoint(TraineeID);//todo, SelectedTrainee, SelectedExercise, GetRole(RoleClicked));

                            ((UNET_Button.UNET_Button)sender).P2PCallState = UNET_Button.P2PState.psP2PCallPending;
                            log.Info("start pending call to:" + TraineeID + "_P2P");

                            break;
                        }
                    case UNET_Button.P2PState.psCalledByTrainee: //on the trainee, this should never happen
                        {
                            log.Error("It should not be possible that ann instructor calls you!!");
                            break;
                        }
                    case UNET_Button.P2PState.psCalledByInstructor: //when you get called by an trainee
                        {

                            //usecase 3.1.3.2.1 (rec_UNET_SRS7 tm 9): Opzetten P2P door instructor
                            service.AcknowledgeP2P(TraineeID); //let know the call is answered 
                            MakeCall(TraineeID + "_P2P", true, true, true, true, true, true); //and then call
                            log.Info("Made call back to:" + TraineeID + "_P2P");

                            ((UNET_Button.UNET_Button)sender).P2PCallState = UNET_Button.P2PState.psP2PInProgress;
                            break;
                        }
                    default:
                    case UNET_Button.P2PState.psP2PCallPending:
                        {
                            //just wait..
                            ((UNET_Button.UNET_Button)sender).BackColor = Color.Black;
                            ((UNET_Button.UNET_Button)sender).ForeColor = Color.White;
                            ((UNET_Button.UNET_Button)sender).P2PCallState = UNET_Button.P2PState.psP2PCallPending;

                            break;
                        }
                    case UNET_Button.P2PState.psP2PInProgress:
                        {
                            HangupCall(TraineeID); //hang up the call
                            ((UNET_Button.UNET_Button)sender).P2PCallState = UNET_Button.P2PState.psNoP2PCall;
                            ((UNET_Button.UNET_Button)sender).ImageIndex = -1;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {

                log.Error("Error clicking Role button " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeCall("1011", true, true, false, true, false, false);

        }

        /// <summary>
        /// Hangup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void button2_Click(object sender, EventArgs e)
        {
            HangupAllCalls();
        }

       /// <summary>
        /// Hangup all active calls
        /// </summary>
        private void HangupAllCalls()
        {
            try
            {


                foreach (Call call in useragent.acc.Calls)
                {
                    if (call != null)
                    {
                        CallInfo ci = call.getInfo();
                        CallOpParam cop = new CallOpParam();
                        cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                        call.hangup(cop);
                        lblPtt.Text = "...";
                        Console.Write("Successfully hanged up call: " + ci.id + " Totalduration: " + ci.totalDuration);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write("Exception hanging up calls: " + ex.Message);
            }
         }

        private void button3_Click(object sender, EventArgs e)
        {
            MakeCall("1010", true, true, false, false, false, true);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MakeCall("1010", true, false, false, true, true, true);

        }
    }
}