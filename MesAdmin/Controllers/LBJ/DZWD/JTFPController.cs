using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.DZWD;
using ZDMesInterfaces.LBJ.JTGL;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.DZWD
{
    [RoutePrefix("api/lbj/jtfp")]
    public class JTFPController : ApiController
    {
        private IDbOperate<zxjc_t_jstcfp> _jtfpservice;
        private IAudit<zxjc_t_jstc> _auditservice;
        private IJsTc _jtservice;
        private IJTFP _jtfp;
        private IUser _user;
        public JTFPController(IDbOperate<zxjc_t_jstcfp> jtfpservice, IAudit<zxjc_t_jstc> auditservice, IJsTc jtservice, IJTFP jtfp,IUser user)
        {
            _jtfpservice = jtfpservice;
            _jtservice = jtservice;
            _auditservice = auditservice;
            _jtfp = jtfp;
            _user = user;
        }
        [HttpGet,Route("wfplist")]
        public IHttpActionResult GetWfpList()
        {
            try
            {
               var list = _jtfp.Get_WFP_List();
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
                var list = _jtfpservice.GetList(parm, out resultcount);
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
        [HttpPost,Route("fp")]
        public IHttpActionResult FPJT(sys_jtfp_form form)
        {
            try
            {
                List<zxjc_t_jstcfp> postdata = new List<zxjc_t_jstcfp>();
                string token = ZDToolHelper.TokenHelper.GetToken;
                mes_user_entity userinfo = _user.GetUserByToken(token);
                postdata.Add(new zxjc_t_jstcfp()
                {
                    jtid = form.jstc.jtid,
                    gcdm = form.gcdm,
                    scx = form.scx,
                    scxzx= form.scxzx,
                    bz = form.bz,
                    rylist = form.rylist,
                    lrr1 = userinfo.name,
                    lrsj1 = DateTime.Now
                });
                int ret = _jtfpservice.Add(postdata);
                if (ret > 0)
                {
                    List<string> ids = new List<string>();
                    ids.Add(form.jstc.jtid);
                    _auditservice.AuditBill(ids);
                    _jtservice.Update_FpFlag(ids);
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "分配成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "分配失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("qxfp")]
        public IHttpActionResult Del_JtFP(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ret = _jtfp.CancleFP(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "取消分配成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "取消分配失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("add")]
        public IHttpActionResult Add(List<zxjc_t_jstcfp> entitys)
        {
            try
            {
                int ret = _jtfpservice.Add(entitys);
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
        public IHttpActionResult Edit(List<zxjc_t_jstcfp> entitys)
        {
            try
            {
                var ret = _jtfpservice.Modify(entitys);
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
        public IHttpActionResult Del(List<zxjc_t_jstcfp> entitys)
        {
            try
            {
                var ret = _jtfpservice.Del(entitys);
                if (ret)
                {
                    var ids = entitys.Select(t => t.jtid).ToList();
                    //重置未分配状态
                    _jtfp.Update_JtFpZt(ids);
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