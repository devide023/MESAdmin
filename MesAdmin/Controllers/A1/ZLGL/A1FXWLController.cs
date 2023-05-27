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
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.ZLGL
{
    [RoutePrefix("api/a1/fxwl")]
    public class A1FXWLController : BaseApiController<base_fxwl>
    {
        public A1FXWLController(IDbOperate<base_fxwl> baseservice, IRequireVerify requireverfify, IImportData<base_fxwl> importservice) : base(baseservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }

        [TemplateVerify("ZDMesModels.TJ.A1.base_fxwl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_fxwl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_fxwl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}