using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.JhMgr
{
    [RoutePrefix("api/ducar/zpjh")]
    public class DuCarZPJHController : BaseApiController<pp_zpjh>
    {
        private IDuCarOrder _orderservice;
        public DuCarZPJHController(IDbOperate<pp_zpjh> baseservice, IDuCarOrder orderservice) : base(baseservice)
        {
            _orderservice = orderservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "jhsj";
            return base.GetList(parm);
        }
        [HttpPost,Route("yqt")]
        public IHttpActionResult YQt(List<pp_zpjh> orders)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                foreach (var order in orders)
                {
                    var ret = _orderservice.YQT(order.order_no);
                    msg.Append(ret.msg);
                }
                return Json(new
                {
                    code = 1,
                    msg = msg.ToString(),
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("wqt")]
        public IHttpActionResult WQt(List<pp_zpjh> orders)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                foreach (var order in orders)
                {
                    var ret = _orderservice.WQT(order.order_no);
                    msg.Append(ret.msg);
                }
                return Json(new
                {
                    code = 1,
                    msg = msg.ToString(),
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("bom")]
        public IHttpActionResult CheckBOM(List<pp_zpjh> orders)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                foreach (var order in orders)
                {
                    var ret = _orderservice.Set_OrderJy_BOM(order.order_no);
                    msg.Append(ret.msg);
                }
                return Json(new
                {
                    code = 1,
                    msg = msg.ToString(),
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("gylx")]
        public IHttpActionResult CheckGylx(List<pp_zpjh> orders)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                foreach (var order in orders)
                {
                    var ret = _orderservice.Set_OrderJy_Gylx(order.order_no);
                    msg.Append(ret.msg);
                }
                return Json(new
                {
                    code = 1,
                    msg = msg.ToString(),
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("alljy")]
        public IHttpActionResult CheckAll(List<pp_zpjh> orders)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                foreach (var order in orders)
                {
                    var qtret = _orderservice.YQT(order.order_no);
                    msg.Append(qtret.msg);
                    var gylxret = _orderservice.Set_OrderJy_Gylx(order.order_no);
                    msg.Append(gylxret.msg);
                    var bomret = _orderservice.Set_OrderJy_BOM(order.order_no);
                    msg.Append(bomret.msg);
                }
                return Json(new
                {
                    code = 1,
                    msg = msg.ToString(),
                });
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}