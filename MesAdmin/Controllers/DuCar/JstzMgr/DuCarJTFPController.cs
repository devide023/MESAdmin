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

namespace MesAdmin.Controllers.DuCar.JstzMgr
{
    [RoutePrefix("api/ducar/jtfp")]
    public class DuCarJTFPController : BaseApiController<zxjc_t_jstcfp>
    {
        public DuCarJTFPController(IDbOperate<zxjc_t_jstcfp> baseservice, IRequireVerify requireverfify, IImportData<zxjc_t_jstcfp> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstcfp,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstcfp,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstcfp,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}