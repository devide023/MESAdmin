using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.CDGC;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.JJBGL
{
    /// <summary>
    /// 电机壳交接班
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/djkjjb")]
    public class DJKJJBController : BaseApiController<zxjc_djkjjb_bill>
    {
        //基础服务
        private IDbOperate<zxjc_djkjjb_bill> _djk_base_service;
        //扩展服务
        private IDjkjjb _djkservice;
        public DJKJJBController(IDbOperate<zxjc_djkjjb_bill> djk_base_service, IDjkjjb djkservice) : base(djk_base_service)
        {
            _djk_base_service = djk_base_service;
            _djkservice = djkservice;
        }

        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _djk_base_service.GetList(parm, out resultcount);
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
        [HttpGet,Route("load_bc_data")]
        public IHttpActionResult Load_BC_Data(string rq,string bc)
        {
            try
            {
                var bill = _djkservice.Get_Djkjjb_Bill_ByBc(rq, bc);
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

        [HttpPost,Route("save_jjb")]

        public IHttpActionResult Save_Djkjjb(zxjc_djkjjb_bill form)
        {
            try
            {
                if (form.isadmin)
                {
                    zxjc_djkjjb_bill bill = new zxjc_djkjjb_bill();
                    bill = form.Copy();
                    bill.djkjjbdetail = null;
                    bill.djkjjbdetailhx = null;
                    List<zxjc_djkjjb_detail> jjmx = new List<zxjc_djkjjb_detail>();
                    List<zxjc_djkjjb_hx_detail> hxmx = new List<zxjc_djkjjb_hx_detail>();
                    jjmx = form.djkjjbdetail;
                    hxmx = form.djkjjbdetailhx;
                    var ret = _djkservice.Save_Djkjjb(bill, jjmx, hxmx);
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
                        zxjc_djkjjb_bill bill = new zxjc_djkjjb_bill();
                        bill = form.Copy();
                        bill.djkjjbdetail = null;
                        bill.djkjjbdetailhx = null;
                        List<zxjc_djkjjb_detail> jjmx = new List<zxjc_djkjjb_detail>();
                        List<zxjc_djkjjb_hx_detail> hxmx = new List<zxjc_djkjjb_hx_detail>();
                        jjmx = form.djkjjbdetail;
                        hxmx = form.djkjjbdetailhx;
                        var ret = _djkservice.Save_Djkjjb(bill, jjmx, hxmx);
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
    }
}