// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullTimer
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  internal class NullTimer : ITimer
  {
    public int Interval
    {
      get
      {
        return 100;
      }
      set
      {
      }
    }

    public TimerExpiredCallback Elapsed
    {
      set
      {
      }
    }

    public bool Start()
    {
      return false;
    }

    public bool Stop()
    {
      return false;
    }
  }
}
