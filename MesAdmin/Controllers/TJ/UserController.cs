using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.TJ
{
    [RoutePrefix("api/tj/user")]
    public class UserController : ApiController
    {
        private IDbOperate<mes_user_entity> _userservice;
        private IUser _user;
        public UserController(IDbOperate<mes_user_entity> userservice,IUser user)
        {
            _userservice = userservice;
            _user = user;
        }
        [HttpPost, Route("add")]
        public IHttpActionResult Add(mes_user_entity entity)
        {
            try
            {
                var ret = _userservice.Add(entity);
                if (ret > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据保存失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult Del(mes_user_entity entity)
        {
            try
            {
                var ret = _userservice.Del(entity);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据删除成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据删除失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(mes_user_entity entity)
        {
            try
            {
                var ret = _userservice.Modify(entity);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetRoles(sys_page parm)
        {
            try
            {
                //var scope = GlobalConfiguration.Configuration.DependencyResolver.GetRequestLifetimeScope();
                //scope.Resolve<IDbOperate<mes_role_entity>>();
                int resultcount = 0;
                var list = _userservice.GetList(parm, out resultcount);
                return Json(new sys_search_result()
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}