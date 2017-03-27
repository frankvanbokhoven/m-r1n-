// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.ICallLogInterface
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;
using System.Collections.Generic;

namespace Sipek.Common
{
  public interface ICallLogInterface
  {
    void addCall(ECallType type, string number, string name, DateTime time, TimeSpan duration);

    void save();

    Stack<CCallRecord> getList();

    Stack<CCallRecord> getList(ECallType type);

    void deleteRecord(CCallRecord record);
  }
}
