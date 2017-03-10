using pjsip4net;
using pjsip4net.Accounts;
using pjsip4net.Calls;
using pjsip4net.Configuration;
using pjsip4net.Core;
using pjsip4net.Core.Configuration;
using pjsip4net.Core.Data;
using pjsip4net.Interfaces;
using pjsip.Interop;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_TrainerClient.UNET_Server_Reference;
using pjsip4net.Core.Interfaces.ApiProviders;
using pjsip4net.Core.Interfaces;
using pjsip4net.Core.Utils;
using System.Net;

namespace UNET_TrainerClient
{


    public partial class FrmMain : Form
    {

        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public FrmMain()
        {
            InitializeComponent();
        }


        /// <summary>
        /// open form..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGreen;
            GetAvailableList();
            GetRoles();

            _registered = false;
            btnRegister.Text = "Registreer!";
        }



        #region "Properties"
        IAccount _myAccount;
        private IAccount account { get; set; }

        ISipUserAgent myUa;
        private ISipUserAgent ua { get; set; }

        #endregion
        private bool _registered;

        #region "Log"
        internal delegate bool dlg_logWrite(string text);
        internal bool logWrite(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new dlg_logWrite(invLogWrite), new object[] { text });
            }
            else {
                invLogWrite(text);
            }
            return true;
        }
        internal bool invLogWrite(string text)
        {
            rtbMessages.AppendText(text);
            rtbMessages.ScrollToCaret();
            return true;
        }
        #endregion

        #region "Pjsip4Net Event-Routines"
        private static void intLog(object sender, LogEventArgs e)
        {
            switch (e.Level)
            {
                case 0:
                    log.Fatal(e.Data);// ("FATAL: " + e.Data);
                    return;
                case 1:
                    log.Fatal(e.Data);// ("FATAL: " + e.Data);
                    return;
                case 2:
                    log.Warn(e.Data);// ("FATAL: " + e.Data);
                    return;
                case 3:
                    log.Info(e.Data);// ("FATAL: " + e.Data);
                    return;
                case 4:
                    break;
                case 5:
                    log.Debug(e.Data);// ("FATAL: " + e.Data);
                    return;
                default:
                    return;
            }
        }
        private static void incomingCall(object sender, pjsip4net.Core.Utils.EventArgs<pjsip4net.Interfaces.ICall> e)
        {
            log.Info("Incoming Call from " + e.Data.RemoteInfo + Environment.NewLine);
        }
        private static void CallManager_CallStateChanged(object sender, CallStateChangedEventArgs e)
        {
            log.Info(e.MediaState + " " + e.DestinationUri + " " + e.Duration.TotalMinutes + Environment.NewLine);
            log.Info(e.Id + Environment.NewLine);
        }
        private static void Accounts_AccountStateChanged(object sender, AccountStateChangedEventArgs e)
        {
            log.Info(Environment.NewLine + "Account State has changed!" + Environment.NewLine);
            log.Info(e.StatusText + Environment.NewLine);
        }
        #endregion

        #region "SIP-Account Registration"
        private bool register()
        {
            try
            {
                //frank: terugzetten
                ua = BuildUserAgent.Build(BuildUserAgent.Build(ConfigureVersion_1_4.WithVersion_1_4(Configure.Pjsip4Net(new MyConfigurator())))); //  .Pjsip4Net.With(new MyConfigurator()))));
                ua.Log += new EventHandler<LogEventArgs>(// TODO: Warning!!!! NULL EXPRESSION DETECTED...            );
                ua.CallManager.CallStateChanged += new EventHandler<CallStateChangedEventArgs>(// TODO: Warning!!!! NULL EXPRESSION DETECTED...            );
                ua.AccountManager.AccountStateChanged += new EventHandler<AccountStateChangedEventArgs>(// TODO: Warning!!!! NULL EXPRESSION DETECTED...            );
                ua.CallManager.IncomingCall += new EventHandler<pjsip4net.Core.Utils.EventArgs<pjsip4net.Interfaces.ICall>>();// TODO: Warning!!!! NULL EXPRESSION DETECTED...            );
                return true;
            }
            catch (PjsipErrorException ex)
            {
                log.Error(ex.Message);
                return false;
            }
            catch (SystemException se)
            {
                log.Error(se.Message);
                return false;
            }

        }
        //private bool register()
        //{
        //    try
        //    {
        //        ua = BuildUserAgent.Start(BuildUserAgent.Build(ConfigureVersion_1_4.WithVersion_1_4(Configure.Pjsip4Net.With(new MyConfigurator()))));
        //        //ua = BuildUserAgent.Build(BuildUserAgent.Build(ConfigureVersion_1_4.WithVersion_1_4()));// . .Pjsip4Net.With(new MyConfigurator()))));


        //        ua.Log += new EventHandler<LogEventArgs>(intLog);
        //        ua.CallManager.CallStateChanged += new EventHandler<CallStateChangedEventArgs>(FrmMain.CallManager_CallStateChanged);
        //        ua.AccountManager.AccountStateChanged += new EventHandler<AccountStateChangedEventArgs>(FrmMain.Accounts_AccountStateChanged);
        //        ua.CallManager.IncomingCall += new EventHandler<pjsip4net.Core.Utils.EventArgs<pjsip4net.Interfaces.ICall>>(FrmMain.incomingCall);
        //        return true;
        //    }
        //    catch (PjsipErrorException ex)
        //    {
        //        log.Error(ex.Message);
        //        return false;
        //    }
        //    catch (SystemException se)
        //    {

        //        log.Error(se.Message);
        //        return false;
        //    }
        //}

        private void unregister()
        {
            ua.Log -= intLog;
            ua.CallManager.CallStateChanged -= FrmMain.CallManager_CallStateChanged;
            ua.AccountManager.AccountStateChanged -= FrmMain.Accounts_AccountStateChanged;
            ua.CallManager.IncomingCall -= FrmMain.incomingCall;
            ua.Dispose();
        }
        #endregion


        /// <summary>
        /// laadt de beschikbare files (ALS TEST)
        /// </summary>
        private void GetAvailableList()
        {
            try
            {
                using (UNET_Server_Reference.Service1Client reference = new UNET_Server_Reference.Service1Client())
                {
                    reference.Open();

                    cbxDownloads.Items.AddRange(reference.GetAvailableFiles());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error retrieving file list");
                // throw;
            }
        }

        /// <summary>
        /// Get roles (TEST method!)
        /// </summary>
        private void GetRoles()
        {
            try
            {
                using (UNET_Server_Reference.Service1Client reference = new UNET_Server_Reference.Service1Client())
                {
                    reference.Open();

                    cbxRoles.Items.AddRange(reference.GetTestRoles());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error retrieving Roles");
                // throw;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //registreer methodes
            btnRegister.Enabled = false;
            if (_registered)
            {
                this.unregister();
                btnRegister.Text = "Registreer!";
                _registered = false;
            }
            else {
                if (this.register())
                {
                    btnRegister.Text = "Stop!";
                    _registered = true;
                    log.Info("Account successfully geregistreerd." + Environment.NewLine);
                }
                else {
                    log.Info("Account could not be registered." + Environment.NewLine);
                }
            }
            btnRegister.Enabled = true;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_registered)
            {
                this.unregister();
            }
        }
    }

    public class MyConfigurator : IConfigurationProvider
    {

        public void Configure(IConfigurationContext context)
        {
            //string registrar = ("sip:" + FrmMain.txtDomain.Text);
            //string accountId = new SipUriBuilder().AppendDomain(FrmMain.txtDomain.Text).AppendExtension(FrmMain.txtUser.Text).ToString();
            //string proxy = ("sip:" + FrmMain.txtProxy.Text);
            //AccountConfig[] accountConfigArray = new AccountConfig[1 - 1];
            //AccountConfig accountConfig = new AccountConfig();
            //accountConfig.RegUri = registrar;
            //accountConfig.Id = accountId;
            //List<NetworkCredential> networkCreds = new List<NetworkCredential>();
            //networkCreds.Add(new NetworkCredential(FrmMain.txtUser.Text, FrmMain.txtPasswd.Text, FrmMain.txtDomain.Text));
            //accountConfig.Credentials = networkCreds;
            //List<string> strs = new List<string>();
            //strs.Add(proxy);
            //accountConfig.Proxy = strs;
            //accountConfigArray[0] = accountConfig;
            //context.RegisterAccounts(accountConfigArray);
        }
    }

}
