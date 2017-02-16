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
        }
    }
}
