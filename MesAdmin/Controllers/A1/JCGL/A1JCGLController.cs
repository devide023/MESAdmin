using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using MesAdmin.Filters;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.A1.JCGL
{
    /// <summary>
    /// 人员奖惩
    /// </summary>
    [RoutePrefix("api/a1/jcgl")]
    public class A1JCGLController : BaseApiController<zxjc_jcgl>
    {
        public A1JCGLController(IDbOperate<zxjc_jcgl> jcglservice, IRequireVerify requireverfify, IImportData<zxjc_jcgl> importservice) :base(jcglservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_jcgl>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_jcgl> entitys)
        {
            return base.Add(entitys);
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_jcgl>), "BatSetValue")]
        public override IHttpActionResult Edit(List<zxjc_jcgl> entitys)
        {
            return base.Edit(entitys);  
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_jcgl>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}