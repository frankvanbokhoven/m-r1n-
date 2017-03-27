// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CTerminatedState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common.CallControl
{
  internal class CTerminatedState : IAbstractState
  {
    public CTerminatedState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.TERMINATED;
    }

    public override void onEntry()
    {
      if (this._smref.startTimer(ETimerType.ERELEASED))
        return;
      this._smref.destroy();
    }

    public override void onExit()
    {
      this._smref.stopAllTimers();
    }

    public override bool endCall()
    {
      this.CallProxy.endCall();
      return true;
    }

    public override bool releasedTimerExpired(int sessionId)
    {
      this._smref.destroy();
      return true;
    }

    public override void onAlerting()
    {
      this.CallProxy.endCall();
    }

    public override void onConnect()
    {
      this.CallProxy.endCall();
    }

    public override void onReleased()
    {
      this._smref.destroy();
    }
  }
}
