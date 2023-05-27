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
    [RoutePrefix("api/ducar/jxgl")]
    public class DuCarJcxxController : BaseApiController<sc_jxgl>
    {
        public DuCarJcxxController(IDbOperate<sc_jxgl> baseservice, IRequireVerify requireverfify, IImportData<sc_jxgl> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }

        [TemplateVerify("ZDMesModels.Ducar.sc_jxgl,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<sc_jxgl>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.sc_jxgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.sc_jxgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}