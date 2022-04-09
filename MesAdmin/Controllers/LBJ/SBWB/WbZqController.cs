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
using ZDMesInterfaces.LBJ.SBWB;

namespace MesAdmin.Controllers.LBJ.SBWB
{
    [RoutePrefix("api/lbj/wbzq")]
    public class WbZqController : ApiController
    {
        private IDbOperate<base_sbwb_ls> _wbzqservice;
        private ISbWbZq _sbwbzq;
        private IUser _user;
        public WbZqController(IDbOperate<base_sbwb_ls> wbzqservice, ISbWbZq sbwbzq,IUser user)
        {
            _wbzqservice = wbzqservice;
            _sbwbzq = sbwbzq;
            _user = user;
        }
        [HttpGet,Route("wbzq_list")]
        public IHttpActionResult GetWbzqList()
        {
            try
            {
               var list = _sbwbzq.WbZqList();
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
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (!string.IsNullOrEmpty(parm.orderbyexp))
                {
                    parm.orderbyexp = parm.orderbyexp + ",wbjhsj desc,gcdm asc,scx asc,wbsh asc";
                }
                else
                {
                    parm.orderbyexp = " order by wbjhsj desc,gcdm asc,scx asc,wbsh asc";
                }
                var list = _wbzqservice.GetList(parm, out resultcount);
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
        [HttpPost, Route("add")]
        public IHttpActionResult Add(sys_wbzq_form form)
        {
            try
            {
                List<base_sbwb_ls> savedata = new List<base_sbwb_ls>();
                string token = ZDToolHelper.TokenHelper.GetToken;
                var userinfo = _user.GetUserByToken(token);
                int xsh = 1;
                string gwh = string.Empty, scx = string.Empty;
                foreach (var item in form.sbwbls)
                {
                    item.autoid = Guid.NewGuid().ToString();
                    item.wbjhsj = form.next_date;
                    item.lrr = userinfo.name;
                    item.lrsj = DateTime.Now;
                    item.wbzt = "计划中";
                    item.wbwcr = "";
                    item.wbwcsj = null;
                    gwh = item.gwh;
                    scx = item.scx;
                    if(gwh != item.gwh && scx != item.scx)
                    {
                        xsh = 1;
                    }
                    item.wbsh = xsh;
                    savedata.Add(item);
                    xsh++;
                }
                int ret = _wbzqservice.Add(savedata);
                if (ret > 0)
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

        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<base_sbwb_ls> entitys)
        {
            try
            {
                var ret = _wbzqservice.Modify(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<base_sbwb_ls> entitys)
        {
            try
            {
                var ret = _wbzqservice.Del(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据删除成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据删除失败"
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