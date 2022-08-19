using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.App;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.App
{
    [RoutePrefix("api/lbj/approle")]
    public class AppRoleController : ApiController
    {
        IDbOperate<app_role> _service;
        IApp _appservice;
        public AppRoleController(IDbOperate<app_role> service, IApp appservice)
        {
            _service = service;
            _appservice = appservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _service.GetList(parm, out resultcount);
                return Json(new
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
        [HttpPost,CheckData, Route("add")]
        public IHttpActionResult Add(List<app_role> entitys)
        {
            try
            {
                int ret = _service.Add(entitys);
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

        [HttpPost, CheckData,Route("edit")]
        public IHttpActionResult Edit(List<app_role> entitys)
        {
            try
            {
                var ret = _service.Modify(entitys);
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
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<app_role> entitys)
        {
            try
            {
                var ret = _service.Del(entitys);
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
        [HttpGet,Route("appusers")]
        public IHttpActionResult Get_AppUsers()
        {
            try
            {
               var list = _appservice.Get_All_Users().Select(t=>new { label=t.name+"("+t.code+")",value=t.tel});
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet,Route("search_user")]
        public IHttpActionResult Search_User(string key)
        {
            try
            {
                var list = _appservice.Get_All_Users().Where(t=>t.name.Contains(key) || t.code.Contains(key) || t.tel.Contains(key));
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("role_user_list")]
        public IHttpActionResult Get_Users_By_Roleid(int roleid)
        {
            try
            {
               var list =  _appservice.Get_Role_Users(roleid);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("app_menutree")]
        public IHttpActionResult App_MenuTree()
        {
            try
            {
                var list = _appservice.Get_App_Menus();
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("role_menu_list")]
        public IHttpActionResult Get_Menus_By_Roleid(int roleid)
        {
            try
            {
                var list = _appservice.Get_Role_Menus(roleid);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("save_role_user")]
        public IHttpActionResult Save_Role_User(form_app_role_user form)
        {
            try
            {
                var ret = _appservice.Save_Role_Users(form);
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
        [HttpPost, Route("save_role_menu")]
        public IHttpActionResult Save_Role_Menu(form_app_role_menu form)
        {
            try
            {
                var ret = _appservice.Save_Role_Menus(form);
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
    }
}