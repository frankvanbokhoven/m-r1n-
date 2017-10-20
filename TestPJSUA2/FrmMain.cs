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
namespace TestPJSUA2
{
    public partial class FrmMain : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Classes.ConsoleCatcher cc;
        private SIP.UserAgent useragent;
        //  private Boolean Muted = false;
        //  private Boolean MonitorTrainee = false;
        //  private Boolean MonitorRadio = false;
        private const string cOphangen = "Ophangen";
        private const string cOpnemen = "Opnemen";
        private List<SIP.SIPCall> CallStack = new List<SIP.SIPCall>();
        public int TraineeID = 1;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region threadsafecalls
        //private void setTextUnsafe(object sender, EventArgs e)
        //{
        //    this.demoThread = new Thread(new ThreadStart(this.ThreadProcUnsafe()));

        //    this.demoThread.Start();
        //}

        //private void ThreadProcUnsafe(string _text)
        //{
        //    listBox1.AppendText(Environment.NewLine);
        //    listBox1.AppendText(DateTime.Now.ToShortDateString() + " : " + _text);

        //}
        #endregion

        //public void AddItemThreadSafe(string item)
        //{

        //    if (listbox1.InvokeRequired)
        //    {
        //        tb.Invoke(new MethodInvoker(delegate
        //        {
        //            tb.AppendText(Environment.NewLine);
        //            tb.AppendText(DateTime.Now.ToShortDateString() + " : " + item);
        //        }));
        //    }
        //    else
        //    {
        //        tb.AppendText(Environment.NewLine);
        //        tb.AppendText(DateTime.Now.ToShortDateString() + " : " + item);
        //    }
        //}

        public void AddToListbox(string _text)
        {


            this.Invoke((MethodInvoker)(() => listBox1.AppendText(Environment.NewLine)));
            this.Invoke((MethodInvoker)(() => listBox1.AppendText(DateTime.Now.ToShortDateString() + " : " + _text)));

            if (_text.ToLower().Contains("incoming"))
            {
                btnAnswer.Visible = true;
                btnAnswer.BackColor = Color.LimeGreen;
                btnAnswer.Text = cOpnemen;
            }
            else
            if (_text.ToLower().Contains("answered"))
            {
                btnAnswer.Visible = true;
                btnAnswer.BackColor = Color.Red;
                btnAnswer.Text = cOphangen;
            }
            else
            if (_text.ToLower().Contains("hangup"))
            {
                btnAnswer.Visible = false;
                btnAnswer.BackColor = Color.LimeGreen;
                btnAnswer.Text = cOpnemen;
            }
            else
            if (_text.ToLower().Contains("account:"))
            {
                this.Invoke((MethodInvoker)(() => toolStripStatusLabel1.Text = "Registered: " + _text.Substring(_text.IndexOf("Account:"))));
            }
            else

            if (_text.ToLower().Contains("register:"))
            {
                this.Invoke((MethodInvoker)(() => toolStripStatusLabel1.Text = "Registered: " + _text.Substring(_text.IndexOf(":"))));
            }


        }

