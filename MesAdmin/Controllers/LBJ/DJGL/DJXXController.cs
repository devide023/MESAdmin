using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.LBJ.DJGL
{
    [RoutePrefix("api/lbj/djxx")]
    public class DJXXController : ApiController
    {
        private IDbOperate<zxjc_djxx> _djxxservice;
        private IBaseInfo _baseinfo;
        public DJXXController(IDbOperate<zxjc_djxx> djxxservice, IBaseInfo baseinfo)
        {
            _djxxservice = djxxservice;
            _baseinfo = baseinfo;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var scxzxlist = _baseinfo.Get_ALL_ScxXX_JJ();
                var list = _djxxservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var options = new List<option_list>();
                    var l = gwzdlist.Where(t => t.scx == item.scx);
                    var scxzxs = scxzxlist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.scxzxmc, value = t.scxzx }).OrderBy(t => t.value);
                    foreach (var o in l)
                    {
                        var q = options.Where(t => t.value == o.gwh);
                        if (q.Count() == 0)
                        {
                            options.Add(new option_list { label = o.gwmc, value = o.gwh });
                        }
                    }
                    item.gwhs = options;
                    item.scxzxs = scxzxs.ToList();
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
    }
}