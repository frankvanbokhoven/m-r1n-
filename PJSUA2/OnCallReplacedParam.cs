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
    public class OnCallReplacedParam : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal OnCallReplacedParam(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OnCallReplacedParam obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~OnCallReplacedParam()
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
                        pjsua2PINVOKE.delete_OnCallReplacedParam(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }

        public int newCallId
        {
            set
            {
                pjsua2PINVOKE.OnCallReplacedParam_newCallId_set(swigCPtr, value);
            }
            get
            {
                int ret = pjsua2PINVOKE.OnCallReplacedParam_newCallId_get(swigCPtr);
                return ret;
            }
        }

        public OnCallReplacedParam() : this(pjsua2PINVOKE.new_OnCallReplacedParam(), true)
        {
        }

    }
}