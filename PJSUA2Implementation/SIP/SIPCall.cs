﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;


namespace PJSUA2Implementation.SIP
{
    public class SIPCall : pjsua2.Call
    {
        private SipAccount UAacc;

        /// <summary>
        /// callid is used to keep track of the different conferences and calls the trainers make
        /// </summary>
        public int CallID { get; set; }

        public const int PJSUA_INVALID_ID = -1; //zie: http://www.pjsip.org/docs/book-latest/html/reference.html)


        /// <summary>
        /// constructor
        /// We explicitly reroute to the constructor of the Call baseclass
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="callID"></param>
        public SIPCall(pjsua2.Account acc, int callID = PJSUA_INVALID_ID) : base(acc, callID)
        {
            //    SIPCall(Account acc, int callID = PJSUA_INVALID_ID);
            UAacc = (SipAccount)acc;
            //noot: hier staat in de c++ code een uitgesterde regel
            CallID = callID;
        }


        private void sendCallState(int state)
        {
            CallInfo ci = getInfo();
            if (ci.state == pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED)
            {
                //delete the call
                this.Dispose(); //todo: testen of dispose werkt om de call op te hangen
            }
        }


        /// <summary>
        /// brief SipCall::onCallState
        /// </summary>
        /// <param name="_oncallstateparam"></param>
        public void onCallState(pjsua2.OnCallStateParam _prm)
        {

            // Print the new call state
            pjsua2.CallInfo ci = new CallInfo();

            ci = getInfo(); //hier wordt de getInfo methode van de Call baseclass gebruikt!!

             Console.Write("*** Call: " + ci.remoteUri + " [" + ci.stateText + "]");

            // Execute commands according to the new state
            switch (ci.state)
            {
                case pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED:

                    // Remove the call from the account
                    //todo: moet verwijderen!!!!! UAacc.removeCall();

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
                                aud_med = (pjsua2.AudioMedia)this.getMedia(Convert.ToUInt16(i));
                                StreamInfo si = this.getStreamInfo(Convert.ToUInt16(i));
                                Console.Write("*** Media codec: " + si.codecName);
                                break;
                            }
                        }

                        if (aud_med != null)
                        {
                            // Get playback & capture devices
                            AudioMedia play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                            AudioMedia cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();

                            // Start audio transmissions
                            aud_med.startTransmit(play_med);
                            cap_med.startTransmit(aud_med);
                        }
                        else {
                            Console.Write("****** NO AUDIO FOUND IN CALL ******");
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
