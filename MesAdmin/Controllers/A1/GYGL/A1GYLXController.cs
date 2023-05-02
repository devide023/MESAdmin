using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.TJ;
using ZDMesServices.TJ.Common;

namespace MesAdmin.Controllers.A1.GYGL
{
    [RoutePrefix("api/a1/gylx")]
    public class A1GYLXController : BaseApiController<mes_zxjc_gylx>
    {
        private IDbOperate<mes_zxjc_gylx> _gylxservice;
        private IEntityDetail<string> _detailservice;
        private IA1GYLX _a1gylx;
        private IUser _userservice;
        public A1GYLXController(IDbOperate<mes_zxjc_gylx> gylxservice, IRequireVerify requireverfify, IImportData<mes_zxjc_gylx> importservice, IEntityDetail<string> detailservice,IA1GYLX a1gylx, IUser user) : base(gylxservice)
        {
            _gylxservice = gylxservice;
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _detailservice = detailservice;
            _a1gylx = a1gylx;
            _userservice= user;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gylxservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    item.statusno_list = _detailservice.Details(item.jxno).ToList().Select(t => new { label = t, value = t });
                }
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
        [HttpPost, Route("copy")]
        public IHttpActionResult CopyData(List<mes_zxjc_gylx> entitys)
        {
            try
            {
                string jxno = string.Empty;
                string statusno = string.Empty;
                if (entitys != null && entitys.Count > 0)
                {
                    var jxnull = entitys.Where(t => string.IsNullOrEmpty(t.jxno));
                    if (jxnull.Count() > 0)
                    {
                        return Json(new
                        {
                            code = 0,
                            msg = "请完善机型为空的行"
                        });
                    }
                    jxno = entitys.Select(t => t.jxno).Distinct().First();
                    statusno = entitys.Select(t => t.statusno).Distinct().First();
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "未接收到数据"
                    });

                }
                if (!string.IsNullOrEmpty(jxno) && string.IsNullOrEmpty(statusno))
                {
                    var isexsit = _a1gylx.IsExsit(jxno);
                    if (isexsit)
                    {
                        return Json(new
                        {
                            code = 0,
                            msg = $"机型：{jxno},已存在！"
                        });
                    }
                }
                if (!string.IsNullOrEmpty(jxno) && !string.IsNullOrEmpty(statusno))
                {
                    var isexsit = _a1gylx.IsExsit(jxno, statusno);
                    if (isexsit)
                    {
                        return Json(new
                        {
                            code = 0,
                            msg = $"机型：{jxno},状态：{statusno}已存在！"
                        });
                    }
                }
                entitys.ForEach(t => { t.lrsj = DateTime.Now;t.lrr = _userservice.CurrentUser().name; });
                var cnt = _gylxservice.Add(entitys);
                if (cnt > 0)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "复制数据成功"
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "复制数据失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult Add(List<mes_zxjc_gylx> entitys)
        {
            try
            {
                IEnumerable<mes_zxjc_gylx> repeatlist = new List<mes_zxjc_gylx>();
                var ret = _gylxservice.Add(entitys, out repeatlist);
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
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult Edit(List<mes_zxjc_gylx> entitys)
        {
            return base.Edit(entitys);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.mes_zxjc_gylx,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<mes_zxjc_gylx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }

    }
}