        /// <summary>
        /// the onclick event for the Call button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCall_Click(object sender, EventArgs e)
        {
            if (cbxAccount.Text.Length == 0)
            {
                MessageBox.Show("You MUST enter an account!!");
            }
            else
            {
                AddToListbox("Starting a call to: " + cbxAccount.Text);
                try
                {
                    string sipserver = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
                    AddToListbox(string.Format("Calling: {0}@unet", cbxAccount.Text.Trim()));


                    //hier worden de channels gekoppeld aan de call die wordt opgezet
                    List<SIP.InputChannels> lstinputchannels = new List<SIP.InputChannels>();
                    if(cbxLeft.Checked)
                       lstinputchannels.Add(SIP.InputChannels.ichLeft);
                    if (cbxRight.Checked)
                        lstinputchannels.Add(SIP.InputChannels.ichRight);
                    if (cbxSpeaker.Checked)
                        lstinputchannels.Add(SIP.InputChannels.ichSpeaker);


                    List<SIP.OutputChannels> lstoutputchannels = new List<SIP.OutputChannels>();
                    if(cbxLeft.Checked)
                      lstoutputchannels.Add(SIP.OutputChannels.ochLeft);
                    if (cbxLeft.Checked)
                        lstoutputchannels.Add(SIP.OutputChannels.ochRight);
                    if (cbxSpeaker.Checked)
                        lstoutputchannels.Add(SIP.OutputChannels.ochSpeaker);

                    //Hier wordt de sipcall daadwerkelijk gestart
                    SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, ref lstinputchannels, ref lstoutputchannels, TraineeID);
                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.makeCall(string.Format("sip:{0}@{1}", cbxAccount.Text.Trim(), sipserver), cop);
                    //if it is successfully made, we can add it to the callstack
                    CallStack.Add(sc);
                    lblCallstackCount.Text = CallStack.Count.ToString();
                    AddToListbox(string.Format("Call successfully made to: {0}@{1}", cbxAccount.Text.Trim(), sipserver));
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
            AddToListbox("Started this test client");///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            TraineeID = Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);
            timerSIPMessages.Enabled = true;
            //   cc = new Classes.ConsoleCatcher(listBox1); //the consolecatcher makes all messages that are written to the console, appear in the listbox on the form
            // using (var consoleWriter = new Classes.ConsoleWriter())
            // {
            ////     consoleWriter.WriteEvent += consoleWriter_WriteEvent;
            //     Console.SetOut(consoleWriter);
            // }
            lblCallstackCount.Text = CallStack.Count.ToString();

            btnAnswer.Visible = false;

            try
            {
                //the useragent holds everything needed for the sip communication
                useragent = new SIP.UserAgent();
                useragent.frmm = this;
                useragent.UserAgentStart();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Error creating accounts " + ex.Message);
            }
        }

        /// <summary>
        /// this event catches the console messages and writes them asynchronously to the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private  void consoleWriter_WriteEvent(object sender, Classes.ConsoleWriterEventArgs e)
        //{
        //    AddItemThreadSafe(e.Value);           
        //}

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //stop the sip connection in a nice manner before closing
            useragent.ep.hangupAllCalls();
            useragent.UserAgentStop();
        }

        private void timerSIPMessages_Tick(object sender, EventArgs e)
        {
            //  string messages = Classes.WCFcaller.GetSIPStatusMessages();

            //string[]  messagelist = messages.Split('|');
            //  foreach (string str in messagelist )
            //  {
            //      if(str.Length >0)
            //        AddToListbox(str);
            //  }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            if (btnAnswer.Text.ToLower().Contains("ophangen"))
            {
                AddToListbox("Hanging up the call: " + cbxAccount.Text);
                try
                {
                    string sipserver = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
                    //  SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, "Testcall");
                    List<SIP.InputChannels> lstinputchannels = new List<SIP.InputChannels>();
                    lstinputchannels.Add(SIP.InputChannels.ichLeft);
                    List<SIP.OutputChannels> lstoutputchannels = new List<SIP.OutputChannels>();
                    lstoutputchannels.Add(SIP.OutputChannels.ochLeft);
                    SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, ref lstinputchannels, ref lstoutputchannels, TraineeID);

                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.hangup(cop);
                    CallStack.Remove(sc);
                    lblCallstackCount.Text = CallStack.Count.ToString();
                    AddToListbox(string.Format("Call hanged-up successfully: {0}@{1}", cbxAccount.Text.Trim(), sipserver));
                    btnAnswer.Text = cOpnemen;
                }
                catch (Exception ex)
                {
                    AddToListbox(("Error answering the call" + ex.Message));
                    // throw;
                }

            }
            else
            {
                AddToListbox("Answering call: " + cbxAccount.Text);
                try
                {
                    string sipserver = ConfigurationManager.AppSettings["SipServer"].ToString().Trim();
                    AddToListbox(string.Format("Calling: {0}@unet", cbxAccount.Text.Trim()));
                    //  SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, TraineeID);
                    List<SIP.InputChannels> lstinputchannels = new List<SIP.InputChannels>();
                    lstinputchannels.Add(SIP.InputChannels.ichLeft);
                    List<SIP.OutputChannels> lstoutputchannels = new List<SIP.OutputChannels>();
                    lstoutputchannels.Add(SIP.OutputChannels.ochLeft);
                    SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, ref lstinputchannels, ref lstoutputchannels, TraineeID);

                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.makeCall(string.Format("sip:{0}@{1}", cbxAccount.Text.Trim(), sipserver), cop);
                    //if it is successfully made, we can add it to the callstack
                    CallStack.Add(sc);
                    lblCallstackCount.Text = CallStack.Count.ToString();

                    AddToListbox(string.Format("Call successfully made to: {0}@{1}", cbxAccount.Text.Trim(), sipserver));
                    btnAnswer.Text = cOphangen;
                }
                catch (Exception ex)
                {
                    AddToListbox(("Error answering the call" + ex.Message));
                    // throw;
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxLeft_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
