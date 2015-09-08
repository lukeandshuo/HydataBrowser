using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Cef3;
using HyData.common;
using HyData.Core;
namespace HyData
{
    internal class Global
    {
        internal static List<ChromeWebBrowser> BrowserList = new List<ChromeWebBrowser>();
        internal static Dictionary<int, CwbElement> RootList = new Dictionary<int,CwbElement>();
        internal static ClientApp app;
        internal static ChromeWebBrowser instance;
        internal static string Result;
        internal static bool flag;
        internal static object JsEvaResult;

        const string ERROR_CALL_NOTFOUND = "error: this method can not be found.";
        const string ERROR_CALL_PARAMETER = "error: parameter is incorrect";

        /*summary:javascript 调用c#浏览器中的方法，查找当前激活窗口对应的chromeWebBrowser
                 * 在其中查找methodname,并根据method的参数变量类型，解析从js传入的字符串变量值
                 * 运行该函数并将结果转换为string类型并返回*/
        /*param1:当前激活的浏览器*/
        /*param2:要调用的方法*/
        /*param3:从js传入的函数参数*/
        /*res:函数执行后的结果 string类型*/
        internal static string CallMethod(CefBrowser browser,string methodName, string paramValues)
        {
            Type t = null;
            object form = null;
            foreach(ChromeWebBrowser c in BrowserList)
            {
                if (c == null) continue;
                if (c.browser.Identifier == browser.Identifier)
                {
                    t = c.FindForm().GetType();
                    form = c.FindForm();
                    break;
                }
            }

            if (t == null)
            {
                return ERROR_CALL_NOTFOUND;
            }

            
            MethodInfo m = t.GetMethod(methodName);
            if (m == null)
            {
                return ERROR_CALL_NOTFOUND;
            }
            object[] objArray = null;
            string[] values = new string[0];
            if (paramValues != null)
                values = paramValues.Split(new char[] { ',' });
            objArray = new object[values.Length];
            ParameterInfo[] pa = m.GetParameters();

            if (objArray.Length != pa.Length)
            {
                return ERROR_CALL_PARAMETER;
            }

            int i = 0;
            foreach (ParameterInfo p in pa)
            {
                switch (p.ParameterType.Name)
                {
                    case "String":
                        objArray[i] = values[i];
                        break;
                    case "Int32":
                        objArray[i] = Int32.Parse(values[i]);
                        break;
                    case "Boolean":
                        objArray[i] = Boolean.Parse(values[i]);
                        break;
                    case "Double":
                        objArray[i] = Double.Parse(values[i]);
                        break;
                }
                i++;
            }
            object o = m.Invoke(form, objArray);
            string retVal = string.Empty;
            if (o != null)
                retVal = o.ToString();
            return retVal;
        }
    }

    enum CwbBusinStyle
    {
        bsGetElementValue = 0,
        bsSetElementValue = 1,
        bsAddElementEvent = 2,
        bsVisitDocument = 3,
        bsFocusElement = 4,
        bsAttachElementEvent = 5,
        bsNone = -1
    }

    enum CwbCookieStyle
    {
        csDeleteAllCookie = 0,
        csVisitUrlCookie = 1
    }


}
