using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesInterfaces.LBJ.OEE;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.OEE
{
    [RoutePrefix("api/lbj/scxoee")]
    public class ScxOEEController : BaseApiController<zxjc_scx_oee>
    {
        private ILBJOEE _lbjoee;
        private IBaseInfo _baseinfo;
        private IDbOperate<zxjc_scx_oee> _bases;
        public ScxOEEController(IDbOperate<zxjc_scx_oee> baseservice,ILBJOEE lbjoee,IBaseInfo baseinfo) : base(baseservice)
        {
            _bases = baseservice;
            _lbjoee = lbjoee;
            _baseinfo = baseinfo;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "rq";
            return base.GetList(parm);
        }
        /// <summary>
        /// 生产线OEE模板数据
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        [HttpGet,Route("oeebyscx")]
        public IHttpActionResult Get_OEETemplate(string scx)
        {
            try
            {
                var oeedata = _lbjoee.Get_OEEDataByScx(scx);
                var scxzx = _baseinfo.Get_ScxXX_JJ(scx).Select(t=>new { label=t.scxzxmc,value=t.scxzx});
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    oee = oeedata,
                    scxzx= scxzx
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("oeebyrq")]
        public IHttpActionResult Get_HgsByRq(dynamic obj)
        {
            try
            {
                if (obj.scx != null && obj.rq != null)
                {
                    var proinfo = _lbjoee.Get_ProInfo(obj.scx.ToString(), Convert.ToDateTime(obj.rq));
                    return Json(new
                    {
                        code = 1,
                        msg = "ok",
                        proinfo
                    });
                }
                else
                {
                    return Json(new { code = 0, msg = "查询参数错误" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [RequireVerify]
        public override IHttpActionResult Add(List<zxjc_scx_oee> entitys)
        {
            try
            {
                IEnumerable<zxjc_scx_oee> repeatlist = new List<zxjc_scx_oee>();
                var cnt = _bases.Add(entitys, out repeatlist);
                if (cnt > 0)
                {
                    if (repeatlist.Count() > 0)
                    {
                        return Json(new
                        {
                            code = 1,
                            msg = $"有{repeatlist.Count()}条数据重复跳过,其它数据保存成功！"
                        });
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = "数据保存成功"
                        });
                    }
                }
                else
                {
                    if (repeatlist.Count() > 0)
                    {
                        return Json(new
                        {
                            code = 0,
                            msg = $"有{repeatlist.Count()}条数据重复"
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
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}