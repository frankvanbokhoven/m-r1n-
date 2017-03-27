// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullConfigurator
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System.Collections.Generic;

namespace Sipek.Common
{
  internal class NullConfigurator : IConfiguratorInterface
  {
    private List<IAccount> _accountList = new List<IAccount>();

    public bool IsNull
    {
      get
      {
        return true;
      }
    }

    public bool CFUFlag
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public string CFUNumber
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public bool CFNRFlag
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public string CFNRNumber
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public bool CFBFlag
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public string CFBNumber
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public bool DNDFlag
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public bool AAFlag
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public int SIPPort
    {
      get
      {
        return 5060;
      }
      set
      {
      }
    }

    public int DefaultAccountIndex
    {
      get
      {
        return 0;
      }
      set
      {
      }
    }

    public bool PublishEnabled
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    public List<IAccount> Accounts
    {
      get
      {
        return this._accountList;
      }
    }

    public List<string> CodecList
    {
      get
      {
        return (List<string>) null;
      }
      set
      {
      }
    }

    public void Save()
    {
    }

    public class NullAccount : IAccount
    {
      public int Index
      {
        get
        {
          return 0;
        }
        set
        {
        }
      }

      public string AccountName
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string HostName
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string Id
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string UserName
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string Password
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string DisplayName
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public string DomainName
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public int Port
      {
        get
        {
          return 0;
        }
        set
        {
        }
      }

      public int RegState
      {
        get
        {
          return 0;
        }
        set
        {
        }
      }

      public string ProxyAddress
      {
        get
        {
          return "";
        }
        set
        {
        }
      }

      public ETransportMode TransportMode
      {
        get
        {
          return ETransportMode.TM_UDP;
        }
        set
        {
        }
      }
    }
  }
}
