using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.JhMgr
{
    [RoutePrefix("api/ducar/jhpc")]
    public class DuCarOrderXhController : BaseApiController<zxjc_order_sxh>
    {
        private IDuCarBaseInfo _baseinfoservice;
        private IDuCarOrder _orderservice;
        public DuCarOrderXhController(IDbOperate<zxjc_order_sxh> baseservice, IDuCarBaseInfo baseinfoservice, IDuCarOrder orderservice) : base(baseservice)
        {
            _baseinfoservice = baseinfoservice;
            _orderservice = orderservice;
        }

        [HttpGet,Route("orderinfo_by_orderno")]
        public IHttpActionResult Get_OrderInfo_By_Orderno(string orderno)
        {
            try
            {
                pp_zpjh orderinfo = _orderservice.Get_OrdrInfo(orderno);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    orderinfo = orderinfo
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("orderno_by_code")]
        public IHttpActionResult Get_OrderNo_By_Key(string key)
        {
            try
            {
                var list = _baseinfoservice.Get_OrderNo_By_Key(key).Select(t => new { label=t.scx,value=t.order_no});
                return Json(new
                {
                    code = 1,
                    msg = "ok",
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