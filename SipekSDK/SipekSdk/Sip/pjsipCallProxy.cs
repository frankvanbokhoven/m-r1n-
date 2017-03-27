// SipekSdk with PJSUA2, 2017
// Type: Sipek.Sip.pjsipCallProxy
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using Sipek.Common;
using System.Runtime.InteropServices;
using System.Text;

namespace Sipek.Sip
{
  internal class pjsipCallProxy : ICallProxyInterface
  {
    private static OnCallStateChanged csDel = new OnCallStateChanged(pjsipCallProxy.onCallStateChanged);
    private static OnCallIncoming ciDel = new OnCallIncoming(pjsipCallProxy.onCallIncoming);
    private static OnCallHoldConfirm chDel = new OnCallHoldConfirm(pjsipCallProxy.onCallHoldConfirm);
    private IConfiguratorInterface _config = (IConfiguratorInterface) new NullConfigurator();
    internal const string PJSIP_DLL = "pjsipDll.dll";
    private int _sessionId;

    private IConfiguratorInterface Config
    {
      get
      {
        return this._config;
      }
    }

    public override int SessionId
    {
      get
      {
        return this._sessionId;
      }
      set
      {
        this._sessionId = value;
      }
    }

    internal pjsipCallProxy(IConfiguratorInterface config)
    {
      this._config = config;
    }

    [DllImport("pjsipDll.dll")]
    private static extern int dll_makeCall(int accountId, string uri);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_releaseCall(int callId);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_answerCall(int callId, int code);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_holdCall(int callId);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_retrieveCall(int callId);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_xferCall(int callId, string uri);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_xferCallWithReplaces(int callId, int dstSession);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_serviceReq(int callId, int serviceCode, string destUri);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_dialDtmf(int callId, string digits, int mode);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_getCurrentCodec(int callId, StringBuilder codec);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_makeConference(int callId);

    [DllImport("pjsipDll.dll")]
    private static extern int onCallStateCallback(OnCallStateChanged cb);

    [DllImport("pjsipDll.dll")]
    private static extern int onCallIncoming(OnCallIncoming cb);

    [DllImport("pjsipDll.dll")]
    private static extern int onCallHoldConfirmCallback(OnCallHoldConfirm cb);

    public static void initialize()
    {
      pjsipCallProxy.onCallIncoming(pjsipCallProxy.ciDel);
      pjsipCallProxy.onCallStateCallback(pjsipCallProxy.csDel);
      pjsipCallProxy.onCallHoldConfirmCallback(pjsipCallProxy.chDel);
    }

    public override int makeCall(string dialedNo, int accountId)
    {
      string sipuri = dialedNo.IndexOf("sip:") != 0 ? "sip:" + dialedNo + "@" + this.Config.Accounts[accountId].HostName : dialedNo;
      string uri = pjsipStackProxy.Instance.SetTransport(accountId, sipuri);
      this.SessionId = pjsipCallProxy.dll_makeCall(this.Config.Accounts[accountId].Index, uri);
      return this.SessionId;
    }

    public override bool endCall()
    {
      pjsipCallProxy.dll_releaseCall(this.SessionId);
      return true;
    }

    public override bool alerted()
    {
      pjsipCallProxy.dll_answerCall(this.SessionId, 180);
      return true;
    }

    public override bool acceptCall()
    {
      pjsipCallProxy.dll_answerCall(this.SessionId, 200);
      return true;
    }

    public override bool holdCall()
    {
      pjsipCallProxy.dll_holdCall(this.SessionId);
      return true;
    }

    public override bool retrieveCall()
    {
      pjsipCallProxy.dll_retrieveCall(this.SessionId);
      return true;
    }

    public override bool xferCall(string number)
    {
      pjsipCallProxy.dll_xferCall(this.SessionId, "sip:" + number + "@" + this.Config.Accounts[this.Config.DefaultAccountIndex].HostName);
      return true;
    }

    public override bool xferCallSession(int session)
    {
      pjsipCallProxy.dll_xferCallWithReplaces(this.SessionId, session);
      return true;
    }

    public override bool threePtyCall(int session)
    {
      pjsipCallProxy.dll_serviceReq(this.SessionId, 4, "");
      return true;
    }

    public override bool serviceRequest(int code, string dest)
    {
      string destUri = "<sip:" + dest + "@" + this.Config.Accounts[this.Config.DefaultAccountIndex].HostName + ">";
      pjsipCallProxy.dll_serviceReq(this.SessionId, code, destUri);
      return true;
    }

    public override bool dialDtmf(string digits, EDtmfMode mode)
    {
      pjsipCallProxy.dll_dialDtmf(this.SessionId, digits, (int) mode);
      return true;
    }

    public override string getCurrentCodec()
    {
      StringBuilder codec = new StringBuilder(256);
      pjsipCallProxy.dll_getCurrentCodec(this.SessionId, codec);
      return codec.ToString();
    }

    public override bool conferenceCall()
    {
      return pjsipCallProxy.dll_makeConference(this.SessionId) == 1;
    }

    private static int onCallStateChanged(int callId, ESessionState callState)
    {
      ICallProxyInterface.BaseCallStateChanged(callId, callState, "");
      return 0;
    }

    private static int onCallIncoming(int callId, string sturi)
    {
      string str = sturi;
      string info = "";
      string number = "";
      if (str != null)
      {
        int startIndex1 = str.IndexOf("<sip:");
        int num = str.IndexOf('@');
        if (startIndex1 >= 0 && num > startIndex1)
          number = str.Substring(startIndex1 + 5, num - startIndex1 - 5);
        if (startIndex1 >= 0)
        {
          info = str.Remove(startIndex1, str.Length - startIndex1).Trim();
        }
        else
        {
          int startIndex2 = info.IndexOf(';');
          if (startIndex2 >= 0)
          {
            info = info.Remove(startIndex2, info.Length - startIndex2);
          }
          else
          {
            int startIndex3 = info.IndexOf(':');
            if (startIndex3 >= 0)
              info = info.Remove(startIndex3, info.Length - startIndex3);
          }
        }
      }
      ICallProxyInterface.BaseIncomingCall(callId, number, info);
      return 1;
    }

    private static int onCallHoldConfirm(int callId)
    {
      ICallProxyInterface.BaseCallNotification(callId, ECallNotification.CN_HOLDCONFIRM, "");
      return 1;
    }
  }
}
