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
    /// 模板检查
    /// </summary>
    public class TemplateVerifyAttribute: ActionFilterAttribute
    {
        private ITemplateVerify _template_verify;
        public TemplateVerifyAttribute(string modelname)
        {
            ModelName = modelname;
        }

        public string ModelName { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var requestScope = actionContext.Request.GetDependencyScope();
                _template_verify = requestScope.GetService(typeof(ITemplateVerify)) as ITemplateVerify;
                object template_filename = null;
                var isfind = actionContext.ActionArguments.TryGetValue("fileid", out template_filename);
                if (isfind && template_filename != null)
                {
                    List<object> outlist = new List<object>();
                    _template_verify.ImportTemplateVerify(ModelName, template_filename.ToString(), out outlist);
                    actionContext.Request.Properties.Remove("template_datalist");
                    actionContext.Request.Properties.Add("template_datalist", outlist);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}