using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.Report
{
    /// <summary>
    /// 成都工厂缸体交接班报表
    /// 
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/report")]
    public class CDGCGTJJBController : ApiController
    {
        private IReport _reportservice;
        public CDGCGTJJBController(IReport reportservice)
        {
            _reportservice = reportservice;
        }

        [HttpPost,Route("gtjjb")]
        public IHttpActionResult Report_GTJJB(form_gtjjb form)
        {
            try
            {
                int resultcount = 0;
                var list = _reportservice.Get_GTJJB_Report(form, out resultcount);

                return Json(new
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