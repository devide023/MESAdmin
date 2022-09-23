using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/dbinfo")]
    public class DBInfoController : ApiController
    {
        private IDbInfo _dbinfoservice;
        public DBInfoController(IDbInfo dbinfoservice)
        {
            _dbinfoservice = dbinfoservice;
        }
        [HttpGet,Route("tables")]
        public IHttpActionResult GetTableList(string key)
        {
            try
            {
                var list = _dbinfoservice.GetTable(key);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("tablecolinfo")]
        public IHttpActionResult GetColInfo(string table)
        {
            try
            {
                var list = _dbinfoservice.GetColInfoByTable(table);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}