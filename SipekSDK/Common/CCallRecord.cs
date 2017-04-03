// SipekSdk with PJSUA2, 2017
// Type: Sipek.Common.CCallRecord
// Assembly: SipekSdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABACC414-BA95-4A69-B54D-1CD412EEFEEF
// Assembly location: C:\Marine\GitSources\SIP_Tester\bin\SipekSdk.dll

using System;

namespace Sipek.Common
{
  public class CCallRecord
  {
    private string _name = "";
    private string _number = "";
    private ECallType _type;
    private DateTime _time;
    private TimeSpan _duration;
    private int _count;

    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    public string Number
    {
      get
      {
        return this._number;
      }
      set
      {
        this._number = value;
      }
    }

    public ECallType Type
    {
      get
      {
        return this._type;
      }
      set
      {
        this._type = value;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return this._duration;
      }
      set
      {
        this._duration = value;
      }
    }

    public DateTime Time
    {
      get
      {
        return this._time;
      }
      set
      {
        this._time = value;
      }
    }

    public int Count
    {
      get
      {
        return this._count;
      }
      set
      {
        this._count = value;
      }
    }
  }
}
