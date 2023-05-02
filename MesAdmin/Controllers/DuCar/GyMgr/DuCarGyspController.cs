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
    [RoutePrefix("api/ducar/gysp")]
    public class DuCarGyspController : BaseApiController<zxjc_t_dzgy_sp>
    {
        public DuCarGyspController(IDbOperate<zxjc_t_dzgy_sp> baseservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy_sp> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_dzgy_sp,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy_sp>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_dzgy_sp,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy_sp>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_dzgy_sp,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy_sp>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}