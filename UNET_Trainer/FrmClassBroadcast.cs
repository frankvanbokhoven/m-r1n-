using pjsua2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Classes;
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmClassBroadcast : Form// FrmUNETbaseSub
    {
        private bool AllTraineesSelected = false;
        private bool AllInstructorsSelected = false;
        private bool EveryoneSelected = false;
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();
        private DateTime LastUpdate = DateTime.MinValue;

        public FrmUNETMain frmmain = null;
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        public FrmClassBroadcast()
        {
            InitializeComponent();
            pnlInstructors.Paint += UC_Paint;
            pnlTrainees.Paint += UC_Paint;
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

        private void FrmClassBroadcast_Load(object sender, EventArgs e)
        {
            this.Text = "Class broadcast";

            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.SetFormSizeAndPosition(this);

            timer1.Enabled = true;
            InitState();

        }


        /// <summary>
        /// Init this screen as described in 2.2.1
        /// </summary>
        private void InitState()
        {
            // A little amateur.. but it just is the fastest manner
            btnTrainee01.BackColor = Constants.cExtinguised;
            btnTrainee02.BackColor = Constants.cExtinguised;
            btnTrainee03.BackColor = Constants.cExtinguised;
            btnTrainee04.BackColor = Constants.cExtinguised;
            btnTrainee05.BackColor = Constants.cExtinguised;
            btnTrainee06.BackColor = Constants.cExtinguised;
            btnTrainee07.BackColor = Constants.cExtinguised;
            btnTrainee08.BackColor = Constants.cExtinguised;
            btnTrainee09.BackColor = Constants.cExtinguised;
            btnTrainee10.BackColor = Constants.cExtinguised;
            btnTrainee11.BackColor = Constants.cExtinguised;
            btnTrainee12.BackColor = Constants.cExtinguised;
            btnTrainee13.BackColor = Constants.cExtinguised;
            btnTrainee14.BackColor = Constants.cExtinguised;
            btnTrainee15.BackColor = Constants.cExtinguised;
            btnTrainee16.BackColor = Constants.cExtinguised;

            btnInstructor01.BackColor = Constants.cExtinguised;
            btnInstructor02.BackColor = Constants.cExtinguised;
            btnInstructor03.BackColor = Constants.cExtinguised;
            btnInstructor04.BackColor = Constants.cExtinguised;

            btnDeSelect.BackColor = Constants.cExtinguised;
            btnDeSelect.ForeColor = Constants.cFontNotSelected;
            btnBroadcast.BackColor = Constants.cExtinguised;
            btnBroadcast.ForeColor = Constants.cFontNotSelected;
            btnSelectAllInstructors.BackColor = Constants.cExtinguised;
            btnSelectAllInstructors.ForeColor = Constants.cFontNotSelected;
            btnSelectAllPositions.BackColor = Constants.cExtinguised;
            btnSelectAllPositions.ForeColor = Constants.cFontNotSelected;
            btnSelectAllTrainees.BackColor = Constants.cExtinguised;
            btnSelectAllTrainees.ForeColor = Constants.cFontNotSelected;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                //  SetButtonStatus(this);
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                if (service.GetPendingChanges() > LastUpdate) //only if the last-changed-datetime on the server is more recent than on the client, we need to update
                {

                    SetButtonStatus(this);
                    LastUpdate = DateTime.Now; //todo: eigenlijk moet hier het resultaat van GetPendingchanges in komen
                }

            }
        }

        private void FrmClassBroadcast_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();
        }

        #region Show radio buttons

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
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in pnlTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }
                int listindex = 1;

                //now we make visible a button for every existing trainee
                foreach (UNET_Classes.Trainee trainee in lstTrainee)
                {
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text = string.Format("Trainee {0}{1}{2}", trainee.ID, Environment.NewLine, trainee.Name);
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].BackColor = Theming.TraineeNotSelectedButton;
                    listindex++;
                }

                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(pnlTrainees, lstTrainee.Count, "trainee");

                var instructorlist = service.GetInstructors();
                var resultlist = service.GetInstructors();
                List<UNET_Classes.Instructor> lstInstructor = instructorlist.ToList<UNET_Classes.Instructor>();
                foreach (Control ctrl in pnlInstructors.Controls) //first DISABLE all buttons
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((Button)ctrl).BackColor = Theming.Background;
                    }
                }


                listindex = 1;

                //now we make visible a button for every existing role
                foreach (UNET_Classes.Instructor inst in lstInstructor)
                {
                    pnlInstructors.Controls["btnInstructor" + listindex.ToString("00")].Text = string.Format("Instructor {0}{1}{2}", inst.ID, Environment.NewLine, inst.Name);
                    pnlInstructors.Controls["btnInstructor" + listindex.ToString("00")].Enabled = true;
                    listindex++;
                }

                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(pnlInstructors, lstInstructor.Count, "instructor");
                if (!AllInstructorsSelected && !AllTraineesSelected)
                {
                    btnBroadcast.Enabled = false;
                    btnBroadcast.BackColor = Constants.cInActive;
                }
                else
                {
                    btnBroadcast.Enabled = true;
                    btnBroadcast.BackColor = Constants.cExtinguised;
                }



            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }
        #endregion

        private void btnSelectAllPositions_Click(object sender, EventArgs e)
        {
            AllTrainees(true);
            AllInstructors(true);
        }

        private void AllTrainees(bool _select)
        {
            if (_select)
            {
                foreach (Control ctrl in pnlTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ((Button)ctrl).ForeColor = Constants.cFontSelected;
                        ((Button)ctrl).BackColor = Constants.cControlSelected;
                    }

                }
                AllTraineesSelected = true;
            }
            else
            {
                foreach (Control ctrl in pnlTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ((Button)ctrl).ForeColor = Constants.cFontNotSelected;
                        ((Button)ctrl).BackColor = Constants.cExtinguised;
                    }
                }
                AllTraineesSelected = false;
            }
        }


        private void AllInstructors(bool _select)
        {
            if (_select)
            {
                foreach (Control ctrl in pnlInstructors.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ((Button)ctrl).ForeColor = Constants.cFontSelected;
                        ((Button)ctrl).BackColor = Constants.cControlSelected;
                    }
                }
                AllInstructorsSelected = true;
            }
            else
            {
                foreach (Control ctrl in pnlInstructors.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ((Button)ctrl).ForeColor = Constants.cFontNotSelected;
                        ((Button)ctrl).BackColor = Constants.cExtinguised;
                    }
                }
                AllInstructorsSelected = false;
            }
        }

        private void btnSelectAllTrainees_Click(object sender, EventArgs e)
        {
            AllTrainees(true);
        }

        private void btnSelectAllInstructors_Click(object sender, EventArgs e)
        {
            AllInstructors(true);
        }


        private void btnDeSelect_Click(object sender, EventArgs e)
        {
            //zet alles weer terug naar de beginwaarden
            btnBroadcast.BackColor = Constants.cExtinguised;
            btnBroadcast.ForeColor = Constants.cFontNotSelected;
            btnBroadcast.Text = "Broadcast";

            AllInstructors(false);
            AllTrainees(false);
            SetButtonStatus(this);
        }

        private void btnMainPage_Click(object sender, EventArgs e)
        {
             // based on:  http://stackoverflow.com/questions/1403600/how-to-avoid-multiple-instances-of-windows-form-in-c-sharp
            FrmUNETMain.GetForm.Show();
            this.Close();
        }

        private void btnBroadcast_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnBroadcast.BackColor == Constants.cExtinguised) //if the buttons is NOT in a broadcasting state
                {
                    btnBroadcast.BackColor = Constants.cBroadcasting;
                    btnBroadcast.ForeColor = Constants.cFontBroadcasting;
                    btnBroadcast.Text = "Broadcast" + Environment.NewLine + "BROADCASTING";
                    log.Info("Start ClassBroadcast");


                    //only when there is no class broadcast in progress, we can start a class broadcast
                    if (AllTraineesSelected && !AllInstructorsSelected)
                    {
                        frmmain.MakeCall(Constants.cClassBroadcastAllTrainees, true, false, false, true, false, false);
                        log.Info(" Started class broadcast to all trainees (10000");
                    }
                    if (!AllTraineesSelected && AllInstructorsSelected)
                    {
                        frmmain.MakeCall(Constants.cClassBroadcastAllInstructors, true, false, false, true, false, false);
                        log.Info(" Started class broadcast to all Instructors (11000");

                    }
                    if (AllTraineesSelected && AllInstructorsSelected)
                    {
                        frmmain.MakeCall(Constants.cClassBroadcastAll, true, false, false, true, false, false);
                        log.Info(" Started class broadcast to all trainees and all Instructors (12000");
                    }

                }
                else
                {
                    //zet alles weer terug naar de beginwaarden
                    btnBroadcast.BackColor = Constants.cExtinguised;
                    btnBroadcast.ForeColor = Constants.cFontNotSelected;
                    btnBroadcast.Text = "Broadcast";

                    AllInstructors(false);
                    AllTrainees(false);

                    log.Info("Mouseup ClassBroadcast");
                    if (frmmain != null)
                        frmmain.HangupCall(Constants.cClassBroadcastAll); //20000 is de code voor de class broadcast conferentie
                }

            }
            catch (Exception ex)
            {
                log.Error(" Something went wrong during class broadcast: " + ex.Message);

                //throw
            }
        }


    }
}
