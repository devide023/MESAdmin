using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.GyMgr
{
    [RoutePrefix("api/ducar/gwzd")]
    public class DuCarGwzdController : BaseApiController<base_gwzd>
    {
        private IDuCarKhGw _khgw;
        public DuCarGwzdController(IDbOperate<base_gwzd> baseservice, IRequireVerify requireverfify, IImportData<base_gwzd> importservice, IDuCarKhGw khgw) : base(baseservice)
        {
            _requireverfify = requireverfify;
            _importservice = importservice;
            _khgw = khgw;
        }
        [HttpPost, Route("get_khgw")]
        public IHttpActionResult Get_Khgw(dynamic obj)
        {
            try
            {
                if (obj != null)
                {
                    List<base_gwzd> list = new List<base_gwzd>();
                    if (obj.scx != null && obj.gwh != null)
                    {
                        list = _khgw.Get_Khgw_List(obj.scx.ToString(), obj.gwh.ToString());
                    }
                    return Json(new { code = 1, msg = "ok", list = list });
                }
                else
                {
                    return Json(new { code = 0, msg = "参数错误" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gwzd,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gwzd,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.Ducar.base_gwzd,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}