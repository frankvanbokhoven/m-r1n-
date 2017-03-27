// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.ICallProxyInterface
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public abstract class ICallProxyInterface
  {
    public abstract int SessionId { get; set; }

    public static event DCallStateChanged CallStateChanged;

    public static event DCallIncoming CallIncoming;

    public static event DCallNotification CallNotification;

    protected static void BaseCallStateChanged(int callId, ESessionState callState, string info)
    {
      if (ICallProxyInterface.CallStateChanged == null)
        return;
      ICallProxyInterface.CallStateChanged(callId, callState, info);
    }

    protected static void BaseIncomingCall(int callId, string number, string info)
    {
      if (ICallProxyInterface.CallIncoming == null)
        return;
      ICallProxyInterface.CallIncoming(callId, number, info);
    }

    protected static void BaseCallNotification(int callId, ECallNotification notifFlag, string text)
    {
      if (ICallProxyInterface.CallNotification == null)
        return;
      ICallProxyInterface.CallNotification(callId, notifFlag, text);
    }

    public abstract int makeCall(string dialedNo, int accountId);

    public abstract bool endCall();

    public abstract bool alerted();

    public abstract bool acceptCall();

    public abstract bool holdCall();

    public abstract bool retrieveCall();

    public abstract bool xferCall(string number);

    public abstract bool xferCallSession(int partnersession);

    public abstract bool threePtyCall(int partnersession);

    public abstract bool serviceRequest(int code, string dest);

    public abstract bool dialDtmf(string digits, EDtmfMode mode);

    public abstract string getCurrentCodec();

    public abstract bool conferenceCall();
  }
}
