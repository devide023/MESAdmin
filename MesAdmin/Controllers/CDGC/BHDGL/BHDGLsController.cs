using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.CDGC.BHDGL
{
    /// <summary>
    /// 变化点基础信息
    /// </summary>
    [RoutePrefix("api/cdgc/bhdgl")]
    public class BHDGLsController : BaseApiController<base_bhdxx>
    {
        private IDbOperate<base_bhdxx> _bhdxxservice;
        public BHDGLsController(IDbOperate<base_bhdxx> bhdxxservice):base(bhdxxservice)
        {
            _bhdxxservice = bhdxxservice;
        }

    }
}