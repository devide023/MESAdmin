using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.CDGC;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.JJBGL
{
    /// <summary>
    /// 电机壳交接班
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/djkjjb")]
    public class DJKJJBController : BaseApiController<zxjc_djkjjb_bill>
    {
        //基础服务
        private IDbOperate<zxjc_djkjjb_bill> _djk_base_service;
        //扩展服务
        private IDjkjjb _djkservice;
        public DJKJJBController(IDbOperate<zxjc_djkjjb_bill> djk_base_service, IDjkjjb djkservice) : base(djk_base_service)
        {
            _djk_base_service = djk_base_service;
            _djkservice = djkservice;
        }

        [HttpPost,Route("save_jjb")]

        public IHttpActionResult Save_Djkjjb(zxjc_djkjjb_bill bill)
        {
            try
            {
               var ret = _djkservice.Save_Djkjjb(bill);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据保存失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}