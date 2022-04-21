using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.LBJ.BHDGL;

namespace MesAdmin.Controllers.LBJ.BHDGL
{
    [RoutePrefix("api/lbj/bhdxx")]
    public class BHDXXController : ApiController
    {
        private IDbOperate<base_bhdxx> _bhdxxservice;
        private IBHD _bhd;
        private int index = 0;
        public BHDXXController(IDbOperate<base_bhdxx> bhdxxservice, IBHD bhd)
        {
            _bhdxxservice = bhdxxservice;
            _bhd = bhd;
        }
        
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _bhdxxservice.GetList(parm, out resultcount);
                return Json(new sys_search_result()
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
        [HttpPost, Route("add")]
        public IHttpActionResult Add(List<base_bhdxx> entitys)
        {
            try
            {
                int i = 1;
                var no = _bhd.Get_Max_BHD();
                foreach (var item in entitys)
                {
                    item.bhdbh = Create_Bhdno(no+i);
                    i++;
                }
                int ret = _bhdxxservice.Add(entitys);
                if (ret > 0)
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

        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<base_bhdxx> entitys)
        {
            try
            {
                var ret = _bhdxxservice.Modify(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<base_bhdxx> entitys)
        {
            try
            {
                var ret = _bhdxxservice.Del(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据删除成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据删除失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string Create_Bhdno(int id)
        {
            try
            {
                string tempcode = "BHD"+ id.ToString().PadLeft(5, '0');
                if (_bhd.IsExistBhdNo(tempcode)) {
                    index++;
                   return Create_Bhdno(id+index);
                }
                else
                {
                    return tempcode;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}