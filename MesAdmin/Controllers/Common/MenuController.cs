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
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        private IDbOperate<mes_menu_entity> _menuservice;
        private IMenu _menu;
        public MenuController(IDbOperate<mes_menu_entity> menuservice, IMenu menu)
        {
            _menuservice = menuservice;
            _menu = menu;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _menuservice.GetList(parm, out resultcount);
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
        [HttpPost, Route("add")]
        public IHttpActionResult Add(List<mes_menu_entity> entitys)
        {
            try
            {
                if (entitys.Count > 0)
                {
                    var entity = entitys.First();
                    if (entity.menutype == "03" || entity.menutype == "04")
                    {
                        var bm = entity.code;
                        int maxid = _menu.Get_MenuMaxCode(entity.pid);
                        for (int i = 0; i < entitys.Count; i++)
                        {
                            entitys[i].code = bm + (maxid + i + 1).ToString().PadLeft(2, '0');
                        }
                    }
                }
                int ret = _menuservice.Add(entitys);
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
        public IHttpActionResult Del(List<mes_menu_entity> entitys)
        {
            try
            {
                var ret = _menuservice.Del(entitys);
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
        public IHttpActionResult Edit(List<mes_menu_entity> entitys)
        {
            try
            {
                var ret = _menuservice.Modify(entitys);
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

        [HttpGet,Route("tree")]
        public IHttpActionResult Tree()
        {
            try
            {
                var list = _menu.Get_MenuTree();
                var collist = _menu.Get_ColsTree();
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list,
                    fields = collist,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}