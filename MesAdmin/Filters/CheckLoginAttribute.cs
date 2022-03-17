using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using ZDToolHelper;

namespace MesAdmin.Filters
{
    public class CheckLoginAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                //从http请求的头里面获取身份验证信息，验证是否是请求发起方的token
                var authorization = actionContext.Request.Headers.Authorization;
                if ((authorization != null) && (authorization.Parameter != null))
                {
                    //校验Token合法及是否过期
                    var token = authorization.Parameter;
                    var isok = new JWTHelper().CheckToken(token);
                    switch (isok)
                    {
                        case 2000:
                            {
                                //缓存用户
                            }
                            break;
                        case 2001:
                            {
                                string jsonResult = JsonConvert.SerializeObject(new
                                {
                                    code = 0,
                                    msg = "Token已失效,请重新登录"
                                });
                                HttpResponseMessage result = new HttpResponseMessage();
                                result.Content = new StringContent(jsonResult, System.Text.Encoding.GetEncoding("UTF-8"), "application/json");
                                actionContext.Response = result;
                            }
                            break;
                        case 2002:
                            {
                                string jsonResult = JsonConvert.SerializeObject(new
                                {
                                    code = 0,
                                    msg = "非法的Token,请重新登录"
                                });
                                HttpResponseMessage result = new HttpResponseMessage();
                                result.Content = new StringContent(jsonResult, System.Text.Encoding.GetEncoding("UTF-8"), "application/json");
                                actionContext.Response = result;
                            }
                            break;
                        default:
                            break;
                    }
                }
                //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                else
                {
                    //HandleUnauthorizedRequest(actionContext);
                    string jsonResult = JsonConvert.SerializeObject(new
                    {
                        code = 0,
                        msg = "验证失败,请重新登录"
                    });
                    HttpResponseMessage result = new HttpResponseMessage();
                    result.Content = new StringContent(jsonResult, System.Text.Encoding.GetEncoding("UTF-8"), "application/json");
                    actionContext.Response = result;
                }
            }
        }
    }
}