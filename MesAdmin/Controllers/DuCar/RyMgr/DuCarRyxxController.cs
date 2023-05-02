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

namespace MesAdmin.Controllers.CuCar.RyMgr
{
    [RoutePrefix("api/ducar/ryxx")]
    public class DuCarRyxxController : BaseApiController<zxjc_ryxx>
    {
        public DuCarRyxxController(IDbOperate<zxjc_ryxx> baseservice, IRequireVerify requireverfify, IImportData<zxjc_ryxx> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_ryxx> entitys)
        {
            return base.Add(entitys);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}