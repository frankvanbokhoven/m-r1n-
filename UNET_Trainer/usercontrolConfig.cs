using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace UNET_Trainer
{
    public partial class usercontrolConfig : UserControl
    {
        public usercontrolConfig()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            try
            {
            //    cbxTraineeID.Text = ConfigurationManager.AppSettings["TraineeID"].ToString();
            //    txtSIPServer.Text = ConfigurationManager.AppSettings["SIPServer"].ToString();
            //    txtSIPport.Text = ConfigurationManager.AppSettings["SIPPort"].ToString();
            //    txtSIPdomain.Text = ConfigurationManager.AppSettings["SIPDomain"].ToString();
            //    txtPort.Text = ConfigurationManager.AppSettings["Port"].ToString();
            //    txtSIPaccount.Text = ConfigurationManager.AppSettings["SIPAccount"].ToString();
            //    txtLogLevel.Text = ConfigurationManager.AppSettings["LogLevel"].ToString();
            //    txtSIPretry.Text = ConfigurationManager.AppSettings["SIPRetry"].ToString();
            //    txtSIPtimeout.Text = ConfigurationManager.AppSettings["SIPTimeout"].ToString();
            //    txtMaxVoiceVolume.Text = ConfigurationManager.AppSettings["MaxVoiceVolume"].ToString();
            //    txtMinVoiceVolume.Text = ConfigurationManager.AppSettings["MinVoiceVolume"].ToString();
            //    txtVoiceVolumeSweep.Text = ConfigurationManager.AppSettings["VoiceVolumeSweep"].ToString();
            //    txtSystemUserName.Text = ConfigurationManager.AppSettings["SystemUserName"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading settings from app.config: " + ex.Message);
            }
        }

        private void usercontrolConfig_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
