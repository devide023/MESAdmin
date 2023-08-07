using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using ZDMesModels;
using MesAdmin.Filters;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.LBJ.GYGL
{
    [RoutePrefix("api/lbj/gylx")]
    public class LBJGYLXController : BaseApiController<zxjc_gylx>
    {
        private IBaseInfo _baseinfo;

        public LBJGYLXController(IDbOperate<zxjc_gylx> baseservice, IBaseInfo baseinfo) : base(baseservice)
        {
            _baseinfo = baseinfo;
        }

        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var list = this._baseservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var gwhs = gwzdlist.Where(t => t.scx == item.scx).GroupBy(t=>new {label=t.gwmc,value=t.gwh}).Select(t=>new option_list() {label=t.Key.label,value=t.Key.value});
                    item.gwhs = gwhs.OrderBy(t=>t.value).ToList();
                }
                return Json(new sys_search_result()
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

        /*[HttpPost, SearchFilter, Route("list")]
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var list = _baseservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var gwhs = gwzdlist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.gwmc, value = t.gwh });
                    item.gwhs = gwhs.ToList();
                }
                return Json(new sys_search_result()
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
        }*/
    }
}