﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.LBJ.SBWB;
using Dapper;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.LBJ.SBWB
{
    [RoutePrefix("api/lbj/wbzq")]
    public class WbZqController : ApiController
    {
        private IDbOperate<base_sbwb_ls> _wbzqservice;
        private IDbOperate<base_sbwb> _wbxxservice;
        private ISbWbZq _sbwbzq;
        private IUser _user;
        private IBaseInfo _baseinfo;
        public WbZqController(IDbOperate<base_sbwb_ls> wbzqservice, IDbOperate<base_sbwb> wbxxservice, ISbWbZq sbwbzq,IUser user, IBaseInfo baseinfo)
        {
            _wbzqservice = wbzqservice;
            _wbxxservice = wbxxservice;
            _sbwbzq = sbwbzq;
            _user = user;
            _baseinfo = baseinfo;
        }
        [HttpGet,Route("scxs_zxs_list")]
        public IHttpActionResult Get_Scxs_Zxs_List()
        {
            try
            {
                var list = _baseinfo.Get_ALL_ScxXX_JJ().OrderBy(t => t.scx).ThenBy(t => t.scxzx).Select(t => new option_list() { label = t.scxmc + "-" + t.scxzxmc, value = t.scx + "-" + t.scxzx });
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

        [HttpPost,Route("get_wbitems")]
        public IHttpActionResult GetWbItems(base_sbwb parm)
        {
            try
            {
                var scxlist = _baseinfo.GetScxXX("9902");
                var sbxxlist = _baseinfo.Get_SBXX_List();
                var scxslist = _baseinfo.Get_ALL_ScxXX_JJ();
                var list = _sbwbzq.ScxZxWbXxList(parm).OrderBy(t => t.scx).ThenBy(t=>t.scxzx).ThenBy(t => t.wbsh);
                foreach (var item in list)
                {
                    item.scxoptions = scxlist.Where(t => t.scx == item.scx).Select(t => new sys_column_options() { label = t.scxmc, value = t.scx }).ToList();
                    item.scxzxs = scxslist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.scxzxmc, value = t.scxzx }).ToList();
                    item.sbxxoptions = sbxxlist.Where(t => t.scx == item.scx).Select(t => new sys_column_options() { label = t.sbmc, value = t.sbbh }).ToList();
                }
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
        [HttpPost,SearchFilter, Route("wbjhlist")]
        public IHttpActionResult GetWbJHList(sys_page parm)
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
                var zxlist = _baseinfo.Get_ALL_ScxXX_JJ();
                var list = _sbwbzq.Get_WbJh_List(parm, out resultcount);
                foreach (var item in list)
                {
                    item.scxzxs = zxlist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.scxzxmc, value = t.scxzx }).ToList();
                }
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
        [HttpPost,Route("wbzq_list")]
        public IHttpActionResult GetWbzqList(base_sbwb parm)
        {
            try
            {
               var list = _sbwbzq.WbXxList(parm).OrderBy(t=>t.scx).ThenBy(t=>t.wbsh);
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
        /// <summary>
        /// 维保数据过滤
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost,Route("query")]
        public IHttpActionResult QuerySbWb(sys_wbzq_gl_form obj)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                List<string> explist = new List<string>();
                string expstr = string.Empty;
                sys_page parm = new sys_page();
                p.Add(":pageindex", 1, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                p.Add(":pagesize", int.MaxValue, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                if (obj.scx != null && !String.IsNullOrEmpty(obj.scx.ToString()))
                {
                    explist.Add(" scx = :scx");
                    p.Add(":scx", obj.scx.ToString(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    
                }
                if (obj.sbbh.Count>0)
                {
                    explist.Add(" sbbh in :sbbh");
                    p.Add(":sbbh", obj.sbbh.ToArray());
                }
                for (int i = 0; i < explist.Count; i++)
                {
                    var item = explist[i];
                    if(i == explist.Count - 1)
                    {
                        expstr = expstr + item;
                    }
                    else
                    {
                        expstr = expstr + item + " and ";
                    }
                    
                }
                parm.sqlexp = expstr;
                parm.sqlparam = p;
                int resultcount = 0;
                var list = _wbxxservice.GetList(parm, out resultcount);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list,
                    resultcount = resultcount,
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
                var zxlist = _baseinfo.Get_ALL_ScxXX_JJ();
                var list = _wbzqservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    item.scxzxs = zxlist.Where(t => t.scx == item.scx).Select(t=>new option_list() {label=t.scxzxmc,value=t.scxzx }).ToList();
                }
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
        [HttpPost, Route("addbyscxzx")]
        public IHttpActionResult Add_ScxZx(sys_wbzq_form form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.kssj) || string.IsNullOrEmpty(form.jssj))
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "请选择维保时间!"
                    });
                }
                var ksrq = Convert.ToDateTime(form.kssj);
                var jsrq = Convert.ToDateTime(form.jssj);
                if (DateTime.Compare(ksrq, jsrq) > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "结束时间应大于开始时间!"
                    });
                }
                if (form.sbwbls.Count == 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "请勾选维保项!"
                    });
                }

                List<base_sbwb_ls> savedata = new List<base_sbwb_ls>();
                int xsh = 1;
                string sbbh = string.Empty, scx = string.Empty;
                foreach (var item in form.sbwbls.OrderBy(t => t.scx).ThenBy(t=>t.scxzx))
                {
                    item.autoid = Guid.NewGuid().ToString();
                    item.wbjhsj = Convert.ToDateTime(form.kssj);
                    item.wbjhsjend = Convert.ToDateTime(form.jssj);
                    item.lrr = _user.CurrentUser().name;
                    item.lrsj = DateTime.Now;
                    item.wbzt = "计划中";
                    item.wbwcr = "";
                    item.wbwcsj = null;
                    if (sbbh != item.sbbh && scx != item.scx)
                    {
                        xsh = 1;
                        sbbh = item.sbbh;
                        scx = item.scx;
                    }
                    item.wbsh = xsh;
                    savedata.Add(item);
                    xsh++;
                }
                var ret = _sbwbzq.SaveSbWbInfo(savedata);
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
        [HttpPost, Route("add")]
        public IHttpActionResult Add(sys_wbzq_form form)
        {
            try
            {
                if (form.next_date == null)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "请选择维保时间!"
                    });
                }
                else
                {
                   var ksrq = Convert.ToDateTime(form.next_date[0]);
                    var jsrq = Convert.ToDateTime(form.next_date[1]);
                    if(DateTime.Compare(ksrq,jsrq)>0)
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "结束时间应大于开始时间!"
                        });
                    }
                }
                if (form.sbwbls.Count == 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "请勾选维保项!"
                    }) ;
                }

                List<base_sbwb_ls> savedata = new List<base_sbwb_ls>();
                string token = ZDToolHelper.TokenHelper.GetToken;
                var userinfo = _user.GetUserByToken(token);
                int xsh = 1;
                string sbbh = string.Empty, scx = string.Empty;
                foreach (var item in form.sbwbls.OrderBy(t=>t.scx).Where(t=>!string.IsNullOrEmpty(t.gwh)))
                {
                    item.autoid = Guid.NewGuid().ToString();
                    
                    item.wbjhsj = Convert.ToDateTime(form.next_date[0]);
                    item.wbjhsjend = Convert.ToDateTime(form.next_date[1]);
                    item.lrr = userinfo.name;
                    item.lrsj = DateTime.Now;
                    item.wbzt = "计划中";
                    item.wbwcr = "";
                    item.wbwcsj = null;
                    if(sbbh != item.sbbh && scx != item.scx)
                    {
                        xsh = 1;
                        sbbh = item.sbbh;
                        scx = item.scx;
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