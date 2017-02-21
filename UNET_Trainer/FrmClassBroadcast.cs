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
    public partial class FrmClassBroadcast : FrmUNETbaseSub
    {
        public FrmClassBroadcast()
        {
            InitializeComponent();
        }

        private void FrmClassBroadcast_Load(object sender, EventArgs e)
        {
            FormTitle = "Class broadcast";
        }
    }
}
