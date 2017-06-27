using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmRadioSetup : FrmUNETbaseSub
    {
        private int SelectedButtonIndex = -1; //this property is set after the click of a radio button
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
     //   private int NoiseLevel = 1;
        public FrmRadioSetup()
        {
            InitializeComponent();


            SetNoiseLevel(1);
        }


#region NoiseLevel

        private void SetNoiseLevel(int _noiselevel)
        {
           //  we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
           // if (service.State != System.ServiceModel.CommunicationState.Opened)
           // {
           //     service.Open();
           // }
           //   service.SetNoiseLevelChanged()


             switch (_noiselevel)
                {
                case 0:
                    {
                        btn1.BackColor = Color.White;
                        btn2.BackColor = Color.White;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.LightBlue; ;
                        break;
                    }
                case 1:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.White;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 2:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 3:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 4:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.LightBlue;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 5:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.LightBlue;
                        btn5.BackColor = Color.LightBlue;
                        btnOff.BackColor = Color.White;
                        break;
                    }
            }
        }
        #endregion

        #region noiselevel
        private void btnOff_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(0); //todo: enum van maken
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(5);
        }
        #endregion

        private void FrmRadioSetup_Load(object sender, EventArgs e)
        {
            FormTitle = "Radio Setup";
            timer1.Enabled = true;
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

                //enable the Roles buttons
                var radiolist = service.GetRadios();
                List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen

                btnRadio01.Enabled = lstRadio.Count >= 1;
                btnRadio02.Enabled = lstRadio.Count >= 2;
                btnRadio03.Enabled = lstRadio.Count >= 3;
                btnRadio04.Enabled = lstRadio.Count >= 4;
                btnRadio05.Enabled = lstRadio.Count >= 5;
                btnRadio06.Enabled = lstRadio.Count >= 6;
                btnRadio07.Enabled = lstRadio.Count >= 7;
                btnRadio08.Enabled = lstRadio.Count >= 8;
                btnRadio09.Enabled = lstRadio.Count >= 9;
                btnRadio10.Enabled = lstRadio.Count >= 10;
                btnRadio11.Enabled = lstRadio.Count >= 11;
                btnRadio12.Enabled = lstRadio.Count >= 12;
                btnRadio13.Enabled = lstRadio.Count >= 13;
                btnRadio14.Enabled = lstRadio.Count >= 14;
                btnRadio15.Enabled = lstRadio.Count >= 15;
                btnRadio16.Enabled = lstRadio.Count >= 16;
                btnRadio17.Enabled = lstRadio.Count >= 17;
                btnRadio18.Enabled = lstRadio.Count >= 18;
                btnRadio19.Enabled = lstRadio.Count >= 19;
                btnRadio20.Enabled = lstRadio.Count >= 20;
     
                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtons(pnlRadios, lstRadio.Count, "radio");
             }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }

        #endregion

        private void FrmRadioSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();
        }

        private void btnRadio01_Click(object sender, EventArgs e)
        {
           // SetRadioStatus();      
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
            //zet eerst alles weer op de oude kleur
            foreach (Control c in pnlRadios.Controls)
            {
                if ((c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("radio"))) && c.Enabled)
                {
                    ((Button)c).BackColor = System.Drawing.Color.SaddleBrown;
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
            SelectedButtonIndex = Convert.ToInt16( Regex.Replace(_btn.Name, "[^0-9.]", "")); //haal het indexnummer op van de button
            int noiselevel = service.GetNoiseLevel(SelectedButtonIndex);

             SetNoiseLevel(noiselevel);

            //enable the Roles buttons
            var radiolist = service.GetRadios();
            List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
        }
    }
}
