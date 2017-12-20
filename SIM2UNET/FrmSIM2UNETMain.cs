using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIM2UNET
{
    public partial class FrmSIM2UNETMain : Form
    {
        private UDPListenerSingleton singleton = UDPListenerSingleton.Instance;
        public FrmSIM2UNETMain()
        {
            InitializeComponent();
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {

        }
    }
}
