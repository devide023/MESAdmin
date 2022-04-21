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
namespace MesAdmin.Controllers.LBJ.BHDGL
{
    [RoutePrefix("api/lbj/bhdjl")]
    public class BHDJLController : ApiController
    {
        private IDbOperate<lbj_qms_4mbhd> _bhdjlservice;
        public BHDJLController(IDbOperate<lbj_qms_4mbhd> bhdjlservice)
        {
            _bhdjlservice = bhdjlservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _bhdjlservice.GetList(parm, out resultcount);
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