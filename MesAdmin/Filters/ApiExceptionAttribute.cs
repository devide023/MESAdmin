using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;
using Newtonsoft.Json;
using ZDToolHelper;
namespace MesAdmin.Filters
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        private ILog log;
        public ApiExceptionAttribute()
        {
            log = LogManager.GetLogger(this.GetType());
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string ip = Tool.GetIP();
            string brow = Tool.GetBrowser();
            string os = Tool.GetOSVersion();
            var method = actionExecutedContext.Request.Method.Method;
            //var action = actionExecutedContext.Request.GetRouteData().Values["action"].ToString();
            //var ctrl = actionExecutedContext.Request.GetRouteData().Values["controller"].ToString();
            log.Error(actionExecutedContext.Exception.StackTrace);
            string jsonResult = JsonConvert.SerializeObject(new
            {
                code = 0,
                browser = brow,
                os = os,
                ip=ip,
                method = method,
                msg = actionExecutedContext.Exception.Message
            });
            HttpResponseMessage result = new HttpResponseMessage();
            result.Content = new StringContent(jsonResult, System.Text.Encoding.GetEncoding("UTF-8"), "application/json");
            actionExecutedContext.Response = result;
            base.OnException(actionExecutedContext);
        }
    }
}