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
           public void forwardNewCallState(int state)
        { //todo: implementern
        }
           public void forwardNewRegState(int state)
        {
            //todo: implementeren
        }
           public void forwardNewIM(String IM)
        {
            //todo: implementeren
        }
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
                log.Error("Initialization error: " + ex.Message);
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
               log.Error( "Transport creation error: " +  ex.Message);
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
            proxy.Add(sipServer + ";transport=udp"); //todo: was: push_back

            acc_cfg.sipConfig.proxies = proxy;
            acc_cfg.sipConfig.authCreds.Add(new AuthCredInfo("digest", ConfigurationManager.AppSettings["SIPSever"].ToString(), ConfigurationManager.AppSettings["SIPAccount"].ToString(), 0, "password")); //todo: was: push_back

            // Create SIP account
            //std::auto_ptr<UAAccount> acc(new UAAccount);
            acc = new SipAccount();
            acc.create(acc_cfg);

            // Create & set presence
            setPresence(acc, pjsua_buddy_status.PJSUA_BUDDY_STATUS_ONLINE);
            // Create buddies
            BuddyConfig pCfg = new BuddyConfig();//hier is de new erbijgezet
            BuddyConfig sCfg = new BuddyConfig();//hier is de new erbijgezet
            SipBuddy platformBuddy = new SipBuddy("Platform", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
            SipBuddy serverBuddy = new SipBuddy("Server", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);

            pCfg.uri = "sip:" + platformBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
            sCfg.uri = "sip:" + serverBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();

            try
            {
                platformBuddy = new Buddy("Naam van de platform buddy", pCfg.uri.ToString(),acc);
                platformBuddy.subscribePresence(true);

                serverBuddy = new SipBuddy("Naam van de server buddy", sCfg.uri.ToString(), acc);
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
            connect(acc, SIGNAL(sendNewIM(String)), this, SLOT(receiveNewIM(String)));
        }


         /// <summary>
         /// brief UserAgent::UserAgentStop
         /// </summary>
        public void UserAgentStop()
        {

            Console.Write("Stopping endpoint");

            //  Register thread if necessary
            //  if (!ep.libIsThreadRegistered()) //todo: die 'if' moet terug anders wordt te vaak geregistreerd
                ep.libRegisterWorkerThread("program thread");// .libRegisterThread("program thread");

            // Disconnect account;
            acc.Dispose();// .disconnect();

            //  Stop endpoint
            ep.libDestroy();

            // Send new state
            forwardNewRegState(-2);
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
            // Configure codecs

            // Set L16 to highest priority
            ep.codecSetPriority("L16/44100/1", 255);

            // Set G722 to first fallback
            ep.codecSetPriority("G722/16000/1", 253);

            // Set Speex to second fallback
            ep.codecSetPriority("speex/16000/1", 250);

            // Disable GSM codec
            ep.codecSetPriority("GSM/8000/1", 0);

            pjsua2.CodecInfoVector civ = ep.codecEnum();

            log.Info("<--- Start codec list --->");

            for (int i = 0; i < civ.Count -1; i++)
            {
                CodecInfo ci = civ[i];
                log.Info("ID: " + ci.codecId + "\tPriority: " + ci.priority.ToString() + "\tDesc: " + ci.desc);
                Console.Write("ID: " + ci.codecId + "\tPriority: " + ci.priority.ToString() + "\tDesc: " + ci.desc);
            }

            log.Info("<--- End codec list --->");
            Console.Write("<--- End codec list --->");

            //    Turn off Voice Activation Detection
            ep.audDevManager().setVad(false, false);

            //    Turn off Echo Cancelation
            ep.audDevManager().setEcOptions(0, 0);

            //   Turn off Packet loss concealment
            ep.audDevManager().setPlc(false, true);

            //    Turn off Comfort noise generator
            ep.audDevManager().setCng(false, true);

            MediaFormatAudio aud = new MediaFormatAudio(); //todo: dit ding initialiseren
            
            ep.audDevManager().setExtFormat(aud, true);

            //   --------------------------------------
            ep.audDevManager().getCaptureDev();

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

            forwardNewCallState(state);
        }

        /*!
         * \brief UserAgent::receiveNewRegState
         * \param state
         */
        public void receiveNewRegState(int state)
        {

            forwardNewRegState(state);
        }

        /*!
         * \brief UserAgent::receiveNewIM
         * \param IM
         */
        public void receiveNewIM(String IM)
        {
            forwardNewIM(IM);
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

                if (buddies[i].getName().ToLower().CompareTo(destination) == 0)  //.compare(destination, Qt::CaseInsensitive) == 0)
                {

                    SendInstantMessageParam prm = new SendInstantMessageParam();
                    prm.content = message;
                //todo: terugzetten    buddies[i].sendInstantMessage(prm);
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

