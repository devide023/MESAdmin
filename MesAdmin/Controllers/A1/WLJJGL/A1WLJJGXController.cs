using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.WLJJGL
{
    [RoutePrefix("api/a1/wljjgxb")]
    public class A1WLJJGXController : BaseApiController<base_wljjgxb>
    {
        public A1WLJJGXController(IDbOperate<base_wljjgxb> baseservice, IRequireVerify requireverfify, IImportData<base_wljjgxb> importservice) : base(baseservice)
        {
            _requireverfify= requireverfify;
            _importservice=importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_wljjgxb,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_wljjgxb,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_wljjgxb,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}