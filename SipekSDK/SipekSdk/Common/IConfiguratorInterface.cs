// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.IConfiguratorInterface
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System.Collections.Generic;

namespace Sipek.Common
{
  public interface IConfiguratorInterface
  {
    bool DNDFlag { get; set; }

    bool AAFlag { get; set; }

    bool CFUFlag { get; set; }

    string CFUNumber { get; set; }

    bool CFNRFlag { get; set; }

    string CFNRNumber { get; set; }

    bool CFBFlag { get; set; }

    string CFBNumber { get; set; }

    int SIPPort { get; set; }

    int DefaultAccountIndex { get; }

    List<string> CodecList { get; set; }

    bool PublishEnabled { get; set; }

    List<IAccount> Accounts { get; }

    bool IsNull { get; }

    void Save();
  }
}
