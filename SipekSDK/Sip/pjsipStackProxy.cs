// SipekSdk with PJSUA2, 2017
// Type: Sipek.Sip.pjsipStackProxy
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using Sipek.Common;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Sipek.Sip
{
  public class pjsipStackProxy : IVoipProxy
  {
    private static pjsipStackProxy _instance = (pjsipStackProxy) null;
    private static OnDtmfDigitCallback dtdel = new OnDtmfDigitCallback(pjsipStackProxy.onDtmfDigitCallback);
    private static OnMessageWaitingCallback mwidel = new OnMessageWaitingCallback(pjsipStackProxy.onMessageWaitingCallback);
    private static OnCallReplacedCallback crepdel = new OnCallReplacedCallback(pjsipStackProxy.onCallReplacedCallback);
    public SipConfigStruct ConfigMore = SipConfigStruct.Instance;
    internal const string PJSIP_DLL = "pjsipDll.dll";
    private bool _initialized;

    public static pjsipStackProxy Instance
    {
      get
      {
        if (pjsipStackProxy._instance == null)
          pjsipStackProxy._instance = new pjsipStackProxy();
        return pjsipStackProxy._instance;
      }
    }

    public override bool IsInitialized
    {
      get
      {
        return this._initialized;
      }
      set
      {
        this._initialized = value;
      }
    }

    protected pjsipStackProxy()
    {
    }

    [DllImport("pjsipDll.dll")]
    private static extern int dll_init();

    [DllImport("pjsipDll.dll")]
    private static extern int dll_main();

    [DllImport("pjsipDll.dll")]
    private static extern int dll_shutdown();

    [DllImport("pjsipDll.dll")]
    private static extern void dll_setSipConfig(SipConfigStruct config);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_getCodec(int index, StringBuilder codec);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_getNumOfCodecs();

    [DllImport("pjsipDll.dll")]
    private static extern int dll_setCodecPriority(string name, int prio);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_setSoundDevice(string playbackDeviceId, string recordingDeviceId);

    [DllImport("pjsipDll.dll")]
    private static extern int onDtmfDigitCallback(OnDtmfDigitCallback cb);

    [DllImport("pjsipDll.dll")]
    private static extern int onMessageWaitingCallback(OnMessageWaitingCallback cb);

    [DllImport("pjsipDll.dll", EntryPoint = "onCallReplaced")]
    private static extern int onCallReplacedCallback(OnCallReplacedCallback cb);

    private int start()
    {
      if (!this.Config.IsNull)
        this.ConfigMore.listenPort = this.Config.SIPPort;
      pjsipStackProxy.dll_setSipConfig(this.ConfigMore);
      int num = pjsipStackProxy.dll_init();
      if (num != 0)
        return num;
      return num | pjsipStackProxy.dll_main();
    }

    public override int initialize()
    {
      this.shutdown();
      pjsipStackProxy.onDtmfDigitCallback(pjsipStackProxy.dtdel);
      pjsipStackProxy.onMessageWaitingCallback(pjsipStackProxy.mwidel);
      pjsipStackProxy.onCallReplacedCallback(pjsipStackProxy.crepdel);
      pjsipCallProxy.initialize();
      int num = this.start();
      this.IsInitialized = num == 0;
      return num;
    }

    public override int shutdown()
    {
      return pjsipStackProxy.dll_shutdown();
    }

    public override string getCodec(int index)
    {
      if (!this.IsInitialized)
        return "";
      StringBuilder codec = new StringBuilder(256);
      pjsipStackProxy.dll_getCodec(index, codec);
      return codec.ToString();
    }

    public override int getNoOfCodecs()
    {
      if (!this.IsInitialized)
        return 0;
      return pjsipStackProxy.dll_getNumOfCodecs();
    }

    public override void setCodecPriority(string codecname, int priority)
    {
      if (!this.IsInitialized)
        return;
      pjsipStackProxy.dll_setCodecPriority(codecname, priority);
    }

    public override ICallProxyInterface createCallProxy()
    {
      return (ICallProxyInterface) new pjsipCallProxy(this.Config);
    }

    public void setSoundDevice(string playbackDeviceId, string recordingDeviceId)
    {
      pjsipStackProxy.dll_setSoundDevice(playbackDeviceId, recordingDeviceId);
    }

    private static int onDtmfDigitCallback(int callId, int digit)
    {
      pjsipStackProxy.Instance.BaseDtmfDigitReceived(callId, digit);
      return 1;
    }

    private static int onMessageWaitingCallback(int mwi, string info)
    {
      if (info == null)
        return -1;
      pjsipStackProxy.Instance.BaseMessageWaitingIndication(mwi, info.ToString());
      return 1;
    }

    private static int onCallReplacedCallback(int oldid, int newid)
    {
      pjsipStackProxy.Instance.BaseCallReplacedCallback(oldid, newid);
      return 1;
    }

    internal string SetTransport(int accountId, string sipuri)
    {
      string str = sipuri;
      try
      {
        switch (this.Config.Accounts[accountId].TransportMode)
        {
          case ETransportMode.TM_TCP:
            str = sipuri + ";transport=tcp";
            break;
          case ETransportMode.TM_TLS:
            str = sipuri + ";transport=tls";
            break;
        }
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      return str;
    }
  }
}
