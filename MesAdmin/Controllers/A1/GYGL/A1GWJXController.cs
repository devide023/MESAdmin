using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using ZDMesServices.TJ.A1.GYGL;

namespace MesAdmin.Controllers.A1.GYGL
{
    /// <summary>
    /// 岗位名称与机型对应关系
    /// </summary>
    [RoutePrefix("api/a1/gwjx")]
    public class A1GWJXController : BaseApiController<base_gwzx_jx>
    {
        private IDbOperate<base_gwzx_jx> _gwjxservice;
        public A1GWJXController(IDbOperate<base_gwzx_jx> gwjxservice, IRequireVerify requireverfify, IImportData<base_gwzx_jx> importservice):base(gwjxservice)
        {
            _gwjxservice = gwjxservice;
            _requireverfify = requireverfify;
            _importservice = importservice;
        }

        public override IHttpActionResult Add(List<base_gwzx_jx> entitys)
        {
            try
            {
                IEnumerable<base_gwzx_jx> repeatlist = new List<base_gwzx_jx>();
                var ret = _gwjxservice.Add(entitys, out repeatlist);
                if (ret > 0)
                {
                    if (repeatlist.Count() > 0)
                    {
                        return Json(new
                        {
                            code = 1,
                            msg = $"有{repeatlist.Count()}条数据重复跳过,其它数据保存成功！"
                        });
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = "数据保存成功"
                        });
                    }
                }
                else
                {
                    if (repeatlist.Count() > 0)
                    {
                        return Json(new
                        {
                            code = 0,
                            msg = $"有{repeatlist.Count()}条数据重复"
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
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.base_gwzx_jx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}