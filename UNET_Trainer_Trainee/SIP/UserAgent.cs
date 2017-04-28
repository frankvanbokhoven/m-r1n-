using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer_Trainee.SIP
{
   public class UserAgent
    {
     private PJSUA2.Endpoint ep;
     private  Config conf;
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

   //public void UserAgentStart()
   // {

   //  //todo   std::cout << "Starting User Agent" << std::endl;

   //     // Create endpoint
   //     try
   //     {
   //       PJSUA2.Endpoint  ep = new PJSUA2.Endpoint();
   //         ep.libCreate();
   //     }
   //     catch (Exception ex) {
   //      //todo   std::cout << "Exception " << err.info() << std::endl;
   //     }

   //     // Init library
   //     try
   //     {
   //         PJSUA2.EpConfig ep_cfg = new PJSUA2.EpConfig();//hier is de new erbijgezet
   //         ep_cfg.logConfig.level = conf.getLogLevel(); // Default = 4
   //         ep.libInit(ep_cfg);
   //     }
   //     catch (Exception ex)
   //     {
   //      //todo   std::cout << "Initialization error: " << err.info() << std::endl;
   //     }

   //     // Create transport
   //     try
   //     {
   //         PJSUA2.TransportConfig tcfg = new PJSUA2.TransportConfig();//frank: hier is de new erbij gezet
   //         tcfg.port = conf.getPort();
   //         ep.transportCreate(PJSUA2.pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);
   //     }
   //     catch (Exception ex) {
   //       //todo  std::cout << "Transport creation error: " << err.info() << std::endl;
   //     }

   //     // Start library
   //     try
   //     {
   //         ep.libStart();
   //     }
   //     catch (Exception ex) {
   //      //todo   std::cout << "Startup error: " << err.info() << std::endl;
   //     }

   //     // Configure sound devices
   //     configureSoundDevices();

   //     // Create account configuration
   //     PJSUA2.AccountConfig acc_cfg;
   //     string accountName = "sip:" + conf.getSipAccount().toStdString() + "@" + conf.getSipDomain().toStdString();
   //     string sipServer = "sip:" + conf.getSipServer().toStdString();
   //     acc_cfg.idUri = accountName;

   //     acc_cfg.regConfig.registrarUri = sipServer;
   //     acc_cfg.regConfig.timeoutSec = conf.getSipTimeOut();
   //     acc_cfg.regConfig.retryIntervalSec = conf.getSipRetry();

   //     // Set server proxy
   //     PJSUA2.StringVector proxy = acc_cfg.sipConfig.proxies;
   //     proxy.push_back(sipServer + ";transport=udp");

   //     acc_cfg.sipConfig.proxies = proxy;
   //     acc_cfg.sipConfig.authCreds.push_back(AuthCredInfo("digest", conf.getSipServer().toStdString(),
   //                                                         conf.getSipAccount().toStdString(), 0, "password"));

   //     // Create SIP account
   //     //std::auto_ptr<UAAccount> acc(new UAAccount);
   //     acc = new SipAccount();
   //     acc.create(acc_cfg);

   //     // Create & set presence
   //     setPresence(acc);

   //         // Create buddies
   //         PJSUA2.BuddyConfig pCfg = new PJSUA2.BuddyConfig();//hier is de new erbijgezet
   //         PJSUA2.BuddyConfig sCfg = new PJSUA2.BuddyConfig();//hier is de new erbijgezet
   //     SipBuddy platformBuddy = new SipBuddy("Platform", conf.getSipDomain(), acc);
   //     SipBuddy serverBuddy = new SipBuddy("Server", conf.getSipDomain(), acc);

   //     pCfg.uri = "sip:" + platformBuddy.getName().ToString() + "@" + conf.getSipDomain().toStdString();
   //     sCfg.uri = "sip:" + serverBuddy.getName().ToString() + "@" + conf.getSipDomain().toStdString();

   //     try
   //     {
   //         platformBuddy.create(acc, pCfg);
   //         platformBuddy.subscribePresence(true);

   //         serverBuddy.create(*acc, sCfg);
   //         serverBuddy.subscribePresence(true);

   //         buddies.append(platformBuddy);
   //         buddies.append(serverBuddy);
   //     }
   //     catch (Exception ex) {
   //      //todo   std::cout << err.info() << std::endl;
   //     }

   //     // Connect signals & slots
   //     connect(acc, SIGNAL(sendNewCallState(int)), this, SLOT(receiveNewCallState(int)));
   //     connect(acc, SIGNAL(sendNewRegState(int)), this, SLOT(receiveNewRegState(int)));
   //     connect(acc, SIGNAL(sendNewIM(QString)), this, SLOT(receiveNewIM(QString)));
   //     }

        /*!
         * \brief UserAgent::UserAgentStop
         */
      public void UserAgentStop()
    {

          //todo  std::cout << "Stopping endpoint" << std::endl;

            // Register thread if necessary
            if (!ep.libIsThreadRegistered())
                ep.libRegisterThread("program thread");

            // Disconnect account
         //   acc.disconnect();

            // Stop endpoint
            ep.libDestroy();

            // Send new state
       //     emit forwardNewRegState(-2);
        }

        /*!
         * \brief UserAgent::setPresence
         * \param acc
         * \param status
         */
        public void setPresence(SipAccount acc, PJSUA2.pjsua_buddy_status status) {

            try
            {

                PJSUA2.PresenceStatus ps = new PJSUA2.PresenceStatus();
                ps.status = status;

                // Optional, set the activity and some note
                //    ps.activity = PJRPID_ACTIVITY_BUSY;
                //    ps.note = "On the phone";

             //   acc.setOnlineStatus(ps);
            }
            catch (Exception ex)
            {

                //todo    std::cout << "*** Presence Error: " << err.reason << std::endl;
            }
        }

            /*!
             * \brief UserAgent::configureSoundDevices
             */
          public  void configureSoundDevices() {

                // Configure codecs

                // Set L16 to highest priority
                ep.codecSetPriority("L16/44100/1", 255);

                // Set G722 to first fallback
                ep.codecSetPriority("G722/16000/1", 253);

                // Set Speex to second fallback
                ep.codecSetPriority("speex/16000/1", 250);

                // Disable GSM codec
                ep.codecSetPriority("GSM/8000/1", 0);

                PJSUA2.CodecInfoVector civ = ep.codecEnum();

             //todo:   std::cout << "<--- Start codec list --->" << std::endl;

              //  for (uint i = 0; i < civ.size(); i++)
               // {
               //     PJSUA2.CodecInfo ci = civ.at(i);
                 //todo   std::cout << "ID: " << ci->codecId << "\tPriority: " << (uint)ci->priority << "\tDesc: " << ci->desc << std::endl;
               // }

             //todo   std::cout << "<--- End codec list --->" << std::endl;

//# ifdef ISLINUX
//                // Set playback and capture devices
//                AudioDevInfoVector v = ep->audDevManager().enumDev();
//                /*
//                  std::cout << "<--- Start device list --->" << std::endl;

//                  for(uint i=0;i<v.size();i++) {

//                    AudioDevInfo *dev = v.at(i);

//                    std::cout << "Name: " << dev->name << std::endl;
//                    std::cout << "\tID: " << i << std::endl;
//                    std::cout << "\tChannels: " << dev->outputCount << std::endl;
//                    std::cout << "\tDriver: " << dev->driver << std::endl;
//                  }

//                  std::cout << "<--- End device list --->" << std::endl;
//                */
//                const QString capDevice = "mic-in-plug";
//                const QString playDevice = "uwt-plug-out";
//                const QString driver = "PA";

//                try
//                {
//                    int pD = ep->audDevManager().lookupDev(driver.toStdString(), playDevice.toStdString());
//                    ep->audDevManager().setPlaybackDev(pD);
//                    std::cout << "### Playback Device set " << pD << std::endl;
//                }
//                catch (Exception ex) {

//                    std::cout << "*** Could not set Playback Device: " << err.reason << std::endl;
//                }

//                try
//                {
//                    int cD = ep->audDevManager().lookupDev(driver.toStdString(), capDevice.toStdString());
//                    ep->audDevManager().setCaptureDev(cD);
//                    std::cout << "### Capture Device set " << cD << std::endl;
//                }
//                catch (Exception ex) {

//                    std::cout << "*** Could not set Capture Device: " << err.reason << std::endl;
//                }
//#endif

                // Turn off Voice Activation Detection
                //  ep->audDevManager().setVad(false, false);

                // Turn off Echo Cancelation
                ep.audDevManager().setEcOptions(0, 0);

                // Turn off Packet loss concealment
                //  ep->audDevManager().setPlc(false, true);

                // Turn off Comfort noise generator
                //  ep->audDevManager().setCng(false, true);

                //ep->audDevManager().setExtFormat();

                //--------------------------------------
                //  ep->audDevManager().getCaptureDev

                //  std::cout << "###" << "VAD Check" << std::endl;
                //  std::cout << "###" << (ep->audDevManager().getVad() ? "VAD Detected" : "VAD Not Detected") << std::endl;
                }

                /*!
                 * \brief UserAgent::run
                 */
              public  void run() {

                }

                // Slots:

                /*!
                 * \brief UserAgent::receiveNewCallState
                 * \param state
                 */
             // public  void receiveNewCallState(int state) {

             //       emit forwardNewCallState(state);
             //   }

             //   /*!
             //    * \brief UserAgent::receiveNewRegState
             //    * \param state
             //    */
             // public  void receiveNewRegState(int state) {

             //       emit forwardNewRegState(state);
             //   }

             //   /*!
             //    * \brief UserAgent::receiveNewIM
             //    * \param IM
             //    */
             // public  void receiveNewIM(String IM) {
             //       emit forwardNewIM(IM);
             //   }

             //   /*!
             //    * \brief UserAgent::receiveIMRequest
             //    * \param destination
             //    * \param message
             //    */
             //public void receiveIMRequest(String destination, String message) {

             //       // Find buddy matching destination
             //       for (int i = 0; i < buddies.Count -1; i++)
             //       {

             //           if (buddies.at(i)->getName().compare(destination, Qt::CaseInsensitive) == 0)
             //           {

             //               SendInstantMessageParam prm;
             //               prm.content = message.toStdString();
             //               buddies.at(i)->sendInstantMessage(prm);
             //               break;
             //           }
             //       }
             //   }

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

