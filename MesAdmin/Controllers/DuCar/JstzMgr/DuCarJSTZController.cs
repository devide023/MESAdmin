using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.JstzMgr
{
    [RoutePrefix("api/ducar/jtgl")]
    public class DuCarJSTZController : BaseApiController<zxjc_t_jstc>
    {
        private IDuCarJstz _ducarjstzservice;
        private IUser _userservice;
        IDbOperate<zxjc_t_jstcfp> _jtfpservice;
        public DuCarJSTZController(IDbOperate<zxjc_t_jstc> baseservice, IRequireVerify requireverfify, IImportData<zxjc_t_jstc> importservice, IDuCarJstz ducarjstzservice, IDbOperate<zxjc_t_jstcfp> jtfpservice, IUser userservice) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
            _ducarjstzservice = ducarjstzservice;
            _userservice = userservice;
            _jtfpservice = jtfpservice;
        }
        /// <summary>
        /// 未分配技通列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost, SearchFilter, Route("wfplist")]
        public IHttpActionResult Get_WfpList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _ducarjstzservice.Wfp_JtTz_List(parm, out resultcount);
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
        /// 技通分配
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost, Route("jtfp")]
        public IHttpActionResult Jtfp(sys_jtfp_form form)
        {
            try
            {
                List<zxjc_t_jstcfp> postdata = new List<zxjc_t_jstcfp>();
                foreach (var gwh in form.gwh)
                {
                    postdata.Add(new zxjc_t_jstcfp()
                    {
                        jtid = form.jtid,
                        gwh = gwh,
                        jxno = form.jxno,
                        statusno = form.statusno,
                        bz = form.bz,
                        scx = form.scx,
                        lrr1 = _userservice.CurrentUser().name,
                        lrsj1 = DateTime.Now
                    });
                }
                var ret = _jtfpservice.Add(postdata) > 0;
                if (ret)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "技通分配成功",
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "技通分配失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("fpmx")]
        public IHttpActionResult Fpmx(string jtid)
        {
            try
            {
                var list = _ducarjstzservice.Jtfp_Detail(jtid);
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
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}