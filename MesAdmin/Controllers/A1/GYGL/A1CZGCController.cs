using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1.CZGC;

namespace MesAdmin.Controllers.A1.GYGL
{
    /// <summary>
    /// 操作规程
    /// </summary>
    /// 
    [RoutePrefix("api/a1/czgc")]
    public class A1CZGCController : BaseApiController<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>
    {
        private IA1GYGL _gyglservice;
        public A1CZGCController(IDbOperate<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> baseservice, IA1GYGL gyglservice, IRequireVerify requireverfify, IImportData<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> importservice) : base(baseservice)
        {
            _gyglservice= gyglservice;
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {            
            try
            {
                int resultcount = 0;
                var list = _gyglservice.Get_Czgc_List(parm, out resultcount);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override IHttpActionResult Add(List<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> entitys)
        {
            try
            {
                IEnumerable<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> repeatlist = new List<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>();
                var ret = this._baseservice.Add(entitys, out repeatlist);
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
        [TemplateVerify("ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}