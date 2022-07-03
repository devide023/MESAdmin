using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using ZDMesModels;
using MesAdmin.Filters;

namespace MesAdmin.Controllers.LBJ.GWZD
{
    [RoutePrefix("api/lbj/gwzd")]
    public class GWZDController : ApiController
    {
        private IDbOperate<base_gwzd> _gwzdservice;
        public GWZDController(IDbOperate<base_gwzd> gwzdservice)
        {
            _gwzdservice = gwzdservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gwzdservice.GetList(parm, out resultcount);
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
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<base_gwzd> entitys)
        {
            try
            {
                var ret = _gwzdservice.Modify(entitys);
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