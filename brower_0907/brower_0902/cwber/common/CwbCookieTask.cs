using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cef3;

namespace HyData.common
{
    /*class context：设置cookie*/
    class CwbCookieTask : CefTask
    {
        private string Url;
        private CefCookie Cookie;
        public CwbCookieTask(string url, CefCookie cookie)
        {
            Url = url;
            Cookie = cookie;
        }

        protected override void Execute()
        {
            CefCookieManager.Global.SetCookie(Url, Cookie);
        }

    }
}
