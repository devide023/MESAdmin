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
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/gysp")]
    public class A1GYSPController : BaseApiController<zxjc_t_dzgy>
    {
        private IA1GYGL _spgyservice;
        public A1GYSPController(IDbOperate<zxjc_t_dzgy> dzgyservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice, IA1GYGL spgyservice) :base(dzgyservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _spgyservice = spgyservice;
        }

        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _spgyservice.Get_GySpList(parm, out resultcount);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}