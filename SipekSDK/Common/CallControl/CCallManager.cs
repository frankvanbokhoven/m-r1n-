// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CallControl.CCallManager
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;
using System.Collections.Generic;

namespace Sipek.Common.CallControl
{
  public class CCallManager
  {
    private AbstractFactory _factory = (AbstractFactory) new NullFactory();
    private IMediaProxyInterface _media = (IMediaProxyInterface) new NullMediaProxy();
    private ICallLogInterface _callLog = (ICallLogInterface) new NullCallLogger();
    private IVoipProxy _stack = (IVoipProxy) new NullVoipProxy();
    private IConfiguratorInterface _config = (IConfiguratorInterface) new NullConfigurator();
    private static CCallManager _instance;
    private Dictionary<int, IStateMachine> _calls;
    private CCallManager.PendingAction _pendingAction;
    private bool _initialized;

    public AbstractFactory Factory
    {
      get
      {
        return this._factory;
      }
      set
      {
        this._factory = value;
      }
    }

    public IMediaProxyInterface MediaProxy
    {
      get
      {
        return this._media;
      }
      set
      {
        this._media = value;
      }
    }

    public ICallLogInterface CallLogger
    {
      get
      {
        return this._callLog;
      }
      set
      {
        this._callLog = value;
      }
    }

    public IVoipProxy StackProxy
    {
      get
      {
        return this._stack;
      }
      set
      {
        this._stack = value;
      }
    }

    public IConfiguratorInterface Config
    {
      get
      {
        return this._config;
      }
      set
      {
        this._config = value;
      }
    }

    public IStateMachine this[int index]
    {
      get
      {
        if (!this._calls.ContainsKey(index))
          return (IStateMachine) new NullStateMachine();
        return this._calls[index];
      }
    }

    public Dictionary<int, IStateMachine> CallList
    {
      get
      {
        return this._calls;
      }
    }

    public int Count
    {
      get
      {
        return this._calls.Count;
      }
    }

    public bool Is3Pty
    {
      get
      {
        return this.getNoCallsInState(EStateId.ACTIVE) == 2;
      }
    }

    public bool IsInitialized
    {
      get
      {
        return this._initialized;
      }
    }

    public static CCallManager Instance
    {
      get
      {
        if (CCallManager._instance == null)
          CCallManager._instance = new CCallManager();
        return CCallManager._instance;
      }
    }

    public event DCallStateRefresh CallStateRefresh;

    public event DIncomingCallNotification IncomingCallNotification;

    public void updateGui(int sessionId)
    {
      if (this.CallStateRefresh == null)
        return;
      this.CallStateRefresh(sessionId);
    }

    public int Initialize()
    {
      return this.Initialize(this._stack);
    }

    public int Initialize(IVoipProxy proxy)
    {
      this._stack = proxy;
      if (!this.IsInitialized)
      {
        ICallProxyInterface.CallStateChanged += new DCallStateChanged(this.OnCallStateChanged);
        ICallProxyInterface.CallIncoming += new DCallIncoming(this.OnIncomingCall);
        ICallProxyInterface.CallNotification += new DCallNotification(this.OnCallNotification);
        this._calls = new Dictionary<int, IStateMachine>();
      }
      int num = this.StackProxy.initialize();
      if (num != 0)
        return num;
      this._initialized = true;
      return num;
    }

    public void Shutdown()
    {
      IStateMachine[] array = new IStateMachine[this.CallList.Count];
      this.CallList.Values.CopyTo(array, 0);
      for (int index = 0; index < array.Length; ++index)
        array[index].destroy();
      this.CallList.Clear();
      this.StackProxy.shutdown();
      this._initialized = false;
      this.CallStateRefresh = (DCallStateRefresh) null;
      this.IncomingCallNotification = (DIncomingCallNotification) null;
      ICallProxyInterface.CallStateChanged -= new DCallStateChanged(this.OnCallStateChanged);
      ICallProxyInterface.CallIncoming -= new DCallIncoming(this.OnIncomingCall);
      ICallProxyInterface.CallNotification -= new DCallNotification(this.OnCallNotification);
      this.StackProxy.CallReplaced -= new DCallReplaced(this.OnCallReplaced);
    }

