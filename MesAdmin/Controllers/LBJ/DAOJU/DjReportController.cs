using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/djreport")]
    public class DjReportController : ApiController
    {
        private IDjReport _report;
        public DjReportController(IDjReport report)
        {
            _report = report;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _report.GetRjZxList(parm, out resultcount);
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
        [HttpGet,Route("sbxxbyscx")]
        public IHttpActionResult GetSbxxListByScx(string scx)
        {
            try
            {
                var list = _report.Get_SbxxBy_Scx(scx);
                return Json(new sys_search_result()
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
        [HttpGet, Route("dbxxbysbbh")]
        public IHttpActionResult GetDbxxBySbh(string scx,string sbh)
        {
            try
            {
                var list = _report.Get_ZxDbXX_By_Sbbh(scx,sbh);
                return Json(new sys_search_result()
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
    }
}