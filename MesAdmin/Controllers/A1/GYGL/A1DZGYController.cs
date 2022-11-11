using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/dzgy")]
    public class A1DZGYController : BaseApiController<zxjc_t_dzgy>
    {
        private IDbOperate<zxjc_t_dzgy> _dzgyservice;
        private IEntityDetail<string> _detailservice;
        public A1DZGYController(IDbOperate<zxjc_t_dzgy> dzgyservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice, IEntityDetail<string> detailservice) :base(dzgyservice)
        {
            _dzgyservice = dzgyservice;
            _requireverfify = requireverfify;
            _importservice = importservice;
            _detailservice = detailservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _dzgyservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var _temp = _detailservice.Details(item.jxno);
                    var ztlist = new List<dynamic>();
                    foreach (var zt in _temp)
                    {
                        ztlist.Add(new { label = zt, value = zt });
                    }
                    item.statusno_list = ztlist;
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}