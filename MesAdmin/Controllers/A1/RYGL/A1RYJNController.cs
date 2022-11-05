using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using MesAdmin.Filters;
using ZDMesModels;

namespace MesAdmin.Controllers.A1.RYGL
{
    [RoutePrefix("api/a1/ryjn")]
    public class A1RYJNController : BaseApiController<zxjc_ryxx_jn>
    {
        private IRYJN _ryjn;
        public A1RYJNController(IDbOperate<zxjc_ryxx_jn> ryjnservice, IRYJN ryjn, IRequireVerify requireverfify, IImportData<zxjc_ryxx_jn> importservice) :base(ryjnservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _ryjn = ryjn;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_ryxx_jn> entitys)
        {
            return base.Add(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return  base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}