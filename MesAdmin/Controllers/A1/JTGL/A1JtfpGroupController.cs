using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtfp/group")]
    public class A1JtfpGroupController : BaseApiController<zxjc_t_jstcfp_group>
    {
        private IJTFPRY _jtfpgroup;
        public A1JtfpGroupController(IDbOperate<zxjc_t_jstcfp_group> baseservice, IJTFPRY jtfpgroup) : base(baseservice)
        {
            _jtfpgroup = jtfpgroup;
        }
        /// <summary>
        /// 所有技通分配分组列表
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("all_zblist")]
        public IHttpActionResult Get_JtFpGroup()
        {
            try
            {
                var list = _jtfpgroup.Get_All_Group();
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new {label=t.zbmc,value=t.zbid}) });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}