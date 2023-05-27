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

namespace MesAdmin.Controllers.A1.CheckMgr
{
    [RoutePrefix("api/a1/jcjcxx")]
    public class A1JcjcxxController : BaseApiController<zxjc_jcjcxx>
    {
        public A1JcjcxxController(IDbOperate<zxjc_jcjcxx> baseservice, IRequireVerify requireverfify, IImportData<zxjc_jcjcxx> importservice) : base(baseservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcjcxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcjcxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcjcxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}