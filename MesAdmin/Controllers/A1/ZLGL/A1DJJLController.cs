using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesInterfaces.LBJ.ImportData;

namespace MesAdmin.Controllers.A1.ZLGL
{
    /// <summary>
    /// 点检记录
    /// </summary>
    [RoutePrefix("api/a1/djjl")]
    public class A1DJJLController : BaseApiController<zxjc_djxx>
    {
        public A1DJJLController(IDbOperate<zxjc_djxx> djxxservice, IRequireVerify requireverfify, IImportData<zxjc_djxx> importservice) :base(djxxservice)
        {
            this._requireverfify= requireverfify;
            this._importservice=importservice;
        }        
    }
}