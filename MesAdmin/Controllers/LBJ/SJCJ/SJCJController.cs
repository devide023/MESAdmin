using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.SJCJ
{
    [RoutePrefix("api/lbj/sjcj")]
    public class SJCJController : ApiController
    {
        private IDbOperate<zxjc_sbxx_ls_cnc> _cnc;
        private IDbOperate<zxjc_sbxx_ls_qx> _qx;
        private IDbOperate<zxjc_sbxx_ls_hg> _hg;
        private IDbOperate<zxjc_sbxx_ls_gj> _gj;
        private IDbOperate<zxjc_sbxx_ls_spc> _spc;
        public SJCJController(IDbOperate<zxjc_sbxx_ls_cnc> cnc, IDbOperate<zxjc_sbxx_ls_qx> qx, IDbOperate<zxjc_sbxx_ls_hg> hg, IDbOperate<zxjc_sbxx_ls_gj> gj, IDbOperate<zxjc_sbxx_ls_spc> spc)
        {
            _cnc = cnc;
            _qx = qx;
            _hg = hg;
            _gj = gj;
            _spc = spc;
        }
        [HttpPost, SearchFilter, Route("cnc")]
        public IHttpActionResult Get_Cnc_Data_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _cnc.GetList(parm, out resultcount);
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
        [HttpPost, SearchFilter, Route("qx")]
        public IHttpActionResult Get_Qx_Data_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _qx.GetList(parm, out resultcount);
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
        [HttpPost, SearchFilter, Route("hg")]
        public IHttpActionResult Get_Hg_Data_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _hg.GetList(parm, out resultcount);
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
        [HttpPost, SearchFilter, Route("gj")]
        public IHttpActionResult Get_Gj_Data_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gj.GetList(parm, out resultcount);
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
        [HttpPost, SearchFilter, Route("spc")]
        public IHttpActionResult Get_Spc_Data_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _spc.GetList(parm, out resultcount);
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
    }
}