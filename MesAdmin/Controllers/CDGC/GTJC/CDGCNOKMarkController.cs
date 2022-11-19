using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.CDGC;
namespace MesAdmin.Controllers.CDGC.GTJC
{
    [RoutePrefix("api/cdgc/nokmark")]
    public class CDGCNOKMarkController : BaseApiController<zxjc_nok_mark>
    {
        public CDGCNOKMarkController(IDbOperate<zxjc_nok_mark> nokmarkservice, IRequireVerify requireverfify) :base(nokmarkservice)
        {
            this._requireverfify = requireverfify;
        }
        public override IHttpActionResult Add(List<zxjc_nok_mark> entitys)
        {
            return base.Add(entitys);
        }
        public override IHttpActionResult Edit(List<zxjc_nok_mark> entitys)
        {
            return base.Edit(entitys);
        }
    }
}