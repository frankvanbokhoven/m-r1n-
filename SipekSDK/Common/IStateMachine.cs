// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IStateMachine
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common
{
  public abstract class IStateMachine
  {
    public abstract bool IsNull { get; }

    public abstract string CallingName { get; set; }

    public abstract string CallingNumber { get; set; }

    public abstract bool Incoming { get; set; }

    public abstract bool Is3Pty { get; set; }

    public abstract bool IsHeld { get; set; }

    public abstract int Session { get; set; }

    public abstract TimeSpan RuntimeDuration { get; }

    public abstract TimeSpan Duration { get; set; }

    public abstract EStateId StateId { get; }

    public abstract string Codec { get; }

    internal abstract bool DisableStateNotifications { get; set; }

    internal abstract int NumberOfCalls { get; }

    internal abstract IAbstractState State { get; }

    internal abstract bool RetrieveRequested { get; set; }

    internal abstract bool HoldRequested { get; set; }

    internal abstract ICallProxyInterface CallProxy { get; }

    internal abstract IConfiguratorInterface Config { get; }

    internal abstract IMediaProxyInterface MediaProxy { get; }

    internal abstract ECallType Type { get; set; }

    internal abstract DateTime Time { get; set; }

    internal abstract bool Counting { get; set; }

    public abstract void changeState(EStateId stateId);

    public abstract void destroy();

    internal abstract bool startTimer(ETimerType ttype);

    internal abstract bool stopTimer(ETimerType ttype);

    internal abstract void stopAllTimers();

    internal abstract void activatePendingAction();
  }
}
