// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CHoldingState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common.CallControl
{
  internal class CHoldingState : IAbstractState
  {
    public CHoldingState(CStateMachine sm)
      : base((IStateMachine) sm)
    {
      this.Id = EStateId.HOLDING;
    }

    public override void onEntry()
    {
    }

    public override void onExit()
    {
    }

    public override bool retrieveCall()
    {
      this._smref.RetrieveRequested = true;
      this.CallProxy.retrieveCall();
      this._smref.changeState(EStateId.ACTIVE);
      return true;
    }

    public override void onReleased()
    {
      this._smref.changeState(EStateId.RELEASED);
    }

    public override bool endCall()
    {
      this.CallProxy.endCall();
      this._smref.changeState(EStateId.TERMINATED);
      return base.endCall();
    }
  }
}
