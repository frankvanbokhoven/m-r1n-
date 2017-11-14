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
    public partial class FrmTrainees : Form // FrmUNETbaseSub
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

        private Instructor CurrentInstructor;

        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        private int SelectedExercise;
        private int InstructorID;
        #region constructors
        public FrmTrainees()
        {
            InitializeComponent();
            pnlTrainees.Paint += UC_Paint;
        }

        public FrmTrainees(int _exersise, int _instructorID)
        {
            SelectedExercise = _exersise;
            InstructorID = _instructorID;
            InitializeComponent();
            
            pnlTrainees.Paint += UC_Paint;
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
            the.InitPanels(pnlTrainees);

            the.SetFormSizeAndPosition(this);

            lblTraineeTitle.Text = "Trainee assignment    Selected excercise: " + SelectedExercise + "   Instructor: " + InstructorID;
            timer1.Enabled = true;
     }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle) //only if the form is actually visible to the user
            {
                SetButtonStatus(this);
            }

        }

        private void FrmTrainees_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                CurrentInstructor = service.GetAllInstructorData(InstructorID);
                
                ////now resize all buttons to make optimal use of the available room
           //     Application.DoEvents();
                //enable the Trainees buttons, for the number of trainees that are in
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

                //now we make visible a button for every existing role
                foreach (UNET_Classes.Trainee trainee in lstTrainee)
                {
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text = string.Format("Trainee {0}{1}{2}", trainee.ID, Environment.NewLine, trainee.Name);

                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].BackColor = Theming.TraineeNotSelectedButton;
                    listindex++;
                }

                //loop nu door de lijst van toegewezen trainees heen en kijk of er een is die aan deze instructor/exercise is toegewezen. 
                //zoja, vul de informatie in en enable de knop met de trainee-toegewezen-kleur
                if (InstructorID != -1)
                {
                    if (!Object.ReferenceEquals(CurrentInstructor, null))
                    {
                        if (!Object.ReferenceEquals(CurrentInstructor.Exercises, null))
                        {
                            if (SelectedExercise != -1)
                            {
                                listindex = 1;
                                foreach (Trainee assignedTrainee in CurrentInstructor.Exercises.FirstOrDefault(x => x.Number == SelectedExercise).TraineesAssigned)
                                {
                                    //    if (assignedRole.ID == role.ID)
                                    //    {
                                    //   pnlRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].BackColor = Theming.TraineeSelectedButton;
                                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text += string.Format("{0}Instructor: {1}", Environment.NewLine, CurrentInstructor.ID + " " + CurrentInstructor.Name);
                                    listindex++;

                                    //    }
                                }
                            }
                        }
                    }
                }

                UNET_Classes.Helpers.ResizeButtons(pnlTrainees, lstTrainee.Count, "trainee");
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
                int traineeIndex = Convert.ToInt16(splitstring[1].ToString());

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
    }
}
