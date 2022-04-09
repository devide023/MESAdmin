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

namespace MesAdmin.Controllers.LBJ.JHGL
{
    [RoutePrefix("api/lbj/scjh")]
    public class SCJHController : ApiController
    {
        private IDbOperate<pp_zpjh> _zpjhservice;
        public SCJHController(IDbOperate<pp_zpjh> zpjhservice)
        {
            _zpjhservice = zpjhservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _zpjhservice.GetList(parm, out resultcount);
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