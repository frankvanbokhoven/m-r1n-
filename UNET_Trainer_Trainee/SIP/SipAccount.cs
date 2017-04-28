using PJSUA2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer_Trainee.SIP
{
    public class SipAccount : PJSUA2.Account
    {
   

        public SipAccount()
        {
              
        }
 //       //  std::vector<PJSUA2.Call> calls;
 //       public List<PJSUA2.Call> calls;
 //       // signals:
 //       public void sendNewCallState(int state)
 //       {
 //       }

 //       public void sendNewRegState(int state)
 //       {

 //       }

 //       public void sendNewIM(String IM)
 //       {

 //       }
 
 //        /*!
 //* \brief SipAccount::removeCall Removes the selected call
 //* \param call
 //*/
 //       public void removeCall(PJSUA2.Call call)
 //       {
 //           foreach(PJSUA2.Call callitr in calls)
 //           {

 //           //    callitr.Remove();
 //               callitr.Dispose();
 //           }
 //           //for (std::vector<PJSUA2.Call>::iterator it = calls.begin(); it != calls.end(); ++it)
 //           //{

 //           //    if (it == call)
 //           //    {
 //           //        calls.erase(it);
 //           //        break;
 //           //    }
 //           //}
 //       }


 //       /*!
 //        * \brief SipAccount::onRegState
 //        * \param prm
 //        */
 //      public void onRegState(PJSUA2.OnRegStateParam _prm)
 //       {
 //           PJSUA2.AccountInfo ai = getInfo();
 //          //todo std::cout << (ai.regIsActive ? "*** Register: code=" : "*** Unregister: code=") << _prm.code << std::endl;

 //           switch (_prm.code)
 //           {
 //               case PJSUA2.pjsip_status_code.PJSIP_SC_OK:

 //                   break;
 //               case PJSUA2.pjsip_status_code.PJSIP_SC_REQUEST_TIMEOUT:
 //                   break;
 //               default:
 //                   break;
 //           }

 //           // Emit the new registration state
 //           emit sendNewRegState(prm.code);
 //       }

 //       /*!
 //        * \brief SipAccount::onIncomingCall
 //        * \param iprm
 //        */
 //    public   void onIncomingCall(PJSUA2.OnIncomingCallParam _prm)
 //       {

 //           SIPCall call = new SIPCall(this, _prm.callId);
 //           PJSUA2.CallInfo ci = call.getInfo();
 //           PJSUA2.CallOpParam prm;

 //       //todo    std::cout << "*** Incoming Call: " << ci.remoteUri << " [" << ci.stateText << "]" << std::endl;

 //           // Store this call
 //           calls.push_back(call);
 //           _prm.statusCode = (PJSUA2.pjsip_status_code)200;

 //           // Answer the call
 //           call.answer(prm);
 //       }

 //       /*!
 //        * \brief SipAccount::onInstantMessage
 //        * \param prm
 //        */
 //     public  void onInstantMessage(PJSUA2.OnInstantMessageParam _prm)
 //       {

 //           String message = "todo: somestring";// String::fromStdString(_prm.msgBody);

 //         //  std::cout << "*** Incomming IM: " << prm.msgBody << std::endl;

 //           emit sendNewIM(message);
 //       }

 //       /*!
 //        * \brief SipAccount::newCallState
 //        * \param state
 //        */
 //     public  void newCallState(int state)
 //       {

 //           emit sendNewCallState(state);
 //       }

    }
}
