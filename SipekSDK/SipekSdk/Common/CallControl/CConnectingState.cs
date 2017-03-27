// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CConnectingState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common.CallControl
{
  internal class CConnectingState : IAbstractState
  {
    public CConnectingState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.CONNECTING;
    }

    public override void onEntry()
    {
      this._smref.Type = ECallType.EDialed;
    }

    public override void onExit()
    {
    }

    public override void onReleased()
    {
      this._smref.changeState(EStateId.RELEASED);
    }

    public override void onAlerting()
    {
      this._smref.changeState(EStateId.ALERTING);
    }

    public override void onConnect()
    {
      this._smref.changeState(EStateId.ACTIVE);
    }

    public override bool endCall()
    {
      this._smref.changeState(EStateId.TERMINATED);
      this.CallProxy.endCall();
      return base.endCall();
    }
  }
}
