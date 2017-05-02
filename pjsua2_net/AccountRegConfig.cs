/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace pjsua2 {

public class AccountRegConfig : PersistentObject {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal AccountRegConfig(global::System.IntPtr cPtr, bool cMemoryOwn) : base(pjsua2PINVOKE.AccountRegConfig_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AccountRegConfig obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AccountRegConfig() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          pjsua2PINVOKE.delete_AccountRegConfig(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public string registrarUri {
    set {
      pjsua2PINVOKE.AccountRegConfig_registrarUri_set(swigCPtr, value);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = pjsua2PINVOKE.AccountRegConfig_registrarUri_get(swigCPtr);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool registerOnAdd {
    set {
      pjsua2PINVOKE.AccountRegConfig_registerOnAdd_set(swigCPtr, value);
    } 
    get {
      bool ret = pjsua2PINVOKE.AccountRegConfig_registerOnAdd_get(swigCPtr);
      return ret;
    } 
  }

  public SipHeaderVector headers {
    set {
      pjsua2PINVOKE.AccountRegConfig_headers_set(swigCPtr, SipHeaderVector.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.AccountRegConfig_headers_get(swigCPtr);
      SipHeaderVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new SipHeaderVector(cPtr, false);
      return ret;
    } 
  }

  public uint timeoutSec {
    set {
      pjsua2PINVOKE.AccountRegConfig_timeoutSec_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_timeoutSec_get(swigCPtr);
      return ret;
    } 
  }

  public uint retryIntervalSec {
    set {
      pjsua2PINVOKE.AccountRegConfig_retryIntervalSec_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_retryIntervalSec_get(swigCPtr);
      return ret;
    } 
  }

  public uint firstRetryIntervalSec {
    set {
      pjsua2PINVOKE.AccountRegConfig_firstRetryIntervalSec_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_firstRetryIntervalSec_get(swigCPtr);
      return ret;
    } 
  }

  public uint delayBeforeRefreshSec {
    set {
      pjsua2PINVOKE.AccountRegConfig_delayBeforeRefreshSec_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_delayBeforeRefreshSec_get(swigCPtr);
      return ret;
    } 
  }

  public bool dropCallsOnFail {
    set {
      pjsua2PINVOKE.AccountRegConfig_dropCallsOnFail_set(swigCPtr, value);
    } 
    get {
      bool ret = pjsua2PINVOKE.AccountRegConfig_dropCallsOnFail_get(swigCPtr);
      return ret;
    } 
  }

  public uint unregWaitSec {
    set {
      pjsua2PINVOKE.AccountRegConfig_unregWaitMsec_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_unregWaitMsec_get(swigCPtr);
      return ret;
    } 
  }

  public uint proxyUse {
    set {
      pjsua2PINVOKE.AccountRegConfig_proxyUse_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountRegConfig_proxyUse_get(swigCPtr);
      return ret;
    } 
  }

  public override void readObject(ContainerNode node) {
    pjsua2PINVOKE.AccountRegConfig_readObject(swigCPtr, ContainerNode.getCPtr(node));
    if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
  }

  public override void writeObject(ContainerNode node) {
    pjsua2PINVOKE.AccountRegConfig_writeObject(swigCPtr, ContainerNode.getCPtr(node));
    if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
  }

  public AccountRegConfig() : this(pjsua2PINVOKE.new_AccountRegConfig(), true) {
  }

}

}
