using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/dzgy")]
    public class A1DZGYController : BaseApiController<zxjc_t_dzgy>
    {
        private IDbOperate<zxjc_t_dzgy> _dzgyservice;
        public A1DZGYController(IDbOperate<zxjc_t_dzgy> dzgyservice,  IRequireVerify requireverfify, IImportData<zxjc_t_dzgy> importservice):base(dzgyservice)
        {
            _dzgyservice = dzgyservice;
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        public override IHttpActionResult Add(List<zxjc_t_dzgy> entitys)
        {
            try
            {
                IEnumerable<zxjc_t_dzgy> repeatlist = new List<zxjc_t_dzgy>();
                var ret = _dzgyservice.Add(entitys, out repeatlist);
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}