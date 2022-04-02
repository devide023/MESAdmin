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
    }
}