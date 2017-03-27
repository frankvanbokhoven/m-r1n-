// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IVoipProxy
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public abstract class IVoipProxy
  {
    private IConfiguratorInterface _config = (IConfiguratorInterface) new NullConfigurator();

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

    public abstract bool IsInitialized { get; set; }

    public event DDtmfDigitReceived DtmfDigitReceived;

    public event DMessageWaitingNotification MessageWaitingIndication;

    public event DCallReplaced CallReplaced;

    protected void BaseDtmfDigitReceived(int callId, int digit)
    {
      if (this.DtmfDigitReceived == null)
        return;
      this.DtmfDigitReceived(callId, digit);
    }

    protected void BaseMessageWaitingIndication(int mwi, string text)
    {
      if (this.MessageWaitingIndication == null)
        return;
      this.MessageWaitingIndication(mwi, text);
    }

    protected void BaseCallReplacedCallback(int oldid, int newid)
    {
      if (this.CallReplaced == null)
        return;
      this.CallReplaced(oldid, newid);
    }

    public abstract int initialize();

    public virtual int shutdown()
    {
      this.DtmfDigitReceived = (DDtmfDigitReceived) null;
      this.MessageWaitingIndication = (DMessageWaitingNotification) null;
      return 1;
    }

    public abstract void setCodecPriority(string item, int p);

    public abstract int getNoOfCodecs();

    public abstract string getCodec(int i);

    public abstract ICallProxyInterface createCallProxy();
  }
}
