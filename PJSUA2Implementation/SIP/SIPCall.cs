using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;


namespace PJSUA2Implementation.SIP
{
    public enum aud_med_call_Channel { amcRight = 0, amcLeft = 1, amcSpeaker = 2, amcReverse = 3 };//frank: bepaalt waar de audio heen gaat
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


        private  void sendCallState(int state)
        {
            CallInfo ci = getInfo();
            if (ci.state == pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED)
            {
                //delete the call
                this.Dispose(); //todo: testen of dispose werkt om de call op te hangen
            }
        }


        public override void onStreamCreated(OnStreamCreatedParam prm)
        {
            base.onStreamCreated(prm);
        }


        /// <summary>
        /// hier moeten we de toegewezen poorten opruimen.
        /// </summary>
        /// <param name="prm"></param>
        public override void onStreamDestroyed(OnStreamDestroyedParam prm)
        {
            base.onStreamDestroyed(prm);
        }

        /// <summary>
        /// brief SipCall::onCallState
        /// </summary>
        /// <param name="_oncallstateparam"></param>
        //public void onCallState(pjsua2.OnCallStateParam _prm)
        //   {
        public override void onCallState(pjsua2.OnCallStateParam _prm)
        {
            try
            {
                base.onCallState(_prm);


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
                                //   SWIGTYPE_p_void port = new SWIGTYPE_p_void();


                                AudioMedia left = null;
                                AudioMedia right = null;
                                AudioMedia speaker = null;

                                //todo: hier de 4 relevante regels voor de conferencebridge programmeren.
                                foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)
                                {

                                    Console.Write(">> audiomediadevice: " + audiomediadevice.getPortId());

                                    int id = audiomediadevice.getPortId();

                                    if (audiomediadevice.getPortId() == 0)
                                    {
                                        left = audiomediadevice;
                                    }

                                    if (audiomediadevice.getPortId() == 1)
                                    {
                                        right = audiomediadevice;
                                    }

                                    if (audiomediadevice.getPortId() == 2)
                                    {
                                        speaker = audiomediadevice;
                                    }
                                }
                                left.startTransmit(right);
                                left.startTransmit(left);
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
            catch(Exception ex)
            {
                string fout = ex.Message;
            }

        }
    }
}
