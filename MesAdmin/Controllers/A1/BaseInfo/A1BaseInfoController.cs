using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.BaseInfo
{
    [RoutePrefix("api/a1/baseinfo")]
    public class A1BaseInfoController : ApiController
    {
        private IA1BaseInfo _a1baseinfo;
        public A1BaseInfoController(IA1BaseInfo a1baseinfo)
        {
            _a1baseinfo = a1baseinfo;
        }
        [HttpGet, Route("gwzd")]
        public IHttpActionResult GetGwZd()
        {
            try
            {
                var list = _a1baseinfo.GetGWList();
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.gwmc, value = t.gwh }) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("gwh_by_key")]
        public IHttpActionResult GetGwHByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var list = _a1baseinfo.GetGWList();
                    var retlist = list.Where(t => t.gwh.Contains(key) || t.gwmc.Contains(key));
                    return Json(new { code = 1, msg = "ok", list = retlist.Select(t => new { label = t.gwmc, value = t.gwh }) });
                }
                else
                {
                    List<dynamic> list = new List<dynamic>();
                    return Json(new { code = 1, msg = "ok", list = list });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("sbxx_by_gwh")]
        public IHttpActionResult GetSbxx(string gwh)
        {
            try
            {
                var list = new List<base_sbxx>();
                if (!string.IsNullOrEmpty(gwh))
                {
                    list = _a1baseinfo.GetSbXxList().Where(t => t.gwh == gwh).OrderBy(t=>t.sbbh).ToList();
                }
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.sbmc, value = t.sbbh }) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("ryxx_by_code")]
        public IHttpActionResult GetRyByCode(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var list = _a1baseinfo.GetRyXxList();
                    list = list.Where(t=>t.usercode.Contains(key) || t.username.Contains(key)).ToList();
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.username, value = t.usercode }) });
                }
                else
                {
                    List<dynamic> ret = new List<dynamic>();
                    return Json(new { code = 1, msg = "ok", list = ret});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("jxno_by_code")]
        public IHttpActionResult GetJxNoByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var list = _a1baseinfo.GetJxNoByKey(key);
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.ccmc, value = t.ccbm }) });
                }
                else
                {
                    List<dynamic> ret = new List<dynamic>();
                    return Json(new { code = 1, msg = "ok", list = ret });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("ztbm_by_jxno")]
        public IHttpActionResult GetZtBMByJxNo(string jxno)
        {
            try
            {
                if (!string.IsNullOrEmpty(jxno))
                {
                    var list = _a1baseinfo.GetZtBMByJxNo(jxno);
                    return Json(new { code = 1, msg = "ok", list = list });
                }
                else
                {
                    List<string> ret = new List<string>();
                    return Json(new { code = 1, msg = "ok", list = ret });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}