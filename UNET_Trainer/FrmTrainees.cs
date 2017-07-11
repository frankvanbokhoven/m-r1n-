using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmTrainees : FrmUNETbaseSub
    {
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        private int SelectedExercise;

        public FrmTrainees()
        {
            InitializeComponent();
            pnlTrainees.Paint += UC_Paint;
        }

        public FrmTrainees(int _exersise)
        {
            SelectedExercise = _exersise;
             FormTitle = String.Format("Trainee Assignment                              Selected exersise: Exersise{0}", _exersise.ToString("00"));
             InitializeComponent();
       
            pnlTrainees.Paint += UC_Paint;
          }

        private void FrmTrainees_Load(object sender, EventArgs e)
        {
             timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
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
                //  {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //enable the Trainee buttons. All available trainees
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
       
                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtons(pnlTrainees, lstTrainee.Count, "trainee");

                //now retrieve which trainees are assigned to this exercise
                //and color these darkblue
                List<UNET_Classes.Trainee> lstTraineeAssigned = service.GetTraineesAssigned(SelectedExercise);
                foreach(UNET_Classes.Trainee trainee in lstTraineeAssigned)
                {
                   Control ctrl =  this.Controls.Find("btnTrainee" + trainee.ID, false).First<Control>();
                  if(ctrl != null)
                  {
                        ((Button)ctrl).BackColor = Color.DarkBlue;
                        ((Button)ctrl).ForeColor = Color.White;
                  }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            SetStatusAndColorTraineeButtons((Button)sender);
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
    }
}
