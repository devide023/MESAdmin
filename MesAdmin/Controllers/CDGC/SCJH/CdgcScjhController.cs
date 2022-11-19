using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.CDGC.SCJH
{
    [RoutePrefix("api/cdgc/scjh")]
    public class CdgcScjhController : BaseApiController<zxjc_scjh>
    {
        public CdgcScjhController(IDbOperate<zxjc_scjh> cdgcscjhservice,IRequireVerify verifyservice):base(cdgcscjhservice)
        {
            this._requireverfify = verifyservice;
        }
    }
}