using System;
using System.Collections.Generic;
using pjsua2;
using System.Configuration;
using System.IO;
using System.Reflection;


namespace PJSUA2Implementation.SIP
{
    public  class UserAgent
    {
 
        public pjsua2.Endpoint ep;
        public SipAccount acc;


        private AudioMedia play_med;
        private AudioMedia cap_med;

        // signals
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
        public string Account { get; set; }
        public string DisplayName { get; set; }
        public string SipServer { get; set; }
        public int Port { get; set; }
        public string Domain { get; set; }
        public string Password { get; set; }



        ///
        /// brief UserAgent::~UserAgent
        ////
        public UserAgent()
        {
            Account = ConfigurationManager.AppSettings["SIPAccount"].ToString();
            DisplayName = ConfigurationManager.AppSettings["displayname"].ToString();
            Domain = ConfigurationManager.AppSettings["SIPDomain"].ToString();
            SipServer = string.Format("sip:{0}", ConfigurationManager.AppSettings["SIPServer"]);
            Password = ConfigurationManager.AppSettings["sipPassword"];
            Port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
        }

        /// <summary>
        /// Constructor that fills the most important parameters
        /// </summary>
        /// <param name="_account"></param>
        /// <param name="_sipserver"></param>
        /// <param name="_port"></param>
        /// <param name="_domain"></param>
        public UserAgent(string _account, string _sipserver, int _port, string _domain, string _password, string _displayname)
        {
            Account = _account;
            DisplayName = _displayname;
            SipServer = _sipserver;
            Port = _port;
            Domain = _domain;
            Password = _password;

            
        }



        /// <summary>
        /// Determine an always unique string
        /// find out a nice threadname, this HAS to be unique for the system. so even if two instances of the same
            //  app start, this name must still be unique
            /// <param name="_namepart"></param>
            /// <returns></returns>
        private string RandomThreadString(string _namepart)
        {
            string result = _namepart + "_" + Guid.NewGuid()
                .ToString()
                .Replace("-", "")
                .Substring(0, 10);
            return result;
        }
         

        /// <summary>
        /// Init the pjsua2 and start it
        /// </summary>
        public void UserAgentStart(string _threadName)
        {
            // Create endpoint
            try
            {
                if (ep == null)
                {
                    ep = new Endpoint();
                    ep.libCreate();
              //      ep.libRegisterThread(RandomThreadString("PJSUA2" + _threadName));
                }
            }
            catch (Exception ex)
            {
               Logging.LogAppender.AppendToLog("Useragent libcreate Exception: " + ex.Message + ex.InnerException);

                Console.Write("Useragent libcreate Exception: " + ex.Message, ex);
             }
            // Init library
            EpConfig ep_cfg = new EpConfig();//hier is de new erbijgezet
            ep_cfg.logConfig.level = Convert.ToUInt16(ConfigurationManager.AppSettings["LogLevel"]); // Default = 4
            ep_cfg.uaConfig.maxCalls = Convert.ToUInt16(ConfigurationManager.AppSettings["maxcalls"]);
            ep_cfg.medConfig.sndClockRate = Convert.ToUInt16(ConfigurationManager.AppSettings["sndClockRate"]);
            ep_cfg.logConfig.filename = "pjsip_" + DateTime.Today.Date.ToString("yyMMdd") + ".log";
            ep.libInit(ep_cfg);
            // Configure Audio Interface
            try
            {
                ep.Media_Configure_Audio_Interface("ASIO4ALL v2");

                play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();

            }
            catch (Exception ex)
            {
               Logging.LogAppender.AppendToLog("Useragent audiointerface start Exception: " + ex.Message + ex.InnerException);

                Console.Write("Useragent AudioInterface start Exception: " + ex.Message, ex);
            }
            // Create transport
            try
            {
                TransportConfig tcfg = new TransportConfig();
                tcfg.port = Convert.ToUInt16(Port);
                ep.transportCreate(pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);

            }
            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Useragent transport start Exception: " + ex.Message + ex.InnerException);

                Console.Write("Useragent Transport start Exception: " + ex.Message, ex);
            }

            // Start library
            try
            {
                ep.libStart();
                Logging.LogAppender.AppendToLog("libstart successfull");

            }
            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Useragent libstart Exception: " + ex.Message + ex.InnerException);

                Console.Write("Useragent libstart Exception: " + ex.Message, ex);
            }