    public IStateMachine createOutboundCall(string number)
    {
      int defaultAccountIndex = this.Config.DefaultAccountIndex;
      return this.createOutboundCall(number, defaultAccountIndex);
    }

    public IStateMachine createOutboundCall(string number, int accountId)
    {
      if (!this.IsInitialized)
        return (IStateMachine) new NullStateMachine();
      if (this.getNoCallsInStates(6) > 0)
        return (IStateMachine) new NullStateMachine();
      if (this.getNoCallsInState(EStateId.ACTIVE) == 0)
      {
        IStateMachine stateMachine = this.Factory.createStateMachine();
        if (stateMachine == null)
          return (IStateMachine) null;
        int key = stateMachine.State.makeCall(number, accountId);
        if (key == -1)
          return (IStateMachine) new NullStateMachine();
        try
        {
          stateMachine.Session = key;
          this._calls.Add(key, stateMachine);
        }
        catch (ArgumentException)
        {
          this._calls[key].destroy();
          this._calls.Add(key, stateMachine);
        }
        return stateMachine;
      }
      this._pendingAction = new CCallManager.PendingAction(CCallManager.EPendingActions.ECreateSession, number, accountId);
      this.getCallInState(EStateId.ACTIVE).State.holdCall();
      return (IStateMachine) new NullStateMachine();
    }

    internal void destroySession(int session)
    {
      bool flag = true;
      if (this.getCall(session).DisableStateNotifications)
        flag = false;
      this._calls.Remove(session);
      if (!flag)
        return;
      this.updateGui(session);
    }

    public IStateMachine getCall(int session)
    {
      if (this._calls.Count == 0 || !this._calls.ContainsKey(session))
        return (IStateMachine) new NullStateMachine();
      return this._calls[session];
    }

    public IStateMachine getCallInState(EStateId stateId)
    {
      if (this._calls.Count == 0)
        return (IStateMachine) new NullStateMachine();
      foreach (KeyValuePair<int, IStateMachine> call in this._calls)
      {
        if (call.Value.State.Id == stateId)
          return call.Value;
      }
      return (IStateMachine) new NullStateMachine();
    }

    public int getNoCallsInState(EStateId stateId)
    {
      int num = 0;
      foreach (KeyValuePair<int, IStateMachine> call in this._calls)
      {
        if (stateId == call.Value.State.Id)
          ++num;
      }
      return num;
    }

    private int getNoCallsInStates(int states)
    {
      int num = 0;
      foreach (KeyValuePair<int, IStateMachine> call in this._calls)
      {
        if (((EStateId) states & call.Value.State.Id) == call.Value.State.Id)
          ++num;
      }
      return num;
    }

    public ICollection<IStateMachine> enumCallsInState(EStateId stateId)
    {
      List<IStateMachine> istateMachineList = new List<IStateMachine>();
      foreach (KeyValuePair<int, IStateMachine> call in this._calls)
      {
        if (stateId == call.Value.State.Id)
          istateMachineList.Add(call.Value);
      }
      return (ICollection<IStateMachine>) istateMachineList;
    }

    public void onUserRelease(int session)
    {
      this[session].State.endCall();
    }

    public void onUserAnswer(int session)
    {
      List<IStateMachine> istateMachineList = (List<IStateMachine>) this.enumCallsInState(EStateId.ACTIVE);
      if (istateMachineList.Count > 0)
      {
        IStateMachine istateMachine = istateMachineList[0];
        if (!istateMachine.IsNull)
          istateMachine.State.holdCall();
        this._pendingAction = new CCallManager.PendingAction(CCallManager.EPendingActions.EUserAnswer, session);
      }
      else
        this[session].State.acceptCall();
    }

    public void onUserHoldRetrieve(int session)
    {
      IAbstractState state = this[session].State;
      if (state.Id == EStateId.ACTIVE)
      {
        this.getCall(session).State.holdCall();
      }
      else
      {
        if (state.Id != EStateId.HOLDING)
          return;
        if (this.getNoCallsInState(EStateId.ACTIVE) > 0)
        {
          IStateMachine istateMachine = ((List<IStateMachine>) this.enumCallsInState(EStateId.ACTIVE))[0];
          if (!istateMachine.IsNull)
            istateMachine.State.holdCall();
          this._pendingAction = new CCallManager.PendingAction(CCallManager.EPendingActions.EUserHold, session);
        }
        else
          this[session].State.retrieveCall();
      }
    }

