using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using log4net;
using pjsua2;
using PJSUA2Implementation.SIP;
using System.Threading;


namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETMain : FrmUNETbase
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string SIPServer = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
        public string SIPAccountname = ConfigurationManager.AppSettings["sipAccount"].ToString().Trim();
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();


        //pjsua2
        //  public Endpoint endpoint;
        //  public EpConfig ep_cfg;
        //  public AccountConfig acfg = new AccountConfig();
        // public TransportConfig tcfg = new TransportConfig();
        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;

        //  private Boolean Muted = false;
        //  private Boolean MonitorTrainee = false;
        //  private Boolean MonitorRadio = false;
        public int TraineeID = 1;

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
            this.Close();
        }

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            this.Text = "UNET Trainee";
            //todo: terugzetten   timer1.Enabled = true;

            ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            TraineeID = Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);

            try
            {
                //the useragent holds everything needed for the sip communication
                useragent = new PJSUA2Implementation.SIP.UserAgent();
                useragent.UserAgentStart();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Error creating accounts " + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetButtonStatus(this);

            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    //enable the Exercise buttons
                    var currentInfo = service.GetExerciseInfo(TraineeID);
                    if (currentInfo != null)
                    {
                        lblPlatform.Text = currentInfo.Platform;
                        lblConsole.Text = currentInfo.ConsoleRole;
                        lblExerciseMode.Text = currentInfo.ExerciseMode;
                        lblExerciseName.Text = currentInfo.ExerciseName;
                    }
                    else
                    {
                        //TODO: HIER IETS ZINVOLS VERZINNEN  Console.Write("The service.getexerciseinfo object is empty!!!");
                    }

                    service.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
                // throw;
            }

            SetButtonStatus(pnlPointToPoint);
            SetButtonStatus(pnlRadios);
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
                // visible/invisible and also set the statusled
                //  {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

             
                //enable the Roles buttons
                var rolelist = service.GetRoles();
                List<Classes.Role> lstrole = rolelist.ToList<Classes.Role>(); //C# v3 manier om een array in een list te krijgen

                btnRole1.Enabled = lstrole.Count >= 1;
                btnRole2.Enabled = lstrole.Count >= 2;
                btnRole3.Enabled = lstrole.Count >= 3; 
                btnRole4.Enabled = lstrole.Count >= 4;
                btnRole5.Enabled = lstrole.Count >= 5;
                btnRole6.Enabled = lstrole.Count >= 6;
                btnRole7.Enabled = lstrole.Count >= 7;
                btnRole8.Enabled = lstrole.Count >= 8;
                btnRole9.Enabled = lstrole.Count >= 9;
                btnRole10.Enabled = lstrole.Count >= 10;
                btnRole11.Enabled = lstrole.Count >= 11;
                btnRole12.Enabled = lstrole.Count >= 12;
                btnRole13.Enabled = lstrole.Count >= 13;
                btnRole14.Enabled = lstrole.Count >= 14;
                btnRole15.Enabled = lstrole.Count >= 15;
                btnRole16.Enabled = lstrole.Count >= 16;
                btnRole17.Enabled = lstrole.Count >= 17;
                btnRole18.Enabled = lstrole.Count >= 18;
                btnRole19.Enabled = lstrole.Count >= 19;
                btnRole20.Enabled = lstrole.Count >= 20;

                //enable the Roles buttons
                var radiolist = service.GetRadios();
                List<Classes.Radio> lstRadio = radiolist.ToList<Classes.Radio>(); //C# v3 manier om een array in een list te krijgen

                btnRadio01.Enabled = lstRadio.Count >= 1;
                btnRadio02.Enabled = lstRadio.Count >= 2;
                btnRadio03.Enabled = lstRadio.Count >= 3;
                btnRadio04.Enabled = lstRadio.Count >= 4;
                btnRadio05.Enabled = lstRadio.Count >= 5;
                //now resize all buttons to make optimal use of the available room
                ResizeButtons(pnlRadios, lstRadio.Count, "radio");
                ResizeButtons(pnlPointToPoint, lstrole.Count, "role");
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
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
                useragent.UserAgentStop();
            }
            //close the connection to the wcf service, if it is still opened
            if (service.State == System.ServiceModel.CommunicationState.Opened)
            {
                service.Close();
            }
        }

        private void btnRadio01_Click(object sender, EventArgs e)
        {
            MakeCall(1);
        }

        private void MakeCall(int traineeid)
        {
            try
            {
                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, TraineeID);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                sc.makeCall(string.Format("sip:{0}@{1}",SIPAccountname, SIPServer ), cop);
            }
            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
                // throw;
            }
        }
    }
}