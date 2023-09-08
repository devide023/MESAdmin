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
using ZDMesModels.TJ.A1.LSWT;

namespace MesAdmin.Controllers.A1.GYGL
{
    /// <summary>
    /// 历史问题
    /// </summary>
    /// 
    [RoutePrefix("api/a1/lswt")]
    public class A1LSWTController : BaseApiController<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy>
    {
        private IA1GYGL _gyglservice;
        public A1LSWTController(IDbOperate<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy> baseservice, IA1GYGL gyglservice, IRequireVerify requireverfify, IImportData<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy> importservice) : base(baseservice)
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
                var list = _gyglservice.Get_Lswt_List(parm, out resultcount);
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

        [TemplateVerify("ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}