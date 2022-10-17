﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.A1.ZLGL
{
    /// <summary>
    /// 返修明细
    /// </summary>
    [RoutePrefix("api/a1/fxmx")]
    public class A1FXMXController : BaseApiController<zxjc_gwzd_fxmx>
    {
        public A1FXMXController(IDbOperate<zxjc_gwzd_fxmx> fxmxservice):base(fxmxservice)
        {

        }
    }
}