using System;
using System.Collections;
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
        private string _methodname;
        public AtachValueAttribute(Type injecttype,string methodname)
        {
            _injecttype = injecttype;
            _methodname = methodname;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestScope = actionContext.Request.GetDependencyScope();
            var obj = requestScope.GetService(_injecttype);
            var  istemplate = actionContext.Request.Properties.ContainsKey("template_datalist");
            //模板数据，数据处理
            if (istemplate)
            {
                object objlist = null;
                var ok = actionContext.Request.Properties.TryGetValue("template_datalist", out objlist);
                if (ok)
                {
                    var list = objlist as List<object>;
                    var mquery = _injecttype.GetMethods().Where(t => t.Name.ToLower() == _methodname.ToLower());
                    if (mquery.Count() > 0)
                    {
                        int parindex = 0;
                        var mi = mquery.First();
                        var parlist = mi.GetParameters();
                        object[] mpars = new object[parlist.Length];
                        foreach (var par in parlist)
                        {
                            var ptype = par.ParameterType;
                            if (ptype.IsConstructedGenericType)
                            {
                                var cslx = Type.GetType(ptype.GenericTypeArguments[0].FullName + ",ZDMesModels");
                                var specificType = typeof(List<>).MakeGenericType(new Type[] { cslx });
                                IList vlist = Activator.CreateInstance(specificType) as IList;
                                foreach (var item in list)
                                {
                                    vlist.Add(item);
                                }
                                mpars[parindex] = vlist;
                            }
                            parindex++;
                        }
                        var result = mi.Invoke(obj, mpars);
                        actionContext.Request.Properties.Remove("template_datalist");
                        actionContext.Request.Properties.Add("template_datalist", result);
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
                        var mquery = _injecttype.GetMethods().Where(t => t.Name.ToLower() == _methodname.ToLower());
                        if (mquery.Count() > 0)
                        {
                            int parindex = 0;
                            var mi = mquery.First();
                            var parlist = mi.GetParameters();
                            object[] mpars = new object[parlist.Length];
                            foreach (var par in parlist)
                            {
                                var ptype = par.ParameterType;
                                if (ptype.IsConstructedGenericType)
                                {
                                    var cslx = Type.GetType(ptype.GenericTypeArguments[0].FullName + ",ZDMesModels");
                                    var specificType = typeof(List<>).MakeGenericType(new Type[] { cslx });
                                    IList vlist = Activator.CreateInstance(specificType) as IList;
                                    foreach (var item in list)
                                    {
                                        vlist.Add(item);
                                    }
                                    mpars[parindex] = vlist;
                                }
                                parindex++;
                            }
                            var result = mi.Invoke(obj, mpars);
                            actionContext.Request.Properties.Remove("template_datalist");
                            actionContext.Request.Properties.Add("template_datalist", result);
                        }
                    }
                }
            }
        }
    }
}