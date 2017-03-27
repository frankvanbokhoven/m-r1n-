// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullCallProxy
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  internal class NullCallProxy : ICallProxyInterface
  {
    public override int SessionId
    {
      get
      {
        return 0;
      }
      set
      {
      }
    }

    public override int makeCall(string dialedNo, int accountId)
    {
      return 1;
    }

    public int makeCallByUri(string uri)
    {
      return 1;
    }

    public override bool endCall()
    {
      return false;
    }

    public override bool alerted()
    {
      return false;
    }

    public override bool acceptCall()
    {
      return false;
    }

    public override bool holdCall()
    {
      return false;
    }

    public override bool retrieveCall()
    {
      return false;
    }

    public override bool xferCall(string number)
    {
      return false;
    }

    public override bool xferCallSession(int session)
    {
      return false;
    }

    public override bool threePtyCall(int session)
    {
      return false;
    }

    public override bool serviceRequest(int code, string dest)
    {
      return false;
    }

    public override bool dialDtmf(string digits, EDtmfMode mode)
    {
      return false;
    }

    public override string getCurrentCodec()
    {
      return "PCMA";
    }

    public override bool conferenceCall()
    {
      return false;
    }
  }
}
