// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullVoipProxy
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

namespace Sipek.Common
{
  public class NullVoipProxy : IVoipProxy
  {
    public override bool IsInitialized
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override int initialize()
    {
      return 1;
    }

    public override int shutdown()
    {
      return 1;
    }

    public override void setCodecPriority(string item, int p)
    {
    }

    public override int getNoOfCodecs()
    {
      return 0;
    }

    public override string getCodec(int i)
    {
      return "";
    }

    public override ICallProxyInterface createCallProxy()
    {
      return (ICallProxyInterface) new NullCallProxy();
    }
  }
}
