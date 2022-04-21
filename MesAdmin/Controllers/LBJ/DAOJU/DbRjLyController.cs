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
using ZDMesInterfaces.LBJ.DaoJu;

namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/dbrjly")]
    public class DbRjLyController : ApiController
    {
        private IDbOperate<base_dbrjzx> _dbrjzxservice;
        private IDaoJu _gxservice;
        public DbRjLyController(IDbOperate<base_dbrjzx> dbrjzxservice, IDaoJu gxservice)
        {
            _dbrjzxservice = dbrjzxservice;
            _gxservice = gxservice;
        }
        /// <summary>
        /// 刀柄刃具在用
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        [HttpGet, Route("dbrjzx_list")]
        public IHttpActionResult Get_DbrjZx(string dbh)
        {
            try
            {
                var list = _gxservice.DbRjZxList(dbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 根据刀柄号查询关联的刃具信息(排除已领用的刃具)
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        [HttpGet,Route("dbrjgx_list")]
        public IHttpActionResult GetDbRjList(string dbh)
        {
            try
            {
                var list = _gxservice.DbRjGxList(dbh);
                return Json(new { code = 1, msg = "ok", list = list });
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
                var list = _dbrjzxservice.GetList(parm, out resultcount);
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
        /// <summary>
        /// 首次领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost,Route("scly")]
        public IHttpActionResult First_Use(dbrjly_form form)
        {
            try
            {
               var ret = _dbrjzxservice.Add(form.dbrjzx);
                if (ret>0)
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
        /// <summary>
        /// 换刀领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        public IHttpActionResult Add(dbrjly_form form)
        {
            try
            {
                var ret = _gxservice.DbRjLy(form);
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

        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<base_dbrjzx> entitys)
        {
            try
            {
                var ret = _dbrjzxservice.Modify(entitys);
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
        public IHttpActionResult Del(List<base_dbrjzx> entitys)
        {
            try
            {
                var ret = _dbrjzxservice.Del(entitys);
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
        [HttpGet, Route("sbbh_by_scx")]
        public IHttpActionResult Get_CNCList_By_Scx(string scx)
        {
            try
            {
                var list = _gxservice.Get_CnC_By_Scx(scx).Select(t => new { label = t.sbmc, value = t.sbbh });
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
        /// 设备编号获取刀柄信息
        /// </summary>
        /// <param name="sbbh"></param>
        /// <returns></returns>
        [HttpGet,Route("db_by_sbbh")]
        public IHttpActionResult Get_Dbxx_By_Sbbh(string sbbh)
        {
            try
            {
                var list = _gxservice.GetDbxxBySbbh(sbbh).Select(t=>new {label=t.dblx+"("+t.dbmc+")",value=t.dbh}).Distinct();
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
        [HttpPost,Route("zxrjrm")]
        public IHttpActionResult SetZxRjRm(List<int> id)
        {
            try
            {
                var ret = _gxservice.SetRjSm(id);
                return Json(new
                {
                    code = 1,
                    msg = "数据操作成功"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}