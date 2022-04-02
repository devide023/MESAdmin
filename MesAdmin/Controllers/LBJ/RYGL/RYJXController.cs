using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesInterfaces.LBJ.RyMgr;
using ZDMesModels;

namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/ryjx")]
    public class RYJXController : ApiController
    {
        private IRyJx _ryjxservice;
        public RYJXController(IRyJx ryjxservice)
        {
            _ryjxservice = ryjxservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _ryjxservice.Get_RyJxList(parm, out resultcount);
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