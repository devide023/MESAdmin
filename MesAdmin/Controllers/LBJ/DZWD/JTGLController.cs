using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.DZWD
{
    [RoutePrefix("api/lbj/jtgl")]
    public class JTGLController : ApiController
    {
        private IDbOperate<zxjc_t_jstc> _jtglservice;
        public JTGLController(IDbOperate<zxjc_t_jstc> jtglservice)
        {
            _jtglservice = jtglservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _jtglservice.GetList(parm, out resultcount);
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
        public IHttpActionResult AddRyxx(List<zxjc_t_jstc> entitys)
        {
            try
            {
                entitys.ForEach(i => i.jtid = Guid.NewGuid().ToString());
                int ret = _jtglservice.Add(entitys);
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
        public IHttpActionResult EditRyxx(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ret = _jtglservice.Modify(entitys);
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
        public IHttpActionResult DelRyxx(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ret = _jtglservice.Del(entitys);
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