using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ZDMesInterfaces.LBJ;
using ZDMesServices.LBJ.CheckData;

namespace MesAdmin.Filters
{
    public class CheckDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
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
                                //var list = argvalue as IEnumerable<object>;
                                //var n = list.First().GetType().FullName+ ",ZDMesModels";
                                //Type t = Type.GetType("ZDMesInterfaces.LBJ.ICheckData,ZDMesInterfaces");
                                //Type[] params_type = new Type[2];
                                //params_type[0] = Type.GetType("System.String");
                                //params_type[1] = Type.GetType("System.Object");
                                //Object[] params_obj = new Object[2];
                                //params_obj[0] = n;
                                //params_obj[1] = list;
                                //MethodInfo mi = t.GetMethod("Valid", params_type);
                                //mi.MakeGenericMethod(new Type[] { Type.GetType(n) });
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