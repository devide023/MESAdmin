using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.CDGC.GTJC
{
    /// <summary>
    /// 缸体检测
    /// </summary>
    [RoutePrefix("api/cdgc/gtjc/checkdata")]
    public class GTJCDataController : ApiController
    {
        private IGtjc_Result _gtjc_jgservice;
        private IDbOperate<zxjc_gtjc_bill> _gtjcbaseservice;
        private IUser _userservice;
        public GTJCDataController(IDbOperate<zxjc_gtjc_bill> gtjcbaseservice,IGtjc_Result gtjc_jgservice,IUser userservice)
        {
            _gtjcbaseservice = gtjcbaseservice;
            _gtjc_jgservice = gtjc_jgservice;
            _userservice = userservice; 
        }
        [HttpPost,Route("deal_gtjc")]
        public IHttpActionResult Deal_Gtjc_Bill(zxjc_gtjc_bill entity)
        {
            try
            {
                _gtjcbaseservice.Modify(entity);
                return Json(new
                {
                    code = 1,
                    msg = "数据保存成功"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("get_check_data")]
        public IHttpActionResult Get_CheckData_By_Vin(string rq,string ewm,string cplx)
        {
            try
            {
                var list = _gtjc_jgservice.Get_CheckData_Vin(rq,ewm,cplx);
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
        [HttpGet, Route("create_bill")]
        public IHttpActionResult Create_Bill(string rq,string cplx)
        {
            try
            {
                DateTime jcrq = DateTime.Now;
                if (string.IsNullOrEmpty(rq))
                {
                    jcrq = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    jcrq = Convert.ToDateTime(Convert.ToDateTime(rq).ToString("yyyy-MM-dd"));
                }
               var ids = _gtjc_jgservice.Create_Bill_Ids(cplx, jcrq);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    ids = ids
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("save_bill")]

        public IHttpActionResult Save_Check_Data(List<zxjc_gtjc_bill> bills)
        {
            try
            {
                List<zxjc_gtjc_bill> error = new List<zxjc_gtjc_bill>();
                foreach (var item in bills)
                {
                    foreach (var sitem in item.zxjcgtjcdetail)
                    {
                        if ((string.IsNullOrEmpty(sitem.kjval) && (sitem.kjtype=="radio" || sitem.kjtype =="text"))
                            ||
                            (string.IsNullOrEmpty(sitem.sdmjval) && sitem.sdtype == "text")
                            )
                        {
                            error.Add(item);
                            break;
                        }
                    }
                }
                if (error.Count() > 0)
                {
                    string tempmsg = string.Empty;
                    string msg = string.Empty;
                    error.Select(t => t.jth).ToList().ForEach(t => tempmsg = tempmsg + t + ",");
                    msg = $"机台号：{tempmsg}孔径尺寸或深度面距尺寸有空项未填写 ";
                    return Json(new
                    {
                        code = 0,
                        msg = msg,
                    });
                }
                else
                {
                    foreach (var bill in bills)
                    {
                        bill.lrr = _userservice.CurrentUser().name;
                        _gtjc_jgservice.Save_Gtjc_CheckData(bill);
                    }
                    return Json(new
                    {
                        code = 1,
                        msg = "ok",
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}