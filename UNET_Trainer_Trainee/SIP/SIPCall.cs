using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;


namespace UNET_Trainer_Trainee.SIP
{
    public class SIPCall
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SipAccount UAacc;
        public const int PJSUA_INVALID_ID = -1; //zie: http://www.pjsip.org/docs/book-latest/html/reference.html)

        public void Call(Account acc, int callID = PJSUA_INVALID_ID)
        {
            //    SIPCall(Account acc, int callID = PJSUA_INVALID_ID);
            UAacc = (SipAccount)acc;
         // todo   connect(this, SIGNAL(sendCallState(int)), UAacc, SLOT(newCallState(int)));
        }


        private void sendCallState(int state)
        {
            //todo!
        }

        /*!
         * \brief SipCall::onCallState
         * \param prm
         */
     public void onCallState(pjsua2.OnCallStateParam _oncallstateparam)
        {

            // Print the new call state
            pjsua2.CallInfo ci = new CallInfo();
       //todo     ci.getInfo();
            log.Info("*** Call: " + ci.remoteUri + " [" + ci.stateText + "]");

            // Execute commands according to the new state
            switch (ci.state)
            {
                case pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED:

                    // Remove the call from the account
               //todo     UAacc.removeCall(this);

                    // Show we are now disconnected
                    UAacc.newCallState(0);

                    // Delete the call object
                    GC.Collect();//  delete this;

                    break;
                case pjsip_inv_state.PJSIP_INV_STATE_CONFIRMED:
                    {

                        AudioMedia aud_med = null;

                        // Find Audio in call
                        for (int i = 0; i < ci.media.Count; i++)
                        {
                            if (ci.media[i].type == pjsua2.pjmedia_type.PJMEDIA_TYPE_AUDIO)
                            {
                                //todo
                                //aud_med = (pjsua2.AudioMedia)this.getMedia(i);
                                //StreamInfo si = this.getStreamInfo(i);
                                //log.Info("*** Media codec: " + si.codecName);
                                break;
                            }
                        }

                        if (aud_med != null)
                        {
                            // Get playback & capture devices
                            //todo: terugzetten
                        //    AudioMedia & play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                        //    AudioMedia & cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();

                        //    // Start audio transmissions
                        //    cap_med.startTransmit(aud_med);
                        //    aud_med.startTransmit(play_med);
                        }
                        else {
                            log.Info("******\t NO AUDIO FOUND IN CALL \t******");
                        }

                        // Show we are connected
                        UAacc.newCallState(1);
                        break;
                    }
                case pjsip_inv_state.PJSIP_INV_STATE_NULL:
                    break;
                case pjsip_inv_state.PJSIP_INV_STATE_EARLY:
                    break;
                case pjsip_inv_state.PJSIP_INV_STATE_INCOMING:
                    break;
                case pjsip_inv_state.PJSIP_INV_STATE_CALLING:
                    break;
                default:
                    break;
            }

        }
    }
}
