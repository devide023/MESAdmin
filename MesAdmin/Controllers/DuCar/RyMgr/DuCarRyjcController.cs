using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.RyMgr
{
    [RoutePrefix("api/ducar/ryjc")]
    public class DuCarRyjcController : BaseApiController<zxjc_jcgl>
    {
        public DuCarRyjcController(IDbOperate<zxjc_jcgl> baseservice, IRequireVerify requireverfify, IImportData<zxjc_jcgl> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
    }
}