using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/dbrjzxnew")]
    public class LbjDbRjZxNewController : ApiController
    {
        private IDBRJZX _dbrjzxnew;
        public LbjDbRjZxNewController(IDBRJZX dbrjzxnew)
        {
            _dbrjzxnew = dbrjzxnew;
        }

        [HttpPost,Route("dbrjzx")]
        public IHttpActionResult Get_DbRjzx(sys_dbrjgx_form form)
        {
            try
            {
                var list = _dbrjzxnew.Get_DbRjZx_List(form);
                return Json(new sys_search_result()
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
        [HttpPost,Route("save_dbrjzx_new")]
        public IHttpActionResult Save_Dbrjzx_New(sys_save_dbrjzx_form form)
        {
            try
            {
                if (form == null)
                {
                    return Json(new sys_search_result()
                    {
                        code = 0,
                        msg = "提交参数为空"
                    });
                }
                if (form.dbrjzx == null || form.dbrjzx.Count == 0)
                {
                    return Json(new sys_search_result()
                    {
                        code = 0,
                        msg = "提交参数为空"
                    });
                }
                var q1 = form.dbrjzx.Where(t => string.IsNullOrEmpty(t.dbh));
                var q2 = form.dbrjzx.Where(t => string.IsNullOrEmpty(t.rjlx));
                if(q1.Count()>0 || q2.Count() > 0)
                {
                    return Json(new sys_search_result()
                    {
                        code = 0,
                        msg = "刀柄编号或刃具类型有空值,请完善"
                    }) ;
                }
                var ret = _dbrjzxnew.Save_DbRjZx_New(form);
                if (ret)
                {
                    return Json(new sys_search_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_search_result()
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
    }
}