using System;
using System.Windows.Forms;
using UNET_ConferenceBridge;
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmAudio : Form// FrmUNETbaseSub
    {
        public FrmAudio()
        {
            InitializeComponent();
        }

        private void FrmAudio_Load(object sender, EventArgs e)
        {
            this.Text = "Audio setup";

            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            tbLeftESMMM.Value = conference.LeftESM;
            tbLeftShadow.Value = conference.LeftShadow;
            tbLeftVolume.Value = conference.LeftVolume;
            tbMicGain.Value = conference.MicGain;
            tbRightESMMM.Value = conference.RightESM;
            tbRightShadow.Value = conference.RightShadow;
            tbRightVolume.Value = conference.RightVolume;

            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.SetFormSizeAndPosition(this);
        }

        private void tbLeftShadow_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.LeftShadow = tbLeftShadow.Value;

        }

        private void tbRightShadow_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.RightShadow = tbRightShadow.Value;

        }

        private void tbLeftVolume_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.LeftVolume = tbLeftVolume.Value;

        }

        private void tbRightVolume_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.RightVolume = tbRightVolume.Value;

        }

        private void tbLeftESMMM_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.LeftESM = tbLeftESMMM.Value;

        }

        private void tbRightESMMM_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.RightESM = tbRightESMMM.Value;

        }

        private void tbMicGain_ValueChanged(object sender, decimal value)
        {
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            conference.MicGain = tbMicGain.Value;

        }

        private void btnMainPage_Click(object sender, EventArgs e)
        {
            //  FrmUNETMain frm = new FrmUNETMain();
            //      frm.Show();
            // based on:  http://stackoverflow.com/questions/1403600/how-to-avoid-multiple-instances-of-windows-form-in-c-sharp
            FrmUNETMain.GetForm.Show();
            this.Close();

        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FrmSetup frm = new FrmSetup();
            frm.Show();

        }

        private void FrmAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            //sla de wijzigingen op naar de app.config van deze trainer
            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            try
            {
                ///haal de settings op uit de registry. Dit mislukt de allereerste keer
                RegistryAccess.SetStringRegistryValue(@"UNET", @"LeftShadow", tbLeftShadow.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"RightShadow", tbRightShadow.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"LeftESM", tbLeftESMMM.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"RightESM", tbRightESMMM.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"MicGain", tbMicGain.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"LeftVolume", tbLeftVolume.Value.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"RightVolume", tbRightVolume.Value.ToString());
            }
            catch (Exception ex)
            {
                string messages = ex.Message;
            }
        }
    }
}
