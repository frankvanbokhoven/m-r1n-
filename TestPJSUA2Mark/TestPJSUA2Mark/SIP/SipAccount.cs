using pjsua2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPJSUA2Mark.SIP
{
    public class SipAccount : pjsua2.Account
    {

        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SipAccount()
        {
            Calls = new List<Call>();
        }
        public List<pjsua2.Call> Calls;

        // signals:
        public void sendNewCallState(int state)
        {
        }

        public void sendNewRegState(int state)
        {

        }

        /// <summary>
        /// Stuur een melding naar de console
        /// </summary>
        /// <param name="_im"></param>
        public void sendNewIM(String _im)
        {
            Classes.WCFcaller.SetSIPStatusMessage(string.Format("IM message: {0}", _im));
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

                Classes.WCFcaller.SetSIPStatusMessage("*** removed Call: " + callitr.ToString());
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
        /// </summary>
        /// TODO: LET OP: de override is door Frank bijgevoegd omdat VS dit voorstelde
        /// <param name="_prm"></param>
        public override void onRegState(pjsua2.OnRegStateParam _prm)
        {
            pjsua2.AccountInfo ai = getInfo();
         //   log.Info(ai.regIsActive ? "*** Register: code=" : "*** Unregister: code=" + ai.id + " " + ai.uri );
        //    Classes.WCFcaller.SetSIPStatusMessage(ai.regIsActive ? "*** Register: code=" : "*** Unregister: code=" + ai.id + " " + ai.uri);
            Classes.WCFcaller.SetSIPStatusMessage(ai.regIsActive ? "*** Register: code=" : "*** Unregister: code=" + ai.uri  + " " + _prm.status + " " + _prm.reason);

            switch (_prm.code)
            {
                case pjsip_status_code.PJSIP_SC_OK:

                    break;
                case pjsip_status_code.PJSIP_SC_REQUEST_TIMEOUT:
                    break;
                default:
                    break;
            }

            // Emit the new registration state
            sendNewRegState(Convert.ToInt16(_prm.code));
        }



        /// <summary>
        ///  SipAccount::onIncomingCall
        /// TODO: LET OP: de override is door Frank bijgevoegd omdat VS dit voorstelde
        /// </summary> 
        public override void onIncomingCall(OnIncomingCallParam _prm)
        {
            try
            {
                Call call = new Call(this, _prm.callId);

                CallInfo ci = call.getInfo();
                CallOpParam prm = new CallOpParam();

                Classes.WCFcaller.SetSIPStatusMessage("*** Incoming Call: " + ci.remoteUri + " [" + ci.stateText + "]");

                // Store this call
                Calls.Add(call);
                prm.statusCode = (pjsua2.pjsip_status_code)200;

                // Answer the call
                call.answer(prm);

            }
            catch (Exception ex)
            {
                Classes.WCFcaller.SetSIPStatusMessage("*** Error on incoming call: " + ex.Message + ex.InnerException);
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

            Classes.WCFcaller.SetSIPStatusMessage("*** Incomming IM: " + _prm.msgBody);

            sendNewIM(message);
        }

        /*!
         * \brief SipAccount::newCallState
         * \param state
         */
        public void newCallState(int state)
        {

            sendNewCallState(state);
        }

    }
}

