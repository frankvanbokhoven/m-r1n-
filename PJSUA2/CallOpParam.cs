//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class CallOpParam : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CallOpParam(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CallOpParam obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CallOpParam() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          pjsua2PINVOKE.delete_CallOpParam(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public CallSetting opt {
    set {
      pjsua2PINVOKE.CallOpParam_opt_set(swigCPtr, CallSetting.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.CallOpParam_opt_get(swigCPtr);
      CallSetting ret = (cPtr == global::System.IntPtr.Zero) ? null : new CallSetting(cPtr, false);
      return ret;
    } 
  }

  public pjsip_status_code statusCode {
    set {
      pjsua2PINVOKE.CallOpParam_statusCode_set(swigCPtr, (int)value);
    } 
    get {
      pjsip_status_code ret = (pjsip_status_code)pjsua2PINVOKE.CallOpParam_statusCode_get(swigCPtr);
      return ret;
    } 
  }

  public string reason {
    set {
      pjsua2PINVOKE.CallOpParam_reason_set(swigCPtr, value);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = pjsua2PINVOKE.CallOpParam_reason_get(swigCPtr);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public uint options {
    set {
      pjsua2PINVOKE.CallOpParam_options_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.CallOpParam_options_get(swigCPtr);
      return ret;
    } 
  }

  public SipTxOption txOption {
    set {
      pjsua2PINVOKE.CallOpParam_txOption_set(swigCPtr, SipTxOption.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.CallOpParam_txOption_get(swigCPtr);
      SipTxOption ret = (cPtr == global::System.IntPtr.Zero) ? null : new SipTxOption(cPtr, false);
      return ret;
    } 
  }

  public CallOpParam(bool useDefaultCallSetting) : this(pjsua2PINVOKE.new_CallOpParam__SWIG_0(useDefaultCallSetting), true) {
  }

  public CallOpParam() : this(pjsua2PINVOKE.new_CallOpParam__SWIG_1(), true) {
  }

}
