using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cef3;

namespace HyData.common
{
    /*class context: v8处理的扩充，js调用本c#系统中扩充的方法*/
    class CwbJsExtendHandler : CefV8Handler
    {
        CefBrowser activeBrowser;
        public CwbJsExtendHandler(CefBrowser browser)
        {
            activeBrowser = browser;
        }
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            switch (name)
            {
                case "CallCSharpMethod":
                    string methodName = arguments[0].GetStringValue();
                    string values = arguments[1].GetStringValue();
                    string res = Global.CallMethod(activeBrowser, methodName, values);
                    returnValue = CefV8Value.CreateString(res);
                    return true;
            }

            returnValue = null;
            return false;
        }
    }
}
