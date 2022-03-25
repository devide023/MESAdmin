﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using Autofac.Integration.WebApi;
using Autofac;
using ZDMesServices.Common;

namespace MesAdmin.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        private IPageConfig _pageconfig;
        public CommonController(IPageConfig pageconfig)
        {
            _pageconfig = pageconfig;
        }
        
        [HttpGet, Route("pageconf")]
        public IHttpActionResult GetPageConfig(string path)
        {
            try
            {
                //前端页面配置项
                string config = _pageconfig.GetPageConf(path);
                var token = ZDToolHelper.TokenHelper.GetToken;
                var pagepermis = _pageconfig.GetPagePermis(path, token);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    pageconfig = config,
                    pagepermis = pagepermis
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("pagefields")]
        public IHttpActionResult GetPageFields(int pageid)
        {
            try
            {
                var fields_infos = _pageconfig.GetPageFields(pageid);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    fields = fields_infos
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}