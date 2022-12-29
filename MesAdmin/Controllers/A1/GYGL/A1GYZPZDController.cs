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
using ZDMesModels.TJ.A1.GYZD;

namespace MesAdmin.Controllers.A1.GYGL
{
    /// <summary>
    /// 装配指导
    /// </summary>
    /// 
    [RoutePrefix("api/a1/gyzpzd")]
    public class A1GYZPZDController : BaseApiController<zxjc_t_dzgy>
    {
        public A1GYZPZDController(IDbOperate<zxjc_t_dzgy> dzgyservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice) : base(dzgyservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}