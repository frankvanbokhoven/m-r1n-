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
    /*Fix - Mark Kruger - Added pjmedia_tone_digit_map_digits*/
    public class pjmedia_tone_digit_map : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;
        private pjmedia_tone_digit_map_digits Digits = null;

        internal pjmedia_tone_digit_map(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
            Digits = new pjmedia_tone_digit_map_digits(cPtr, 16);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(pjmedia_tone_digit_map obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~pjmedia_tone_digit_map()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            lock (this)
            {

                Digits?.Dispose();
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        pjsua2PINVOKE.delete_pjmedia_tone_digit_map(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }

        public uint count
        {
            set
            {
                pjsua2PINVOKE.pjmedia_tone_digit_map_count_set(swigCPtr, value);
            }
            get
            {
                uint ret = pjsua2PINVOKE.pjmedia_tone_digit_map_count_get(swigCPtr);
                return ret;
            }
        }

        public pjmedia_tone_digit_map_digits digits
        {
            get
            {
                return Digits;
            }
        }

        public pjmedia_tone_digit_map() : this(pjsua2PINVOKE.new_pjmedia_tone_digit_map(), true)
        {
        }

    }
}