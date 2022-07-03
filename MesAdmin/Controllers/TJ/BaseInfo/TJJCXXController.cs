using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.TJ.BaseInfo
{
    [RoutePrefix("api/tj/jcxx")]
    public class TJJCXXController : ApiController
    {
        private ITJBaseInfo _baseinfoservice;
        public TJJCXXController(ITJBaseInfo baseinfoservice)
        {
            _baseinfoservice = baseinfoservice;
        }
        [HttpGet, Route("scx")]
        public IHttpActionResult GetScxInfo()
        {
            try
            {
                var list = _baseinfoservice.GetScxXx().Select(t=>new {label=t.scxmc,value=t.scx});
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("gwzd")]
        public IHttpActionResult GetGWZD()
        {
            try
            {
                var list = _baseinfoservice.GetGWZD().Select(t => new { label = t.workname, value = t.workno }).OrderBy(t => t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}