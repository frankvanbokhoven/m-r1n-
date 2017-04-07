using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Tester.UNET_Service;

namespace UNET_Tester
{
    public partial class frmUNETTester_Main : Form
    {
        public frmUNETTester_Main()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();


       
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                // throw;
            }
        }
    }
}
