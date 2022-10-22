using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;
using MesAdmin.Filters;
namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/gylx")]
    public class A1GYLXController : BaseApiController<mes_zxjc_gylx>
    {
        public A1GYLXController(IDbOperate<mes_zxjc_gylx> gylxservice, IRequireVerify requireverfify, IImportData<mes_zxjc_gylx> importservice) :base(gylxservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }

    }
}