using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Classes;

namespace UNET_Trainer
{
    public partial class FrmClassBroadcast : FrmUNETbaseSub
    {


        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        public FrmClassBroadcast()
        {
            InitializeComponent();
            pnlInstructors.Paint += UC_Paint;
            pnlTrainees.Paint += UC_Paint;
        }

        private void FrmClassBroadcast_Load(object sender, EventArgs e)
        {
            FormTitle = "Class broadcast";
            timer1.Enabled = true;
            InitState();
        }


        /// <summary>
        /// Init this screen as described in 2.2.1
        /// </summary>
        private void InitState()
        {
            //loop thrue the Monitortraineearray to set the proper status
            //  for (int i = 0; i <= 15; i++)
            //  {
            //      MonitorTraineeArray[i] = false;
            //  }

            // A little amateur.. but it just is the fastest manner
            btnTraineeAA.BackColor = Constants.cExtinguised;
            btnTraineeBB.BackColor = Constants.cExtinguised;
            btnTraineeCC.BackColor = Constants.cExtinguised;
            btnTraineeDD.BackColor = Constants.cExtinguised;
            btnTraineeEE.BackColor = Constants.cExtinguised;
            btnTraineeFF.BackColor = Constants.cExtinguised;
            btnTraineeGG.BackColor = Constants.cExtinguised;
            btnTraineeHH.BackColor = Constants.cExtinguised;
            btnTraineeJJ.BackColor = Constants.cExtinguised;
            btnTraineeKK.BackColor = Constants.cExtinguised;
            btnTraineeLL.BackColor = Constants.cExtinguised;
            btnTraineeMM.BackColor = Constants.cExtinguised;
            btnTraineeNN.BackColor = Constants.cExtinguised;
            btnTraineePP.BackColor = Constants.cExtinguised;
            btnTraineeRR.BackColor = Constants.cExtinguised;
            btnTraineeSS.BackColor = Constants.cExtinguised;

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
                SetButtonStatus(this);
            }
            //      UNET_Classes.Helpers.ResizeButtons(pnlTrainees, 5,"trainee");
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
                Application.DoEvents();
            }

            // first the trainees, we assume the name of the button component is the key for the function
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("instructor")))
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
                var traineelist = service.GetTrainees();
                List<UNET_Classes.Trainee> lstRadio = traineelist.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen

                btnTraineeAA.Enabled = lstRadio.Count >= 1;
                btnTraineeBB.Enabled = lstRadio.Count >= 2;
                btnTraineeCC.Enabled = lstRadio.Count >= 3;
                btnTraineeDD.Enabled = lstRadio.Count >= 4;
                btnTraineeEE.Enabled = lstRadio.Count >= 5;
                btnTraineeFF.Enabled = lstRadio.Count >= 6;
                btnTraineeGG.Enabled = lstRadio.Count >= 7;
                btnTraineeHH.Enabled = lstRadio.Count >= 8;
                btnTraineeJJ.Enabled = lstRadio.Count >= 9;
                btnTraineeKK.Enabled = lstRadio.Count >= 10;
                btnTraineeLL.Enabled = lstRadio.Count >= 11;
                btnTraineeMM.Enabled = lstRadio.Count >= 12;
                btnTraineeNN.Enabled = lstRadio.Count >= 13;
                btnTraineePP.Enabled = lstRadio.Count >= 14;
                btnTraineeRR.Enabled = lstRadio.Count >= 15;
                btnTraineeSS.Enabled = lstRadio.Count >= 16;

                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(pnlTrainees, lstRadio.Count, "trainee");

                //enable the Roles buttons
                var instructorlist = service.GetInstructors();
                List<UNET_Classes.Instructor> lstInstructor = instructorlist.ToList<UNET_Classes.Instructor>(); //C# v3 manier om een array in een list te krijgen

                btnInstructor01.Enabled = lstInstructor.Count >= 1;
                btnInstructor02.Enabled = lstInstructor.Count >= 2;
                btnInstructor03.Enabled = lstInstructor.Count >= 3;
                btnInstructor04.Enabled = lstInstructor.Count >= 4;


                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtonsVertical(pnlInstructors, lstInstructor.Count, "instructor");

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

  
        private void btnBroadcast_MouseDown(object sender, MouseEventArgs e)
        {
            btnBroadcast.BackColor = Constants.cBroadcasting;
            btnBroadcast.ForeColor = Constants.cFontBroadcasting;
            btnBroadcast.Text = "Broadcast" + Environment.NewLine + "BROADCASTING";
        }

        private void btnBroadcast_MouseUp(object sender, MouseEventArgs e)
        {
            //zet alles weer terug naar de beginwaarden
            btnBroadcast.BackColor = Constants.cExtinguised;
            btnBroadcast.ForeColor = Constants.cFontNotSelected;
            btnBroadcast.Text = "Broadcast";

            AllInstructors(false);
            AllTrainees(false);

        }

        private void btnDeSelect_Click(object sender, EventArgs e)
        {
            //zet alles weer terug naar de beginwaarden
            btnBroadcast.BackColor = Constants.cExtinguised;
            btnBroadcast.ForeColor = Constants.cFontNotSelected;
            btnBroadcast.Text = "Broadcast";

            AllInstructors(false);
            AllTrainees(false);
        }
    }
}
