using System;
using System.Collections.Generic;
using pjsua2;
using System.Threading;
using System.Configuration;
using UNET_Classes;


namespace PJSUA2Implementation.SIP
{
    public class SipAccount : pjsua2.Account
    {

        public UNET_Classes.SyncList<pjsua2.Call> Calls;
  //     public List<pjsua2.Call> Calls;


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
            foreach (pjsua2.Call callitr in Calls)
            {

                //    callitr.Remove();

                Console.WriteLine ("*** removed Call: " + callitr.ToString());
                callitr.Dispose();
            }

            foreach (Call indcall in Calls)
            {
                CallOpParam cop = new CallOpParam();
                cop.reason = "Frank heeft opgehangen"; //todo: iets zinnigers invullen..
                indcall.hangup(cop);
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
        ///  Hier 
         /// </summary> 
        public override void onIncomingCall(OnIncomingCallParam _prm)
        {
            try
            {
                Call call = new Call(this, _prm.callId);
            //    SIPCall call = new SIPCall(this, _prm.callId);
                CallInfo ci = call.getInfo();
                CallOpParam prm = new CallOpParam();

                Console.WriteLine("*** Incoming Call: " + ci.remoteUri + " [" + ci.stateText + "]");

                // Store this call
                Calls.Add(call);
                prm.statusCode = (pjsua2.pjsip_status_code)200;
                // Answer the call
                call.answer(prm);             

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

