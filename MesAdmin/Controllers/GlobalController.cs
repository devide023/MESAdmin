using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers
{
    [RoutePrefix("api/global")]
    public class GlobalController : ApiController
    {
        private IColumnValueReplace _colvalreplace;
        public GlobalController(IColumnValueReplace colvalreplace)
        {
            _colvalreplace = colvalreplace;
        }
        [HttpPost, Route("colval_replace")]
        public virtual IHttpActionResult ColumnVal_Replace(sys_colval_replace parm)
        {
            try
            {
                if (parm.queryexp.Count > 0 && parm.replaceexp.Count > 0)
                {
                    var q = parm.queryexp.Where(t => string.IsNullOrEmpty(t.colname) || string.IsNullOrEmpty(t.value) || string.IsNullOrEmpty(t.oper));
                    var q1 = parm.replaceexp.Where(t => string.IsNullOrEmpty(t.colname) || string.IsNullOrEmpty(t.replacevalue));
                    if (q.Count() > 0)
                    {
                        return Json(new { code = 0, msg = "过滤条件：字段、条件、条件值需选择" });
                    }
                    else if (q1.Count() > 0)
                    {
                        return Json(new { code = 0, msg = "替换条件：字段、替换值需选择" });
                    }
                    else
                    {
                        //var requestScope = Request.GetDependencyScope();
                        //var _colvalreplace = requestScope.GetService(typeof(IColumnValueReplace)) as IColumnValueReplace;
                        var ret = _colvalreplace.Replace_Column_Value(parm);
                        if (ret > 0)
                        {
                            return Json(new { code = 1, msg = $"成功替换{ret}条数据" });
                        }
                        else
                        {
                            return Json(new { code = 0, msg = "数据操作失败" });
                        }
                    }
                }
                else
                {
                    return Json(new { code = 0, msg = "数据操作失败" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}