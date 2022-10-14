using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;

namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/gycs")]
    public class A1GYCSController : BaseApiController<base_gycs>
    {
        public A1GYCSController(IDbOperate<base_gycs> gycsservice, IRequireVerify requireverfify, IImportData<base_gycs> importservice) :base(gycsservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }

        [TemplateVerify("ZDMesModels.TJ.A1.base_gycs,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gycs,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gycs,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}