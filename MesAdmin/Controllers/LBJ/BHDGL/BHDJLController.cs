using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.BHDGL;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.BHDGL
{
    [RoutePrefix("api/lbj/bhdjl")]
    public class BHDJLController : ApiController
    {
        private IDbOperate<lbj_qms_4mbhd> _bhdjlservice;
        private IDeal4MBHD _4mbhddealservice;
        public BHDJLController(IDbOperate<lbj_qms_4mbhd> bhdjlservice,IDeal4MBHD bhddealservice)
        {
            _bhdjlservice = bhdjlservice;
            _4mbhddealservice = bhddealservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _bhdjlservice.GetList(parm, out resultcount);
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
        /// 操作员变化点待处理列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, SearchFilter, Route("czy_dealbhd_list")]
        public IHttpActionResult Get_Czy_DealBhd_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _4mbhddealservice.Get_Czy_BHD_List(parm, out resultcount);
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
        [HttpPost, Route("save_czy_dealbhd")]
        public IHttpActionResult Save_Czy_DealBhd(lbj_qms_4mbhd entity)
        {
            try
            {
                var ret = _4mbhddealservice.Save_Czy_BHD_Deal(entity);
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new
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
        /// 生产班组变化点待处理列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, SearchFilter, Route("scbz_dealbhd_list")]
        public IHttpActionResult Get_Scbz_DealBhd_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _4mbhddealservice.Get_Scbz_BHD_List(parm, out resultcount);
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
        [HttpPost, Route("save_scbz_dealbhd")]
        public IHttpActionResult Save_Scbz_DealBhd(lbj_qms_4mbhd entity)
        {
            try
            {
                var ret = _4mbhddealservice.Save_Scbz_BHD_Deal(entity);
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new
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
        /// 现场巡检变化点待处理列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, SearchFilter, Route("xcxj_dealbhd_list")]
        public IHttpActionResult Get_Xcxj_DealBhd_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _4mbhddealservice.Get_Xcxj_BHD_List(parm, out resultcount);
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

        [HttpPost, Route("save_xcxj_dealbhd")]
        public IHttpActionResult Save_Xcxj_DealBhd(lbj_qms_4mbhd entity)
        {
            try
            {
                var ret = _4mbhddealservice.Save_Xcxj_BHD_Deal(entity);
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new
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