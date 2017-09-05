using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using pjsua2;
using UNET_Classes;
using System.Drawing;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETMain :  Form//FrmUNETbase
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;
        public int TraineeID = 1;
        public string SIPServer = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
        public string SIPAccountname = ConfigurationManager.AppSettings["sipAccount"].ToString().Trim();
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        UNET_ConferenceBridge.ConferenceBridge_Singleton ucb = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer


        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;
     //   private object rolelist;

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
            timer1.Enabled = true;
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
                MessageBox.Show(ex.Message + " cannot continue. " + Environment.NewLine +
                    ex.InnerException + Environment.NewLine +
                    "Contact your system administrator");
                log.Error("Error creating accounts " + ex.Message);
                this.Close();
            }

            // Set the text displayed in the caption.
            this.Text = "UNET";
            this.BackColor = Color.White;
            // Set the opacity to 75%.
            this.Opacity = 1;
            // Size the form to be 300 pixels in height and width.
            this.Size = new Size(800, 600);
            // Display the form in the center of the screen.
            // this.StartPosition = FormStartPosition.
            //   SetFormSizeAndPosition();
            this.Top = 0;
            this.Left = 0;
            this.Height = 600;
            this.Width = 800;
            SetTheme(Theme, this);
        }

        private void timer1_Tick(object sender, EventArgs e)
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
                List<UNET_Classes.Role> rolelist = service.GetRoles().ToList<UNET_Classes.Role>();// service.GetRoles().Cast<Role>();

                UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

                //todo!!! er zit een groot verschil tussen de instructor en trainee client; de eerste gebruikt de classes van de SERVICE
                //in plaats van de classes in de eigen classes directory. In deze trainee, de .tolist werkt niet. daarom is deze linq cast gebruikt
                //zie: https://stackoverflow.com/questions/4922129/how-do-i-convert-an-array-to-a-listobject-in-c
                //  List<UNET_Classes.Role> lstrole = rolelist.Cast<UNET_Classes.Role>().ToList(); //C# v3 manier om een array in een list te krijgen
                conference.Roles = rolelist.ToList<Role>(); //C# v3 manier om een array in een list te krijgen

                //  List<Role> lstrole = rolelist.ToList<Role>(); //C# v3 manier om een array in een list te krijgen

                btnRole1.Enabled = conference.Roles.Count >= 1;
                btnRole2.Enabled = conference.Roles.Count >= 2;
                btnRole3.Enabled = conference.Roles.Count >= 3;
                btnRole4.Enabled = conference.Roles.Count >= 4;
                btnRole5.Enabled = conference.Roles.Count >= 5;
                btnRole6.Enabled = conference.Roles.Count >= 6;
                btnRole7.Enabled = conference.Roles.Count >= 7;
                btnRole8.Enabled = conference.Roles.Count >= 8;
                btnRole9.Enabled = conference.Roles.Count >= 9;
                btnRole10.Enabled = conference.Roles.Count >= 10;
                btnRole11.Enabled = conference.Roles.Count >= 11;
                btnRole12.Enabled = conference.Roles.Count >= 12;
                btnRole13.Enabled = conference.Roles.Count >= 13;
                btnRole14.Enabled = conference.Roles.Count >= 14;
                btnRole15.Enabled = conference.Roles.Count >= 15;
                btnRole16.Enabled = conference.Roles.Count >= 16;
                btnRole17.Enabled = conference.Roles.Count >= 17;
                btnRole18.Enabled = conference.Roles.Count >= 18;
                btnRole19.Enabled = conference.Roles.Count >= 19;
                btnRole20.Enabled = conference.Roles.Count >= 20;

                ////enable the Roles buttons
                var radiolist = service.GetRadios();
                //todo!!! er zit een groot verschil tussen de instructor en trainee client; de eerste gebruikt de classes van de SERVICE
                //in plaats van de classes in de eigen classes directory. In deze trainee, de .tolist werkt niet. daarom is deze linq cast gebruikt
                //zie: https://stackoverflow.com/questions/4922129/how-do-i-convert-an-array-to-a-listobject-in-c
                conference.Radios  = radiolist.Cast<UNET_Classes.Radio>().ToList(); 
                btnRadio01.Enabled = conference.Radios.Count >= 1;
                btnRadio02.Enabled = conference.Radios.Count >= 2;
                btnRadio03.Enabled = conference.Radios.Count >= 3;
                btnRadio04.Enabled = conference.Radios.Count >= 4;
                btnRadio05.Enabled = conference.Radios.Count >= 5;


                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical (pnlRadios, conference.Radios.Count, "radio");
                UNET_Classes.Helpers.ResizeButtonsVertical(pnlPointToPoint, conference.Roles.Count, "role");
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
                //  switch(ucb.Radios[radioid].State)
                switch (state)
                {
                    //3 zet de status   
                    // case UNETRadioState.rsRx.ToString():
                    case "Rx":
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsTx;
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Tx");
                            MakeCall(TraineeID);
                            break;
                        }
                    //    case UNETRadioState.rsOff:
                    case "Off":
                    default:
                        {
                            ucb.Radios[radioNumber - 1].State = UNETRadioState.rsRx;//we zetten hem 1 status HOGER dan de huidige status, en zitten dit in de singleton en op de hmi
                            ((Button)sender).Text = string.Format("Radio {0}{1}{2}", radioNumber, Environment.NewLine, "Rx");
                            break;
                        }

                    //   case UNETRadioState.rsTx:
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



                MakeCall(Convert.ToInt16(radioNumber));
            }
            catch (Exception ex)
            {
                log.Error("Error setting the status:" + radioNumber , ex);
                // throw;
            }

        }


        #region CALL
        private void MakeCall(int traineeid)
        {
            try
            {
                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, TraineeID);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                sc.makeCall(string.Format("sip:{0}@{1}", SIPAccountname, SIPServer), cop);
            }
            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
                // throw;
            }
        }
        #endregion

        #region theme

        /// <summary>
        /// Set the colors of the
        /// </summary>
        /// <param name="_theme"></param>
        protected void SetTheme(UNETTheme _theme, Control _parent)
        {
            //we willen de parent ZELF ook themen als het een form is..
            if (_parent.GetType().BaseType.BaseType == typeof(System.Windows.Forms.Form))
            {
                ((Form)_parent).ForeColor = Color.White;
                ((Form)_parent).BackColor = Color.DimGray;
            }

            //loop thrue the controls of the parent
            foreach (Control ctrl in _parent.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.Form))
                {
                    ((Form)ctrl).ForeColor = Color.White;
                    ((Form)ctrl).BackColor = Color.DimGray;
                }
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    ((GroupBox)ctrl).ForeColor = Color.White;
                    ((GroupBox)ctrl).BackColor = Color.Gray;
                }



                if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (((Button)ctrl).Name.ToLower().Contains("radio"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.DarkKhaki;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("close"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Red;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("trainee"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Peru;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("exersise"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.LimeGreen;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("role"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("noise"))
                    {
                        ((Button)ctrl).ForeColor = Color.White;
                        ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }

                    if (((Button)ctrl).Name.ToLower().Contains("audio") ||
                         ((Button)ctrl).Name.ToLower().Contains("assist") ||
                         ((Button)ctrl).Name.ToLower().Contains("intercom"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Gray;
                    }

                    //   else
                    //   {
                    //       ((Button)ctrl).ForeColor = Color.White;
                    //       ((Button)ctrl).BackColor = Color.DimGray;
                    //   }
                }
                SetTheme(_theme, ctrl);
            }
        }
        #endregion
    }
}