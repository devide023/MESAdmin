using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.LBJ.Setting
{
    [RoutePrefix("api/lbj/setting")]
    public class LbjSettingController : BaseApiController<base_param>
    {
        public LbjSettingController(IDbOperate<base_param> baseservice) : base(baseservice)
        {
        }
    }
}