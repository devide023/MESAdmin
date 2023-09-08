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
        private IA1GYGL _gyglservice;
        public A1GYZPZDController(IDbOperate<zxjc_t_dzgy> dzgyservice, IA1GYGL gyglservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice) : base(dzgyservice)
        {
            _gyglservice = gyglservice;
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gyglservice.Get_ZpzdList(parm, out resultcount);
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