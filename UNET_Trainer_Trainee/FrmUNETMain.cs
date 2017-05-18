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
using System.Collections.Specialized;
using log4net;
using pjsua2;
using System.Threading;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETMain : FrmUNETbase
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //pjsua2
        public Endpoint endpoint;
        public EpConfig ep_cfg;
        public AccountConfig acfg = new AccountConfig();
        public TransportConfig tcfg = new TransportConfig();
        //the accounts
        pjsua2.Account account;
        SIP.SipAccount saccount;


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
         //   timer1.Enabled = true;

            ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            TraineeID = Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);

            endpoint = new Endpoint();
            endpoint.libCreate();
          //  endpoint.libRegisterWorkerThread("UNETthread");
            endpoint.libRegisterThread("UNETthread");

            ep_cfg = new EpConfig();
            ep_cfg.logConfig.level = Convert.ToUInt16(ConfigurationManager.AppSettings["LogLevel"]);
            ep_cfg.uaConfig.maxCalls = Convert.ToUInt16(ConfigurationManager.AppSettings["maxcalls"]);
            ep_cfg.medConfig.sndClockRate = Convert.ToUInt16(ConfigurationManager.AppSettings["sndClockRate"]);
            //todo: hier kunnen nog 1001 params geconfigureerd worden
            
            //Initialize endpoint
            endpoint.libInit(ep_cfg);
             //Create sip transport and errorhandling
            tcfg.port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
           
            try
            {
                endpoint.transportCreate(pjsua2.pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);
                SIP.UserAgent useragent = new SIP.UserAgent();
                useragent.UserAgentStart(endpoint);
             
                //Configure an AccountConfig (zie pagina 43 pjsua2doc.pdf)
              //  acfg.idUri = string.Format("sip:{0}@unet", ConfigurationManager.AppSettings["sipAccount"]);
              //  acfg.regConfig.registrarUri = string.Format("sip:{0}", ConfigurationManager.AppSettings["sipServer"]);

              //  AuthCredInfo cred = new AuthCredInfo("digest", "*", ConfigurationManager.AppSettings["sipAccount"], 0, "1234");
                
              //  acfg.sipConfig.authCreds.Add(cred);
              //  acfg.regConfig.registerOnAdd = true;             
                // account = new Account();
                // account.create(acfg);
             //   saccount = new SIP.SipAccount();
             //   SIP.SipBuddy buddy = new SIP.SipBuddy("1025@unet", "unet", saccount);
             //   saccount.addBuddy(buddy );         
             //   saccount.setDefault();
              //   PresenceStatus ps = new PresenceStatus();
              //   ps.status = pjsua_buddy_status.PJSUA_BUDDY_STATUS_ONLINE;
              //   saccount.setOnlineStatus(ps);
              //   saccount.setRegistration(true);
                
             //   saccount.create(acfg,true);
             //   Thread.Sleep(500);
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
                else
                {
                    SetButtonStatus(c);
                }
            }


        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            FrmAudio frm = new FrmAudio();
            frm.Show();
        }

        private void FrmUNETMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///this code destroys the SIP connection and clears the relevant objects
            try
            {
                //dispose all sip objects, so they can be garbage collected
                if (!object.ReferenceEquals(saccount, null))
                {
                    saccount.Dispose();
                }
              //  account.Dispose();

                endpoint.libDestroy();
                endpoint.Dispose();

                //force garbage collection of all disposed objects
                GC.Collect();
            }
            catch (Exception ex)
            {
                log.Error("Error UN-registering SIP connection", ex);
                // throw;
            }
        }
    }
}