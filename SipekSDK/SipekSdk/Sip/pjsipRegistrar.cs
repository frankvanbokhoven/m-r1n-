// SipekSdk with PJSUA2, 2017
// Type: Sipek.Sip.pjsipRegistrar
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using Sipek.Common;
using System.Runtime.InteropServices;

namespace Sipek.Sip
{
  public class pjsipRegistrar : IRegistrar
  {
    private static pjsipRegistrar _instance = (pjsipRegistrar) null;
    private static pjsipRegistrar.OnRegStateChanged rsDel = new pjsipRegistrar.OnRegStateChanged(pjsipRegistrar.onRegStateChanged);
    internal const string PJSIP_DLL = "pjsipDll.dll";

    public static pjsipRegistrar Instance
    {
      get
      {
        if (pjsipRegistrar._instance == null)
          pjsipRegistrar._instance = new pjsipRegistrar();
        return pjsipRegistrar._instance;
      }
    }

    private pjsipRegistrar()
    {
      pjsipRegistrar.onRegStateCallback(pjsipRegistrar.rsDel);
    }

    [DllImport("pjsipDll.dll")]
    private static extern int dll_registerAccount(string uri, string reguri, string domain, string username, string password, string proxy, bool isdefault);

    [DllImport("pjsipDll.dll")]
    private static extern int dll_removeAccounts();

    [DllImport("pjsipDll.dll")]
    private static extern int onRegStateCallback(pjsipRegistrar.OnRegStateChanged cb);

    public override int registerAccounts()
    {
      if (!pjsipStackProxy.Instance.IsInitialized)
        return -1;
      if (this.Config.Accounts.Count <= 0)
        return 0;
      pjsipRegistrar.dll_removeAccounts();
      for (int accountId = 0; accountId < this.Config.Accounts.Count; ++accountId)
      {
        IAccount account = this.Config.Accounts[accountId];
        if (account == null)
          return -1;
        this.Config.Accounts[accountId].Index = -1;
        this.BaseAccountStateChanged(accountId, 0);
        if (account.Id.Length > 0 && account.HostName.Length > 0)
        {
          string displayName = account.DisplayName;
          string uri = "sip:" + account.UserName;
          if (account.UserName.IndexOf("@") < 0)
            uri = uri + "@" + account.HostName;
          string sipuri = "sip:" + account.HostName;
          string reguri = pjsipStackProxy.Instance.SetTransport(accountId, sipuri);
          string domainName = account.DomainName;
          string userName = account.UserName;
          string password = account.Password;
          string proxy = "";
          if (account.ProxyAddress.Length > 0)
            proxy = "sip:" + account.ProxyAddress;
          int num = pjsipRegistrar.dll_registerAccount(uri, reguri, domainName, userName, password, proxy, accountId == this.Config.DefaultAccountIndex);
          this.Config.Accounts[accountId].Index = num;
        }
      }
      return 1;
    }

    public override int unregisterAccounts()
    {
      return pjsipRegistrar.dll_removeAccounts();
    }

    private static int onRegStateChanged(int accId, int regState)
    {
      for (int accountId = 0; accountId < pjsipRegistrar.Instance.Config.Accounts.Count; ++accountId)
      {
        if (pjsipRegistrar.Instance.Config.Accounts[accountId].Index == accId)
        {
          pjsipRegistrar.Instance.Config.Accounts[accountId].RegState = regState;
          pjsipRegistrar.Instance.BaseAccountStateChanged(accountId, regState);
          return 1;
        }
      }
      return -1;
    }

    private delegate int OnRegStateChanged(int accountId, int regState);
  }
}
