using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.YCGL
{
    /// <summary>
    /// 异常管理控制器
    /// </summary>
    [RoutePrefix("api/lbj/ycgl")]
    public class YCGLController : ApiController
    {
        private IDbOperate<ad_bjxx> _ycglservice;
        public YCGLController(IDbOperate<ad_bjxx> ycglservice)
        {
            _ycglservice = ycglservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            int resultcount = 0;
            var list = _ycglservice.GetList(parm, out resultcount);
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