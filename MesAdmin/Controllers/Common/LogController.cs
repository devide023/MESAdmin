using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/log")]   
    public class LogController : ApiController
    {
        private IDbOperate<mes_oper_log> _operatelogservice;
        public LogController(IDbOperate<mes_oper_log> operatelogservice)
        {
            _operatelogservice = operatelogservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _operatelogservice.GetList(parm, out resultcount);
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