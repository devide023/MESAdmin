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
using ZDMesInterfaces.LBJ.RyMgr;

namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/ryjn")]
    public class RYJNController : ApiController
    {
        private int i = 0;
        private IDbOperate<zxjc_ryxx_jn> _ryjnservice;
        private IRyJn _ryjn;
        public RYJNController(IDbOperate<zxjc_ryxx_jn> ryjnservice, IRyJn ryjn)
        {
            _ryjnservice = ryjnservice;
            _ryjn = ryjn;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _ryjnservice.GetList(parm, out resultcount);
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
        private string CheckJnNo(int maxno)
        {
            try
            {
                var jnno = "JN" + maxno.ToString().PadLeft(4, '0');
                var sfcz = _ryjn.IsExistJnNo(jnno);
                if (!sfcz) {
                    return jnno;
                }
                else
                {
                    i++;
                    return CheckJnNo(_ryjn.MaxJnNo()+ i);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("add")]
        public IHttpActionResult Add(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                int maxno = _ryjn.MaxJnNo();
                int no = 1;
                foreach (var item in entitys)
                {
                    item.jnbh = CheckJnNo(maxno + no);
                    no++;
                }
                var ret = _ryjnservice.Add(entitys);
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
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                var ret = _ryjnservice.Del(entitys);
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
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                var ret = _ryjnservice.Modify(entitys);
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
    }
}