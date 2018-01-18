using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETbaseSub : FrmUNETbase
    {
        private string _formtitle = "...";
        public string FormTitle
        {
            get
            {
                return _formtitle;
            }
            set
            {
                _formtitle = value;
                lblTitle.Text = _formtitle;
            }
        }
        public FrmUNETbaseSub()
        {
            InitializeComponent();
        }


        private void btnServiceRequest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A service request has been made", "Service Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void FrmUNETbaseSub_Resize(object sender, EventArgs e)
        {
     
        }



        private void FrmUNETbaseSub_Shown(object sender, EventArgs e)
        {
     
        }

        private void FrmUNETbaseSub_Load(object sender, EventArgs e)
        {
            ShowIcon = false;
            ShowInTaskbar = false;
        }
    }
}
