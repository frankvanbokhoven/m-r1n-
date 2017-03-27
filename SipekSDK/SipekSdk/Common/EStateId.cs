// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.EStateId
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public enum EStateId
  {
    NULL = 0,
    IDLE = 1,
    CONNECTING = 2,
    ALERTING = 4,
    ACTIVE = 8,
    RELEASED = 16,
    INCOMING = 32,
    HOLDING = 64,
    TERMINATED = 128,
  }
}
