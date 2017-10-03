using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;


namespace TestPJSUA2.SIP

{
    public class SIPCall : pjsua2.Call
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SipAccount UAacc;
        public const int PJSUA_INVALID_ID = -1; //zie: http://www.pjsip.org/docs/book-latest/html/reference.html)


        /// <summary>
        /// constructor
        /// We explicitly reroute to the constructor of the Call baseclass
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="callID"></param>
        public SIPCall(Account acc, int callID = PJSUA_INVALID_ID) : base(acc, callID)
        {
            //    SIPCall(Account acc, int callID = PJSUA_INVALID_ID);
            UAacc = (SipAccount)acc;
            //noot: hier staat in de c++ code een uitgesterde regel
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

        public override void onTypingIndication(OnTypingIndicationParam prm)
        {
            base.onTypingIndication(prm);
            Classes.WCFcaller.SetSIPStatusMessage("*** Typing: " + prm.rdata );

        }
        /// <summary>
        /// brief SipCall::onCallState
        /// </summary>
        /// <param name="_oncallstateparam"></param>
        public override void onCallState(pjsua2.OnCallStateParam _prm)
        {
            base.onCallState(_prm);
            // Print the new call state
            pjsua2.CallInfo ci = new CallInfo();

            ci = getInfo(); //hier wordt de getInfo methode van de Call baseclass gebruikt!!

            log.Info("*** Call: " + ci.remoteUri + " [" + ci.stateText + "]");
            Classes.WCFcaller.SetSIPStatusMessage("*** Call: " + ci.remoteUri + " [" + ci.stateText + "]");

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
                    Classes.WCFcaller.SetSIPStatusMessage("*** Disconnected: " + ci.remoteUri );

                    break;
                //case pjsip_inv_state.PJSIP_INV_STATE_CONFIRMED:
                //    {
                //        Classes.WCFcaller.SetSIPStatusMessage(ci.remoteUri + " Has answered the call!!");

                //        AudioMedia aud_med = null;

                //        //// Find Audio in call
                //        //for (int i = 0; i < ci.media.Count; i++)
                //        //{
                //        //    if (ci.media[i].type == pjsua2.pjmedia_type.PJMEDIA_TYPE_AUDIO)
                //        //    {
                //        //        aud_med = (pjsua2.AudioMedia)this.getMedia(Convert.ToUInt16(i));
                //        //        StreamInfo si = this.getStreamInfo(Convert.ToUInt16(i));
                //        //        log.Info("*** Media codec: " + si.codecName);
                //        //        Classes.WCFcaller.SetSIPStatusMessage("*** Media codec: " + si.codecName);

                //        //        break;
                //        //    }
                //        //}

                //        if (aud_med != null)
                //        {
                //            // Get playback & capture devices
                //            AudioMedia play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                //            AudioMedia cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();

                //            // Start audio transmissions
                //            aud_med.startTransmit(play_med);
                //            cap_med.startTransmit(aud_med);
                //            Classes.WCFcaller.SetSIPStatusMessage("Audio media ");

                //        }
                //        else {
                //            log.Info("******\t NO AUDIO FOUND IN CALL \t******");
                //            Classes.WCFcaller.SetSIPStatusMessage("******\t NO AUDIO FOUND IN CALL \t******");

                //        }

                //        // Show we are connected
                //        UAacc.newCallState(1);
                //        break;
                //    }
                case pjsip_inv_state.PJSIP_INV_STATE_CONFIRMED:
                    {

                        Console.Write(ci.remoteUri + " Has answered the call!!");
                        //    MessageSink.Instance.Publish(new ErrorMessage(SeverityLevel.Info, "SIPCall", str)); // Show info
                        AudioMedia aud_med_call = null;
                        Media med = null;

                        // Find Audio in call
                        for (int i = 0; i < ci.media.Count; i++)
                        {
                            if (ci.media[i].type == pjsua2.pjmedia_type.PJMEDIA_TYPE_AUDIO)
                            {
                                med = this.getMedia(Convert.ToUInt16(i));
                                // aud_med = med as AudioMedia;
                                aud_med_call = AudioMedia.typecastFromMedia(med);
                                StreamInfo si = this.getStreamInfo(Convert.ToUInt16(i));

                                break;
                            }
                        }

                        if (aud_med_call != null)
                        {
                            // Get playback & capture devices
                            //  AudioMedia play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                            //  AudioMedia cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();
                            // Start audio transmissions
                            //  aud_med.startTransmit(play_med);
                            //  cap_med.startTransmit(aud_med);
                            AudioMediaVector audioMediaVectorDevices = Endpoint.instance().mediaEnumPorts();

                            foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)
                            {
                                // if (_Direction == DIRECTION.RECSEND || _Direction == DIRECTION.SEND)
                                //  {
                                if (audiomediadevice.getPortId() == 0)
                                {
                                    //MIC sends
                                    audiomediadevice.startTransmit(aud_med_call);
                                }

                                //     }
                                //if (_Direction == DIRECTION.RECSEND || _Direction == DIRECTION.REC)
                                //{
                                //    if (audiomediadevice.getPortId() >= 0 && audiomediadevice.getPortId() <= 2)
                                //    {
                                //        //send call media to speakers
                                //        aud_med_call.startTransmit(audiomediadevice);
                                //     }
                                // }
                            }
                        }
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

