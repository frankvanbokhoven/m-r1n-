using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmRadioSetup : Form// FrmUNETbaseSub
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();

        private int SelectedNoiseButtonIndex = -1; //this property is set after the click of a radio button
        private int SelectedRadioButtonIndex = -1; // index of one of the radio buttons
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();

        public FrmRadioSetup()
        {
            InitializeComponent();
            pnlRadios.Paint += UC_Paint;
            panelNoise.Paint += UC_Paint;

        }
        public FrmRadioSetup(int _exersise)
        {
            this.Text = String.Format("Radio Setup                                   Selected exersise: Exersise{0}", _exersise.ToString("00"));

            InitializeComponent();
            pnlRadios.Paint += UC_Paint;
            panelNoise.Paint += UC_Paint;

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
        #region NoiseLevel


        /// <summary>
        /// after a radio is selected, the appropriate noiselevel is loaded from wcf and set into the noise buttons
        /// </summary>
        private void SetNoiseLevel()
        {
            //open the connection to the service
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            int noiselevel = service.GetNoiseLevel(SelectedRadioButtonIndex);


            switch (noiselevel)
            {
                case 0:
                    {
                        btnNoise1.BackColor = Color.White;
                        btnNoise1.ForeColor = Color.Black;
                        btnNoise2.BackColor = Color.White;
                        btnNoise2.ForeColor = Color.Black;
                        btnNoise3.BackColor = Color.White;
                        btnNoise3.ForeColor = Color.Black;
                        btnNoise4.BackColor = Color.White;
                        btnNoise4.ForeColor = Color.Black;
                        btnNoise5.BackColor = Color.White;
                        btnNoise5.ForeColor = Color.Black;
                        btnNoiseOff.BackColor = Color.DarkBlue;
                        btnNoiseOff.ForeColor = Color.White;

                        break;
                    }
                case 1:
                    {
                        btnNoise1.BackColor = Color.DarkBlue;
                        btnNoise1.ForeColor = Color.White;
                        btnNoise2.BackColor = Color.White;
                        btnNoise2.ForeColor = Color.Black;
                        btnNoise3.BackColor = Color.White;
                        btnNoise3.ForeColor = Color.Black;
                        btnNoise4.BackColor = Color.White;
                        btnNoise4.ForeColor = Color.Black;
                        btnNoise5.BackColor = Color.White;
                        btnNoise5.ForeColor = Color.Black;
                        btnNoiseOff.BackColor = Color.White;
                        btnNoiseOff.ForeColor = Color.Black;
                        break;
                    }
                case 2:
                    {
                        btnNoise1.BackColor = Color.DarkBlue;
                        btnNoise1.ForeColor = Color.White;
                        btnNoise2.BackColor = Color.DarkBlue;
                        btnNoise2.ForeColor = Color.White;
                        btnNoise3.BackColor = Color.White;
                        btnNoise3.ForeColor = Color.Black;
                        btnNoise4.BackColor = Color.White;
                        btnNoise4.ForeColor = Color.Black;
                        btnNoise5.BackColor = Color.White;
                        btnNoise5.ForeColor = Color.Black;
                        btnNoiseOff.BackColor = Color.White;
                        btnNoiseOff.ForeColor = Color.Black;

                        break;
                    }
                case 3:
                    {
                        btnNoise1.BackColor = Color.DarkBlue;
                        btnNoise1.ForeColor = Color.White;
                        btnNoise2.BackColor = Color.DarkBlue;
                        btnNoise2.ForeColor = Color.White;
                        btnNoise3.BackColor = Color.DarkBlue;
                        btnNoise3.ForeColor = Color.White;
                        btnNoise4.BackColor = Color.White;
                        btnNoise4.ForeColor = Color.Black;
                        btnNoise5.BackColor = Color.White;
                        btnNoise5.ForeColor = Color.Black;
                        btnNoiseOff.BackColor = Color.White;
                        btnNoiseOff.ForeColor = Color.Black;
                        break;
                    }
                case 4:
                    {
                        btnNoise1.BackColor = Color.DarkBlue;
                        btnNoise1.ForeColor = Color.White;
                        btnNoise2.BackColor = Color.DarkBlue;
                        btnNoise2.ForeColor = Color.White;
                        btnNoise3.BackColor = Color.DarkBlue;
                        btnNoise3.ForeColor = Color.White;
                        btnNoise4.BackColor = Color.DarkBlue;
                        btnNoise4.ForeColor = Color.White;
                        btnNoise5.BackColor = Color.White;
                        btnNoise5.ForeColor = Color.Black;
                        btnNoiseOff.BackColor = Color.White;
                        btnNoiseOff.ForeColor = Color.Black;
                        break;
                    }
                case 5:
                    {
                        btnNoise1.BackColor = Color.DarkBlue;
                        btnNoise1.ForeColor = Color.White;
                        btnNoise2.BackColor = Color.DarkBlue;
                        btnNoise2.ForeColor = Color.White;
                        btnNoise3.BackColor = Color.DarkBlue;
                        btnNoise3.ForeColor = Color.White;
                        btnNoise4.BackColor = Color.DarkBlue;
                        btnNoise4.ForeColor = Color.White;
                        btnNoise5.BackColor = Color.DarkBlue;
                        btnNoise5.ForeColor = Color.White;
                        btnNoiseOff.BackColor = Color.White;
                        btnNoiseOff.ForeColor = Color.Black;
                        break;
                    }
            }
        }


        /// <summary>
        /// Set the selected noise level for the selected radio
        /// </summary>
        /// <param name="_noiselevel"></param>
        private void SetNoiseLevelWCF(int _noiselevel)
        {
            if (SelectedRadioButtonIndex >= 0)
            {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                SelectedNoiseButtonIndex = _noiselevel;

                var radiolist = service.SetNoiseLevel(SelectedRadioButtonIndex, SelectedNoiseButtonIndex);
                service.SetNoiseLevelChanged(SelectedRadioButtonIndex, true);
            }
        }


        private void FrmRadioSetup_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.SetFormSizeAndPosition(this);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                SetButtonStatus(this);
            }
        }

        private void FrmRadioSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();
        }

        private void btnRadio01_Click(object sender, EventArgs e)
        {
            // SetRadioStatus();      
            SetStatusAndColorRadioButtons((Button)sender);
        }
        #endregion

        #region set noiselevel
        private void btnNoiseOff_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(0);
        }

        private void btnNoise1_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(1);
        }

        private void btnNoise2_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(2);
        }

        private void btnNoise3_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(3);
        }

        private void btnNoise4_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(4);
        }

        private void btnNoise5_Click(object sender, EventArgs e)
        {
            SetNoiseLevel();
            SetNoiseLevelWCF(5);
        }
        #endregion


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
            SelectedRadioButtonIndex = Convert.ToInt16(Regex.Replace(_btn.Name, "[^0-9.]", "")) - 1; //haal het indexnummer op van de button maar -1 want de index start bij  0
            int noiselevel = service.GetNoiseLevel(SelectedRadioButtonIndex);

            SetNoiseLevel();

            //enable the Roles buttons
            //    var radiolist = service.GetRadios();
            //   List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
        }
        #endregion

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
