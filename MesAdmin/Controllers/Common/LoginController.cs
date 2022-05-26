using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.Common
{
    /// <summary>
    /// 登录
    /// </summary>
    /// 
    [RoutePrefix("api")]
    public class LoginController : ApiController
    {
        private IUser _userservice;
        public LoginController(IUser userservice)
        {
            _userservice = userservice;
        }
        [AllowAnonymous,HttpPost,Route("login")]
        public IHttpActionResult Login(sys_user userinfo)
        {
            try
            {
                var result = _userservice.Login(userinfo);
                return Json(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [AllowAnonymous,HttpGet,Route("userinfo")]
        public IHttpActionResult UserInfo(string token)
        {
            try
            {
                var result = _userservice.GetUserInfo(token);
                return Json(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("logout")]
        public IHttpActionResult Logout()
        {
            try
            {
               var ret = _userservice.Logout();
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "成功退出"
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "退出失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}