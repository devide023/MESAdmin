using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ZDMesInterfaces.LBJ;
using ZDMesServices.LBJ.CheckData;
using System.IO;
using Newtonsoft.Json;
using ZDMesModels;
using System.Text;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;
using ZDMesServices;
using Autofac.Integration.WebApi;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using Autofac;

namespace MesAdmin.Filters
{
    public class CheckDataAttribute : ActionFilterAttribute
    {
        IFormCheck _formcheckservice;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var requestScope = actionContext.Request.GetDependencyScope();
                _formcheckservice = requestScope.GetService(typeof(IFormCheck)) as IFormCheck;
                sys_result result = new sys_result();
                var argdic = actionContext.ActionArguments;
                foreach (var item in argdic.Keys)
                {
                    object argvalue;
                    var isok = argdic.TryGetValue(item, out argvalue);
                    if (isok)
                    {
                        string typename = argvalue.GetType().Name;
                        switch (typename)
                        {
                            case "List`1":
                                var list = argvalue as IEnumerable<object>;
                                var r = _formcheckservice.Check_Form_Data(list.ToList(), out result);
                                if (!r)
                                {
                                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { code = result.code, result.msg }, "application/json");
                                    break;
                                }
                                break;
                            default:
                                break;
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