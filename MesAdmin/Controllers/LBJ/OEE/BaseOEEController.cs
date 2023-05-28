using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.OEE
{
    [RoutePrefix("api/lbj/oee")]
    public class BaseOEEController : BaseApiController<base_template_scx_oee>
    {
        IDbOperate<base_template_scx_oee> _bases;
        public BaseOEEController(IDbOperate<base_template_scx_oee> baseservice, IRequireVerify requireverfify, IImportData<base_template_scx_oee> importservice) : base(baseservice)
        {
            _bases = baseservice;
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        public override IHttpActionResult Add(List<base_template_scx_oee> entitys)
        {
            try
            {
                IEnumerable<base_template_scx_oee> repeatlist = new List<base_template_scx_oee>();
                var cnt = _bases.Add(entitys, out repeatlist);
                if (cnt > 0)
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
        [TemplateVerify("ZDMesModels.LBJ.base_template_scx_oee,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.base_template_scx_oee,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.base_template_scx_oee,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}