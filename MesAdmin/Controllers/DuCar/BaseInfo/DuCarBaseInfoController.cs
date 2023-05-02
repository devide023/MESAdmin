using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.BaseInfo
{
    [RoutePrefix("api/ducar/baseinfo")]
    public class DuCarBaseInfoController : ApiController
    {
        private IDuCarBaseInfo _basesinfoservice;
        public DuCarBaseInfoController(IDuCarBaseInfo basesinfoservice)
        {
            _basesinfoservice = basesinfoservice;
        }

        [HttpGet, Route("scx")]
        public IHttpActionResult GetScxList()
        {
            try
            {
                List<base_scxxx> list = _basesinfoservice.Get_All_ScxList().ToList();
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.scxmc, value = t.scx }).OrderBy(x => x.label) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("gwzd")]
        public IHttpActionResult GetGwzdList()
        {
            try
            {
                List<base_gwzd> list = _basesinfoservice.GetGWList().ToList();
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.gwmc, value = t.gwh }).OrderBy(x => x.value) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("gwzdbyscx")]
        public IHttpActionResult GetGwZd(string scx)
        {
            try
            {
                var list = _basesinfoservice.GetGWList(scx);
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.gwmc, value = t.gwh }) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("ryxx_by_scx_code")]
        public IHttpActionResult GetRyxxByScxCode(dynamic parm)
        {
            try
            {
                if (parm != null)
                {
                    IEnumerable<zxjc_ryxx> list = _basesinfoservice.GetScxRyXxByKey(parm.scx.ToString(), parm.key.ToString());
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.username, value = t.usercode }) });
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
        [HttpGet, Route("ryxx_by_code")]
        public IHttpActionResult GetRyByCode(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var list = _basesinfoservice.GetRyXxByKey(key);
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.username, value = t.usercode }) });
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
        [HttpPost,Route("sbbhbygwh")]
        public IHttpActionResult Get_Sbbh_By_Gwh(dynamic parm)
        {
            try
            {
                if(parm == null)
                {
                    return Json(new { code = 0, msg = "查询参数为空" });
                }
                    IEnumerable<base_sbxx> list = _basesinfoservice.GetSbbhsByGwh(parm.scx.ToString(),parm.gwh.ToString());
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.sbmc, value = t.sbbh }) });
                

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("faultno_by_key")]
        public IHttpActionResult Get_FaultnoByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var list = _basesinfoservice.Get_FaultNo_By_Key(key);
                    return Json(new { code = 1, msg = "ok", list = list.Select(t => new { label = t.faultname, value = t.faultno }) });
                }
                else
                {
                    return Json(new { code = 0, msg = "查询参数为空" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}