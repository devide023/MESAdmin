using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.A1.ZLGL
{
    /// <summary>
    /// 点检基础信息
    /// </summary>
    [RoutePrefix("api/a1/djxx")]
    public class A1DJXXController : BaseApiController<zxjc_djgw>
    {
        private IDJGL _djgl;
        public A1DJXXController(IDbOperate<zxjc_djgw> djservice, IRequireVerify requireverfify, IImportData<zxjc_djgw> importservice,IDJGL djgl) :base(djservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _djgl = djgl;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_djgw>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_djgw> entitys)
        {
            return base.Add(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_djgw>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_djgw>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_djgw>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}