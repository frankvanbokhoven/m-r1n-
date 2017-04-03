// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CStateMachine
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common.CallControl
{
  public class CStateMachine : IStateMachine
  {
    private int _session = -1;
    private string _callingNumber = "";
    private string _callingName = "";
    private IAbstractState _state;
    private CIdleState _stateIdle;
    private CConnectingState _stateCalling;
    private CAlertingState _stateAlerting;
    private CActiveState _stateActive;
    private CReleasedState _stateReleased;
    private CIncomingState _stateIncoming;
    private CHoldingState _stateHolding;
    private CTerminatedState _stateTerminated;
    private ECallType _callType;
    private TimeSpan _duration;
    private DateTime _timestamp;
    private CCallManager _manager;
    protected ITimer _noreplyTimer;
    protected ITimer _releasedTimer;
    protected ITimer _noresponseTimer;
    private ICallProxyInterface _sigProxy;
    private bool _incoming;
    private bool _isHeld;
    private bool _is3Pty;
    private bool _counting;
    private bool _holdRequested;
    private bool _retrieveRequested;
    private bool _disableStateNotifications;

    public CCallManager Manager
    {
      get
      {
        return this._manager;
      }
    }

    public override int Session
    {
      get
      {
        return this._session;
      }
      set
      {
        this._session = value;
        this.CallProxy.SessionId = value;
      }
    }

    public override string CallingNumber
    {
      get
      {
        return this._callingNumber;
      }
      set
      {
        this._callingNumber = value;
      }
    }

    public override string CallingName
    {
      get
      {
        return this._callingName;
      }
      set
      {
        this._callingName = value;
      }
    }

    public override bool Incoming
    {
      get
      {
        return this._incoming;
      }
      set
      {
        this._incoming = value;
      }
    }

    public override bool IsHeld
    {
      get
      {
        return this._isHeld;
      }
      set
      {
        this._isHeld = value;
      }
    }

    public override bool Is3Pty
    {
      get
      {
        return this._is3Pty;
      }
      set
      {
        this._is3Pty = value;
      }
    }

    public override EStateId StateId
    {
      get
      {
        return this._state.Id;
      }
    }

    internal override bool Counting
    {
      get
      {
        return this._counting;
      }
      set
      {
        this._counting = value;
      }
    }

    public override TimeSpan Duration
    {
      get
      {
        return this._duration;
      }
      set
      {
        this._duration = value;
      }
    }

    public override TimeSpan RuntimeDuration
    {
      get
      {
        if (this.Counting)
          return DateTime.Now.Subtract(this.Time);
        return TimeSpan.Zero;
      }
    }

    internal override IAbstractState State
    {
      get
      {
        return this._state;
      }
    }

    public override bool IsNull
    {
      get
      {
        return false;
      }
    }

    internal override ICallProxyInterface CallProxy
    {
      get
      {
        return this._sigProxy;
      }
    }

    internal override IMediaProxyInterface MediaProxy
    {
      get
      {
        return this._manager.MediaProxy;
      }
    }

    internal override ECallType Type
    {
      get
      {
        return this._callType;
      }
      set
      {
        this._callType = value;
      }
    }

    internal override DateTime Time
    {
      get
      {
        return this._timestamp;
      }
      set
      {
        this._timestamp = value;
      }
    }

    internal override bool HoldRequested
    {
      get
      {
        return this._holdRequested;
      }
      set
      {
        this._holdRequested = value;
      }
    }

    internal override bool RetrieveRequested
    {
      get
      {
        return this._retrieveRequested;
      }
      set
      {
        this._retrieveRequested = value;
      }
    }

    internal override IConfiguratorInterface Config
    {
      get
      {
        return this._manager.Config;
      }
    }

    protected ICallLogInterface CallLoger
    {
      get
      {
        return this._manager.CallLogger;
      }
    }

    public override string Codec
    {
      get
      {
        return this._sigProxy.getCurrentCodec();
      }
    }

    internal override bool DisableStateNotifications
    {
      get
      {
        return this._disableStateNotifications;
      }
      set
      {
        this._disableStateNotifications = value;
      }
    }

    internal override int NumberOfCalls
    {
      get
      {
        return this.Manager.Count;
      }
    }

    public CStateMachine()
    {
      this._manager = CCallManager.Instance;
      this._sigProxy = this._manager.StackProxy.createCallProxy();
      this._stateIdle = new CIdleState((IStateMachine) this);
      this._stateAlerting = new CAlertingState(this);
      this._stateActive = new CActiveState(this);
      this._stateCalling = new CConnectingState(this);
      this._stateReleased = new CReleasedState(this);
      this._stateIncoming = new CIncomingState(this);
      this._stateHolding = new CHoldingState(this);
      this._stateTerminated = new CTerminatedState(this);
      this._state = (IAbstractState) this._stateIdle;
      this.Time = DateTime.Now;
      this.Duration = TimeSpan.Zero;
      if (this._manager == null)
        return;
      this._noreplyTimer = this._manager.Factory.createTimer();
      this._noreplyTimer.Interval = 15000;
      this._noreplyTimer.Elapsed = new TimerExpiredCallback(this._noreplyTimer_Elapsed);
      this._releasedTimer = this._manager.Factory.createTimer();
      this._releasedTimer.Interval = 5000;
      this._releasedTimer.Elapsed = new TimerExpiredCallback(this._releasedTimer_Elapsed);
      this._noresponseTimer = this._manager.Factory.createTimer();
      this._noresponseTimer.Interval = 60000;
      this._noresponseTimer.Elapsed = new TimerExpiredCallback(this._noresponseTimer_Elapsed);
    }

    private void changeState(IAbstractState state)
    {
      this._state.onExit();
      this._state = state;
      this._state.onEntry();
    }

    private void _noreplyTimer_Elapsed(object sender, EventArgs e)
    {
      this.State.noReplyTimerExpired(this.Session);
    }

    private void _releasedTimer_Elapsed(object sender, EventArgs e)
    {
      this.State.releasedTimerExpired(this.Session);
    }

    private void _noresponseTimer_Elapsed(object sender, EventArgs e)
    {
      this.State.noResponseTimerExpired(this.Session);
    }

    internal override bool startTimer(ETimerType ttype)
    {
      bool flag = false;
      switch (ttype)
      {
        case ETimerType.ENOREPLY:
          flag = this._noreplyTimer.Start();
          break;
        case ETimerType.ERELEASED:
          flag = this._releasedTimer.Start();
          break;
        case ETimerType.ENORESPONSE:
          flag = this._noresponseTimer.Start();
          break;
      }
      return flag;
    }

    internal override bool stopTimer(ETimerType ttype)
    {
      bool flag = false;
      switch (ttype)
      {
        case ETimerType.ENOREPLY:
          flag = this._noreplyTimer.Stop();
          break;
        case ETimerType.ERELEASED:
          flag = this._releasedTimer.Stop();
          break;
        case ETimerType.ENORESPONSE:
          flag = this._noresponseTimer.Stop();
          break;
      }
      return flag;
    }

    internal override void stopAllTimers()
    {
      this._noreplyTimer.Stop();
      this._releasedTimer.Stop();
      this._noresponseTimer.Stop();
    }

    internal override void activatePendingAction()
    {
      this.Manager.activatePendingAction();
    }

    public override void changeState(EStateId stateId)
    {
      switch (stateId)
      {
        case EStateId.INCOMING:
          this.changeState((IAbstractState) this._stateIncoming);
          break;
        case EStateId.HOLDING:
          this.changeState((IAbstractState) this._stateHolding);
          break;
        case EStateId.TERMINATED:
          this.changeState((IAbstractState) this._stateTerminated);
          break;
        case EStateId.IDLE:
          this.changeState((IAbstractState) this._stateIdle);
          break;
        case EStateId.CONNECTING:
          this.changeState((IAbstractState) this._stateCalling);
          break;
        case EStateId.ALERTING:
          this.changeState((IAbstractState) this._stateAlerting);
          break;
        case EStateId.ACTIVE:
          this.changeState((IAbstractState) this._stateActive);
          break;
        case EStateId.RELEASED:
          this.changeState((IAbstractState) this._stateReleased);
          break;
      }
      if (this._manager == null || this.Session == -1 || this.DisableStateNotifications)
        return;
      this._manager.updateGui(this.Session);
    }

    public override void destroy()
    {
      this.stopAllTimers();
      this.MediaProxy.stopTone();
      if (this.Counting)
        this.Duration = DateTime.Now.Subtract(this.Time);
      if ((this.Type != ECallType.EDialed || this.CallingNumber.Length > 0) && this.Type != ECallType.EUndefined)
      {
        this.CallLoger.addCall(this.Type, this.CallingNumber, this.CallingName, this.Time, this.Duration);
        this.CallLoger.save();
      }
      this.CallingNumber = "";
      this.Incoming = false;
      this.changeState(EStateId.IDLE);
      if (this._manager == null)
        return;
      this._manager.destroySession(this.Session);
    }
  }
}
