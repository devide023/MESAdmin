using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.CDGC;

namespace MesAdmin.Controllers.CDGC.GTJC
{
    /// <summary>
    /// 缸体检测历史记录
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/gtjchis")]
    public class GTJCHISController : ApiController
    {
        private IDbOperate<zxjc_gtjc_bill> _gtjclsservice;
        private IGtjcHis _hisservice;
        public GTJCHISController(IDbOperate<zxjc_gtjc_bill> gtjclsservice, IGtjcHis hisservice)
        {
            _gtjclsservice = gtjclsservice;
            _hisservice = hisservice;
        }

        [HttpPost,SearchFilter,Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                parm.orderbyexp = " order by rq desc";
                var list = _gtjclsservice.GetList(parm, out resultcount);
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

        [HttpGet, Route("billinfo")]
        public IHttpActionResult Get_BillInfo(int billid)
        {
            try
            {
                var bill = _hisservice.Get_GtjcInfo(billid);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    bill = bill
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}