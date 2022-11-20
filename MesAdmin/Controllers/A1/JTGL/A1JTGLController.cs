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
    [RoutePrefix("api/a1/jtgl")]
    public class A1JTGLController : BaseApiController<zxjc_t_jstc>
    {
        private IJTFPSCX _fpscxservice;
        public A1JTGLController(IDbOperate<zxjc_t_jstc> jstzservice, IRequireVerify requireverfify, IImportData<zxjc_t_jstc> importservice, IJTFPSCX fpscxservice) :base(jstzservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _fpscxservice = fpscxservice;
        }
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
        [HttpPost,SearchFilter,Route("toscxlist")]
        public IHttpActionResult JstzToScxList(sys_page parm)
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