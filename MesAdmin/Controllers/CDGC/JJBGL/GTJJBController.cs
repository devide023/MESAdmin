using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.CDGC;
using ZDMesModels;

namespace MesAdmin.Controllers.CDGC.JJBGL
{
    /// <summary>
    /// 缸体交接班
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/gtjjb")]
    public class GTJJBController : BaseApiController<zxjc_gtjjb_bill>
    {
        private IDbOperate<zxjc_gtjjb_bill> _baseservice;
        private IGtjjb _gtjjbservice;
        public GTJJBController(IDbOperate<zxjc_gtjjb_bill> baseservice, IGtjjb gtjjbservice) : base(baseservice)
        {
            _baseservice = baseservice;
            _gtjjbservice = gtjjbservice;
        }
        [HttpGet,Route("get_cplist")]
        public IHttpActionResult Get_CpList()
        {
            try
            {
                var list = _gtjjbservice.Get_CpList().Select(t => new { label = t, value = t });
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
        [HttpPost, Route("save_gtjjb")]
        public IHttpActionResult Save_GtJJB(zxjc_gtjjb_bill form)
        {
            try
            {
                var ret = _gtjjbservice.Save_Gtjjb(form);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据保存失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("get_rq_bc")]
        public IHttpActionResult Get_Rq_BC(string rq, string bc)
        {
            try
            {
                string sybc = string.Empty;
                string ssbc = string.Empty;
                DateTime dt = Convert.ToDateTime(rq);
                string sybc_rq = string.Empty;
                string ssbc_rq = string.Empty;
                switch (bc)
                {
                    case "白班":
                        sybc = "晚班";
                        ssbc = "中班";
                        sybc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        ssbc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case "中班":
                        sybc = "白班";
                        ssbc = "晚班";
                        sybc_rq = dt.ToString("yyyy-MM-dd");
                        ssbc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case "晚班":
                        sybc = "中班";
                        ssbc = "白班";
                        sybc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        ssbc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    default:
                        break;
                }
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    bcinfo = new
                    {
                        sybc = sybc,
                        ssbc = ssbc,
                        sybc_rq = sybc_rq,
                        ssbc_rq = ssbc_rq,
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("load_bc_data")]
        public IHttpActionResult Get_BC_Data(string rq, string bc)
        {
            try
            {
                var list = _gtjjbservice.Get_Gtjjb_List_ByBc(rq, bc);
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