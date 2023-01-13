using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/gylx")]
    public class A1GYLXController : BaseApiController<mes_zxjc_gylx>
    {
        private IDbOperate<mes_zxjc_gylx> _gylxservice;
        private IEntityDetail<string> _detailservice;
        public A1GYLXController(IDbOperate<mes_zxjc_gylx> gylxservice, IRequireVerify requireverfify, IImportData<mes_zxjc_gylx> importservice, IEntityDetail<string> detailservice) :base(gylxservice)
        {
            _gylxservice = gylxservice;
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _detailservice = detailservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gylxservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    item.statusno_list = _detailservice.Details(item.jxno).ToList().Select(t => new { label = t, value = t });
                }
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
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult Add(List<mes_zxjc_gylx> entitys)
        {
            return base.Add(entitys);
        }
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult Edit(List<mes_zxjc_gylx> entitys)
        {
            return base.Edit(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }

    }
}