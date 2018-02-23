﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using pjsua2;
using System.Configuration;
using System.Drawing;
using UNET_Classes;
using System.Runtime.InteropServices;
using UNET_Theming;
using UNET_SignalGenerator;
using System.Media;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using PJSUA2Implementation.SIP;
using System.ComponentModel;

namespace UNET_Trainer
{

    public partial class FrmUNETMain : Form// FrmUNETbase
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow(); //wordt gebruikt om te detecteren of een scherm wel op de voorgrond zit en het dus zin heeft om de schermcomponenten te updaten


        private Boolean Muted = false;
        private Boolean MonitorTrainee = false;
        private Boolean MonitorRadio = false;
        public int InstructorID = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1017"));
        public string  DisplayName = RegistryAccess.GetStringRegistryValue(@"UNET", @"displayname", "1017 instructor");
             
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer
 
        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;
        public string SIPServer = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128"); //ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
        public string SIPAccountname = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013"); // ConfigurationManager.AppSettings["sipAccount"].ToString().Trim();

        UNET_ConferenceBridge.ConferenceBridge_Singleton ucb = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;
        private PJSUA2Implementation.SIP.SIPCall sc;
        private CallOpParam cop;
        /// WCF service
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        //arrays that hold the statusses of the some
        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;
        public UNET_Classes.Exercise SelectedExercise;
        private int ExersiseNumber = -1;
        private int RoleClicked = -1;
        private int RadioClicked = -1;
        private bool ILMode = false;
  
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
            panelExercises.Paint += UC_Paint;
            panelRadios.Paint += UC_Paint;
            panelRoles.Paint += UC_Paint;
            panelRadios.Paint += UC_Paint;
            panelSetup.Paint += UC_Paint;
            panelFunctions.Paint += UC_Paint;
            panelAssist.Paint += UC_Paint;
        }

        /// <summary>
        /// Zorg dat de panels een witte border krijgen als ze een dargray opvulkleur hebben
        /// https://stackoverflow.com/questions/76455/how-do-you-change-the-color-of-the-border-on-a-group-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UC_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.White, ButtonBorderStyle.Solid);

            //   ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.White, 2, ButtonBorderStyle.Solid, Color.White, 2, ButtonBorderStyle.Solid, Color.White, 4, ButtonBorderStyle.Solid, Color.White, 4, ButtonBorderStyle.Solid);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                //close the connection to the wcf service, if it is still opened
                if (service.State == System.ServiceModel.CommunicationState.Opened)
                {
                    service.Close();
                }
                //close the useragent en with that the sip connection
                if (!object.ReferenceEquals(useragent, null))
                {
                    HangupAllCalls();
                    useragent.UserAgentStop();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error Closing UNET_Trainer", ex);
                // throw;
            }
            //try to find and kill the TCPSocketClient process and kill it
            FindAndKillProcess("TCPSocketClient.exe");
            log.Info("Terminated UNET_Instructor");

            Application.Exit();
           
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FrmRoles frm = new FrmRoles(ExersiseNumber, InstructorID);
            frm.Show();
        }

        private void btnTrainees_Click(object sender, EventArgs e)
        {
            FrmTrainees frm = new FrmTrainees(ExersiseNumber, InstructorID);
            frm.Show();
        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            FrmAudio frm = new FrmAudio();
            frm.Show();
        }

        private void lblSetup_Click(object sender, EventArgs e)
        {
            FrmSetup frm = new FrmSetup();
            frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                service.RegisterClient(InstructorID,DisplayName, false); //todo: kijken of het echt nodig is om dit continue te doen!!!!

                if (service.GetPendingChanges() > ucb.LastUpdate) //only if the last-changed-datetime on the server is more recent than on the client, we need to update
                {

                    SetButtonStatus(this);

                    GetAssists(this);
                    ucb.LastUpdate = DateTime.Now; //todo: eigenlijk moet hier het resultaat van GetPendingchanges in komen
                }
            }
        }

        #region Noise

        private UNET_SignalGenerator.SignalGeneratorController signal = new SignalGeneratorController();


        /// <summary>
        /// this starts the noise and at a particular level
        /// </summary>
        private void InitNoiseLevel(int _radiobutton)
        {
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }

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

        /// <summary>
        /// Make a conference for the selected radio and start a conference and then add the selected noise level
        /// </summary>
        /// <param name="_SelectedRadioButtonIndex"></param>
        /// <param name="_SelectedNoiseButtonIndex"></param>
        public void SetNoiseLevel(int _SelectedRadioButtonIndex, int _SelectedNoiseButtonIndex)
        {

         //todo   MakeCall( @"NOISE_RADIO_X\" + GetTraineeAtRadio(_SelectedRadioButtonIndex), _SelectedNoiseButtonIndex, "Noise for radio: " + _SelectedRadioButtonIndex, true, true, false, true, true, false);
        }

        /// <summary>
        /// determine which trainee is connected to a given radio
        /// </summary>
        /// <param name="_radioindex"></param>
        /// <returns></returns>
        private int GetTraineeAtRadio(int _radioindex)
        {
            //todo: determine the radio
            return 1013;
        }
        #endregion

        #region  Button Status setters

        /// <summary>
        /// gets the assistlist for this instructor and sets the status indicators accordingly
        /// </summary>
        /// <param name="parent"></param>
        private void GetAssists(Control parent)
        {
            try
            { 
            foreach (Control c in parent.Controls)
            {
                //reset everything
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("trainee")))
                {
                    ((Button)c).ImageIndex = 0;
                 
                }
            }

                if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
               service.Open();
            }
            //retrieve the pending assists for this instructor
            var resultassists  = service.GetAssists(InstructorID);
            List<Assist> pendingAssists = resultassists.ToList<Assist>();

                //loop thrue the list of pending assists
                foreach (Assist ass in pendingAssists)
                {
                    //then loop thrue the list of btnTrainees
                    foreach (Control ctrl in panelTrainees.Controls)
                    {
                        if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                        {
                            string[] traineelabel = ((Button)ctrl).Text.Split(' ');
                            string traineeid = traineelabel[1];
                            if(traineeid.Trim() == ass.TraineeID.ToString())
                            {
                                if (((Button)ctrl).ImageIndex == 1)
                                {
                                    ((Button)ctrl).ImageIndex = 2;
                                }
                                else
                                {
                                    ((Button)ctrl).ImageIndex = 1;

                                }

                                //and play a sound
                                break;
                            }                         

                        }
                    }



                    Application.DoEvents();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting assists", ex);
                // throw;
            }

        }
        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatus(Control parent)
        {
            try
            {
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                // visible/invisible and also set the statusled
                //  {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                if (SelectedExercise == null)
                {
                    //bij de start van de instructor moet er al een exercise geselecteerd zijn, zoniet, dan zetten we die hier alsnog..
                    service.SetExerciseSelected(InstructorID, 1, true);
                }

                //we moeten  de huidige status ophalen van de instructeur/exercises/trainee/roles/radios
                //en hiermee de knoppen de juiste kleur geven
                Instructor currentInstructor = service.GetAllInstructorData(InstructorID);
                SelectedExercise = currentInstructor.Exercises.SingleOrDefault(x => x.Selected == true); //neem de geselecteerde exercise

                //enable the Exercise buttons
                var avialableExercisisList = service.GetExercises(); //we have to do this, because, all the time, exercises can be added or removed
                List<UNET_Classes.Exercise> availableExerciseslst = avialableExercisisList.ToList<UNET_Classes.Exercise>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelExercises.Controls) //first DISABLE all buttons
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.ExerciseNotSelected;
                    }
                }
                int exerciseselected = -1;
                foreach (UNET_Classes.Exercise exercise in availableExerciseslst) //then ENABLE them, based on whatever is retrieved from the service
                {
                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Enabled = true; //exercises worden altijd visible, want moeten altijd gekozen kunnen worden
                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Text = string.Format("Exercise {0}{1}{2}{3}{4}", exercise.Number, Environment.NewLine, exercise.SpecificationName, Environment.NewLine, exercise.ExerciseName);

                    //loop nu door de lijst van toegewezen exercises heen en kijk of er een is die aan deze instructor is toegewezen. 
                    //zoja, vul de informatie in en enable de knop
                    if (currentInstructor != null)
                    {
                        if (!Object.ReferenceEquals(currentInstructor.Exercises, null))
                        {
                            foreach (Exercise exerciseAssigned in currentInstructor.Exercises)
                            {
                                if (exerciseAssigned.Number == exercise.Number)
                                {
                                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Enabled = true;
                                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Tag = "enable";

                                    //NOT ONLY ENABLE BUT IF IT IS SELECTED (ON THE SERVER) CHANGE THE COLOR
                                    if (exerciseAssigned.Selected)
                                        panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].BackColor = Theming.ExerciseSelectedButton;
                                    else
                                    {
                                        panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].BackColor = Theming.ExerciseNotSelected;

                                    }
                                    exerciseselected = exercise.Number;
                                }
                            }
                        }
                    }
                }
                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(panelExercises, availableExerciseslst.Count, "exersise");


                //enable the Trainees buttons, for the number of trainees that are in
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
                int listindex = 1;
                foreach (Control ctrl in panelTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.TraineeNotSelectedButton;

                    }
                }
                foreach (UNET_Classes.Trainee trainee in lstTrainee)
                {
                    panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                    ///NOTE: THE SPACES IN THE FORMAT STRING ARE IMPORTANT BECAUSE WE NEED THEM IN THE ASSIST ACKNOWLEDGJEMENT!!
                    panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text = string.Format("Trainee {0} {1}{2}{3}Role:{4}", trainee.ID, Environment.NewLine, trainee.Name, Environment.NewLine, "TraineeRole");


                    //loop nu door de lijst van toegewezen trainees heen en kijk of er een is die aan deze instructor/exercise is toegewezen. 
                    //zoja, vul de informatie in en enable de knop
                    if (currentInstructor != null)
                    {
                        if (!Object.ReferenceEquals(currentInstructor.Exercises, null))
                        {
                            if (exerciseselected != -1)
                            {
                                foreach (Trainee assignedTrainee in currentInstructor.Exercises.FirstOrDefault(x => x.Number == exerciseselected).TraineesAssigned) //pak van de bij exercises geselecteerde exercise, de lijst van toegewezen trainees en gebruik die om de buttons te kleuren
                                {
                                    if (assignedTrainee.ID == trainee.ID)
                                    {
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].BackColor = Theming.TraineeSelectedButton;
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Tag = "enable";

                                    }
                                }
                            }
                        }
                    }
                    listindex++;
                }

                UNET_Classes.Helpers.ResizeButtonsVertical(panelTrainees, lstTrainee.Count, "trainee");

                Application.DoEvents();




                //enable the Roles buttons
                foreach (Control ctrl in panelRoles.Controls)
                {
                    if (((ctrl.GetType() == typeof(System.Windows.Forms.Button)) && ((Button)ctrl).Name != "btnClose"))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.Background;

                    }
                }

                if (SelectedExercise != null)
                {
                    foreach (UNET_Classes.Role role in SelectedExercise.RolesAssigned)
                    {
                        panelRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                        panelRoles.Controls["btnRole" + role.ID.ToString("00")].Text = string.Format("Role {0}{1}{2}", role.ID, Environment.NewLine, role.Name);

                        //loop nu door de lijst van toegewezen roles heen en kijk of er een is die aan deze instructor/exercise is toegewezen. 
                        //zoja, vul de informatie in en enable de knop
                        if (currentInstructor != null)
                        {
                            if (!Object.ReferenceEquals(currentInstructor.Exercises, null))
                            {
                                if (exerciseselected != -1)
                                {
                                    foreach (Role assignedRole in currentInstructor.Exercises.FirstOrDefault(x => x.Number == exerciseselected).RolesAssigned) //pak van de bij exercises geselecteerde exercise, de lijst van toegewezen trainees en gebruik die om de buttons te kleuren
                                    {
                                        if (assignedRole.ID == role.ID)
                                        {
                                            panelRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                                            panelRoles.Controls["btnRole" + role.ID.ToString("00")].BackColor = Theming.RoleSelectedButton;
                                            panelRoles.Controls["btnRole" + role.ID.ToString("00")].Tag = "enable";

                                        }
                                    }
                                }
                            }
                        }
                    }
                    UNET_Classes.Helpers.ResizeButtons(panelRoles, SelectedExercise.RolesAssigned.Count, "role");


                    if(ILMode)
                    {
                        btnIL.BackColor = Theming.ILModeActive;
                    }
                    else
                    {
                        btnIL.BackColor = Theming.ILModeInactive;
                    }
                }


                //enable the Radio buttons
                foreach (Control ctrl in panelRadios.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.RadioNotSelectedButton;

                    }
                }
                if (SelectedExercise != null)
                {
                    foreach (UNET_Classes.Radio radio in SelectedExercise.RadiosAssigned)
                    {
                        panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Enabled = true;
                        panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Text = string.Format("Radio {0}{1}{2}{3}Noise:{4}", radio.ID, Environment.NewLine, radio.Description, Environment.NewLine, radio.NoiseLevel);

                        //loop nu door de lijst van toegewezen radios heen en kijk of er een is die aan deze instructor/exercise is toegewezen. 
                        //zoja, vul de informatie in en enable de knop
                        if (currentInstructor != null)
                        {
                            if (!Object.ReferenceEquals(currentInstructor.Exercises, null))
                            {
                                if (exerciseselected != -1)
                                {
                                    foreach (Radio assignedRadio in currentInstructor.Exercises.FirstOrDefault(x => x.Number == exerciseselected).RadiosAssigned) //pak van de bij exercises geselecteerde exercise, de lijst van toegewezen trainees en gebruik die om de buttons te kleuren
                                    {
                                        if (assignedRadio.ID == radio.ID)
                                        {
                                            panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Enabled = true;
                                            panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].BackColor = Theming.RadioSelectedButton;
                                            panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Tag = "enable";

                                        }
                                    }
                                }
                            }
                        }
                    }
                    UNET_Classes.Helpers.ResizeButtons(panelRadios, SelectedExercise.RadiosAssigned.Count, "radio");
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus in Trainer", ex);
                // throw;
            }
        }

        #endregion

        /// <summary>
        /// Dit event gaat af zodra in de Sip>sipaccount>onIncomingCall een call binnenkomt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trigger_CallAlert(object sender, AlertEventArgs e)
        {
            this.Invoke((MethodInvoker)(() => lblPtt.Text = "Ringing!!"));

            Application.DoEvents();
        }

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.Text = "UNET Instructor";
            log.Info("Started UNET_Instructor");

            // Set the text displayed in the caption.
            this.Text = "UNET";
            this.BackColor = Color.White;
            // Set the opacity to 75%.
            this.Opacity = 1;
            // Size the form to be 300 pixels in height and width.
            this.Size = new Size(800, 600);
            // Display the form in the center of the screen.
            // this.StartPosition = FormStartPosition.Manual
            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            //initally, all buttons must be hidden
            the.InitPanels(panelExercises);
            the.InitPanels(panelRadios);
            the.InitPanels(panelRoles);
            the.InitPanels(panelTrainees);

            the.SetFormSizeAndPosition(this);

            ShowIcon = false;
            ShowInTaskbar = false;




            // check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            try
            {
                //the useragent holds everything needed for the sip communication
                string account = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013");
                //displayname
                string displayname = RegistryAccess.GetStringRegistryValue(@"UNET", @"displayname", "1013 trainee");
                //sipserver
                string sipserver = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128");
                //account
                string domain = RegistryAccess.GetStringRegistryValue(@"UNET", @"domain", "unet");
                //account
                UInt16 port = Convert.ToUInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"port", "5060"));
                string password = RegistryAccess.GetStringRegistryValue(@"UNET", @"password", "1234");
                bool debug = (RegistryAccess.GetStringRegistryValue(@"UNET", @"debug", "1").Trim() == "1") ? true : false;;

                button1.Visible = debug;
                btnClose.Visible = debug;

                //the useragent holds everything needed for the sip communication
                useragent = new PJSUA2Implementation.SIP.UserAgent(account, sipserver, port, domain, password, displayname);
                useragent.UserAgentStart("UNETTrainer");


                //koppel het onIncomingCall event aan de frmmain schemupdate
                useragent.acc.CallAlert += new SipAccount.AlertEventHandler(trigger_CallAlert);


                //  sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
                cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;

                //koppel het onIncomingCall event aan de frmmain schemupdate
                useragent.acc.CallAlert += new SipAccount.AlertEventHandler(trigger_CallAlert);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                    ex.InnerException + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine + "User: " + RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013") +
                    "Contact your system administrator");
                log.Error("Error creating accounts " + Environment.NewLine + ex.Message + " cannot continue. " + Environment.NewLine +
                    ex.InnerException + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine + "User: " + RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013") +
                    "Contact your system administrator");
                this.Close();
            }

            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }


            try
            {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //Register this instructor to the wcf service
                service.RegisterClient(InstructorID, DisplayName, false); //'false' means: this is an Instructor
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                    ex.InnerException + Environment.NewLine +
                    "Contact your system administrator");
                log.Error("Error creating accounts " + ex.Message);
                this.Close();
            }
            try
            {
                //this is specially for the COMservice that listens to the PTT and Headset events, generated
                //by the TCPSocketClient 
                Task.Factory.StartNew(() => { StartListinging(); });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception starting PTT and Headset monitoring: " + Environment.NewLine + ex.Message);
            }
        }


        private void btnClassBroadcast_Click(object sender, EventArgs e)
        {
            FrmClassBroadcast frm = new FrmClassBroadcast();
            frm.frmmain = this; //we need the formmain because it holds the SIP connection
            frm.Show();
        }

        private void btnRadios_Click(object sender, EventArgs e)
        {
            FrmRadioSetup frm = new FrmRadioSetup(ExersiseNumber + 1);
            frm.Show();
        }
 

        /// <summary>
        /// 2.1.6 when mute is clicked, the monitoring of all sessions is canceled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (!Muted)
            {
                Muted = true;
                btnMute.BackColor = System.Drawing.Color.Red;
                btnMute.Text = "Muted";
                MonitorRadio = false;
                MonitorTrainee = false;

                //todo: really stop monitoring!!
                useragent.receiveInputMute(true);
            }
            else
            {
                Muted = false;
                btnMute.BackColor = System.Drawing.Color.LightGreen;
                btnMute.Text = "Mute radio";
            }
        }


        /// <summary>
        /// zie: 2.1.13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMonitorTrainee_Click(object sender, EventArgs e)
        {
         //   SetTraineeStatus();
            if (!MonitorTrainee)
            {
                MonitorTrainee = true;
                btnMonitorTrain.BackColor = System.Drawing.Color.SaddleBrown;
                btnMonitorTrain.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                MonitorTrainee = false;
                btnMonitorTrain.BackColor = System.Drawing.Color.Aqua;
                btnMonitorTrain.ForeColor = System.Drawing.Color.Black;
            }
        }

        #region trainee events
        /// <summary>
        /// Hier wordt de kleur van de trainee knop weer teruggezet, alsmede de array
        /// </summary>
        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            int traineeIndex = -1;
 
            traineeIndex = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Trainees), ((Button)sender).Name.Remove(0, 3)));

            /// <summary>
            /// When the button  'monitor trainee' clicked and after that one of the trainee buttons,
            /// this trainee button must be set to brown, and a possible other trainee button must be set to the default color
            /// this generic code covers this for all all buttons at once
            /// </summary>
            if (MonitorTrainee)
            {
                // the trainee buttons are named e.g.: btnTraineeAA , we use this name, to find in the enum the index that is connected to this enum
                MonitorTraineeArray[traineeIndex] = true;
                ((Button)sender).BackColor = System.Drawing.Color.SaddleBrown;
                ((Button)sender).ForeColor = System.Drawing.Color.White;
            }


            //if assist is pending, acknowledge it
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            //retrieve the pending assists for this instructor
            service.AcknowledgeAssist(InstructorID, traineeIndex);


        }
        #endregion


        #region radio events

        /// <summary>
        /// Hier wordt de kleur van de radio knop weer teruggezet, alsmede de array
        /// </summary>
        private void SetRadioStatus()
        {

            //loop thrue the Monitortraineearray to set the proper status
            for (int i = 0; i <= 19; i++)
            {
                MonitorRadioArray[i] = false;
            }
        }


        private void btnMonitorRadio_Click(object sender, EventArgs e)
        {
            SetRadioStatus();
            if (!MonitorRadio)
            {
                MonitorRadio = true;
                btnMonitorRadio.BackColor = System.Drawing.Color.SaddleBrown;
                btnMonitorRadio.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                MonitorRadio = false;
                btnMonitorRadio.BackColor = System.Drawing.Color.Aqua;
                btnMonitorRadio.ForeColor = System.Drawing.Color.Black;
            }

        }

        /// <summary>
        /// determine the destination account when a radio call is started
        /// </summary>
        /// <param name="_radioNumber"></param>
        /// <returns></returns>
        private string GetDestination(int _radioNumber)
        {
            return "RADIO_" + ExersiseNumber + "_" + _radioNumber;// "1010";
        }


        private void btnRadio01_Click(object sender, EventArgs e)
        {
          //  SetRadioStatus();

         //   SetStatusAndColorRadioButtons((Button)sender);

         //   RadioClicked = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Radios), ((Button)sender).Name.Remove(0, 3)));
         //   ((Button)sender).BackColor = Color.Black;
         //   ((Button)sender).ForeColor = Color.White;

            int radioNumber = -1;
            try
            {
                StopNoise();
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
                string btnstate = ((Button)sender).Tag.ToString();
                switch (btnstate) //todo: weer werkend maken
                {
                    //3 zet de status   
                    case "Rx": //if it is 'Rx' then start a call
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsTx;
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Tx");
                            ((Button)sender).Tag = "Tx";
                            //mix noise into the conversation
                            InitNoiseLevel(radioNumber);
                            //Start the actual call
                            MakeCall(GetDestination(radioNumber), true, true, false, true, true, false);
                            break;
                        }
                    case "Off":
                    default:
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsRx;//we zetten hem 1 status HOGER dan de huidige status, en zitten dit in de singleton en op de hmi
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Rx");
                            ((Button)sender).Tag = "Rx";
                            break;
                        }
                    case "Tx":
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsOff;
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "OFF");
                            ((Button)sender).Tag = "Off";
                            break;
                        }
                }
                log.Info("Have set the status of the Radio: " + radioNumber + " to: " + state);
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                //set the new status to the unet_service
                service.SetRadioStatus(Convert.ToInt16(radioNumber), ucb.Radios[radioNumber - 1].State);
                //color the button(s) accordingly
                SetStatusAndColorRadioButtons((Button)sender);

           

       //         MakeCall("1010" + (Convert.ToInt16(radioNumber)), true, true, false, true, true, false);
            }
            catch (Exception ex)
            {
                log.Error("Error setting the status:" + radioNumber, ex);
                // throw;
            }
        }

        /// <summary>
        /// When the button  'monitor radio' is clicked and thereafter one of the radio buttons,
        /// this radio button must be set to brown, and a possible other trainee button must be set to the default color
        /// this generic code covers this for all all buttons at once
        /// </summary>
        /// <param name="_btn"></param>
        private void SetStatusAndColorRadioButtons(Button _btn)
        {
            if (MonitorRadio)
            {
                // the trainee buttons are named e.g.: btnRadioAA , we use this name, to find in the enum the index that is connected to this enum
                int radioIndex = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Radios), _btn.Name.Remove(0, 3)));
                MonitorRadioArray[radioIndex] = true;
                _btn.BackColor = System.Drawing.Color.SaddleBrown;
                _btn.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                //color should be green, described in paragraph 2.1.7
                _btn.BackColor = System.Drawing.Color.LimeGreen;
                _btn.ForeColor = System.Drawing.Color.Black;
            }
        }
        #endregion

        private void btnExersise01_Click(object sender, EventArgs e)
        {
         //   SetExerciseStatus();
            SetStatusAndColorExerciseButtons((Button)sender);
        }


        private void SetStatusAndColorExerciseButtons(Button _btn)
        {
            if (_btn.Name.ToLower() != "btnil")
            {
                ExersiseNumber = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Exercises), _btn.Name.Remove(0, 11)));
            }
            else
            {
                ExersiseNumber = 8; //btn 8 is the IL button
            }
            //Set the selected exercise, start with the array, then send this to the service
            ExerciseArray[ExersiseNumber] = true;
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            service.SetExerciseSelected(InstructorID, ExersiseNumber, true); //now we have told the service that this instructor has selected this exercise


        //    _btn.BackColor = Theming.ExerciseSelectedButton;
        }



        private void FrmUNETMain_FormClosing(object sender, FormClosingEventArgs e)
        {
//#if (!DEBUG)
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
//#endif
            try
            {
                //close the connection to the wcf service, if it is still opened
                if (service.State == System.ServiceModel.CommunicationState.Opened)
                {
                    service.UnRegisterClient(InstructorID, false);
                    service.Close();
                }
                //close the useragent en with that the sip connection
                if (!object.ReferenceEquals(useragent, null))
                {
                    HangupAllCalls();
                    useragent.UserAgentStop();

                    
            //try to find and kill the TCPSocketClient process and kill it
            FindAndKillProcess("TCPSocketClient.exe");
            log.Info("Terminated UNET_Instructor");
          }
                signal.DisposeSignalgenerator();
            }
            catch (Win32Exception winex)
            {
             
                    MessageBox.Show(this, winex.Message, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                
            }
            catch (Exception ex)
            {
                log.Error("Error Closing UNET_Trainer", ex);
                // throw;
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


        private void FrmUNETMain_Activated(object sender, EventArgs e)
        {
            //        SetButtonStatus(this);
        }
        #region CALL
 
        /// <summary>
        /// Make a call to PJSUA2 / Freeswitch
        /// <param name="_destination"></param>
        /// <param name="_inLeft"></param>
        /// <param name="_inRight"></param>
        /// <param name="_inSpeaker"></param>
        /// <param name="_outLeft"></param>
        /// <param name="_outRight"></param>
        /// <param name="_outSpeaker"></param>
        public void MakeCall(string _destination, bool _inMic, bool _inSecondMic, bool _inThirdMic, bool _outLeft, bool _outRight, bool _outSpeaker)
        {
            try
            {
                //hier worden de channels gekoppeld aan de call die wordt opgezet
                List<InputChannels> lstinputchannels = new List<InputChannels>();
                if (_inMic)
                    lstinputchannels.Add(InputChannels.ichMic);
                if (_inSecondMic)
                    lstinputchannels.Add(InputChannels.ichSecondMic);
                if (_inThirdMic)
                    lstinputchannels.Add(InputChannels.ichThirdMic);


                List<OutputChannels> lstoutputchannels = new List<OutputChannels>();
                if (_outLeft)
                    lstoutputchannels.Add(OutputChannels.ochLeft);
                if (_outRight)
                    lstoutputchannels.Add(OutputChannels.ochRight);
                if (_outSpeaker)
                    lstoutputchannels.Add(OutputChannels.ochSpeaker);

                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, ref lstinputchannels, ref lstoutputchannels);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;

                sc.makeCall(string.Format("sip:{0}@{1}", _destination, SIPServer), cop);
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
        public void HangupCall(string _destination)
        {

            try
            {
                log.Info("Hanging up call to: " + _destination);

                useragent.acc.removeCall(sc);

             }
            catch (Exception ex)
            {
                log.Error("Error making call to: " + _destination, ex);
                // throw;
            }

        }


        #endregion

        /// <summary>
        /// Intercom. The intercom sound must be on the left ear on the receiver's end
        /// zie usecase 3.1.3.4 in SRS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIntercom_Click(object sender, EventArgs e)
        {
            try
            {
                //1 zoek uit welke radio geklikt heeft
                string state = ((Button)sender).Text.Trim();

                 if (((Button)sender).Text.Trim().Length > 8)
                {
                    state = ((Button)sender).Text.Trim().Substring(((Button)sender).Text.Trim().Length - 2);
                }
                else
                {
                    state = "Rx"; //if the state is now 'TX', the next one will be 'OFF' and that is what we initially want
                }
                //2 zoek die op in de radios lijst in de conferencebridge
                if (((Button)sender).Tag == null)
                {
                    ((Button)sender).Tag = "Tx";
                }
                string btnstate = ((Button)sender).Tag.ToString();
                switch (btnstate) 
                {
                    //3 zet de status   
                    case "Rx":
                        {
                            ((Button)sender).Text = string.Format("Intercom {0}{1}", Environment.NewLine, "Tx");
                            ((Button)sender).Tag = "Tx";
                            MakeCall("10000", true, false, false, true, false, false);
                            break;
                        }
                    case "Off":
                    default:
                        {
                            ((Button)sender).Text = string.Format("Intercom {0}{1}", Environment.NewLine, "Rx");
                            ((Button)sender).Tag = "Rx";
                            break;
                        }
                    case "Tx":
                        {
                            ((Button)sender).Text = string.Format("Intercom {0}{1}", Environment.NewLine, "OFF");
                            ((Button)sender).Tag = "Off";
                            break;
                        }
                }
                log.Info("Have set the status of the Intercom to: " + state);



    //            MakeCall("10000", true, false, false, true, false, false);//intercom must go left
            }
            catch (Exception ex)
            {
                log.Error("Error setting the intercom status", ex);
                // throw;
            } 
        }

        private void btnAssist_Click(object sender, EventArgs e)
        {
            MakeCall( @"INTERCOM_CUB_" + InstructorID,  true, true, false, false, true, false);// interom must go left

        }

        private void btnRole09_Click(object sender, EventArgs e)
        {
            RoleClicked = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Roles), ((Button)sender).Name.Remove(0, 3)));
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;

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
            catch (Exception ex)
            {
                Console.Write("Exception hanging up calls: " + ex.Message);
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
                Console.WriteLine("Cannot find tcpclient " + ex.Message + ex.StackTrace);

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
             }
        }




        #endregion

        private void btnIL_Click(object sender, EventArgs e)
        {
            ILMode = !ILMode;
        }
    }
}
