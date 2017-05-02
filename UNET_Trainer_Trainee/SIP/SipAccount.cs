using pjsua2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer_Trainee.SIP
{
    public class SipAccount : pjsua2.Account
    {

        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SipAccount()
        {
              
        }
        //  std::vector<PJSUA2.Call> calls;
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
            Console.Write(string.Format("IM message: {0}", _im));
        }

        /*!
* \brief SipAccount::removeCall Removes the selected call
* \param call
*/
        public void removeCall(pjsua2.Call call)
        {
            foreach (pjsua2.Call callitr in Calls)
            {

                //    callitr.Remove();
                callitr.Dispose();
            }

          //  for (vector<PJSUA2.Call>::iterator it = calls.begin(); it != calls.end(); ++it)
            foreach(Call indcall in Calls)
                {
                    CallOpParam cop = new CallOpParam();
                    cop.reason = "Frank heeft opgehangen"; //todo: iets zinnigers invullen..
                    indcall.hangup(cop);
                }
            {

          
            }
        }


        /*!
         * \brief SipAccount::onRegState
         * \param prm
         */
        public void onRegState(pjsua2.OnRegStateParam _prm)
        {
            pjsua2.AccountInfo ai = getInfo();
            log.Info(ai.regIsActive ? "*** Register: code=" : "*** Unregister: code="));

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
            sendNewRegState(_prm.code);
        }

        /*!
         * \brief SipAccount::onIncomingCall
         * \param iprm
         */
        public void onIncomingCall(OnIncomingCallParam _prm)
        {

            SIPCall call = new SIPCall(this, _prm.callId);
            CallInfo ci = call.getInfo();
            CallOpParam prm;

            //todo    std::cout << "*** Incoming Call: " << ci.remoteUri << " [" << ci.stateText << "]" << std::endl;

            // Store this call
            calls.push_back(call);
            _prm.statusCode = (pjsua2.pjsip_status_code)200;

            // Answer the call
            call.answer(prm);
        }

        /*!
         * \brief SipAccount::onInstantMessage
         * \param prm
         */
        public void onInstantMessage(OnInstantMessageParam _prm)
        {

            String message = "todo: somestring";// String::fromStdString(_prm.msgBody);

            //  std::cout << "*** Incomming IM: " << prm.msgBody << std::endl;

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
