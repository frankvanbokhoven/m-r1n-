using System;
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

namespace UNET_Trainer
{

    public partial class FrmUNETMain : Form // FrmUNETbase
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();


        private Boolean Muted = false;
        private Boolean MonitorTrainee = false;
        private Boolean MonitorRadio = false;
        public int InstructorID = Convert.ToInt16(RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1015"));

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
        private int ExersiseIndex = -1;
        private int RoleClicked = -1;
        private int RadioClicked = -1;


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
            Application.Exit();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FrmRoles frm = new FrmRoles(ExersiseIndex, InstructorID);
            frm.Show();
        }

        private void btnTrainees_Click(object sender, EventArgs e)
        {
            FrmTrainees frm = new FrmTrainees(ExersiseIndex, InstructorID);
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
                SetButtonStatus(this);
            }
        }

        #region Noise

        private UNET_SignalGenerator.SignalGeneratorController signal = new SignalGeneratorController();


        private void InitNoiseLevel()
        {
            int SelectedRadioButtonIndex = 1;// Convert.ToInt16(Regex.Replace(_btn.Name, "[^0-9.]", "")); //haal het indexnummer op van de button
            int noiselevel = service.GetNoiseLevel(SelectedRadioButtonIndex);
            signal.NoiseLevel = noiselevel;
            signal.Start();
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

        #region Status setters

        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatus(Control parent)
        {
            //  first the trainees, we assume the name of the button component is the key for the function

            //this loop sets the color of the status led
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

                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("radio")))
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
                else
                {
                    //  SetButtonStatus(c);
                }
                Application.DoEvents();
            }

            try
            {
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                // visible/invisible and also set the statusled
                //  {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                //we moeten  de huidige status ophalen van de instructeur/exercises/trainee/roles/radios
                //en hiermee de knoppen de juiste kleur geven
                Instructor currentInstructor = service.GetAllInstructorData(InstructorID);

                //enable the Exercise buttons
                var resultlist = service.GetExercises();
                List<UNET_Classes.Exercise> lst = resultlist.ToList<UNET_Classes.Exercise>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelExercises.Controls) //first DISABLE all buttons
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.Extinguished;
                    }
                }
                int exerciseselected = -1;
                foreach (UNET_Classes.Exercise exercise in lst) //then ENABLE them, based on whatever comes from the service
                {
                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Enabled = true; //exercises worden altijd visible, want moeten altijd gekozen kunnen worden, vooral initieel
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
                                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].BackColor = Theming.ExerciseSelectedButton;
                                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Tag = "enable";
                                    exerciseselected = exercise.Number;
                                }
                            }
                        }
                    }
                }
                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(panelExercises, lst.Count, "exersise");


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
                        ((Button)ctrl).BackColor = Theming.Extinguished;

                    }
                }
                foreach (UNET_Classes.Trainee trainee in lstTrainee)
                {
                    //  panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                    panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text = string.Format("Trainee {0}{1}{2}{3}Role:{4}", trainee.ID, Environment.NewLine, trainee.Name, Environment.NewLine, "TraineeRole");


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
                var rolelist = service.GetRoles();
                List<UNET_Classes.Role> lstrole = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelRoles.Controls)
                {
                    if (((ctrl.GetType() == typeof(System.Windows.Forms.Button)) && ((Button)ctrl).Name != "btnClose"))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.Extinguished;

                    }
                }
                foreach (UNET_Classes.Role role in lstrole)
                {
                    //     panelRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
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
                UNET_Classes.Helpers.ResizeButtons(panelRoles, lstrole.Count, "role");


                //enable the Roles buttons
                var radiolist = service.GetRadios();

                List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelRadios.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.Extinguished;

                    }
                }
                foreach (UNET_Classes.Radio radio in lstRadio)
                {
                 //   panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Enabled = true;
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
                UNET_Classes.Helpers.ResizeButtons(panelRadios, lstRadio.Count, "radio");

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }

        #endregion

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

            ///
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            service.StartService();


            ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            try
            {
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

              //  sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
                cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;


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
            FrmRadioSetup frm = new FrmRadioSetup(ExersiseIndex + 1);
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

        private void btnMonitorTrainee_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();
            if (!MonitorTrainee)
            {
                MonitorTrainee = true;
                btnMonitorTrainee.BackColor = System.Drawing.Color.SaddleBrown;
                btnMonitorTrainee.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                MonitorTrainee = false;
                btnMonitorTrainee.BackColor = System.Drawing.Color.Aqua;
                btnMonitorTrainee.ForeColor = System.Drawing.Color.Black;
            }
        }

        #region trainee events
        /// <summary>
        /// Hier wordt de kleur van de trainee knop weer teruggezet, alsmede de array
        /// </summary>
        private void SetTraineeStatus()
        {

            //loop thrue the Monitortraineearray to set the proper status
            for (int i = 0; i <= 15; i++)
            {
                MonitorTraineeArray[i] = false;
            }

            // A little amateur.. but it just is the fastest manner
            btnTrainee01.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee02.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee03.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee04.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee05.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee06.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee07.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee08.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee09.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee10.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee11.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee12.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee13.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee14.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee15.BackColor = System.Drawing.Color.LightGreen;
            btnTrainee16.BackColor = System.Drawing.Color.LightGreen;

            btnTrainee01.ForeColor = System.Drawing.Color.Black;
            btnTrainee02.ForeColor = System.Drawing.Color.Black;
            btnTrainee03.ForeColor = System.Drawing.Color.Black;
            btnTrainee04.ForeColor = System.Drawing.Color.Black;
            btnTrainee05.ForeColor = System.Drawing.Color.Black;
            btnTrainee06.ForeColor = System.Drawing.Color.Black;
            btnTrainee07.ForeColor = System.Drawing.Color.Black;
            btnTrainee08.ForeColor = System.Drawing.Color.Black;
            btnTrainee09.ForeColor = System.Drawing.Color.Black;
            btnTrainee10.ForeColor = System.Drawing.Color.Black;
            btnTrainee11.ForeColor = System.Drawing.Color.Black;
            btnTrainee12.ForeColor = System.Drawing.Color.Black;
            btnTrainee13.ForeColor = System.Drawing.Color.Black;
            btnTrainee14.ForeColor = System.Drawing.Color.Black;
            btnTrainee15.ForeColor = System.Drawing.Color.Black;
            btnTrainee16.ForeColor = System.Drawing.Color.Black;

        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();
            int traineeIndex = -1;
            //    SetStatusAndColorTraineeButtons((Button)sender);

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

            // A little amateur.. but it just is the fastest manner
            btnRadio01.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio02.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio03.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio04.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio05.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio06.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio07.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio08.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio09.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio10.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio11.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio12.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio13.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio14.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio15.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio16.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio17.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio18.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio19.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio20.BackColor = System.Drawing.Color.DarkSeaGreen;

            btnRadio01.ForeColor = System.Drawing.Color.Black;
            btnRadio02.ForeColor = System.Drawing.Color.Black;
            btnRadio03.ForeColor = System.Drawing.Color.Black;
            btnRadio04.ForeColor = System.Drawing.Color.Black;
            btnRadio05.ForeColor = System.Drawing.Color.Black;
            btnRadio06.ForeColor = System.Drawing.Color.Black;
            btnRadio07.ForeColor = System.Drawing.Color.Black;
            btnRadio08.ForeColor = System.Drawing.Color.Black;
            btnRadio09.ForeColor = System.Drawing.Color.Black;
            btnRadio10.ForeColor = System.Drawing.Color.Black;
            btnRadio11.ForeColor = System.Drawing.Color.Black;
            btnRadio12.ForeColor = System.Drawing.Color.Black;
            btnRadio13.ForeColor = System.Drawing.Color.Black;
            btnRadio14.ForeColor = System.Drawing.Color.Black;
            btnRadio15.ForeColor = System.Drawing.Color.Black;
            btnRadio16.ForeColor = System.Drawing.Color.Black;
            btnRadio17.ForeColor = System.Drawing.Color.Black;
            btnRadio18.ForeColor = System.Drawing.Color.Black;
            btnRadio19.ForeColor = System.Drawing.Color.Black;
            btnRadio20.ForeColor = System.Drawing.Color.Black;
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


        private void btnRadio01_Click(object sender, EventArgs e)
        {
            SetRadioStatus();

            SetStatusAndColorRadioButtons((Button)sender);

            RadioClicked = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Radios), ((Button)sender).Name.Remove(0, 3)));
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;

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
            SetExerciseStatus();
            SetStatusAndColorExerciseButtons((Button)sender);
        }

        private void SetExerciseStatus()
        {
            //loop thrue the Exersise array to set the proper status
            for (int i = 0; i <= 8; i++)
            {
                ExerciseArray[i] = false;
            }

            //reset the colors of the exersise buttons
            btnExersise01.BackColor = System.Drawing.Color.Aqua;
            btnExersise02.BackColor = System.Drawing.Color.Aqua;
            btnExersise03.BackColor = System.Drawing.Color.Aqua;
            btnExersise04.BackColor = System.Drawing.Color.Aqua;
            btnExersise05.BackColor = System.Drawing.Color.Aqua;
            btnExersise06.BackColor = System.Drawing.Color.Aqua;
            btnExersise07.BackColor = System.Drawing.Color.Aqua;
            btnExersise08.BackColor = System.Drawing.Color.Aqua;
            btnIL.BackColor = System.Drawing.Color.Aqua;

            btnExersise01.ForeColor = System.Drawing.Color.Black;
            btnExersise02.ForeColor = System.Drawing.Color.Black;
            btnExersise03.ForeColor = System.Drawing.Color.Black;
            btnExersise04.ForeColor = System.Drawing.Color.Black;
            btnExersise05.ForeColor = System.Drawing.Color.Black;
            btnExersise06.ForeColor = System.Drawing.Color.Black;
            btnExersise07.ForeColor = System.Drawing.Color.Black;
            btnExersise08.ForeColor = System.Drawing.Color.Black;
            btnIL.ForeColor = System.Drawing.Color.Black;
        }

        private void SetStatusAndColorExerciseButtons(Button _btn)
        {
            // the trainee buttons are named e.g.: btnRadioAA , we use this name, to find in the enum the index that is connected to this enum
            if (_btn.Name.ToLower() != "btnil")
            {
                ExersiseIndex = (int)(Enum.Parse(typeof(UNET_Classes.Enums.Exercises), _btn.Name.Remove(0, 11)));
            }
            else
            {
                ExersiseIndex = 8;
            }
            //Set the selected exercise, start with the array, then send this to the service
            ExerciseArray[ExersiseIndex] = true;
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            service.SetExerciseSelected(InstructorID, ExersiseIndex, true); //now we have told the service that this instructor has selected this exercise


            _btn.BackColor = System.Drawing.Color.SaddleBrown;
            _btn.ForeColor = System.Drawing.Color.White;
        }



        private void FrmUNETMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //close the connection to the wcf service, if it is still opened
                if (service.State == System.ServiceModel.CommunicationState.Opened)
                {
                    service.Close();
                }
                //stop the sip connection in a nice manner before closing
                useragent.ep.hangupAllCalls();
                useragent.UserAgentStop();
            }
            catch (Exception ex)
            {
                log.Error("Error Closing UNET_Trainer", ex);
                // throw;
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


        private void FrmUNETMain_Activated(object sender, EventArgs e)
        {
            //        SetButtonStatus(this);
        }
        #region CALL

 
        /// <summary>
        /// Make a call to Freeswich using PJSUA2
        /// </summary>
        /// <param name="traineeid"></param>
        /// <param name="_destination"></param>
        public void MakeCall(string _destination)
        {
            try
            {
                log.Info("Making call to: " + _destination);
          //      PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
          //      CallOpParam cop = new CallOpParam();
          //      cop.statusCode = pjsip_status_code.PJSIP_SC_OK;

                sc.makeCall(string.Format("sip:{0}@{1}", _destination, SIPServer), cop);
            }
            catch (Exception ex)
            {
                log.Error("Error making call to: " + _destination, ex);
                // throw;
            }
        }

        /// <summary>
        /// Make a call to PJSUA2 / Freeswitch
        /// <param name="_destination"></param>
        /// <param name="_inLeft"></param>
        /// <param name="_inRight"></param>
        /// <param name="_inSpeaker"></param>
        /// <param name="_outLeft"></param>
        /// <param name="_outRight"></param>
        /// <param name="_outSpeaker"></param>
        private void MakeCall(string _destination, bool _inLeft, bool _inRight, bool _inSpeaker, bool _outLeft, bool _outRight, bool _outSpeaker)
        {
            try
            {
                //hier worden de channels gekoppeld aan de call die wordt opgezet
                List<InputChannels> lstinputchannels = new List<InputChannels>();
                if (_inLeft)
                    lstinputchannels.Add(InputChannels.ichLeft);
                if (_inRight)
                    lstinputchannels.Add(InputChannels.ichRight);
                if (_inSpeaker)
                    lstinputchannels.Add(InputChannels.ichSpeaker);


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

              //  PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
             //   CallOpParam cop = new CallOpParam();
             //   cop.statusCode = pjsip_status_code.PJSIP_SC_OK;

             //   sc.makeCall(string.Format("sip:{0}@{1}", _destination, SIPServer), cop);
            }
            catch (Exception ex)
            {
                log.Error("Error making call to: " + _destination, ex);
                // throw;
            }

        }


        #endregion

        private void btnIntercom_Click(object sender, EventArgs e)
        {
            MakeCall( @"INTERCOM_CUB_" + InstructorID, true, true, false, false, true, false);//intercom must go left

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

    }
}
