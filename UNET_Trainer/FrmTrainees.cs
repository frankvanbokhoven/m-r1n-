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
using UNET_Sounds;
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmTrainees : Form // FrmUNETbaseSub
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

        private Instructor currentInstructor;

        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        private int SelectedExercise;
        private string InstructorID;
        private DateTime LastUpdate = DateTime.MinValue;
        private bool ServiceRequestPending = false;
        private UNET_Sounds.UNETSoundsController sound;


        #region constructors
        public FrmTrainees()
        {
            InitializeComponent();
            panelTrainees.Paint += UC_Paint;
        }

        public FrmTrainees(int _exersise, string _instructorID)
        {
            SelectedExercise = _exersise;
            InstructorID = _instructorID;
            InitializeComponent();

            panelTrainees.Paint += UC_Paint;
        }
        #endregion

        /// <summary>
        /// Zorg dat de panels een witte border krijgen als ze een dargray opvulkleur hebben
        /// https://stackoverflow.com/questions/76455/how-do-you-change-the-color-of-the-border-on-a-group-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UC_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void FrmTrainees_Load(object sender, EventArgs e)
        {


            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.InitPanels(panelTrainees);

            the.SetFormSizeAndPosition(this);

            lblTraineeTitle.Text = "Trainee assignment    Selected excercise: " + SelectedExercise + "   Instructor: " + InstructorID;
            timer1.Enabled = true;

            foreach (Control c in this.Controls)
            {
                //reset everything
                if (c.GetType() == typeof(UNET_Button.UNET_Button) && (c.Name.ToLower().Contains("request")))
                {
                    ((UNET_Button.UNET_Button)c).ImageIndex = 0;

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle) //only if the form is actually visible to the user
            {
                //  SetButtonStatus(this);

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                if (service.GetPendingChanges() > LastUpdate) //only if the last-changed-datetime on the server is more recent than on the client, we need to update
                {

                    SetButtonStatus(this);
                    GetAssists(this);
                    LastUpdate = DateTime.Now; //todo: eigenlijk moet hier het resultaat van GetPendingchanges in komen

                }
            }
        }

        /// <summary>
        /// gets the list of pending assists for this instructor and sets the status indicators accordingly
        /// </summary>
        /// <param name="parent"></param>
        private void GetAssists(Control parent)
        {
            try
            {


                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                //retrieve the pending assists for this instructor
                var resultassists = service.GetAssists(InstructorID);
                List<Assist> pendingAssists = resultassists.ToList<Assist>();


                if (pendingAssists.Count > 0)
                {
                    if (btnServiceRequest.ImageIndex == 1)
                    {
                        btnServiceRequest.ImageIndex = 2;
                    }
                    else
                    {
                        btnServiceRequest.ImageIndex = 1;

                    }

                    //and play a sound

                    sound = new UNETSoundsController();
                    sound.PlayAssistBeep();

                }




                Application.DoEvents();


            }
            catch (Exception ex)
            {
                log.Error("Error getting assists", ex);
                // throw;
            }

        }


        private void FrmTrainees_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///the user has acknowledged, so stop the sound
            if (sound != null)
            {
                sound.StopAssistBeep();
            }
            service.Close();
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
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                currentInstructor = service.GetAllInstructorData(InstructorID);

                ////now resize all buttons to make optimal use of the available room
                Application.DoEvents();
                //enable the Trainees buttons, for the number of trainees that are in
                foreach (Control ctrl in panelTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }
                int listindex = 1;


                //UNET_Classes.Helpers.ResizeButtons(pnlTrainees, lstTrainee.Count, "trainee");
                //enable the Trainees buttons, for the number of trainees that are in the training
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in panelTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(UNET_Button.UNET_Button))
                    {
                        ctrl.Enabled = false;
                        ctrl.Tag = "disable";
                        ((UNET_Button.UNET_Button)ctrl).BackColor = Theming.TraineeNotSelectedButton;

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
                            if (SelectedExercise != -1)
                            {
                                foreach (Trainee assignedTrainee in currentInstructor.Exercises.FirstOrDefault(x => x.Number == SelectedExercise).TraineesAssigned) //pak van de bij exercises geselecteerde exercise, de lijst van toegewezen trainees en gebruik die om de buttons te kleuren
                                {
                                    if (assignedTrainee.ID == trainee.ID)
                                    {
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].BackColor = Theming.TraineeSelectedButton;
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].ForeColor = Theming.ButtonDarkSelectedText;
                                        panelTrainees.Controls["btnTrainee" + listindex.ToString("00")].Tag = "enable";

                                    }
                                }
                            }
                        }
                    }
                    listindex++;
                }

                UNET_Classes.Helpers.ResizeButtons(panelTrainees, lstTrainee.Count, "trainee");

                Application.DoEvents();

            }
            catch (Exception ex)
            {
                log.Error("Error using WCF Set trainee button status", ex);
                // throw;
            }

        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            try
            {



                string name = ((Button)sender).Text.Substring(0, ((Button)sender).Text.IndexOf("\r\n"));

                string[] splitstring = name.Split(' ');
                string traineeIndex = splitstring[1].ToString();

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
                if (((Button)sender).BackColor == Theming.TraineeSelectedButton)
                    //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
                    service.SetTraineeAssignedStatus(InstructorID, SelectedExercise, traineeIndex, false);
                else
                    //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
                    service.SetTraineeAssignedStatus(InstructorID, SelectedExercise, traineeIndex, true);


            }
            catch (Exception ex)
            {
                log.Error("Error setting role", ex);
                Console.Write("Error setting role: " + ex.Message);
                // throw;
            }
        }


        private void btnMainPage_Click(object sender, EventArgs e)
        {
            //  FrmUNETMain frm = new FrmUNETMain();
            //      frm.Show();
            // based on:  http://stackoverflow.com/questions/1403600/how-to-avoid-multiple-instances-of-windows-form-in-c-sharp
            FrmUNETMain.GetForm.Show();
            this.Close();
        }

        private void btnServiceRequest_Click(object sender, EventArgs e)
        {
            /// if a servicerequest is made to the instructor, the lid on this button will flash and a "beep" will be played. The instructor must return to the main page to 
            /// determine the requester
            /// 
            if (ServiceRequestPending)
          {
        
              btnMainPage_Click(sender, e); //return to the main page
            }

        }
    }
}
