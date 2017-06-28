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
    public partial class FrmRoles : FrmUNETbaseSub
    {
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        public FrmRoles()
        {
            InitializeComponent();
            pnlRoles.Paint += UC_Paint;
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            FormTitle = "Role Assignment";

            timer1.Enabled = true;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                SetButtonStatus(this);
            }

        }

        private void FrmRoles_FormClosing(object sender, FormClosingEventArgs e)
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
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("role")))
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
                var rolelist = service.GetRoles();
                List<UNET_Classes.Role> lstTrainee = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen

                btnRole1.Enabled = lstTrainee.Count >= 1;
                btnRole2.Enabled = lstTrainee.Count >= 2;
                btnRole3.Enabled = lstTrainee.Count >= 3;
                btnRole4.Enabled = lstTrainee.Count >= 4;
                btnRole5.Enabled = lstTrainee.Count >= 5;
                btnRole6.Enabled = lstTrainee.Count >= 6;
                btnRole7.Enabled = lstTrainee.Count >= 7;
                btnRole8.Enabled = lstTrainee.Count >= 8;
                btnRole9.Enabled = lstTrainee.Count >= 9;
                btnRole10.Enabled = lstTrainee.Count >= 10;
                btnRole11.Enabled = lstTrainee.Count >= 11;
                btnRole12.Enabled = lstTrainee.Count >= 12;
                btnRole13.Enabled = lstTrainee.Count >= 13;
                btnRole14.Enabled = lstTrainee.Count >= 14;
                btnRole15.Enabled = lstTrainee.Count >= 15;
                btnRole16.Enabled = lstTrainee.Count >= 16;
                btnRole17.Enabled = lstTrainee.Count >= 17;
                btnRole18.Enabled = lstTrainee.Count >= 18;
                btnRole19.Enabled = lstTrainee.Count >= 19;
                btnRole20.Enabled = lstTrainee.Count >= 20;

                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtons(pnlRoles, lstTrainee.Count, "role");
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
        private void SetStatusAndColorRoleButtons(Button _btn)
        {
            //zet eerst alles weer op de oude kleur
            foreach (Control c in pnlRoles.Controls)
            {
                if ((c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("role"))) && c.Enabled)
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
          //  var radiolist = service.GetRoles();
           // List<UNET_Classes.Radio> lstRadio = radiolist.ToList<UNET_Classes.Radio>(); //C# v3 manier om een array in een list te krijgen
        }

    }
}
