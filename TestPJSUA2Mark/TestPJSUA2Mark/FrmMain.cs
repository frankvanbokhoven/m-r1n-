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

        private Classes.ConsoleCatcher cc;
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

        private void AddToListbox(string _text)
        {
            listBox1.AppendText(Environment.NewLine);
            listBox1.AppendText(DateTime.Now.ToShortDateString() + " : " + _text);

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
                    AddToListbox(string.Format("Calling: {0}@unet", cbxAccount.Text.Trim()));
                    SIP.SIPCall sc = new SIP.SIPCall(useragent.acc, TraineeID);
                    CallOpParam cop = new CallOpParam();
                    cop.statusCode = pjsip_status_code.PJSIP_SC_OK;
                    sc.makeCall(string.Format("sip:{0}@10.0.128.128", cbxAccount.Text.Trim()), cop);
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
            AddToListbox("Started this test client");///check if this instance of the traineeclient has a traineeid assigned, and if not: prompt for one
            TraineeID = Convert.ToInt16(ConfigurationManager.AppSettings["TraineeID"]);
            timerSIPMessages.Enabled = true;
            //   cc = new Classes.ConsoleCatcher(listBox1); //the consolecatcher makes all messages that are written to the console, appear in the listbox on the form
           // using (var consoleWriter = new Classes.ConsoleWriter())
           // {
           ////     consoleWriter.WriteEvent += consoleWriter_WriteEvent;
           //     Console.SetOut(consoleWriter);
           // }

            try
            {
                //the useragent holds everything needed for the sip communication
                useragent = new SIP.UserAgent();
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
            useragent.UserAgentStop();
        }

        private void timerSIPMessages_Tick(object sender, EventArgs e)
        {
            string messages = Classes.WCFcaller.GetSIPStatusMessages();

          string[]  messagelist = messages.Split('|');
            foreach (string str in messagelist )
            {
                if(str.Length >0)
                  AddToListbox(str);
            }
        }
    }
}
