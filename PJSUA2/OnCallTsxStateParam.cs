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
    public class OnCallTsxStateParam : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal OnCallTsxStateParam(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OnCallTsxStateParam obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~OnCallTsxStateParam()
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
                        pjsua2PINVOKE.delete_OnCallTsxStateParam(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }

        public SipEvent e
        {
            set
            {
                pjsua2PINVOKE.OnCallTsxStateParam_e_set(swigCPtr, SipEvent.getCPtr(value));
            }
            get
            {
                global::System.IntPtr cPtr = pjsua2PINVOKE.OnCallTsxStateParam_e_get(swigCPtr);
                SipEvent ret = (cPtr == global::System.IntPtr.Zero) ? null : new SipEvent(cPtr, false);
                return ret;
            }
        }

        public OnCallTsxStateParam() : this(pjsua2PINVOKE.new_OnCallTsxStateParam(), true)
        {
        }

    }
}