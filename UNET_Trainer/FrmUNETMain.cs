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
using System.Media;

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
        public int InstructorID = Convert.ToInt16( ConfigurationManager.AppSettings["InstructorID"].ToString().Trim());
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer


        //the accounts
        private PJSUA2Implementation.SIP.UserAgent useragent;
        public string SIPServer = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
        public string SIPAccountname = ConfigurationManager.AppSettings["sipAccount"].ToString().Trim();
  
        UNET_ConferenceBridge.ConferenceBridge_Singleton ucb = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

        /// WCF service
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        //arrays that hold the statusses of the some
        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;
        public UNET_Classes.Exercise SelectedExercise;
        private int ExersiseIndex = -1;


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
            FrmRoles frm = new FrmRoles();
            frm.Show();
        }

        private void btnTrainees_Click(object sender, EventArgs e)
        {
            FrmTrainees frm = new FrmTrainees(ExersiseIndex + 1);
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

                //enable the Exercise buttons
                var resultlist = service.GetExercises();
                List<UNET_Classes.Exercise> lst = resultlist.ToList<UNET_Classes.Exercise>(); //C# v3 manier om een array in een list te krijgen

                foreach (UNET_Classes.Exercise exercise in lst)
                {
                   panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Enabled = true;
                }
        
                foreach(UNET_Classes.Exercise exercise in lst)
                {
                  // btnExersise01.Text = string.Format("Exercise {0}{1}{2}{3}{4}", exercise.Number, Environment.NewLine,  exercise.SpecificationName, Environment.NewLine, exercise.ExerciseName);
                    panelExercises.Controls["btnExersise" + exercise.Number.ToString("00")].Text = string.Format("Exercise {0}{1}{2}{3}{4}", exercise.Number, Environment.NewLine, exercise.SpecificationName, Environment.NewLine, exercise.ExerciseName);
                }                 
                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(panelExercises, lst.Count, "exersise");

                //enable the Roles buttons
                var rolelist = service.GetRoles();
                List<UNET_Classes.Role> lstrole = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen
                foreach (UNET_Classes.Role role in lstrole)
                {
                    panelRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                }
                UNET_Classes.Helpers.ResizeButtons(panelRoles, lstrole.Count, "role");


                //enable the Roles buttons
                var radiolist = service.GetRadios();
                List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
                foreach (UNET_Classes.Radio radio in lstRadio)
                {
                    panelRadios.Controls["btnRadio" + radio.ID.ToString("00")].Enabled = true;
                }

                 UNET_Classes.Helpers.ResizeButtons(panelRadios, lstRadio.Count, "radio");

                Application.DoEvents();
                //enable the Trainees buttons, for the number of trainees that are in
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen

                btnTraineeAA.Enabled = lstTrainee.Count >= 1;
                btnTraineeBB.Enabled = lstTrainee.Count >= 2;
                btnTraineeCC.Enabled = lstTrainee.Count >= 3;
                btnTraineeDD.Enabled = lstTrainee.Count >= 4;
                btnTraineeEE.Enabled = lstTrainee.Count >= 5;
                btnTraineeFF.Enabled = lstTrainee.Count >= 6;
                btnTraineeGG.Enabled = lstTrainee.Count >= 7;
                btnTraineeHH.Enabled = lstTrainee.Count >= 8;
                btnTraineeJJ.Enabled = lstTrainee.Count >= 9;
                btnTraineeKK.Enabled = lstTrainee.Count >= 10;
                btnTraineeLL.Enabled = lstTrainee.Count >= 11;
                btnTraineeMM.Enabled = lstTrainee.Count >= 12;
                btnTraineeNN.Enabled = lstTrainee.Count >= 13;
                btnTraineePP.Enabled = lstTrainee.Count >= 14;
                btnTraineeRR.Enabled = lstTrainee.Count >= 15;
                btnTraineeSS.Enabled = lstTrainee.Count >= 16;
                UNET_Classes.Helpers.ResizeButtonsVertical(panelTrainees, lstTrainee.Count, "trainee");
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
            the.SetFormSizeAndPosition(this);
   
            ShowIcon = false;
            ShowInTaskbar = false;

 

            ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one

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
         }     

        private void btnClassBroadcast_Click(object sender, EventArgs e)
        {
            FrmClassBroadcast frm = new FrmClassBroadcast();
            frm.Show();
        }

        private void btnRadios_Click(object sender, EventArgs e)
        {
            FrmRadioSetup frm = new FrmRadioSetup(ExersiseIndex +1);
            frm.Show();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (!Muted)
            {
                Muted = true;
                btnMute.BackColor = System.Drawing.Color.Red;
                btnMute.Text = "Muted";
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
            btnTraineeAA.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeBB.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeCC.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeDD.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeEE.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeFF.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeGG.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeHH.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeJJ.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeKK.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeLL.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeMM.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeNN.BackColor = System.Drawing.Color.LightGreen;
            btnTraineePP.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeRR.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeSS.BackColor = System.Drawing.Color.LightGreen;

            btnTraineeAA.ForeColor = System.Drawing.Color.Black;
            btnTraineeBB.ForeColor = System.Drawing.Color.Black;
            btnTraineeCC.ForeColor = System.Drawing.Color.Black;
            btnTraineeDD.ForeColor = System.Drawing.Color.Black;
            btnTraineeEE.ForeColor = System.Drawing.Color.Black;
            btnTraineeFF.ForeColor = System.Drawing.Color.Black;
            btnTraineeGG.ForeColor = System.Drawing.Color.Black;
            btnTraineeHH.ForeColor = System.Drawing.Color.Black;
            btnTraineeJJ.ForeColor = System.Drawing.Color.Black;
            btnTraineeKK.ForeColor = System.Drawing.Color.Black;
            btnTraineeLL.ForeColor = System.Drawing.Color.Black;
            btnTraineeMM.ForeColor = System.Drawing.Color.Black;
            btnTraineeNN.ForeColor = System.Drawing.Color.Black;
            btnTraineePP.ForeColor = System.Drawing.Color.Black;
            btnTraineeRR.ForeColor = System.Drawing.Color.Black;
            btnTraineeSS.ForeColor = System.Drawing.Color.Black;

        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();
            int traineeIndex = -1;
            //    SetStatusAndColorTraineeButtons((Button)sender);
           traineeIndex = (int)(Enum.Parse(typeof(Enums.Trainees), ((Button)sender).Name.Remove(0, 3)));
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


            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }

            //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
            service.SetTraineeAssignedStatus(InstructorID, ExersiseIndex, traineeIndex, true );
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
                int radioIndex = (int)(Enum.Parse(typeof(Enums.Radios), _btn.Name.Remove(0, 3)));
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
                ExersiseIndex = (int)(Enum.Parse(typeof(Enums.Exercises), _btn.Name.Remove(0, 11)));
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
            }
            catch (Exception ex)
            {
                log.Error("Error Closing UNET_Trainer", ex);
                // throw;
            }
        }

        private void FrmUNETMain_Activated(object sender, EventArgs e)
        {
    //        SetButtonStatus(this);
        }

        private void MakeCall(string _sipAccount)
        {
            //first try to find it in the list of active calls
            bool found = false;


            foreach(pjsua2.Call call in useragent.acc.Calls)
            {
            //    if(call.)
            }
            foreach (PJSUA2Implementation.SIP.SIPCall sipcall in ucb.ActiveCalls)
            {
              //  if(sipcall.CallID ==)
            }
            if (!found)
            {
                try
                {
                    PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc);
                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.makeCall(string.Format("sip:{0}@{1}", SIPAccountname, SIPServer), cop);
                    //voeg de call toe aan de activecall lijst
                    ucb.ActiveCalls.Add(sc);
                }
                catch (Exception ex)
                {
                    log.Error("Error updating screen controls", ex);
                    // throw;
                }
            }
        }


        private void MakeCall(int traineeid)
        {
            try
            {
                PJSUA2Implementation.SIP.SIPCall sc = new PJSUA2Implementation.SIP.SIPCall(useragent.acc, InstructorID);
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

    }
}
