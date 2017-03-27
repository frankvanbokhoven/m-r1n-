// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CIdleState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common.CallControl
{
  internal class CIdleState : IAbstractState
  {
    public CIdleState(IStateMachine sm)
      : base(sm)
    {
      this.Id = EStateId.IDLE;
    }

    public override void onEntry()
    {
    }

    public override void onExit()
    {
    }

    public override bool endCall()
    {
      this._smref.destroy();
      this.CallProxy.endCall();
      return base.endCall();
    }

    public override int makeCall(string dialedNo, int accountId)
    {
      this._smref.CallingNumber = dialedNo;
      this._smref.changeState(EStateId.CONNECTING);
      this._smref.Session = this.CallProxy.makeCall(dialedNo, accountId);
      return this._smref.Session;
    }

    public override void incomingCall(string callingNo, string display)
    {
      if (this._smref.Config.CFUFlag && this._smref.Config.CFUNumber.Length > 0)
      {
        this._smref.DisableStateNotifications = true;
        this.CallProxy.serviceRequest(1, this._smref.Config.CFUNumber);
        this.endCall();
      }
      else if (this._smref.Config.DNDFlag)
      {
        this._smref.DisableStateNotifications = true;
        this.CallProxy.serviceRequest(3, "");
        this.endCall();
      }
      else
      {
        this._smref.CallingNumber = callingNo;
        this._smref.CallingName = display;
        this._smref.changeState(EStateId.INCOMING);
      }
    }
  }
}
