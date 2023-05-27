using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.Report
{
    [RoutePrefix("api/ducar/report")]
    public class DuCarReportController : ApiController
    {
        private IDuCarReport _reportservice;
        public DuCarReportController(IDuCarReport reportservice)
        {
            _reportservice = reportservice;
        }
        /// <summary>
        /// 单台数据追溯
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("dtzs")]
        public IHttpActionResult Dtzs_Report(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _reportservice.Get_Engine_No_Data(parm,out resultcount);

                return Json(new { code = 1, msg = "ok",list = list, resultcount = resultcount });
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 故障统计
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("gztj")]
        public IHttpActionResult Fault_Static_Report(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _reportservice.Get_Fault_Static(parm, out resultcount);

                return Json(new { code = 1, msg = "ok", list = list, resultcount = resultcount });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("gwzs")]
        public IHttpActionResult GwZs_Report(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _reportservice.Get_Gwzs_Data(parm, out resultcount);

                return Json(new { code = 1, msg = "ok", list = list, resultcount = resultcount });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}