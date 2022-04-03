using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
namespace MesAdmin.Controllers.LBJ.BaseInfo
{
    [RoutePrefix("api/lbj/baseinfo")]
    public class BaseInfoController : ApiController
    {
        private IBaseInfo _baseinfo;
        public BaseInfoController(IBaseInfo baseinfo)
        {
            _baseinfo = baseinfo;
        }
        [HttpGet, Route("gcxx")]
        public IHttpActionResult GCXX()
        {
            try
            {
                var list = _baseinfo.GetGCXX().Select(t => new { label = t.gcmc, value = t.gcdm });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("scx")]
        public IHttpActionResult ScxXX(string gcdm)
        {
            try
            {
                var list = _baseinfo.GetScxXX(gcdm).Select(t => new { label = t.scxmc, value = t.scx });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("gwzd")]
        public IHttpActionResult Gwzd()
        {
            try
            {
                var list = _baseinfo.GetGwZd().Select(t => new { label = t.gwmc, value = t.gwh });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("usercode")]
        public IHttpActionResult GetUserCode(string key)
        {
            try
            {
                var list = _baseinfo.GetUserCode(key).Select(t => new { label = t.username+"["+t.usercode+"]",value = t.usercode }).OrderBy(t => t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}