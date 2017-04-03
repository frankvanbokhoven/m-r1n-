// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IAbstractState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  internal abstract class IAbstractState : ICallProxyInterface
  {
    private EStateId _stateId = EStateId.IDLE;
    protected IStateMachine _smref;

    public EStateId Id
    {
      get
      {
        return this._stateId;
      }
      set
      {
        this._stateId = value;
      }
    }

    public ICallProxyInterface CallProxy
    {
      get
      {
        return this._smref.CallProxy;
      }
    }

    public IMediaProxyInterface MediaProxy
    {
      get
      {
        return this._smref.MediaProxy;
      }
    }

    public override int SessionId
    {
      get
      {
        return this._smref.Session;
      }
      set
      {
      }
    }

    public IAbstractState(IStateMachine sm)
    {
      this._smref = sm;
    }

    public override string ToString()
    {
      return this._stateId.ToString();
    }

    public abstract void onEntry();

    public abstract void onExit();

    public virtual bool noReplyTimerExpired(int sessionId)
    {
      return false;
    }

    public virtual bool releasedTimerExpired(int sessionId)
    {
      return false;
    }

    public virtual bool noResponseTimerExpired(int sessionId)
    {
      return false;
    }

    public override int makeCall(string dialedNo, int accountId)
    {
      return -1;
    }

    public override bool endCall()
    {
      return true;
    }

    public override bool acceptCall()
    {
      return true;
    }

    public override bool alerted()
    {
      return true;
    }

    public override bool holdCall()
    {
      return true;
    }

    public override bool retrieveCall()
    {
      return true;
    }

    public override bool xferCall(string number)
    {
      return true;
    }

    public override bool xferCallSession(int partnersession)
    {
      return true;
    }

    public override bool threePtyCall(int partnersession)
    {
      return true;
    }

    public override bool serviceRequest(int code, string dest)
    {
      this.CallProxy.serviceRequest(code, dest);
      return true;
    }

    public override bool dialDtmf(string digits, EDtmfMode mode)
    {
      this.CallProxy.dialDtmf(digits, mode);
      return true;
    }

    public override string getCurrentCodec()
    {
      return "";
    }

    public override bool conferenceCall()
    {
      return false;
    }

    public virtual void incomingCall(string callingNo, string display)
    {
    }

    public virtual void onAlerting()
    {
    }

    public virtual void onConnect()
    {
    }

    public virtual void onReleased()
    {
    }

    public virtual void onHoldConfirm()
    {
    }
  }
}
