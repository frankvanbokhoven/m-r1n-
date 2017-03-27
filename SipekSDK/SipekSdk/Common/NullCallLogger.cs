// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.NullCallLogger
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;
using System.Collections.Generic;

namespace Sipek.Common
{
  public class NullCallLogger : ICallLogInterface
  {
    public void addCall(ECallType type, string number, string name, DateTime time, TimeSpan duration)
    {
    }

    public void save()
    {
    }

    public Stack<CCallRecord> getList()
    {
      return (Stack<CCallRecord>) null;
    }

    public Stack<CCallRecord> getList(ECallType type)
    {
      return (Stack<CCallRecord>) null;
    }

    public void deleteRecord(CCallRecord record)
    {
    }
  }
}
