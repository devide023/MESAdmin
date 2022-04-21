using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.SMJJK
{
    /// <summary>
    /// 首末件监控控制器
    /// </summary>
    [RoutePrefix("api/lbj/smjjk")]
    public class SMJJKController : ApiController
    {
        private IDbOperate<zxjc_smls> _smjjkservice;
        public SMJJKController(IDbOperate<zxjc_smls> smjjkservice)
        {
            _smjjkservice = smjjkservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            int resultcount = 0;
            var list = _smjjkservice.GetList(parm, out resultcount);
            return Json(new sys_search_result()
            {
                code = 1,
                msg = "ok",
                resultcount = resultcount,
                list = list
            });
        }
    }
}