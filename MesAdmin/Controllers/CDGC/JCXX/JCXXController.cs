using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.JCXX
{
    /// <summary>
    /// 成都工厂基础信息
    /// </summary>
    [RoutePrefix("api/cdgc/jcxx")]
    public class JCXXController : ApiController
    {
        private IDbOperate<base_sbxx> _sbxxservice;
        public JCXXController(IDbOperate<base_sbxx> sbxxservice)
        {
            _sbxxservice = sbxxservice;
        }
        [HttpGet,Route("get_sbxx")]
        public IHttpActionResult Get_Sbxx()
        {
            try
            {
                sys_page parm = new sys_page();
                DynamicParameters dyp = new DynamicParameters();
                dyp.Add(":pageindex", 1);
                dyp.Add(":pagesize", 65535);
                parm.sqlparam = dyp;
                var list = _sbxxservice.GetList(parm, out int resultcount);
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
    }
}