using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
namespace MesAdmin.Controllers.LBJ.DJGL
{
    [RoutePrefix("api/lbj/djxx")]
    public class DJXXController : ApiController
    {
        private IDbOperate<zxjc_djxx> _djxxservice;
        public DJXXController(IDbOperate<zxjc_djxx> djxxservice)
        {
            _djxxservice = djxxservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _djxxservice.GetList(parm, out resultcount);
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