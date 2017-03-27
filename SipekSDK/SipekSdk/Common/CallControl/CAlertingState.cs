// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CAlertingState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common.CallControl
{
  internal class CAlertingState : IAbstractState
  {
    public CAlertingState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.ALERTING;
    }

    public override void onEntry()
    {
      this.MediaProxy.playTone(ETones.EToneRingback);
    }

    public override void onExit()
    {
      this.MediaProxy.stopTone();
    }

    public override void onConnect()
    {
      this._smref.Time = DateTime.Now;
      this._smref.changeState(EStateId.ACTIVE);
    }

    public override void onReleased()
    {
      this._smref.changeState(EStateId.RELEASED);
    }

    public override bool endCall()
    {
      this._smref.changeState(EStateId.TERMINATED);
      this.CallProxy.endCall();
      return base.endCall();
    }
  }
}
