using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.GTJC
{
    /// <summary>
    /// 缸体检测
    /// </summary>
    [RoutePrefix("api/cdgc/gtjc/checkdata")]
    public class GTJCDataController : ApiController
    {
        private IGtjc_Result _gtjc_jgservice;
        public GTJCDataController(IGtjc_Result gtjc_jgservice)
        {
            _gtjc_jgservice = gtjc_jgservice;
        }
        [HttpGet, Route("get_check_data")]
        public IHttpActionResult Get_CheckData_By_Vin(string rq,string ewm,string cplx)
        {
            try
            {
                var list = _gtjc_jgservice.Get_CheckData_Vin(rq,ewm,cplx);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("create_bill")]
        public IHttpActionResult Create_Bill(string rq,string cplx)
        {
            try
            {
                DateTime jcrq = DateTime.Now;
                if (string.IsNullOrEmpty(rq))
                {
                    jcrq = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    jcrq = Convert.ToDateTime(Convert.ToDateTime(rq).ToString("yyyy-MM-dd"));
                }
               var ids = _gtjc_jgservice.Create_Bill_Ids(cplx, jcrq);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    ids = ids
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("save_bill")]

        public IHttpActionResult Save_Check_Data(List<zxjc_gtjc_bill> bills)
        {
            try
            {
                foreach (var bill in bills)
                {
                     _gtjc_jgservice.Save_Gtjc_CheckData(bill);
                }
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                });
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}