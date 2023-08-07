using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.ZLGL
{
    [RoutePrefix("api/lbj/checkbill")]
    public class LBJCheckBillController : BaseApiController<zxjc_check_bill>
    {
        private ILBJProductCheck _checkbillservice;
        private ILBJCheckBill _auditbillservice;
        public LBJCheckBillController(IDbOperate<zxjc_check_bill> baseservice, ILBJCheckBill auditbillservice, ILBJProductCheck checkbillservice) : base(baseservice)
        {
            _checkbillservice = checkbillservice;
            _auditbillservice = auditbillservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname= "rq desc,lrsj ";
            return base.GetList(parm);
        }
        [HttpPost, SearchFilter,Route("hjlist")]
        public virtual IHttpActionResult Get_HJList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _auditbillservice.Hj_Check_Bill_List(parm, out resultcount);
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

        [HttpPost, SearchFilter, Route("xjlist")]
        public virtual IHttpActionResult Get_XJList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _auditbillservice.Xj_Check_Bill_List(parm, out resultcount);
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

        [HttpPost,Route("xjaudit")]
        public IHttpActionResult XJAudit(List<zxjc_check_bill> entitys)
        {
            try
            {
                var ret = _auditbillservice.Xj_Audit(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "审核成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "审核失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("hjaudit")]
        public IHttpActionResult HJAudit(List<zxjc_check_bill> entitys)
        {
            try
            {
                var ret = _auditbillservice.Hj_Audit(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "审核成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "审核失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet,Route("get_bill_by_id")]
        public IHttpActionResult Get_BillInfoById(string billid)
        {
            try
            {
                var bill = _checkbillservice.Get_BillInfo_ById(billid);
                return Json(new { code = 1, msg = "ok", bill = bill });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("get_cpxh_list")]
        public IHttpActionResult Get_CpxhList()
        {
            try
            {
                var list = _checkbillservice.GetCpxhList();
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { value = t, label = t }) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("get_cpxh_by_key")]
        public IHttpActionResult Get_CpxhByKey(string key)
        {
            try
            {
                var list = _checkbillservice.GetCpxhByKey(key);
                return Json(new { code = 1, msg = "ok", list = list.Select(t => new { value=t,label=t}) });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("save")]
        public IHttpActionResult Save_CheckBill_Data(sys_checkbill_form entity)
        {
            try
            {
                var q = entity.BillDetails.Where(t => string.IsNullOrEmpty(t.checkval));
                if (q.Count() > 0)
                {
                    return Json(new { code = 0, msg = "有检测项未输入值" });
                }
                var ret = _checkbillservice.Save_CheckBill_Data(entity);
                if (ret)
                {
                    return Json(new { code = 1, msg = "数据保存成功" });
                }
                else
                {
                    return Json(new { code = 0, msg = "数据保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("modify")]
        public IHttpActionResult Edit_CheckBill_Data(sys_checkbill_form entity)
        {
            try
            {
                var q = entity.BillDetails.Where(t => string.IsNullOrEmpty(t.checkval));
                if (q.Count() > 0)
                {
                    return Json(new { code = 0, msg = "有检测项未输入值" });
                }
                List<zxjc_check_bill> postdata = new List<zxjc_check_bill>();
                postdata.Add(new zxjc_check_bill()
                {
                    bc= entity.bc,
                    bmmc= entity.bmmc,
                    bz= entity.bz,
                    cpmc= entity.cpmc,
                    cpxh= entity.cpxh,
                    gxmc= entity.gxmc,
                    id= entity.id,
                    jcjg= entity.jcjg,
                    jjh= entity.jjh,
                    khmc= entity.khmc, 
                    rq = entity.rq,
                    scx= entity.scx,
                    vin= entity.vin, 
                    smjbs= entity.smjbs,
                    BillDetails = entity.BillDetails
                });
                //保存修改历史
                _checkbillservice.Save_CheckDetail_His(entity.id.ToString());
                //修改数据
                return base.Edit(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}