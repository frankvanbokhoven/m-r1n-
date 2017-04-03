// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullState
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  internal class NullState : IAbstractState
  {
    public NullState()
      : base((IStateMachine) new NullStateMachine())
    {
    }

    public override void onEntry()
    {
    }

    public override void onExit()
    {
    }

    public override bool conferenceCall()
    {
      return false;
    }
  }
}
