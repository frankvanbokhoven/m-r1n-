// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CReleasedState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common.CallControl
{
  internal class CReleasedState : IAbstractState
  {
    public CReleasedState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.RELEASED;
    }

    public override void onEntry()
    {
      this.MediaProxy.playTone(ETones.EToneCongestion);
      if (this._smref.startTimer(ETimerType.ERELEASED))
        return;
      this._smref.destroy();
    }

    public override void onExit()
    {
      this.MediaProxy.stopTone();
      this._smref.stopAllTimers();
    }

    public override bool endCall()
    {
      this.CallProxy.endCall();
      this._smref.destroy();
      return true;
    }

    public override bool releasedTimerExpired(int sessionId)
    {
      this._smref.destroy();
      return true;
    }

    public override void onReleased()
    {
      this._smref.destroy();
    }
  }
}
