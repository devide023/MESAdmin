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
    /// 故障基础信息
    /// </summary>
    [RoutePrefix("api/a1/fault")]
    public class A1GZJCXXController : BaseApiController<zxjc_fault>
    {
        private IFault _fault;
        public A1GZJCXXController(IDbOperate<zxjc_fault> faultservice, IRequireVerify requireverfify, IImportData<zxjc_fault> importservice,IFault fault) :base(faultservice)
        {
            _requireverfify= requireverfify;
            _importservice=importservice;
            _fault = fault;
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_fault> entitys)
        {
            return base.Add(entitys);
        }
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult Edit(List<zxjc_fault> entitys)
        {
            return base.Edit(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return  base.ReadTempFile_By_Replace(fileid);    
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_fault,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_fault>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}