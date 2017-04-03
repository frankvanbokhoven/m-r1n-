// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IAccount
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public interface IAccount
  {
    int Index { get; set; }

    string AccountName { get; set; }

    string HostName { get; set; }

    string Id { get; set; }

    string UserName { get; set; }

    string Password { get; set; }

    string DisplayName { get; set; }

    string DomainName { get; set; }

    int RegState { get; set; }

    string ProxyAddress { get; set; }

    ETransportMode TransportMode { get; set; }
  }
}
