using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.SBGL
{
    [RoutePrefix("api/cdgc/sbgl")]
    public class SBGLController : BaseApiController<base_sbxx>
    {
        public SBGLController(IDbOperate<base_sbxx> sbglservice) : base(sbglservice)
        {

        }
    }
}