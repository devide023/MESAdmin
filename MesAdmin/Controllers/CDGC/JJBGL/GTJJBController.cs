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
        private IDbOperate<zxjc_gtjjb_bill> _gtjjbbaseservice;
        private IGtjjb _gtjjbservice;
        private IUser _userservice;
        public GTJJBController(IDbOperate<zxjc_gtjjb_bill> baseservice, IGtjjb gtjjbservice, IUser userservice) : base(baseservice)
        {
            _gtjjbbaseservice = baseservice;
            _gtjjbservice = gtjjbservice;
            _userservice = userservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gtjjbbaseservice.GetList(parm, out resultcount);
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
                if (form.isadmin)
                {
                    form.lrr = _userservice.CurrentUser().name;
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
                else
                {
                    if (DateTime.Compare(Convert.ToDateTime(form.rq).Date, DateTime.Now.Date) == 0 || DateTime.Compare(Convert.ToDateTime(form.rq).Date, DateTime.Now.Date.AddDays(-1)) == 0)
                    {
                        if (DateTime.Compare(Convert.ToDateTime(form.rq).Date, DateTime.Now.Date.AddDays(-1)) == 0 && form.bc != "晚班")
                        {
                            return Json(new sys_result()
                            {
                                code = 0,
                                msg = "日期为昨天时,班次应为晚班"
                            });
                        }
                        form.lrr = _userservice.CurrentUser().name;
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
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "日期应为当天或昨天晚班"
                        });
                    }
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
                        sybc = "夜班";
                        ssbc = "中班";
                        sybc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        ssbc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case "中班":
                        sybc = "白班";
                        ssbc = "夜班";
                        sybc_rq = dt.ToString("yyyy-MM-dd");
                        ssbc_rq = dt.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case "夜班":
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
                }); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}