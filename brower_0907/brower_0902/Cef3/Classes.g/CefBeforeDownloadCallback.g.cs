//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Cef3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Cef3.Interop;
    
    // Role: PROXY
    public sealed unsafe partial class CefBeforeDownloadCallback : IDisposable
    {
        internal static CefBeforeDownloadCallback FromNative(cef_before_download_callback_t* ptr)
        {
            return new CefBeforeDownloadCallback(ptr);
        }
        
        internal static CefBeforeDownloadCallback FromNativeOrNull(cef_before_download_callback_t* ptr)
        {
            if (ptr == null) return null;
            return new CefBeforeDownloadCallback(ptr);
        }
        
        private cef_before_download_callback_t* _self;
        
        private CefBeforeDownloadCallback(cef_before_download_callback_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
        }
        
        ~CefBeforeDownloadCallback()
        {
            if (_self != null)
            {
                Release();
                _self = null;
            }
        }
        
        public void Dispose()
        {
            if (_self != null)
            {
                Release();
                _self = null;
            }
            GC.SuppressFinalize(this);
        }
        
        internal int AddRef()
        {
            return cef_before_download_callback_t.add_ref(_self);
        }
        
        internal int Release()
        {
            return cef_before_download_callback_t.release(_self);
        }
        
        internal int RefCt
        {
            get { return cef_before_download_callback_t.get_refct(_self); }
        }
        
        internal cef_before_download_callback_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
