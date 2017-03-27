// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullFactory
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using Sipek.Common.CallControl;

namespace Sipek.Common
{
  internal class NullFactory : AbstractFactory
  {
    public ITimer createTimer()
    {
      return (ITimer) new NullTimer();
    }

    public IStateMachine createStateMachine()
    {
      return (IStateMachine) new CStateMachine();
    }
  }
}
