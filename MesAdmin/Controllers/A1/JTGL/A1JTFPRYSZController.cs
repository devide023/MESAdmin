using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtfpsysz")]
    /// <summary>
    /// 技通分配人员设置
    /// </summary>
    public class A1JTFPRYSZController : BaseApiController<zxjc_t_jstcfp_ry>
    {
        private IUser _system_userservice;
        public A1JTFPRYSZController(IDbOperate<zxjc_t_jstcfp_ry> baseservice,IUser user) : base(baseservice)
        {
            _system_userservice = user;
        }
        [HttpGet,Route("get_sysuser")]
        public IHttpActionResult GetUserByKey(string key) {
            try
            {
                var list = _system_userservice.GetUserByKey(key).Select(t => new { label = t.name, value = t.code });
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