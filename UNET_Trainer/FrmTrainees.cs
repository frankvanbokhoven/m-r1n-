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
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmTrainees : Form // FrmUNETbaseSub
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

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
            // first the trainees, we assume the name of the button component is the key for the function
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("radio")))
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
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                  
                ////now resize all buttons to make optimal use of the available room
                Application.DoEvents();
                //enable the Trainees buttons, for the number of trainees that are in
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
                int listindex = 1;
                foreach (Control ctrl in pnlTrainees.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }
                foreach (UNET_Classes.Trainee trainee in lstTrainee)
                {
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Enabled = true;
                    pnlTrainees.Controls["btnTrainee" + listindex.ToString("00")].Text = string.Format("Trainee {0}{1}{2}{3}Role:{4}", trainee.ID, Environment.NewLine, trainee.Name, Environment.NewLine, "TraineeRole");
                    listindex++;
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
            SetStatusAndColorTraineeButtons((Button)sender);

            string name = ((Button)sender).Text.Substring(0, ((Button)sender).Text.IndexOf("\r\n"));

            string[] splitstring = name.Split(' ');

            int traineeIndex = Convert.ToInt16( splitstring[1].ToString());
    
     
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }

            //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
            service.SetTraineeAssignedStatus(InstructorID, SelectedExercise,traineeIndex , true);

        }

        /// <summary>
        /// When the button  'monitor radio' is clicked and thereafter one of the radio buttons,
        /// this radio button must be set to brown, and a possible other trainee button must be set to the default color
        /// this generic code covers this for all all buttons at once
        /// </summary>
        /// <param name="_btn"></param>
        private void SetStatusAndColorTraineeButtons(Button _btn)
        {
            //zet eerst alles weer op de oude kleur
            foreach (Control c in pnlTrainees.Controls)
            {
                if ((c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("trainee"))) && c.Enabled)
                {
                    ((Button)c).BackColor = System.Drawing.Color.DarkKhaki;
                    ((Button)c).ForeColor = System.Drawing.Color.White;
                }
            }
            //daarna de button in de param op de gewenste kleur
            _btn.BackColor = System.Drawing.Color.DarkBlue;
            _btn.ForeColor = System.Drawing.Color.White;
            // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
            // visible/invisible and also set the statusled
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            //  SelectedRadioButtonIndex = Convert.ToInt16(Regex.Replace(_btn.Name, "[^0-9.]", "")); //haal het indexnummer op van de button
            //  int noiselevel = service.GetNoiseLevel(SelectedRadioButtonIndex);

            //     SetNoiseLevel();

            //enable the Roles buttons
            // var radiolist = service.GetRadios();
            //  List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
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
