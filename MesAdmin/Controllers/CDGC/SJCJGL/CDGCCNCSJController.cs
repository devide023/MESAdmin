using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels;
using ZDMesModels.CDGC;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.CDGC.SJCJGL
{
    /// <summary>
    /// 成都工厂CNC数据明细
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/sjcj/cnc")]
    public class CDGCCNCSJController : BaseApiController<zxjc_sbxx_ls_cnc>
    {
        public CDGCCNCSJController(IDbOperate<zxjc_sbxx_ls_cnc> sbxxlscncservice):base(sbxxlscncservice)
        {

        }
    }
}