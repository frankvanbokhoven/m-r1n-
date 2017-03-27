// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CActiveState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common.CallControl
{
  internal class CActiveState : IAbstractState
  {
    public CActiveState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.ACTIVE;
    }

    public override void onEntry()
    {
      this._smref.Counting = true;
      this.MediaProxy.stopTone();
    }

    public override void onExit()
    {
    }

    public override bool endCall()
    {
      this._smref.Duration = DateTime.Now.Subtract(this._smref.Time);
      this._smref.changeState(EStateId.TERMINATED);
      this.CallProxy.endCall();
      return base.endCall();
    }

    public override bool holdCall()
    {
      this._smref.HoldRequested = true;
      return this.CallProxy.holdCall();
    }

    public override bool xferCall(string number)
    {
      return this.CallProxy.xferCall(number);
    }

    public override bool xferCallSession(int partnersession)
    {
      return this.CallProxy.xferCallSession(partnersession);
    }

    public override void onHoldConfirm()
    {
      if (this._smref.HoldRequested)
      {
        this._smref.changeState(EStateId.HOLDING);
        this._smref.activatePendingAction();
      }
      this._smref.HoldRequested = false;
    }

    public override void onReleased()
    {
      this._smref.changeState(EStateId.RELEASED);
    }

    public override bool conferenceCall()
    {
      return this.CallProxy.conferenceCall();
    }
  }
}
