using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cef3;
namespace HyData.common
{
    /*class context: 继承策略工厂类 创建的时候返回资源处理类*/
    internal sealed class AppSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new RequestResourceHandler();
        }
    }
}
