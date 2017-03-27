// SipekSdk with PJSUA2, 2017
// Type: Sipek.Sip.pjsipPresenceAndMessaging
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using Sipek.Common;
using System.Runtime.InteropServices;

namespace Sipek.Sip
{
  public class pjsipPresenceAndMessaging : IPresenceAndMessaging
  {
    private static pjsipPresenceAndMessaging.OnMessageReceivedCallback mrdel = new pjsipPresenceAndMessaging.OnMessageReceivedCallback(pjsipPresenceAndMessaging.onMessageReceived);
    private static pjsipPresenceAndMessaging.OnBuddyStatusChangedCallback bscdel = new pjsipPresenceAndMessaging.OnBuddyStatusChangedCallback(pjsipPresenceAndMessaging.onBuddyStatusChanged);
    private static pjsipPresenceAndMessaging _instance = (pjsipPresenceAndMessaging) null;
    internal const string PJSIP_DLL = "pjsipDll.dll";

    public static pjsipPresenceAndMessaging Instance
    {
      get
      {
        if (pjsipPresenceAndMessaging._instance == null)
          pjsipPresenceAndMessaging._instance = new pjsipPresenceAndMessaging();
        return pjsipPresenceAndMessaging._instance;
      }
    }

    private pjsipPresenceAndMessaging()
    {
      pjsipPresenceAndMessaging.onBuddyStatusChangedCallback(pjsipPresenceAndMessaging.bscdel);
      pjsipPresenceAndMessaging.onMessageReceivedCallback(pjsipPresenceAndMessaging.mrdel);
    }

    [DllImport("pjsipDll.dll")]
    private static extern int dll_addBuddy(string uri, bool subscribe);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_removeBuddy(int buddyId);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_sendMessage(int buddyId, string uri, string message);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_setStatus(int accId, int presence_state);

    [DllImport("pjsipDll.dll")]
    private static extern int onMessageReceivedCallback(pjsipPresenceAndMessaging.OnMessageReceivedCallback cb);

    [DllImport("pjsipDll.dll")]
    private static extern int onBuddyStatusChangedCallback(pjsipPresenceAndMessaging.OnBuddyStatusChangedCallback cb);

    public override int addBuddy(string name, bool presence, int accId)
    {
      if (!pjsipStackProxy.Instance.IsInitialized)
        return -1;
      string sipuri = name.IndexOf("sip:") != 0 ? "sip:" + name + "@" + this.Config.Accounts[accId].HostName : name;
      return pjsipPresenceAndMessaging.dll_addBuddy(pjsipStackProxy.Instance.SetTransport(accId, sipuri), presence);
    }

    public override int delBuddy(int buddyId)
    {
      return pjsipPresenceAndMessaging.dll_removeBuddy(buddyId);
    }

    public override int sendMessage(string destAddress, string message, int accId)
    {
      if (!pjsipStackProxy.Instance.IsInitialized)
        return -1;
      string sipuri = destAddress.IndexOf("sip:") != 0 ? "sip:" + destAddress + "@" + this.Config.Accounts[accId].HostName : destAddress;
      string uri = pjsipStackProxy.Instance.SetTransport(accId, sipuri);
      return pjsipPresenceAndMessaging.dll_sendMessage(this.Config.Accounts[accId].Index, uri, message);
    }

    public override int sendMessage(string destAddress, string message)
    {
      return this.sendMessage(destAddress, message, this.Config.Accounts[this.Config.DefaultAccountIndex].Index);
    }

    public override int setStatus(int accId, EUserStatus status)
    {
      if (!pjsipStackProxy.Instance.IsInitialized || accId < 0 || this.Config.Accounts.Count > 0 && this.Config.Accounts[accId].RegState != 200 || !this.Config.PublishEnabled)
        return -1;
      return pjsipPresenceAndMessaging.dll_setStatus(this.Config.Accounts[accId].Index, (int) status);
    }

    private static int onMessageReceived(string from, string text)
    {
      pjsipPresenceAndMessaging.Instance.BaseMessageReceived(from.ToString(), text.ToString());
      return 1;
    }

    private static int onBuddyStatusChanged(int buddyId, int status, string text)
    {
      pjsipPresenceAndMessaging.Instance.BaseBuddyStatusChanged(buddyId, status, text.ToString());
      return 1;
    }

    private delegate int OnMessageReceivedCallback(string from, string message);

    private delegate int OnBuddyStatusChangedCallback(int buddyId, int status, string statusText);
  }
}
