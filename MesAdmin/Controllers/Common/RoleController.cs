using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesServices;
using ZDMesServices.Common;
using Autofac;
using Autofac.Integration.WebApi;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/role")]
    public class RoleController : ApiController
    {
        private IDbOperate<mes_role_entity> _roleservice;
        public RoleController(IDbOperate<mes_role_entity> roleservice)
        {
            _roleservice = roleservice;
        }
        [HttpPost,Route("add")]
        public IHttpActionResult Add(mes_role_entity entity)
        {
            try
            {
               var ret = _roleservice.Add(entity);
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
        public IHttpActionResult Del(mes_role_entity entity)
        {
            try
            {
                var ret = _roleservice.Del(entity);
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
        public IHttpActionResult Edit(mes_role_entity entity)
        {
            try
            {
                var ret = _roleservice.Modify(entity);
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
                var list = _roleservice.GetList(parm, out resultcount);
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