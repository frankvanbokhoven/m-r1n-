﻿// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.AbstractFactory
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public interface AbstractFactory
  {
    ITimer createTimer();

    IStateMachine createStateMachine();
  }
}
