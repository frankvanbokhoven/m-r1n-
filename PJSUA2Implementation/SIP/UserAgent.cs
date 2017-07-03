﻿using System;
using System.Collections.Generic;
using pjsua2;
using System.Configuration;

namespace PJSUA2Implementation.SIP
{
    public class UserAgent
    {
 
        public pjsua2.Endpoint ep;
        public SipAccount acc;
        public List<SipBuddy> buddies = new List<SipBuddy>();


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

        ///
        /// brief UserAgent::~UserAgent
        ////
        public UserAgent()
        {

            //hoeft niet in c#  delete acc;
            //hoeft niet in c#  delete ep;
        }

        public void UserAgentStart()
        {

            Console.Write("Starting User Agent");

            // Create endpoint
            try
            {
                ep = new Endpoint();
                ep.libCreate();
                ep.libRegisterThread("UNETthread");
            }
            catch (Exception ex)
            {
                Console.Write("Exception on Agent Start " + ex.Message);
            }

            // Init library
            try
            {
                EpConfig ep_cfg = new EpConfig();//hier is de new erbijgezet
                ep_cfg.logConfig.level = Convert.ToUInt16(ConfigurationManager.AppSettings["LogLevel"]); // Default = 4
                ep_cfg.uaConfig.maxCalls = Convert.ToUInt16(ConfigurationManager.AppSettings["maxcalls"]);
                ep_cfg.medConfig.sndClockRate = Convert.ToUInt16(ConfigurationManager.AppSettings["sndClockRate"]);

                ep.libInit(ep_cfg);
            }
            catch (Exception ex)
            {
                Console.Write("Initialization error: " + ex.Message);
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
                Console.Write("Transport creation error: " + ex.Message);
            }

            // Start library
            try
            {
                ep.libStart();
            }
            catch (Exception ex)
            {
                Console.Write("Startup error: " + ex.Message);
            }


            // Create & set presence
            // Create account configuration
            AccountConfig acfg = new AccountConfig();
            acfg.idUri = "sip:" + ConfigurationManager.AppSettings["SIPAccount"].ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
            string sipserver = string.Format("sip:{0}", ConfigurationManager.AppSettings["SIPServer"]);
            acfg.regConfig.registrarUri = sipserver;
            acfg.regConfig.timeoutSec = Convert.ToUInt16(ConfigurationManager.AppSettings["Timeout"]); //conf.getSipTimeOut();
            acfg.regConfig.retryIntervalSec = Convert.ToUInt16(ConfigurationManager.AppSettings["SIPRetry"]);
            AuthCredInfo cred = new AuthCredInfo("digest", ConfigurationManager.AppSettings["sipServer"].ToString(), ConfigurationManager.AppSettings["sipAccount"], 0, "1234");
            acfg.sipConfig.authCreds.Add(cred);
            acfg.regConfig.registerOnAdd = true;
            acfg.regConfig.dropCallsOnFail = true;
            //// Set server proxy
            StringVector proxy = acfg.sipConfig.proxies;
            proxy.Add(sipserver + ";transport=udp");
            acfg.sipConfig.proxies = proxy;
            acfg.sipConfig.authCreds.Add(new AuthCredInfo("digest", ConfigurationManager.AppSettings["sipServer"].ToString(), ConfigurationManager.AppSettings["sipAccount"].ToString(), 0, "1234")); //todo: was: push_back
            // Create SIP account
            acc = new SipAccount();
            acc.create(acfg, true);
            setPresence(acc, pjsua_buddy_status.PJSUA_BUDDY_STATUS_ONLINE);
            // Create buddies
            BuddyConfig pCfg = new BuddyConfig();
            BuddyConfig sCfg = new BuddyConfig();
            SipBuddy platformBuddy = new SipBuddy("Platform", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
            SipBuddy serverBuddy = new SipBuddy("Server", ConfigurationManager.AppSettings["SIPDomain"].ToString(), acc);
            pCfg.uri = "sip:" + platformBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();
            sCfg.uri = "sip:" + serverBuddy.getName().ToString() + "@" + ConfigurationManager.AppSettings["SIPDomain"].ToString();

            try
            {
                //  platformBuddy = new SipBuddy("Naam van de platform buddy", pCfg.uri.ToString(), acc);
                Console.Write("Subscribing platform buddy...");
                platformBuddy.create(acc, pCfg);
                platformBuddy.subscribePresence(true);

                //  serverBuddy = new SipBuddy("Naam van de server buddy", sCfg.uri.ToString(), acc);
                Console.Write("Subscribing server buddy....");
                serverBuddy.create(acc, sCfg);
                serverBuddy.subscribePresence(true);

                buddies.Add(platformBuddy);
                buddies.Add(serverBuddy);

                acc.addBuddy(platformBuddy);
                acc.addBuddy(serverBuddy);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            // Connect signals & slots            
            //connect(acc, SIGNAL(sendNewCallState(int)), this, SLOT(receiveNewCallState(int)));
            //connect(acc, SIGNAL(sendNewRegState(int)), this, SLOT(receiveNewRegState(int)));
            //connect(acc, SIGNAL(sendNewIM(String)), this, SLOT(receiveNewIM(String)));
        }

        /// <summary>
        /// brief UserAgent::UserAgentStop
        /// </summary>
        public void UserAgentStop()
        {
            Console.Write("Stopping endpoint");
            //  Register thread if necessary
            // if (!ep.libIsThreadRegistered()) //todo: die 'if' moet terug anders wordt te vaak geregistreerd
            //ep.libStopThreads();// ("program thread");// .libRegisterThread("program thread");

            //// Disconnect account;
            //acc.Dispose();

            ////  Stop endpoint
            //ep.libDestroy();

            //// Send new state
            //forwardNewRegState(-2);



            ///this code destroys the SIP connection and clears the relevant objects
            try
            {
                //dispose all sip objects, so they can be garbage collected
                ///  if (!object.ReferenceEquals(acc, null))
                //  {
                //     acc.Dispose();
                // }

                ep.libDestroy();
                ep.Dispose();

                //force garbage collection of all disposed objects
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.Write("Error UN-registering SIP connection", ex);
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
                Console.Write("*** Presence Error: " + ex.Message + ex.InnerException);
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
