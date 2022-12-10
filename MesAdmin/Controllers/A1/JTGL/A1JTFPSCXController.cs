using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtfpscx")]
    public class A1JTFPSCXController : BaseApiController<zxjc_t_jstc_scx>
    {
        private IJTFPSCX _jtfpscx;
        private IDbOperate<zxjc_t_jstc_scx> _jtfpscxservice;
        public A1JTFPSCXController(IDbOperate<zxjc_t_jstc_scx> jtfpscxservice, IJTFPSCX jtfpscx) :base(jtfpscxservice)
        {
            _jtfpscxservice = jtfpscxservice;
            _jtfpscx = jtfpscx;
        }
        [HttpGet,Route("read_pdm_jstz")]
        public IHttpActionResult ReadPDMTZ(string jcbh)
        {
            try
            {
               var isok = _jtfpscx.Read_PDM_JSTZ(jcbh);
                if (isok)
                {
                    return Json(new { code = 1, msg = $"{jcbh}打开成功" });
                }
                else
                {
                    return Json(new { code = 0, msg = "打开失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IHttpActionResult Add(List<zxjc_t_jstc_scx> entitys)
        {
            return base.Add(entitys);
        }

        public override IHttpActionResult Del(List<zxjc_t_jstc_scx> entitys)
        {
            List<zxjc_t_jstc_scx> candellist = new List<zxjc_t_jstc_scx>();
            List<zxjc_t_jstc_scx> nolist = new List<zxjc_t_jstc_scx>();
            foreach (var item in entitys)
            {
                if (_jtfpscx.CanRemove(item.jcbh))
                {
                    candellist.Add(item);
                }
                else
                {
                    nolist.Add(item);
                }
            }
            if (nolist.Count > 0)
            {
                string msg = string.Empty;
                nolist.ForEach(t => msg = msg + t.jcbh + "、");
                return Json(new { code = 0, msg = $"技通编号：{msg}已分配到岗位,需撤回后才能删除" });
            }
            return base.Del(candellist);
        }
    }
}