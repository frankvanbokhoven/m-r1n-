﻿using System;
using System.Collections.Generic;
using pjsua2;
using System.Threading;
using System.Configuration;
using UNET_Classes;
using System.ComponentModel;
using UNET_Theming;

namespace PJSUA2Implementation.SIP
{
    public class SipAccount : pjsua2.Account
    {
        public UNET_Classes.SyncList<pjsua2.Call> Calls;
        public delegate void AlertEventHandler(Object sender, AlertEventArgs e);
        public event AlertEventHandler  CallAlert;

        [Description("Deze property houdt de volledige accountnaam vast")]
        public string AccountDescription;

        public SipAccount()
        {
            
            try
            {
                Calls = new SyncList<Call>();
            }
            catch(Exception ex)
            {
                Logging.LogAppender.AppendToLog("Error constructor Sipaccount: " + ex.Message);
            }
        }
        public override void Dispose()
        {
            try
            {
                base.Dispose();
            }
            catch (Win32Exception winex)
            {

                Logging.LogAppender.AppendToLog("Error dispose: " + winex.Message);

            }

            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Error dispose: " + ex.Message);


            }
        }

        /// <summary>
        /// Stuur een melding naar de console
        /// </summary>
        /// <param name="_im"></param>
        public void sendNewIM(String _im)
        {
            Console.WriteLine(string.Format("IM message: {0}", _im));
        }

        /// <summary>
        /// brief SipAccount::removeCall Removes the selected call param call
        /// </summary>
        /// <param name="call"></param>
        public void removeCall(pjsua2.Call call)
        {
            try
            {
                foreach (pjsua2.Call callitr in Calls)
                {
                    if ((RegistryAccess.GetStringRegistryValue(@"UNET", @"debug", "1").Trim() == "1") ? true : false) //toon alleen in debug
                    {
                        Console.WriteLine("*** removed Call: " + callitr.ToString());
                    }
                    callitr.Dispose();
                }

                foreach (Call indcall in Calls)
                {
                    if (indcall.getId() == call.getId()) //hang de call op met de meegegeven id
                    {
                        CallOpParam cop = new CallOpParam();
                        cop.reason = "Frank heeft opgehangen"; //todo: iets zinnigers invullen..
                        indcall.hangup(cop);
                    }
                }
            }
            catch (Win32Exception winex)
            {

                Logging.LogAppender.AppendToLog("Error sipaccount>removecall: " + winex.Message);

            }

            catch (Exception ex)
            {
                Logging.LogAppender.AppendToLog("Error sipaccount>removecall: " + ex.Message);


            }
        }



        /// <summary>
        /// SipAccount::onRegState
        /// Event komt binnen zodra de gebruiker zich registreert
        /// </summary>
       /// <param name="_prm"></param>
        public override void onRegState(pjsua2.OnRegStateParam _prm)
        {
            pjsua2.AccountInfo ai = getInfo();

            switch (_prm.code)
            {
                case pjsip_status_code.PJSIP_SC_OK:
                    Console.WriteLine(ai.regIsActive ? "*** Register: code=" : "*** Unregister: code=" + ai.uri + " " + _prm.status + " " + _prm.reason);
                    break;
                case pjsip_status_code.PJSIP_SC_REQUEST_TIMEOUT:
                    break;
                default:
                    break;
            }

            // Emit the new registration state
            sendNewRegState(Convert.ToInt16(_prm.code));

        }

        public override void onIncomingSubscribe(OnIncomingSubscribeParam _prm)

        {
            base.onIncomingSubscribe(_prm);
            Console.WriteLine("*** Incoming subscription:" + _prm.code);
        }

        public override void onRegStarted(OnRegStartedParam _prm)
        {
            base.onRegStarted(_prm);
            Console.WriteLine("*** Started registration: " + _prm.ToString());

        }

        public override void onMwiInfo(OnMwiInfoParam prm)
        {
            base.onMwiInfo(prm);
        }

        /// <summary>
        ///  SipAccount::onIncomingCall
        ///  Hier wordt een inkommende call afgehandeld.
        ///  We gaan ervan uit dat dezelfde call niet twee keer kan binnenkomen
        /// </summary>
        /// <param name="_prm"></param>
        public override void onIncomingCall(OnIncomingCallParam _prm)
        {
            try
            {
                //hier worden de channels gekoppeld aan de call die wordt opgezet (op deze plaats staan ze er alleen omdat de consturcor anders niet werkt)
                List<InputChannels> lstinputchannels = new List<InputChannels>();
                lstinputchannels.Add(InputChannels.ichMic);
                lstinputchannels.Add(InputChannels.ichSecondMic);
                lstinputchannels.Add(InputChannels.ichThirdMic);


                List<OutputChannels> lstoutputchannels = new List<OutputChannels>();
                lstoutputchannels.Add(OutputChannels.ochLeft);
                lstoutputchannels.Add(OutputChannels.ochRight);
                lstoutputchannels.Add(OutputChannels.ochSpeaker);
                SIPCall call = new SIPCall(this, ref lstinputchannels, ref lstoutputchannels, _prm.callId);
                CallInfo ci = call.getInfo();
                CallOpParam prm = new CallOpParam();
                if ((RegistryAccess.GetStringRegistryValue(@"UNET", @"debug", "1").Trim() == "1") ? true : false) //toon alleen in debug
                {
                    Console.WriteLine("*** Incoming Call: " + ci.remoteUri + " [" + ci.stateText + "]");
                }
                call.Caller_AccountName = ci.remoteUri;

                Calls.Add(call);
                prm.statusCode = (pjsua2.pjsip_status_code)200;
                // Answer the call
                call.answer(prm);

                AlertEventArgs alertEventArgs = new AlertEventArgs();
                alertEventArgs.ID = call.getId();
                alertEventArgs.Caller_AccountName = ci.remoteUri;
                alertEventArgs.CallInfo_Of_Call = call.getInfo();
                //  alertEventArgs.Media_Of_Call = call.getMedia();

                CallAlert(new object(), alertEventArgs); //Dit raised een event dat wordt opgepikt in het FrmMain

            }
            catch (Exception ex)
            {
                Console.WriteLine("*** Error on incoming call: " + ex.Message + ex.InnerException);
            }
        }
   



        /// <summary>
        /// brief SipAccount::onInstantMessage
        /// TODO: LET OP: de override is door Frank bijgevoegd omdat VS dit voorstelde
        /// </summary>
        /// <param name="_prm"></param>
        public override void onInstantMessage(OnInstantMessageParam _prm)
        {

            String message = _prm.msgBody;

            Console.WriteLine("*** Incoming IM: " + _prm.msgBody);

            sendNewIM(message);
        }

        /// <summary>
        /// brief SipAccount::newCallState
        /// </summary>
        /// <param name="state"></param>
        public void newCallState(int state)
        {

            sendNewCallState(state);
        }

        // signals:
        public void sendNewCallState(int state)
        {
            Console.Write("sendNewCallState");
        }

        public void sendNewRegState(int state)
        {
            Console.Write("sendNewCallState");
        }

    }
}

