using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;
using System.Configuration;

namespace UNET_Trainer_Trainee.SIP
{
    public class UserAgent
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private pjsua2.Endpoint ep;
        private Config conf;
        private SipAccount acc;
        private List<SipBuddy> buddies;


        // signals:
        //   public void forwardNewCallState(int state);
        //   public void forwardNewRegState(int state);
        //   public void forwardNewIM(QString IM);
        /*!
 * \brief UserAgent::UserAgent
 * \param config
 */
        public UserAgent(Config config)
        {
            conf = config;
        }
        /*!
         * \brief UserAgent::~UserAgent
         */
        public UserAgent()
        {

            //hoeft niet in c#  delete acc;
            //hoeft niet in c#  delete ep;
        }

        public void UserAgentStart()
        {

            //todo   std::cout << "Starting User Agent" << std::endl;

            // Create endpoint
            try
            {
                Endpoint ep = new Endpoint();
                ep.libCreate();
            }
            catch (Exception ex)
            {
                log.Error("Exception on Agent Start " + ex.Message);
            }

            // Init library
            try
            {
                EpConfig ep_cfg = new EpConfig();//hier is de new erbijgezet
                ep_cfg.logConfig.level = Convert.ToUInt16(ConfigurationManager.AppSettings["LogLevel"]); // Default = 4
                ep.libInit(ep_cfg);
            }
            catch (Exception ex)
            {
                //todo   std::cout << "Initialization error: " << err.info() << std::endl;
            }

            // Create transport
            try
            {
                TransportConfig tcfg = new TransportConfig();//frank: hier is de new erbij gezet
                tcfg.port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
                ep.transportCreate(pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);
            }
            catch (Exception ex)
            {
                //todo  std::cout << "Transport creation error: " << err.info() << std::endl;
            }

            // Start library
            try
            {
                ep.libStart();
            }
            catch (Exception ex)
            {
                log.Error("Startup error: " + ex.Message);
            }

            // Configure sound devices
            configureSoundDevices();

            // Create account configuration
            AccountConfig acc_cfg = new AccountConfig();
            string accountName = "sip:" + ConfigurationManager.AppSettings["SIPAccount"].ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
            string sipServer = "sip:" + ConfigurationManager.AppSettings["SIPServer"].ToString();
            acc_cfg.idUri = accountName;

            acc_cfg.regConfig.registrarUri = sipServer;
            acc_cfg.regConfig.timeoutSec = Convert.ToUInt16(ConfigurationManager.AppSettings["Timeout"]); //conf.getSipTimeOut();
            acc_cfg.regConfig.retryIntervalSec = Convert.ToUInt16(ConfigurationManager.AppSettings["SIPRetry"]);

            // Set server proxy
            StringVector proxy = acc_cfg.sipConfig.proxies;
            proxy.push_back(sipServer + ";transport=udp");

            acc_cfg.sipConfig.proxies = proxy;
            acc_cfg.sipConfig.authCreds.push_back(AuthCredInfo("digest", ConfigurationManager.AppSettings["SIPSever"].ToString(), ConfigurationManager.AppSettings["SIPAccount"].ToString(), 0, "password"));

            // Create SIP account
            //std::auto_ptr<UAAccount> acc(new UAAccount);
            acc = new SipAccount();
            acc.create(acc_cfg);

            // Create & set presence
            setPresence(acc);

            // Create buddies
            BuddyConfig pCfg = new BuddyConfig();//hier is de new erbijgezet
            BuddyConfig sCfg = new BuddyConfig();//hier is de new erbijgezet
            SipBuddy platformBuddy = new SipBuddy("Platform", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
            SipBuddy serverBuddy = new SipBuddy("Server", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);

            pCfg.uri = "sip:" + platformBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
            sCfg.uri = "sip:" + serverBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();

            try
            {
                platformBuddy.create(acc, pCfg);
                platformBuddy.subscribePresence(true);

                serverBuddy.create(acc, sCfg);
                serverBuddy.subscribePresence(true);

                buddies.Add(platformBuddy);
                buddies.Add(serverBuddy);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            // Connect signals & slots
            connect(acc, SIGNAL(sendNewCallState(int)), this, SLOT(receiveNewCallState(int)));
            connect(acc, SIGNAL(sendNewRegState(int)), this, SLOT(receiveNewRegState(int)));
            connect(acc, SIGNAL(sendNewIM(QString)), this, SLOT(receiveNewIM(QString)));
        }

        /*!
         * \brief UserAgent::UserAgentStop
         */
        public void UserAgentStop()
        {

            Console.Write("Stopping endpoint");

            //   Register thread if necessary
            if (!ep.libIsThreadRegistered())
                ep.libRegisterThread("program thread");

            // Disconnect account;
            acc.disconnect();

            //  Stop endpoint
            ep.libDestroy();

            // Send new state
            emit forwardNewRegState(-2);
        }

        /*!
         * \brief UserAgent::setPresence
         * \param acc
         * \param status
         */
        public void setPresence(SipAccount acc, pjsua2.pjsua_buddy_status status)
        {

            try
            {

                pjsua2.PresenceStatus ps = new pjsua2.PresenceStatus();
                ps.status = status;

                // Optional, set the activity and some note
                ps.activity = pjrpid_activity.PJRPID_ACTIVITY_BUSY;
                ps.note = "On the phone";

                acc.setOnlineStatus(ps);
            }
            catch (Exception ex)
            {

                Console.Write("*** Presence Error: " + ex.Message + ex.InnerException);
            }
        }

        /*!
         * \brief UserAgent::configureSoundDevices
         */
        public void configureSoundDevices()
        {

            //  Configure codecs

            //      Set L16 to highest priority
            ep.codecSetPriority("L16/44100/1", 255);

            //  Set G722 to first fallback
            ep.codecSetPriority("G722/16000/1", 253);

            //   Set Speex to second fallback
            ep.codecSetPriority("speex/16000/1", 250);

            //  Disable GSM codec
            ep.codecSetPriority("GSM/8000/1", 0);

            pjsua2.CodecInfoVector civ = ep.codecEnum();

            log.Info("<--- Start codec list --->");

            for (uint i = 0; i < civ.size(); i++)
            {
                CodecInfo ci = civ.at(i);
                log.Info("ID: " << ci->codecId << "\tPriority: " << (uint)ci->priority << "\tDesc: " << ci->desc << std::endl;
            }

            log.Info("<--- End codec list --->)");


            //    Turn off Voice Activation Detection
            ep.audDevManager().setVad(false, false);

            //    Turn off Echo Cancelation
            ep.audDevManager().setEcOptions(0, 0);

            //   Turn off Packet loss concealment
            ep.audDevManager().setPlc(false, true);

            //    Turn off Comfort noise generator
            ep.audDevManager().setCng(false, true);

            ep.audDevManager().setExtFormat();

            //   --------------------------------------
            ep.audDevManager().getCaptureDev;

            log.Info("###" + "VAD Check");
            log.Info("###" + (ep.audDevManager().getVad() ? "VAD Detected" : "VAD Not Detected"));
        }

        /*!
         * \brief UserAgent::run
         */
        public void run()
        {

        }

        #region Slots

        /*!
         * \brief UserAgent::receiveNewCallState
         * \param state
         */
        public void receiveNewCallState(int state)
        {

            emit forwardNewCallState(state);
        }

        /*!
         * \brief UserAgent::receiveNewRegState
         * \param state
         */
        public void receiveNewRegState(int state)
        {

            emit forwardNewRegState(state);
        }

        /*!
         * \brief UserAgent::receiveNewIM
         * \param IM
         */
        public void receiveNewIM(String IM)
        {
            emit forwardNewIM(IM);
        }

        /*!
         * \brief UserAgent::receiveIMRequest
         * \param destination
         * \param message
         */
        public void receiveIMRequest(String destination, String message)
        {

            // Find buddy matching destination
            for (int i = 0; i < buddies.Count - 1; i++)
            {

                if (buddies.at(i).getName().compare(destination, Qt::CaseInsensitive) == 0)
                {

                    SendInstantMessageParam prm = new SendInstantMessageParam();
                    prm.content = message;
                    buddies.at(i)->sendInstantMessage(prm);
                    break;
                }
            }
        }
        #endregion
        /*!
         * \brief UserAgent::receiveInputMute
         * \param mute
         */
        public void receiveInputMute(bool mute)
        {

            // Set input volume to 0 when muted
            // NOTE: mute is inversed
            // TODO: real mute function to keep volume value
            float volume = mute ? 1.0f : 0.0f;

            ep.audDevManager().getCaptureDevMedia().adjustTxLevel(volume);
        }

    }
}

