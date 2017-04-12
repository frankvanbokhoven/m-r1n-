//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace PJSUA2
{
    public class OnTransportStateParam : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal OnTransportStateParam(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OnTransportStateParam obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~OnTransportStateParam()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            lock (this)
            {
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        pjsua2PINVOKE.delete_OnTransportStateParam(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }

        public SWIGTYPE_p_void hnd
        {
            set
            {
                pjsua2PINVOKE.OnTransportStateParam_hnd_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
            }
            get
            {
                global::System.IntPtr cPtr = pjsua2PINVOKE.OnTransportStateParam_hnd_get(swigCPtr);
                SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
                return ret;
            }
        }

        public string type
        {
            set
            {
                pjsua2PINVOKE.OnTransportStateParam_type_set(swigCPtr, value);
                if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
            }
            get
            {
                string ret = pjsua2PINVOKE.OnTransportStateParam_type_get(swigCPtr);
                if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public pjsip_transport_state state
        {
            set
            {
                pjsua2PINVOKE.OnTransportStateParam_state_set(swigCPtr, (int)value);
            }
            get
            {
                pjsip_transport_state ret = (pjsip_transport_state)pjsua2PINVOKE.OnTransportStateParam_state_get(swigCPtr);
                return ret;
            }
        }

        public int lastError
        {
            set
            {
                pjsua2PINVOKE.OnTransportStateParam_lastError_set(swigCPtr, value);
            }
            get
            {
                int ret = pjsua2PINVOKE.OnTransportStateParam_lastError_get(swigCPtr);
                return ret;
            }
        }

        public TlsInfo tlsInfo
        {
            set
            {
                pjsua2PINVOKE.OnTransportStateParam_tlsInfo_set(swigCPtr, TlsInfo.getCPtr(value));
            }
            get
            {
                global::System.IntPtr cPtr = pjsua2PINVOKE.OnTransportStateParam_tlsInfo_get(swigCPtr);
                TlsInfo ret = (cPtr == global::System.IntPtr.Zero) ? null : new TlsInfo(cPtr, false);
                return ret;
            }
        }

        public OnTransportStateParam() : this(pjsua2PINVOKE.new_OnTransportStateParam(), true)
        {
        }

    }
}