using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;
namespace MesAdmin.Controllers.A1.GYGL
{
    /// <summary>
    /// 岗位名称与机型对应关系
    /// </summary>
    [RoutePrefix("api/a1/gwjx")]
    public class A1GWJXController : BaseApiController<base_gwzx_jx>
    {
        public A1GWJXController(IDbOperate<base_gwzx_jx> gwjxservice, IRequireVerify requireverfify, IImportData<base_gwzx_jx> importservice):base(gwjxservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }

        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}