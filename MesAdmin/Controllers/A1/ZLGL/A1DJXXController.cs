﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;

namespace MesAdmin.Controllers.A1.ZLGL
{
    /// <summary>
    /// 点检基础信息
    /// </summary>
    [RoutePrefix("api/a1/djxx")]
    public class A1DJXXController : BaseApiController<zxjc_djgw>
    {
        private IDJGL _djgl;
        public A1DJXXController(IDbOperate<zxjc_djgw> djservice, IRequireVerify requireverfify, IImportData<zxjc_djgw> importservice,IDJGL djgl) :base(djservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _djgl = djgl;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_djgw)t);
                    foreach (var item in list)
                    {
                        item.djno = _djgl.Create_DjNo();
                    }
                    _requireverfify.VerifyRequire<zxjc_djgw>(list);
                }
                var ret = _importservice.NewImportData(list);
                if (ret.oklist.Count == list.Count)
                {
                    return Json(new 
                    {
                        code = 1,
                        msg = $"成功导入数据{list.Count()}条"
                    });
                }
                else if (ret.repeatlist.Count > 0)
                {
                    return Json(new 
                    {
                        code = 2,
                        msg = $"文件数据{list.Count()}条，重复{ret.repeatlist.Count}条"
                    });
                }
                else
                {
                    return Json(new 
                    {
                        code = 0,
                        msg = $"数据导入失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_djgw)t);
                    foreach (var item in list)
                    {
                        item.djno = _djgl.Create_DjNo();
                    }
                    _requireverfify.VerifyRequire<zxjc_djgw>(list);
                }
                var ret = _importservice.ReaplaceImportData(list);
                if (ret.oklist.Count == list.Count)
                {
                    return Json(new 
                    {
                        code = 1,
                        msg = $"成功导入数据{list.Count()}条"
                    });
                }
                else if (ret.dellist.Count > 0)
                {
                    return Json(new 
                    {
                        code = 2,
                        msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
                    });
                }
                else
                {
                    return Json(new 
                    {
                        code = 0,
                        msg = $"数据导入失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_djgw,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_djgw)t);
                    foreach (var item in list)
                    {
                        item.djno = _djgl.Create_DjNo();
                    }
                    _requireverfify.VerifyRequire<zxjc_djgw>(list);
                }
                var ret = _importservice.ZhImportData(list);
                if (ret.oklist.Count == list.Count)
                {
                    return Json(new 
                    {
                        code = 1,
                        msg = $"成功导入数据{list.Count()}条"
                    });
                }
                else if (ret.dellist.Count > 0)
                {
                    return Json(new 
                    {
                        code = 2,
                        msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
                    });
                }
                else
                {
                    return Json(new 
                    {
                        code = 0,
                        msg = $"数据导入失败"
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