    public void onUserTransfer(int session, string number)
    {
      this[session].State.xferCall(number);
    }

    public void onUserDialDigit(int session, string digits, EDtmfMode mode)
    {
      this[session].State.dialDtmf(digits, mode);
    }

    public void onUserConference(int session)
    {
      if (this.getNoCallsInState(EStateId.ACTIVE) != 1 || this.getNoCallsInState(EStateId.HOLDING) < 1)
        return;
      IStateMachine callInState = this.getCallInState(EStateId.HOLDING);
      callInState.State.retrieveCall();
      callInState.State.conferenceCall();
    }

    public void activatePendingAction()
    {
      if (this._pendingAction != null)
        this._pendingAction.Activate();
      this._pendingAction = (CCallManager.PendingAction) null;
    }

    private void OnCallStateChanged(int callId, ESessionState callState, string info)
    {
      if (callState == ESessionState.SESSION_STATE_INCOMING)
      {
        IStateMachine stateMachine = this.Factory.createStateMachine();
        if (stateMachine.IsNull && this.Config.CFBFlag)
        {
          this.StackProxy.createCallProxy().serviceRequest(5, this.Config.CFBNumber);
        }
        else
        {
          stateMachine.Session = callId;
          if (this.CallList.ContainsKey(callId))
            this.CallList[callId].State.endCall();
          else
            this._calls.Add(callId, stateMachine);
        }
      }
      else
      {
        IStateMachine call = this.getCall(callId);
        if (call.IsNull)
          return;
        switch (callState)
        {
          case ESessionState.SESSION_STATE_EARLY:
            call.State.onAlerting();
            break;
          case ESessionState.SESSION_STATE_CONNECTING:
            call.State.onConnect();
            break;
          case ESessionState.SESSION_STATE_DISCONNECTED:
            call.State.onReleased();
            break;
        }
      }
    }

    private void OnIncomingCall(int sessionId, string number, string info)
    {
      IStateMachine call = this.getCall(sessionId);
      if (call.IsNull)
        return;
      call.State.incomingCall(number, info);
      if (this.IncomingCallNotification == null || call.DisableStateNotifications)
        return;
      this.IncomingCallNotification(sessionId, number, info);
    }

    private void OnCallNotification(int callId, ECallNotification notFlag, string text)
    {
      if (notFlag != ECallNotification.CN_HOLDCONFIRM)
        return;
      IStateMachine call = this.getCall(callId);
      if (call.IsNull)
        return;
      call.State.onHoldConfirm();
    }

    private void OnCallReplaced(int oldid, int newid)
    {
      IStateMachine call = this.CallList[oldid];
      this._calls.Remove(oldid);
      call.Session = newid;
      this.CallList.Add(newid, call);
    }

    private enum EPendingActions
    {
      EUserAnswer,
      ECreateSession,
      EUserHold,
    }

    private class PendingAction
    {
      private CCallManager.EPendingActions _actionType;
      private int _sessionId;
      private int _accountId;
      private string _number;

      public PendingAction(CCallManager.EPendingActions action, int sessionId)
      {
        this._actionType = action;
        this._sessionId = sessionId;
      }

      public PendingAction(CCallManager.EPendingActions action, string number, int accId)
      {
        this._actionType = action;
        this._sessionId = -1;
        this._number = number;
        this._accountId = accId;
      }

      public void Activate()
      {
        switch (this._actionType)
        {
          case CCallManager.EPendingActions.EUserAnswer:
            CCallManager.Instance.onUserAnswer(this._sessionId);
            break;
          case CCallManager.EPendingActions.ECreateSession:
            CCallManager.Instance.createOutboundCall(this._number, this._accountId);
            break;
          case CCallManager.EPendingActions.EUserHold:
            CCallManager.Instance.onUserHoldRetrieve(this._sessionId);
            break;
        }
      }

      private delegate void DPendingAnswer(int sessionId);

      private delegate void DPendingCreateSession(string number, int accountId);
    }
  }
}
