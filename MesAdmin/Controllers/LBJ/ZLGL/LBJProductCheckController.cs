using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesInterfaces.TJ;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.ZLGL
{
    [RoutePrefix("api/lbj/basecheck")]
    public class LBJProductCheckController : BaseApiController<zxjc_base_check>
    {
        private ILBJProductCheck _cpcheckservice;
        public LBJProductCheckController(IDbOperate<zxjc_base_check> baseservice, ILBJProductCheck cpcheckservice, IRequireVerify requireverfify, IImportData<zxjc_base_check> importservice) : base(baseservice)
        {
            _baseservice = baseservice;
            _requireverfify = requireverfify;
            _importservice = importservice;
            _cpcheckservice = cpcheckservice;
        }
        [HttpGet,Route("checkitems")]
        public IHttpActionResult Get_Cpxh_CheckItems(string cpxh)
        {
            try
            {
                var list = _cpcheckservice.GetCheckItemsByCpxh(cpxh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.LBJ.zxjc_base_check,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_base_check>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.zxjc_base_check,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_base_check>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.zxjc_base_check,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_base_check>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}