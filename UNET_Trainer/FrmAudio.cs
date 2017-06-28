using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_ConferenceBridge;

namespace UNET_Trainer
{
    public partial class FrmAudio : FrmUNETbaseSub
    {
        public FrmAudio()
        {
            InitializeComponent();
        }

        private void FrmAudio_Load(object sender, EventArgs e)
        {
            FormTitle = "Audio setup";

            UNET_ConferenceBridge.ConferenceBridge_Singleton conference = UNET_ConferenceBridge.ConferenceBridge_Singleton.Instance;

            tbLeftESMMM.Value = conference.LeftESM;
            tbLeftShadow.Value = conference.LeftShadow;
            tbLeftVolume.Value = conference.LeftVolume;
            tbMicGain.Value = conference.MicGain;
            tbRightESMMM.Value = conference.RightESM;
            tbRightShadow.Value = conference.RightShadow;
            tbRightVolume.Value = conference.RightVolume;
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
    }
}
