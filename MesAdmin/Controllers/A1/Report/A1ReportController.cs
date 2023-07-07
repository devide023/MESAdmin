using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.Report
{
    [RoutePrefix("api/a1/report")]
    public class A1ReportController : ApiController
    {
        private IA1Report _reportservice;
        public A1ReportController(IA1Report reportservice)
        {
            _reportservice = reportservice;
        }
        /// <summary>
        /// 总检在线检测记录表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost,Route("zj_zxjc_jl")]
        public IHttpActionResult Zj_ZxjcJl(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_GzTj(parm, out resultcount);
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
        /// <summary>
        /// 干水检对比试验
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, Route("gsjdbsy")]
        public IHttpActionResult GsjDbsj(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_GSJDBSY(parm, out resultcount);
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
        /// <summary>
        /// 返修记录表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost,Route("fxjlb")]
        public IHttpActionResult FxjlB(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_FXJLB(parm, out resultcount);
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
        /// <summary>
        /// 检测明细
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, Route("jcmx")]
        public IHttpActionResult Get_JCMX(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_JCMX(parm, out resultcount);
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
        /// <summary>
        /// 检测结果
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, Route("jcjg")]
        public IHttpActionResult Get_JCJG(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_JCJG(parm, out resultcount);
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

        [HttpPost,Route("mjghjl")]
        public IHttpActionResult Get_MJGHJL(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_MjGhjl(parm, out resultcount);
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

        [HttpPost, Route("jcdjjl")]
        public IHttpActionResult Get_DJJLB(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                if (parm.search_condition.Count == 0)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "请输入查询参数"
                    });
                }
                var list = _reportservice.Get_DJJLB(parm, out resultcount);
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
    }
}