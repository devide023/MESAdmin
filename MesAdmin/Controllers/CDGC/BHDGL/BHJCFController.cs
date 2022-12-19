using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.CDGC;

namespace MesAdmin.Controllers.CDGC.BHDGL
{
    /// <summary>
    /// 变化点触发
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/bhdcf")]
    public class BHJCFController : BaseApiController<lbj_qms_4mbhd>
    {
        private IDbOperate<lbj_qms_4mbhd> _4mbhdservice;
        private I4MBHD _bhdservice;
        public BHJCFController(IDbOperate<lbj_qms_4mbhd> fmbhdservice, I4MBHD bhdservice) : base(fmbhdservice)
        {
            _4mbhdservice = fmbhdservice;
            _bhdservice = bhdservice;
        }

        [HttpPost,Route("bhdbh")]
        public IHttpActionResult DealBhd(List<lbj_qms_4mbhd> list)
        {
            try
            {
              var isok = _bhdservice.BHBHD(list);
                if (isok)
                {
                    return Json(new {code=1,msg="保存成功"});
                }
                else
                {
                    return Json(new { code = 0, msg = "保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}