using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cef3;

namespace HyData.common
{
    /*class context: 浏览器地址，标题，状态改变的处理函数*/
    internal sealed class CwbDisplayHandler : CefDisplayHandler
    {
        private ChromeWebBrowser webBrowser;

        public CwbDisplayHandler(ChromeWebBrowser browser)
        {
            webBrowser = browser;
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
            webBrowser.OnTitleChange(title);
        }

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
            if (frame.IsMain)
            {
                webBrowser.OnUrlChange(url);
            }
        }

        protected override void OnStatusMessage(CefBrowser browser, string value)
        {

        }

        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            Console.WriteLine("OnTooltip: {0}", text);
            return false;
        }
    }
}
