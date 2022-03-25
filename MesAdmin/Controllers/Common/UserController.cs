using MesAdmin.Filters;
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
    [RoutePrefix("api/user")]
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
        public IHttpActionResult Add(List<mes_user_entity> entitys)
        {
            try
            {
                IEnumerable<mes_user_entity> noklist = new List<mes_user_entity>();
                var ret = _userservice.Add(entitys,out noklist);
                if (ret > 0)
                {
                    if (noklist.Count() == 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = "数据保存成功"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            code = 2,
                            msg = "数据保存失败",
                            noklist = noklist
                        });
                    }
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
        public IHttpActionResult Del(List<mes_user_entity> entitys)
        {
            try
            {
                var ret = _userservice.Del(entitys);
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
        public IHttpActionResult Edit(List<mes_user_entity> entitys)
        {
            try
            {
                var ret = _userservice.Modify(entitys);
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
        public IHttpActionResult List(sys_page parm)
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