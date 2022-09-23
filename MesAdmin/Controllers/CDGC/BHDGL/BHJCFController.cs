using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
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
        public BHJCFController(IDbOperate<lbj_qms_4mbhd> fmbhdservice) : base(fmbhdservice)
        {
            _4mbhdservice = fmbhdservice;
        }
    }
}