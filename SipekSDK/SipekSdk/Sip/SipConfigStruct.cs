// SipekSdk with PJSUA2, 2017
// Type: Sipek.Sip.SipConfigStruct
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System.Runtime.InteropServices;

namespace Sipek.Sip
{
  [StructLayout(LayoutKind.Sequential)]
  public class SipConfigStruct
  {
    public int listenPort = 5060;
    [MarshalAs(UnmanagedType.I1)]
    public bool noTCP = true;
    public int expires = 3600;
    [MarshalAs(UnmanagedType.I1)]
    public bool VADEnabled = true;
    public int ECTail = 200;
    private static SipConfigStruct _instance;
    [MarshalAs(UnmanagedType.I1)]
    public bool noUDP;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
    public string stunServer;
    [MarshalAs(UnmanagedType.I1)]
    public bool publishEnabled;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
    public string nameServer;
    [MarshalAs(UnmanagedType.I1)]
    public bool pollingEventsEnabled;
    [MarshalAs(UnmanagedType.I1)]
    public bool imsEnabled;
    [MarshalAs(UnmanagedType.I1)]
    public bool imsIPSecHeaders;
    [MarshalAs(UnmanagedType.I1)]
    public bool imsIPSecTransport;

    public static SipConfigStruct Instance
    {
      get
      {
        if (SipConfigStruct._instance == null)
          SipConfigStruct._instance = new SipConfigStruct();
        return SipConfigStruct._instance;
      }
    }
  }
}
