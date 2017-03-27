// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IPresenceAndMessaging
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public abstract class IPresenceAndMessaging
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

    public event DBuddyStatusChanged BuddyStatusChanged;

    public event DMessageReceived MessageReceived;

    public abstract int addBuddy(string ident, bool presence, int accId);

    public int addBuddy(string ident, bool presence)
    {
      return this.addBuddy(ident, presence, this.Config.DefaultAccountIndex);
    }

    public abstract int delBuddy(int buddyId);

    public abstract int sendMessage(string destAddress, string message);

    public abstract int sendMessage(string destAddress, string message, int accId);

    public abstract int setStatus(int accId, EUserStatus presence_state);

    protected void BaseMessageReceived(string from, string text)
    {
      if (this.MessageReceived == null)
        return;
      this.MessageReceived(from, text);
    }

    protected void BaseBuddyStatusChanged(int buddyId, int status, string text)
    {
      if (this.BuddyStatusChanged == null)
        return;
      this.BuddyStatusChanged(buddyId, status, text);
    }
  }
}
