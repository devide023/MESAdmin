using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MesAdmin.Filters
{
    public class AtachValueAttribute: ActionFilterAttribute
    {
        private Type _injecttype;
        private string _proname;
        private string _methodname;
        public AtachValueAttribute(Type injecttype,string proname,string method)
        {
            _injecttype = injecttype;
            _proname = proname;
            _methodname = method;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestScope = actionContext.Request.GetDependencyScope();
            var mi = _injecttype.GetMethods().Where(t=>t.Name == _methodname);
            var service = requestScope.GetService(_injecttype);
            object template_data = null;
            var isok = actionContext.Request.Properties.TryGetValue("template_datalist", out template_data);
            if (isok)
            {
                var fls = template_data as List<object>;
                Type typ = fls.First().GetType();
                mi.First().Invoke(service, new object[] { fls });
               //list = (template_data as List<object>).ConvertAll(t => (typ)t);
            //    _requireverfify.VerifyRequire<T>(list);
            }
            //actionContext.Request.Properties["template_datalist"] = outlist;
            //base.OnActionExecuting(actionContext);
        }
    }
}