// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CIncomingState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common.CallControl
{
  internal class CIncomingState : IAbstractState
  {
    public CIncomingState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.INCOMING;
    }

    public override void onEntry()
    {
      this._smref.Incoming = true;
      int sessionId = this.SessionId;
      this._smref.startTimer(ETimerType.ENORESPONSE);
      this.CallProxy.alerted();
      this._smref.Type = ECallType.EMissed;
      this.MediaProxy.playTone(ETones.EToneRing);
      if (this._smref.Config.CFNRFlag)
        this._smref.startTimer(ETimerType.ENOREPLY);
      if (!this._smref.Config.AAFlag || this._smref.NumberOfCalls != 1)
        return;
      this.acceptCall();
    }

    public override void onExit()
    {
      this.MediaProxy.stopTone();
      this._smref.stopAllTimers();
    }

    public override bool acceptCall()
    {
      this._smref.Type = ECallType.EReceived;
      this._smref.Time = DateTime.Now;
      this.CallProxy.acceptCall();
      this._smref.changeState(EStateId.ACTIVE);
      return true;
    }

    public override void onReleased()
    {
      this._smref.changeState(EStateId.RELEASED);
    }

    public override bool xferCall(string number)
    {
      return this.CallProxy.serviceRequest(0, number);
    }

    public override bool endCall()
    {
      this._smref.changeState(EStateId.TERMINATED);
      this.CallProxy.endCall();
      return base.endCall();
    }

    public override bool noReplyTimerExpired(int sessionId)
    {
      this.CallProxy.serviceRequest(2, this._smref.Config.CFUNumber);
      return true;
    }

    public override bool noResponseTimerExpired(int sessionId)
    {
      this._smref.changeState(EStateId.TERMINATED);
      this.CallProxy.endCall();
      return true;
    }
  }
}
