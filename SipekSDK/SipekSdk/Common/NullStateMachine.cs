// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullStateMachine
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common
{
  internal class NullStateMachine : IStateMachine
  {
    public override EStateId StateId
    {
      get
      {
        return EStateId.NULL;
      }
    }

    public override string CallingName
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public override string CallingNumber
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public override bool Incoming
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override bool Is3Pty
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override bool IsHeld
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override int Session
    {
      get
      {
        return -1;
      }
      set
      {
      }
    }

    internal override IAbstractState State
    {
      get
      {
        return (IAbstractState) new NullState();
      }
    }

    public override bool IsNull
    {
      get
      {
        return true;
      }
    }

    internal override bool RetrieveRequested
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    internal override bool HoldRequested
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    internal override ICallProxyInterface CallProxy
    {
      get
      {
        return (ICallProxyInterface) new NullCallProxy();
      }
    }

    internal override IConfiguratorInterface Config
    {
      get
      {
        return (IConfiguratorInterface) new NullConfigurator();
      }
    }

    internal override IMediaProxyInterface MediaProxy
    {
      get
      {
        return (IMediaProxyInterface) new NullMediaProxy();
      }
    }

    internal override ECallType Type
    {
      get
      {
        return ECallType.EDialed;
      }
      set
      {
      }
    }

    internal override DateTime Time
    {
      get
      {
        return new DateTime();
      }
      set
      {
      }
    }

    public override TimeSpan Duration
    {
      get
      {
        return new TimeSpan();
      }
      set
      {
      }
    }

    public override TimeSpan RuntimeDuration
    {
      get
      {
        return new TimeSpan();
      }
    }

    internal override bool Counting
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override string Codec
    {
      get
      {
        return "PCMA";
      }
    }

    internal override bool DisableStateNotifications
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    internal override int NumberOfCalls
    {
      get
      {
        return 0;
      }
    }

    public override void changeState(EStateId stateId)
    {
    }

    public override void destroy()
    {
    }

    internal override bool startTimer(ETimerType ttype)
    {
      return false;
    }

    internal override bool stopTimer(ETimerType ttype)
    {
      return false;
    }

    internal override void stopAllTimers()
    {
    }

    internal override void activatePendingAction()
    {
    }
  }
}
