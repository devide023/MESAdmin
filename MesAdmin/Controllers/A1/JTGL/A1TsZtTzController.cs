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
namespace MesAdmin.Controllers.A1.JTGL
{
    /// <summary>
    /// 特殊状态通知
    /// </summary>
    /// 
    [RoutePrefix("api/a1/tszttz")]
    public class A1TsZtTzController : BaseApiController<zxjc_t_tstc>
    {
        public A1TsZtTzController(IDbOperate<zxjc_t_tstc> tszttzservice, IRequireVerify requireverfify, IImportData<zxjc_t_tstc> importservice) :base(tszttzservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_tstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_tstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_tstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}