            try
            {
                // Create & set presence
                // Create account configuration
                AccountConfig acfg = new AccountConfig();
                acfg.idUri = DisplayName + " <sip:" + Account + "@" + Domain + ">";
                  Logging.LogAppender.AppendToLog("Account info: " + acfg.idUri  +  "  SipServer:  " + SipServer);

                string sipserver = string.Format("sip:{0}", SipServer);
                acfg.regConfig.registrarUri = sipserver;
                acfg.regConfig.registerOnAdd = true;

                acfg.regConfig.timeoutSec = Convert.ToUInt16(ConfigurationManager.AppSettings["Timeout"]);
                acfg.regConfig.retryIntervalSec = Convert.ToUInt16(ConfigurationManager.AppSettings["SIPRetry"]);
                AuthCredInfo cred = new AuthCredInfo("digest", sipserver, Account, 0, Password);
                cred.realm = Domain;
                acfg.regConfig.registerOnAdd = true;
                acfg.regConfig.timeoutSec = 180;
               

                acfg.sipConfig.authCreds.Add(cred);
                acfg.regConfig.dropCallsOnFail = true;
                Logging.LogAppender.AppendToLog("Account ready to be added: " +acfg.idUri);

                // Create SIP account
                acc = new SipAccount();
                acc.create(acfg, true);
                setPresence(acc, pjsua_buddy_status.PJSUA_BUDDY_STATUS_ONLINE);
                Logging.LogAppender.AppendToLog("Account " + acfg.idUri + " successfully added!");

            }
            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Useragent start Exception: " + ex.Message + ex.InnerException + Environment.NewLine + ex.StackTrace.ToString());

                Console.Write("Useragent start Exception: " + ex.Message, ex);

            }
    
        }
        #region SIPBuddy
        /// <summary>
        /// Create buddies
        /// </summary>
        //  public void UserBuddyStart()
        //  {
        //// 
        //BuddyConfig pCfg = new BuddyConfig();
        //BuddyConfig sCfg = new BuddyConfig();
        //SipBuddy platformBuddy = new SipBuddy("Platform", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
        //SipBuddy serverBuddy = new SipBuddy("Server", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
        //pCfg.uri = "sip:" + platformBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
        //sCfg.uri = "sip:" + serverBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
        //Console.Write("Platform buddy: " + pCfg.uri + " Serverbuddy: " + sCfg.uri);
        //try
        //{
        //    //  platformBuddy = new SipBuddy("Naam van de platform buddy", pCfg.uri.ToString(), acc);
        //    Console.Write("Subscribing platform buddy...");
        //    platformBuddy.create(acc, pCfg);
        //    platformBuddy.subscribePresence(true);

        //    //  serverBuddy = new SipBuddy("Naam van de server buddy", sCfg.uri.ToString(), acc);
        //    Console.Write("Subscribing server buddy....");
        //    serverBuddy.create(acc, sCfg);
        //    serverBuddy.subscribePresence(true);

        //    buddies.Add(platformBuddy);
        //    buddies.Add(serverBuddy);

        //    acc.addBuddy(platformBuddy);
        //    acc.addBuddy(serverBuddy);
        //}
        //catch (Exception ex)
        //{
        //    log.Error(ex.Message);
        //}
        // }
        #endregion

        /// <summary>
        /// brief UserAgent::UserAgentStop
        /// </summary>
        public void UserAgentStop()
        {
            Console.Write("Stopping endpoint");

            ///this code destroys the SIP connection and clears the relevant objects
            try
            {
                ep.mediaRemove(play_med);
                ep.mediaRemove(cap_med);
                //dispose all sip objects, so they can be garbage collected
                
                ep.libStopWorkerThreads();
                ep.libDestroy();
                ep.Dispose();
                //// Send new state
                forwardNewRegState(-2);

                //force garbage collection of all disposed objects
                GC.Collect();
            }
            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Error UN-registering SIP connection: " + ex.Message);


             }
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
                //  ps.activity = pjrpid_activity.PJRPID_ACTIVITY_BUSY;
                //  ps.note = "On the phone";

                acc.setOnlineStatus(ps);
            }
            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("*** Presence Error: " + ex.Message + ex.InnerException);
            }
        }


        ///*!
        // * \brief UserAgent::configureSoundDevices
        // */
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

            Console.Write("<--- Start codec list --->");

            for (int i = 0; i < civ.Count - 1; i++)
            {
                CodecInfo ci = civ[i];
                Console.Write("ID: " + ci.codecId + "\tPriority: " + ci.priority.ToString() + "\tDesc: " + ci.desc);
            }

            Console.Write("<--- End codec list --->");
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

            Console.Write("###" + "VAD Check");
            Console.Write("###" + (ep.audDevManager().getVad() ? "VAD Detected" : "VAD Not Detected"));
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
        //public void receiveIMRequest(String destination, String message)
        //{

        //    // Find buddy matching destination
        //    for (int i = 0; i < buddies.Count - 1; i++)
        //    {

        //        if (buddies[i].getName().ToLower().CompareTo(destination) == 0)  //.compare(destination, Qt::CaseInsensitive) == 0)
        //        {

        //            SendInstantMessageParam prm = new SendInstantMessageParam();
        //            prm.content = message;
        //            //todo: terugzetten    buddies[i].sendInstantMessage(prm);
        //            break;
        //        }
        //    }
        //}
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

