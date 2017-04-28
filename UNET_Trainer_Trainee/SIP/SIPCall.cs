using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UNET_Trainer_Trainee.SIP
{
    public class SIPCall
    {
        private SipAccount UAacc;
        public const int PJSUA_INVALID_ID = -1; //zie: http://www.pjsip.org/docs/book-latest/html/reference.html)

        //public  PJSUA2.Call(acc, callID) SipCall(PJSUA2.Account acc, int callID = PJSUA_INVALID_ID) 
        //{
        //    UAacc = (SipAccount)acc;
        //    // connect(this,SIGNAL(sendCallState(int)),UAacc,SLOT(newCallState(int)));
        //}

  
        //private void sendCallState(int state)
        //{
        //    //todo!
        //}
  
        ///*!
        // * \brief SipCall::onCallState
        // * \param prm
        // */
        //void onCallState(PJSUA2.OnCallStateParam _oncallstateparam)
        //{

        //    // Print the new call state
        //    PJSUA2.CallInfo ci = PJSUA2.Call.getInfo();
        //    //  std::cout << "*** Call: " << ci.remoteUri << " [" << ci.stateText << "]" << std::endl;

        //    // Execute commands according to the new state
        //    switch (ci.state)
        //    {
        //        case PJSUA2.pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED:

        //            // Remove the call from the account
        //            UAacc.removeCall(this);

        //            // Show we are now disconnected
        //            UAacc.newCallState(0);

        //            // Delete the call object
        //            delete this;

        //            break;
        //        case PJSUA2.pjsip_inv_state.PJSIP_INV_STATE_CONFIRMED:
        //            {

        //                PJSUA2.AudioMedia aud_med = null;

        //                // Find Audio in call
        //                for (int i = 0; i < ci.media.size(); i++)
        //                {
        //                    if (ci.media[i].type == PJSUA2.pjmedia_type.PJMEDIA_TYPE_AUDIO)
        //                    {
        //                        aud_med = (PJSUA2.AudioMedia)this.getMedia(i);
        //                        PJSUA2.StreamInfo si = this.getStreamInfo(i);
        //                        //todo   std::cout << "*** Media codec: " << si.codecName << std::endl;
        //                        break;
        //                    }
        //                }

        //                if (aud_med != null)
        //                {
        //                    // Get playback & capture devices
        //                    PJSUA2.AudioMedia & play_med = PJSUA2.Endpoint.instance().audDevManager().getPlaybackDevMedia();
        //                    PJSUA2.AudioMedia & cap_med = PJSUA2.Endpoint.instance().audDevManager().getCaptureDevMedia();

        //                    // Start audio transmissions
        //                    cap_med.startTransmit(aud_med);
        //                    aud_med->startTransmit(play_med);
        //                }
        //                else {
        //                    //todo:   std::cout << std::endl << "******\t NO AUDIO FOUND IN CALL \t******" << std::endl << std::endl;
        //                }

        //                // Show we are connected
        //                UAacc.newCallState(1);
        //                break;
        //            }
        //        //      case PJSIP_INV_STATE_NULL:
        //        //        break;
        //        //      case PJSIP_INV_STATE_EARLY:
        //        //        break;
        //        //      case PJSIP_INV_STATE_INCOMING:
        //        //        break;
        //        //      case PJSIP_INV_STATE_CALLING:
        //        //        break;
        //        default:
        //        break;
        //    }

        //}
    }
}
