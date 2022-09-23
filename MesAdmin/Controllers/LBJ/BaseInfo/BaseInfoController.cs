using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
namespace MesAdmin.Controllers.LBJ.BaseInfo
{
    [RoutePrefix("api/lbj/baseinfo")]
    public class BaseInfoController : ApiController
    {
        private IBaseInfo _baseinfo;
        public BaseInfoController(IBaseInfo baseinfo)
        {
            _baseinfo = baseinfo;
        }
        [HttpGet, Route("gcxx")]
        public IHttpActionResult GCXX()
        {
            try
            {
                var list = _baseinfo.GetGCXX().Where(t=>t.gcdm =="9902").Select(t => new { label = t.gcmc, value = t.gcdm });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("sbxx")]
        public IHttpActionResult Get_SBXX()
        {
            try
            {
                var list = _baseinfo.Get_SBXX_List().Select(t => new { label = t.sbmc, value = t.sbbh }).OrderBy(t => t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("scx")]
        public IHttpActionResult ScxXX(string gcdm)
        {
            try
            {
                var list = _baseinfo.GetScxXX(gcdm).Select(t => new { label = t.scxmc, value = t.scx });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("gwh")]
        public IHttpActionResult GwhByScx(string scx)
        {
            try
            {
                var list = _baseinfo.GetGwXX(scx).Select(t => new { label = t.gwmc, value = t.gwh,disabled = t.disabled }).OrderBy(t=>t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("scx_sbxx")]
        public IHttpActionResult GetSBxxByScx(string scx)
        {
            try
            {
                var list = _baseinfo.Get_SBXX_List().Where(t => t.scx == scx).Select(t => new { label = t.sbmc, value = t.sbbh }).OrderBy(t => t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("scx_gwh")]
        public IHttpActionResult GetGwhByScx(string scx)
        {
            List<sys_column_options> gwhlist = new List<sys_column_options>();
            var list = _baseinfo.GetGwListByScx(scx).OrderBy(t=>t.gwh);
            foreach (var item in list)
            {
                var q = gwhlist.Where(t => item.gwh == t.value);
                if (q.Count() == 0)
                {
                    gwhlist.Add(new sys_column_options { value = item.gwh, label = item.gwmc });
                }
            }
            return Json(new { code = 1, msg = "ok", list = gwhlist });
        }

        [HttpGet,Route("scx_ryxx")]
        public IHttpActionResult GetRyxxByScx(string scx)
        {
            try
            {
                List<sys_column_options> rylist = new List<sys_column_options>();
                var q = _baseinfo.RyxxList().Where(t => t.scx == scx);
                foreach (var item in q)
                {
                    rylist.Add(new sys_column_options() { label= item.username,value=item.usercode});
                }
                return Json(new { code = 1, msg = "ok", list = rylist });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("gwzd")]
        public IHttpActionResult Gwzd()
        {
            try
            {
                List<sys_column_options> gwhlist = new List<sys_column_options>(); 
                var list = _baseinfo.GetGwZd().OrderBy(t=>t.gwh);
                foreach (var item in list)
                {
                  var q = gwhlist.Where(t => item.gwh == t.value);
                    if(q.Count() == 0)
                    {
                        gwhlist.Add(new sys_column_options { value = item.gwh, label = item.gwmc });
                    }
                }
                return Json(new { code = 1, msg = "ok", list = gwhlist });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("usercode")]
        public IHttpActionResult GetUserCode(string key)
        {
            try
            {
                var list = _baseinfo.GetUserCode(key).Select(t => new { label = t.username+"["+t.usercode+"]",value = t.usercode }).OrderBy(t => t.value);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 刀柄基础信息
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("dbxx")]
        public IHttpActionResult GetDbInfo()
        {
            try
            {
                var list = _baseinfo.GetDbInfo().OrderBy(t=>t.dblx);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("dbxxbykey")]
        public IHttpActionResult DbInfo_By_Key(string key)
        {
            try
            {
                var list = _baseinfo.GetDbInfo_By_Key(key).OrderBy(t => t.dbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("unuse_dbxx")]
        public IHttpActionResult UnUse_DbInfo()
        {
            try
            {
                var list = _baseinfo.Get_UnUse_DbInfo().OrderBy(t=>t.dblx);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("unuse_dbrj_tree")]
        public IHttpActionResult UnUse_DbRj_Tree()
        {
            try
            {
                var list = _baseinfo.UnUse_DbRj_Tree().OrderBy(t => t.dblx);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("unuse_db_list")]
        public IHttpActionResult UnUse_Db_List()
        {
            try
            {
                var list = _baseinfo.Get_UnUseDbList().OrderBy(t => t.dblx);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("rjlx")]
        public IHttpActionResult GetRjLxList()
        {
            try
            {
                var list = _baseinfo.GetRjInfo();
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("rjlxbykey")]
        public IHttpActionResult GetRjLxByKey(string key)
        {
            try
            {
                var list = _baseinfo.GetRjInfoByKey(key);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("unuse_rjlx")]
        public IHttpActionResult UnUse_RjLx(List<string> dbh)
        {
            try
            {
                var list = _baseinfo.Get_UnUse_RjInfo(dbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("cnc_list_by_scx")]
        public IHttpActionResult Get_CNC_List(string scx)
        {
            try
            {
                var list = _baseinfo.Get_SBXX_List().Where(t=>t.scx == scx && t.sblx == "CNC").OrderBy(t=>t.sbbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("wlbm_by_key")]
        public IHttpActionResult Get_WLBM_By_Key(string key)
        {
            try
            {
                var list = _baseinfo.WLBM_By_Key(key).Select(t => new { label = t.wlmc, value = t.wlbm });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("userlist")]
        public IHttpActionResult Get_User_List()
        {
            try
            {
                var list = _baseinfo.UserList().Select(t => new { label = t.name, value = t.id });
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}