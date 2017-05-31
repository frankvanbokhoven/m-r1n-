using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pjsua2;
using System.Configuration;

namespace TestPJSUA2Mark
{
    public partial class FrmMain : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private SIP.UserAgent useragent;

        //  private Boolean Muted = false;
        //  private Boolean MonitorTrainee = false;
        //  private Boolean MonitorRadio = false;
        public int TraineeID = 1;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                AddToListbox("Registering: 10031@unet ");
                SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, TraineeID);
                CallOpParam cop = new CallOpParam();
                cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                sc.makeCall("sip:1003@unet", cop);
                AddToListbox("1011@unet successfully registered!!");
            }
            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
                // throw;
            }
        }

        private void AddToListbox(string _text)
        {
            listBox1.AppendText(Environment.NewLine);
            listBox1.AppendText(DateTime.Now.ToShortDateString() + " : " + _text);
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text.Length == 0)
            {
                MessageBox.Show("You MUST enter an account!!");
            }
            else
            {
                AddToListbox("Starting a call to: " + txtAccount.Text);
                try
                {
                    AddToListbox("Registering: 1003@unet");
                    SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, TraineeID);
                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.makeCall("sip:1003@10.0.128.128", cop);
                    AddToListbox("Call successfully made to: 1003@unet");
                }
                catch (Exception ex)
                {
                    log.Error("Error updating screen controls", ex);
                    // throw;
                }
            }

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AddToListbox("Started this test client");        ///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            TraineeID = Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);

            try
            {
                //Create sip transport and errorhandling
                //tcfg.port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
                //endpoint.transportCreate(pjsua2.pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);
                useragent = new SIP.UserAgent();
                useragent.UserAgentStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Error creating accounts " + ex.Message);
            }
        }
    }
}
