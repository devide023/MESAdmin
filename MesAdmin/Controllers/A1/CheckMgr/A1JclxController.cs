using MesAdmin.Filters;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.CheckMgr
{
    [RoutePrefix("api/a1/jclx")]
    public class A1JclxController : BaseApiController<zxjc_jclx>
    {
        private IA1JC _jc;
        public A1JclxController(IDbOperate<zxjc_jclx> baseservice, IRequireVerify requireverfify, IImportData<zxjc_jclx> importservice,IA1JC jc) : base(baseservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _jc = jc;
        }
        [HttpGet, Route("alllist")]
        public IHttpActionResult GetAllList()
        {
            try
            {
                var list = _jc.Get_All_JCLX();
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list.Select(t => new { label = t.jclx, value = t.id })
                }) ;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jclx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jclx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jclx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}