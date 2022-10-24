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
                var isdata = actionContext.Request.Properties.ContainsKey("template_datalist");
                if (isdata)
                {
                    object datalist = null;
                    var  isok = actionContext.Request.Properties.TryGetValue("template_datalist", out datalist);
                    if(isok && datalist != null)
                    {
                        var parmtype = datalist.GetType();
                        if (parmtype.IsConstructedGenericType)
                        {
                            var list = datalist as IEnumerable<object>;
                            var type = Type.GetType(parmtype.GenericTypeArguments[0].FullName + ",ZDMesModels");
                            _requireverify.VerifyRequire(type, list.ToList());
                        }
                    }
                }
                else
                {
                    var parm = actionContext.ActionArguments.FirstOrDefault();
                    var parmval = parm.Value;
                    if (parmval != null)
                    {
                        var partype = parmval.GetType();
                        if (partype.IsConstructedGenericType)
                        {
                            var list = parmval as IEnumerable<object>;
                            var type = Type.GetType(partype.GenericTypeArguments[0].FullName + ",ZDMesModels");
                            _requireverify.VerifyRequire(type, list.ToList());
                        }
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