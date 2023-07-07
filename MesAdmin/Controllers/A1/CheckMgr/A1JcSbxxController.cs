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

namespace MesAdmin.Controllers.A1.CheckMgr
{
    /// <summary>
    /// 检测资产信息表
    /// </summary>
    /// 
    [RoutePrefix("api/a1/jcsbxx")]
    public class A1JcSbxxController : BaseApiController<zxjc_jcsbxx>
    {
        public A1JcSbxxController(IDbOperate<zxjc_jcsbxx> baseservice, IRequireVerify requireverfify, IImportData<zxjc_jcsbxx> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }

        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcsbxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcsbxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcsbxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}