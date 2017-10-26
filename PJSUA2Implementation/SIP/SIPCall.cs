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


    public enum Direction
    {
        RECSEND = 0,
        REC,
        SEND
    }

    public enum InputChannels
    {
        ichLeft,
        ichRight,
        ichSpeaker
    }
    public enum OutputChannels
    {
        ochLeft,
        ochRight,
        ochSpeaker
    }

    public class SIPCall : pjsua2.Call
    {
        private SipAccount UAacc;
        /// <summary>
        /// callid is used to keep track of the different conferences and calls the trainers make
        /// </summary>
        public int CallID { get; set; }
        public const int PJSUA_INVALID_ID = -1; //zie: http://www.pjsip.org/docs/book-latest/html/reference.html)
        private Direction _direction;
        private List<InputChannels> ChannelInputCollection;
        private List<OutputChannels> ChannelOutputCollection;
        /// <summary>
        /// constructor
        /// We explicitly reroute to the constructor of the Call baseclass
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="callID"></param>
        public SIPCall(pjsua2.Account acc, ref List<InputChannels> channelinputcollection, ref List<OutputChannels> channeloutputcollectin, int callID = PJSUA_INVALID_ID) : base(acc, callID)
        {
            UAacc = (SipAccount)acc;
            CallID = callID;

            //deze collections geven aan welke input poort aan welke outputpoort geknoopt wordt
            //mocht er ooit een nieuwe poort bijkomen, dan moet die in de enum worden toegevoegd.
            if(channelinputcollection == null)
            {
              throw  new NotImplementedException("The channelinputcolletion is not initialized!!. Please create a list, fill it and add it to the constructor");
            }

            if(channeloutputcollectin == null)
            {
                throw new NotImplementedException("The channeloutputcolletion is not initialized!!. Please create a list, fill it and add it to the constructor");
            }

            if(channelinputcollection.Count == 0)
            {
                throw new NotImplementedException("The channelinputcollection cannot be empty!");
            }

            if(channeloutputcollectin.Count == 0)
            {
                throw new NotImplementedException("The channeloutputcollection cannot be empty!");
            }

            ChannelInputCollection = channelinputcollection;
            ChannelOutputCollection = channeloutputcollectin;

        }

        //henk's voorbeeld
        //public SIPCall(pjsua2.Account acc, ref System.Collections.Generic.List<int> channelincollection, ref System.Collections.Generic.List<int> channeloutputcollectin, int callID = PJSUA_INVALID_ID) : base(acc, callID)
        //{
        //    //    SIPCall(Account acc, int callID = PJSUA_INVALID_ID);
        //    UAacc = (SipAccount)acc;
        //    //noot: hier staat in de c++ code een uitgesterde regel
        //    CallID = callID;
        //    _Channeloutcollection = channeloutcollection;
        //    _Channelincollection = channelincollection;
        //}

        // public SIPCall(pjsua2.Account acc, int callID = PJSUA_INVALID_ID) : base(acc, callID)
        // {
        // //    SIPCall(Account acc, int callID = PJSUA_INVALID_ID);
        // UAacc = (SipAccount)acc;
        // //noot: hier staat in de c++ code een uitgesterde regel
        // CallID = callID;
        // }

        private void sendCallState(int state)
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
                       Console.Write(("*** Call: " + ci.remoteUri + " disconnected"));

                        // Delete the call object
                        GC.Collect();//  delete this;

                        break;

                    case pjsip_inv_state.PJSIP_INV_STATE_CONFIRMED:
                        {

                            AudioMedia aud_med_call = null;
                            Media med = null;

                            // Find Audio in call
                            for (int i = 0; i < ci.media.Count; i++)
                            {
                                if (ci.media[i].type == pjsua2.pjmedia_type.PJMEDIA_TYPE_AUDIO)
                                {
                                    med = this.getMedia(Convert.ToUInt16(i));
                                    // aud_med = med as AudioMedia;
                                    aud_med_call = AudioMedia.typecastFromMedia(med); //dit is de stream
                                    StreamInfo si = this.getStreamInfo(Convert.ToUInt16(i));

                                    break;
                                }
                            }
                            //   Console.Write(ci.remoteUri + " Has answered the call!!");

                            if (aud_med_call != null)
                            {
                                AudioMedia left = null;
                                AudioMedia right = null;
                                AudioMedia speaker = null;
                                // Get playback & capture devices
                                //  AudioMedia play_med = Endpoint.instance().audDevManager().getPlaybackDevMedia();
                                //  AudioMedia cap_med = Endpoint.instance().audDevManager().getCaptureDevMedia();
                                // Start audio transmissions
                                //  aud_med.startTransmit(play_med);
                                //  cap_med.startTransmit(aud_med);
                                AudioMediaVector audioMediaVectorDevices = Endpoint.instance().mediaEnumPorts(); //het lijstje met de beschikbare hardwarekanalen
                                //   SWIGTYPE_p_void port = new SWIGTYPE_p_void();
                                Endpoint.instance().mediaEnumPorts();

                                if ((ChannelInputCollection != null) && (ChannelOutputCollection != null))
                                {
                                    //Nu gaan we alle mogelijke combinaties langs en koppelen de poorten zoals opgegeven
                                    if ((ChannelInputCollection.Contains(InputChannels.ichLeft)) && (ChannelOutputCollection.Contains(OutputChannels.ochLeft)))
                                    {
                                        left = aud_med_call;
                                        left.startTransmit(left);
                                        //foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                        //{
                                        //    int id = audiomediadevice.getPortId();

                                        //    if (audiomediadevice.getPortId() == 0)
                                        //    {
                                        //       Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Left in and Left out");

                                        //        left = audiomediadevice;
                                        //        left.startTransmit(left);
                                        //    }
                                        //}
                                    }
                                    if ((ChannelInputCollection.Contains(InputChannels.ichLeft)) && (ChannelOutputCollection.Contains(OutputChannels.ochRight)))
                                    {
                                        right = aud_med_call;
                                        right.startTransmit(right);
                                        //foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                        //{
                                        //    int id = audiomediadevice.getPortId();

                                        //    if (audiomediadevice.getPortId() == 1)
                                        //    {
                                        //        Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Left in and Right out");

                                        //        right = audiomediadevice;
                                        //        right.startTransmit(right);
                                        //    }
                                        //}
                                    }

                                    if ((ChannelInputCollection.Contains(InputChannels.ichLeft)) && (ChannelOutputCollection.Contains(OutputChannels.ochSpeaker)))
                                    {
                                        //foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                        //{
                                        //    int id = audiomediadevice.getPortId();

                                        //    if (audiomediadevice.getPortId() == 3)
                                        //    {
                                        //        Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Left in and Speaker out");

                                        //        left = audiomediadevice;
                                        //        left.startTransmit(speaker);
                                        //    }
                                        //}
                                        speaker = aud_med_call;
                                        speaker.startTransmit(speaker);
                                    }

                                    if ((ChannelInputCollection.Contains(InputChannels.ichRight)) && (ChannelOutputCollection.Contains(OutputChannels.ochLeft)))
                                    {
                                        foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                        {
                                            int id = audiomediadevice.getPortId();

                                            if (audiomediadevice.getPortId() == 0)
                                            {
                                                Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Right in and Left out");

                                                right = audiomediadevice;
                                                left.startTransmit(right);//todo: klopt dit???
                                            }
                                        }
                                    }

                                    if ((ChannelInputCollection.Contains(InputChannels.ichRight)) && (ChannelOutputCollection.Contains(OutputChannels.ochRight)))
                                    {
                                        foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                        {
                                            int id = audiomediadevice.getPortId();

                                            if (audiomediadevice.getPortId() == 0)
                                            {
                                                Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Right in and Right out");

                                                right = audiomediadevice;
                                                right.startTransmit(right);
                                            }
                                        }


                                        if ((ChannelInputCollection.Contains(InputChannels.ichRight)) && (ChannelOutputCollection.Contains(OutputChannels.ochSpeaker)))
                                        {
                                            foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)//nu loopen we door de mediadevices enumeratie
                                            {
                                                int id = audiomediadevice.getPortId();

                                                if (audiomediadevice.getPortId() == 0)
                                                {
                                                    Console.Write("audiomediadevice: " + audiomediadevice.getPortId() + "Right in and Speaker out");

                                                    speaker = audiomediadevice;
                                                    speaker.startTransmit(speaker);
                                                }
                                            }
                                        }
                                    }

                                    #region proto code
                                    //    left.startTransmit(right);
                                    //    left.startTransmit(left);
                                    //    speaker.startTransmit(speaker);

                                    //if(ChannelOutputCollection != null)
                                    //{
                                    //	foreach(int channelID in ChannelOutputCollection)
                                    //	{
                                    //		foreach(AudioMedia audiomediadevice in audioMediaVectorDevices)
                                    //		{
                                    //			if(audiomediadevice.getPortId() == channelID)
                                    //			{
                                    //				aud_med_call.startTransmit(audiomediadevice);
                                    //			}
                                    //		}
                                    //	}
                                    //}

                                    //if(ChannelInputCollection != null)
                                    //{
                                    //	foreach(int channelID in ChannelInputCollection)
                                    //	{
                                    //		foreach(AudioMedia audiomediadevice in audioMediaVectorDevices)
                                    //		{
                                    //			if(audiomediadevice.getPortId() == channelID)
                                    //			{
                                    //				//send input to call
                                    //				audiomediadevice.startTransmit(aud_med_call);
                                    //			}
                                    //		}
                                    //	}
                                    //}

                                    //frmm.AddToListbox("Audiomedia and devices connected");
                                    ////show that we are connected
                                    //UAacc.newCallState(1);
                                    //break;



                                    //we gaan er gewoon vanuit dat er slechts 3 kanalen zijn:
                                    // 0: links
                                    // 1: rechts
                                    // 2: speaker
                                    //foreach (AudioMedia audiomediadevice in audioMediaVectorDevices)
                                    //{

                                    //    frmm.AddToListbox((">> audiomediadevice: " + audiomediadevice.getPortId()));
                                    //    int id = audiomediadevice.getPortId();

                                    //    if (audiomediadevice.getPortId() == 0)
                                    //    {
                                    //        left = audiomediadevice;
                                    //    }

                                    //    if (audiomediadevice.getPortId() == 1)
                                    //    {
                                    //        right = audiomediadevice;
                                    //    }

                                    //    if (audiomediadevice.getPortId() == 2)
                                    //    {
                                    //        speaker = audiomediadevice;
                                    //    }
                                }
                                //left.startTransmit(right);
                                //left.startTransmit(left);
                                //speaker.startTransmit(speaker);
                                #endregion
                            }
                            Console.Write(ci.remoteUri + " Has answered the call!!");

                            break;
                        }

                    case pjsip_inv_state.PJSIP_INV_STATE_NULL:
                        {
                            Console.Write("*** Call: " + ci.remoteUri + " NULL");

                            break;
                        }
                    case pjsip_inv_state.PJSIP_INV_STATE_EARLY:
                        {
                            Console.Write("*** Call: " + ci.remoteUri + " EARLY");

                            break;
                        }
                    case pjsip_inv_state.PJSIP_INV_STATE_INCOMING:
                        {
                            Console.Write("*** Call: " + ci.remoteUri + " INCOMING");

                            break;
                        }
                    case pjsip_inv_state.PJSIP_INV_STATE_CALLING:
                        {
                            Console.Write("*** Call: " + ci.remoteUri + " CALLING");

                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                string fout = ex.Message;
            }

        }
    }
}
