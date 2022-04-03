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
namespace MesAdmin.Controllers.LBJ.GYGL
{
    [RoutePrefix("api/lbj/gylx")]
    public class GYLXController : ApiController
    {
        private IDbOperate<zxjc_gylx> _gylxservice;
        public GYLXController(IDbOperate<zxjc_gylx> gylxservice)
        {
            _gylxservice = gylxservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gylxservice.GetList(parm, out resultcount);
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