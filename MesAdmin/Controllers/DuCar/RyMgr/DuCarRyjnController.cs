using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.CuCar.RyMgr
{
    [RoutePrefix("api/ducar/ryjn")]
    public class DuCarRyjnController : BaseApiController<zxjc_ryxx_jn>
    {
        private IDuCarRyjn _ducarryjnservice;
        public DuCarRyjnController(IDbOperate<zxjc_ryxx_jn> baseservice, IRequireVerify requireverfify, IImportData<zxjc_ryxx_jn> importservice, IDuCarRyjn ducarryjnservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
            _ducarryjnservice = ducarryjnservice;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_ryxx_jn> entitys)
        {
            return base.Add(entitys);
        }

        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_ryxx_jn,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx_jn>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}