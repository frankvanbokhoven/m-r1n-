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
    public class OnTimerParam : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal OnTimerParam(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OnTimerParam obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~OnTimerParam()
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
                        pjsua2PINVOKE.delete_OnTimerParam(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }

        public SWIGTYPE_p_void userData
        {
            set
            {
                pjsua2PINVOKE.OnTimerParam_userData_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
            }
            get
            {
                global::System.IntPtr cPtr = pjsua2PINVOKE.OnTimerParam_userData_get(swigCPtr);
                SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
                return ret;
            }
        }

        public uint msecDelay
        {
            set
            {
                pjsua2PINVOKE.OnTimerParam_msecDelay_set(swigCPtr, value);
            }
            get
            {
                uint ret = pjsua2PINVOKE.OnTimerParam_msecDelay_get(swigCPtr);
                return ret;
            }
        }

        public OnTimerParam() : this(pjsua2PINVOKE.new_OnTimerParam(), true)
        {
        }

    }
}