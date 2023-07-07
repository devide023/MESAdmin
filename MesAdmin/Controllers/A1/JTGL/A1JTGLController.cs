using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace MesAdmin.Controllers.A1.JTGL
{
    /// <summary>
    /// PDM通知分配到zxjc_t_jstc列表
    /// </summary>
    [RoutePrefix("api/a1/jtgl")]
    public class A1JTGLController : BaseApiController<zxjc_t_jstc>
    {
        private IJTFPSCX _fpscxservice;
        private IA1MyDoc _pdmfpedservice;
        private IA1JtFpzt _jtfpztservice;
        public A1JTGLController(IDbOperate<zxjc_t_jstc> jstzservice, IRequireVerify requireverfify, IImportData<zxjc_t_jstc> importservice, IJTFPSCX fpscxservice, IA1MyDoc pdmfpedservice , IA1JtFpzt jtfpztservice) :base(jstzservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _fpscxservice = fpscxservice;
            _pdmfpedservice = pdmfpedservice;
            _jtfpztservice = jtfpztservice;
        }
        /// <summary>
        /// 设置技通已分配到岗位
        /// </summary>
        /// <param name="jcbh"></param>
        /// <returns></returns>
        [HttpGet, Route("set_jstz_yfpgwh")]
        public IHttpActionResult Set_Jstz_YfpGwh(string jcbh)
        {
            try
            {
                var ret = _jtfpztservice.Set_JtFpYfpGwh(jcbh);
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "设置成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "设置失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 设置技通已分配状态(技通管理人员功能)
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("set_jstz_fp")]
        public IHttpActionResult Set_Jstz_Fpzt(string jcbh)
        {
            try
            {
               var ret =  _jtfpztservice.Set_JtFpZt(jcbh);
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "设置成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "设置失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IHttpActionResult Add(List<zxjc_t_jstc> entitys)
        {
            List<zxjc_t_jstc> postdata = new List<zxjc_t_jstc>();
            foreach (var item in entitys)
            {
                foreach (var scx in item.scxs)
                {
                    postdata.Add(new zxjc_t_jstc()
                    {
                        gcdm="9100",
                        scx=scx,
                        jtid = Guid.NewGuid().ToString(),
                        jcbh = item.jcbh,
                        jcmc = item.jcmc,
                        jcms = item.jcms,
                        wjlj = item.wjlj,
                        jwdx = item.jwdx,
                        scry = item.scry,
                        scsj = item.scsj,
                        yxqx1 = item.yxqx1,
                        yxqx2 = item.yxqx2,
                        fpflg = "Y",
                        fpsj = item.fpsj,
                        fpr = item.fpr,
                        wjfl = item.wjfl,
                        jtly = 0,
                        lrr = item.lrr,
                        lrsj = item.lrsj
                    });
                }
            }
            return base.Add(postdata);
        }

        [HttpPost, SearchFilter, Route("pdmfped_list")]
        public IHttpActionResult PDM_FPED_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _pdmfpedservice.Get_PDMFP_List(parm, out resultcount);
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
        /// 生产线分配记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, SearchFilter, Route("scxfpjllist")]
        public IHttpActionResult JsFzToScxJlList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _baseservice.GetList(parm, out resultcount);
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
        /// PDM通知列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost,SearchFilter,Route("toscxlist")]
        public IHttpActionResult JstzToScxList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _fpscxservice.Get_PDM_JSTZ_List(parm, out resultcount);
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
        public override IHttpActionResult Del(List<zxjc_t_jstc> entitys)
        {
            try
            {
                List<zxjc_t_jstc> postdata = new List<zxjc_t_jstc>();
                List<zxjc_t_jstc> nolist = new List<zxjc_t_jstc>();
                foreach (var item in entitys)
                {
                   if(_fpscxservice.CanRemove(item.jcbh))
                    {
                        postdata.Add(item);
                    }
                    else
                    {
                        nolist.Add(item);
                    }
                }
                if(nolist.Count > 0)
                {
                    string errormsg = string.Empty;
                    nolist.ForEach(t => errormsg = errormsg + t.jcbh+",");
                    return Json(new
                    {
                        code = 0,
                        msg = errormsg+"已分配到岗位不能被删除！",
                    });
                }
                return base.Del(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}