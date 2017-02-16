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
        public FrmTrainees()
        {
            InitializeComponent();
        }

        private void FrmTrainees_Load(object sender, EventArgs e)
        {
            FormTitle = "Trainees";
        }
    }
}
