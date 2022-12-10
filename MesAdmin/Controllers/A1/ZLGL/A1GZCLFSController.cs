using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.A1.ZLGL
{
    [RoutePrefix("api/a1/clfs")]
    public class A1GZCLFSController : BaseApiController<zxjc_fault_clfs>
    {
        public A1GZCLFSController(IDbOperate<zxjc_fault_clfs> clfsservice, IRequireVerify requireverfify, IImportData<zxjc_fault_clfs> importservice) :base(clfsservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_fault_clfs>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_fault_clfs> entitys)
        {
            return base.Add(entitys);
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_fault_clfs>), "BatSetValue")]
        public override IHttpActionResult Edit(List<zxjc_fault_clfs> entitys)
        {
            return base.Edit(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault_clfs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault_clfs>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault_clfs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault_clfs>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault_clfs,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault_clfs>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}