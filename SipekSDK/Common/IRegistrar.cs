// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IRegistrar
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public abstract class IRegistrar
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

    public event DAccountStateChanged AccountStateChanged;

    public abstract int registerAccounts();

    public abstract int unregisterAccounts();

    protected void BaseAccountStateChanged(int accountId, int accState)
    {
      if (this.AccountStateChanged == null)
        return;
      this.AccountStateChanged(accountId, accState);
    }
  }
}
