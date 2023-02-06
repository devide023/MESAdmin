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
        private IRole _role;
        private IMenu _menu;
        private IUser _user;
        public RoleController(IDbOperate<mes_role_entity> roleservice, IRole role,IMenu menu,IUser user)
        {
            _roleservice = roleservice;
            _role = role;
            _menu = menu;
            _user = user;
        }
        [HttpPost,Route("add")]
        public IHttpActionResult Add(sys_role_form entity)
        {
            try
            {
                var ret = _role.Save_Role_Menus(entity);
                if (ret)
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
        public IHttpActionResult Del(List<mes_role_entity> entitys)
        {
            try
            {
                var ret = _roleservice.Del(entitys);
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
        public IHttpActionResult Edit(sys_role_form entity)
        {
            try
            {
                var ret = _role.Edit_Role_Menus(entity);
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
                parm.default_order_colname = "code";
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
        [HttpGet,Route("user")]
        public IHttpActionResult GetRoleUser(int id)
        {
            try
            {
                var users = _role.Get_Role_Users(id);
                return Json(new { code = 1, msg = "ok", list = users });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("queryuser")]
        public IHttpActionResult GetUserByKey(string key)
        {
            try
            {
               var list = _user.GetUserByKey(key);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("info")]
        public IHttpActionResult GetRoleInfo(int id)
        {
            try
            {
                int resultcount = 0;
                Dapper.DynamicParameters p = new Dapper.DynamicParameters();
                p.Add(":id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                p.Add(":pageindex", 1, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                p.Add(":pagesize", int.MaxValue, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                var role = _roleservice.GetList(new sys_page() { sqlexp = "id=:id", sqlparam = p }, out resultcount).First();
                var role_menus = _role.Get_Role_Menus(id);
                var role_editcol = _role.Get_Role_Edit_Fields(id);
                var role_hidecol = _role.Get_Role_Hide_Fields(id);
                var menutree = _menu.Get_MenuTree();
                var coltree = _menu.Get_ColsTree();
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    menus = role_menus,
                    editcols = role_editcol,
                    hidecols = role_hidecol,
                    role = role,
                    menutree = menutree,
                    coltree = coltree
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("adduser")]
        public IHttpActionResult Save_Role_Users(sys_role_user_form form)
        {
            try
            {
               var ret = _role.Save_Role_Users(form);
                return Json(new { code = 1, msg = "关联用户成功" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("all")]
        public IHttpActionResult Get_All_Roles()
        {
            try
            {
                var list = _role.All().Select(t => new { label = t.name, value = t.id });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}