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

namespace MesAdmin.Controllers.DuCar.GyMgr
{
    [RoutePrefix("api/ducar/gycs")]
    public class DuCarGycsController : BaseApiController<base_gycs>
    {
        public DuCarGycsController(IDbOperate<base_gycs> baseservice, IRequireVerify requireverfify, IImportData<base_gycs> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gycs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gycs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gycs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}