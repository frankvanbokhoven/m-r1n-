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
    public partial class FrmAudio : FrmUNETbaseSub
    {
        public FrmAudio()
        {
            InitializeComponent();
        }

        private void FrmAudio_Load(object sender, EventArgs e)
        {
            FormTitle = "Audio setup";


            tbLeftESMMM.Value = 5;
            tbLeftShadow.Value = 5;
            tbLeftVolume.Value = 5;
            tbMicGain.Value = 5;
            tbRightESMMM.Value = 5;
            tbRightShadow.Value = 5;
            tbRightValue.Value = 5;
        }
    }
}
