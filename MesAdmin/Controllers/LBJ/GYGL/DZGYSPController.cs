using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.GYGL
{
    [RoutePrefix("api/lbj/dzgysp")]
    public class DZGYSPController : ApiController
    {
        private IDbOperate<zxjc_t_dzgy_sp> _dzgy_sp_service;
        public DZGYSPController(IDbOperate<zxjc_t_dzgy_sp> dzgy_sp_service)
        {
            _dzgy_sp_service = dzgy_sp_service;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult Get_Dzgysp_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _dzgy_sp_service.GetList(parm, out resultcount);
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
        public IHttpActionResult AddDzgysp(List<zxjc_t_dzgy_sp> entitys)
        {
            try
            {
                entitys.ForEach(i => i.gyid = Guid.NewGuid().ToString());
                int ret = _dzgy_sp_service.Add(entitys);
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

        [HttpPost, Route("edit")]
        public IHttpActionResult Editdzgysp(List<zxjc_t_dzgy_sp> entitys)
        {
            try
            {
                var ret = _dzgy_sp_service.Modify(entitys);
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
        public IHttpActionResult DelDzgysp(List<zxjc_t_dzgy_sp> entitys)
        {
            try
            {
                var ret = _dzgy_sp_service.Del(entitys);
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
    }
}