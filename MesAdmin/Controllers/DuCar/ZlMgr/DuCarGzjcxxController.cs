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

namespace MesAdmin.Controllers.DuCar.ZlMgr
{
    [RoutePrefix("api/ducar/fault")]
    public class DuCarGzjcxxController : BaseApiController<zxjc_fault>
    {
        public DuCarGzjcxxController(IDbOperate<zxjc_fault> baseservice, IRequireVerify requireverfify, IImportData<zxjc_fault> importservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }

        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_fault> entitys)
        {
            return base.Add(entitys);
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult Edit(List<zxjc_fault> entitys)
        {
            return base.Edit(entitys);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}