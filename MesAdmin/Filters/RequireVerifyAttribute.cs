using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ZDMesInterfaces.Common;

namespace MesAdmin.Filters
{
    /// <summary>
    /// 必填字段检查
    /// </summary>
    public class RequireVerifyAttribute : ActionFilterAttribute
    {
        private IRequireVerify _requireverify;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var requestScope = actionContext.Request.GetDependencyScope();
                _requireverify = requestScope.GetService(typeof(IRequireVerify)) as IRequireVerify;
                var parm = actionContext.ActionArguments.FirstOrDefault();
                var parmval = parm.Value;
                if (parmval != null)
                {
                    var parmtype = parmval.GetType().Name;
                    switch (parmtype)
                    {
                        case "List`1":
                            var list = parmval as IEnumerable<object>;
                            if (list.Count() > 0)
                            {
                                var type = list.First().GetType();
                                _requireverify.VerifyRequire(type, list.ToList());
                            }
                            break;
                        default:
                            List<object> p = new List<object>();
                            p.Add(parmval);
                            _requireverify.VerifyRequire(parmval.GetType(), p);
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}