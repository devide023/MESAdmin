using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.CDGC;
namespace MesAdmin.Controllers.CDGC.GYGL
{
    /// <summary>
    /// 工艺管理
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/gygl")]
    public class CGGCGYGLController : BaseApiController<zxjc_t_dzgy>
    {
        public CGGCGYGLController(IDbOperate<zxjc_t_dzgy> dzgyservice, IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice) : base(dzgyservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.orderbyexp = " order by lrsj desc ";
            return base.GetList(parm);
        }
        [TemplateVerify("ZDMesModels.CDGC.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.CDGC.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.CDGC.zxjc_t_dzgy,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}