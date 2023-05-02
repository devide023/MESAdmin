using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using ZDMesServices.Common;

namespace ZDMesServices.TJ.A1.JTGL
{
    /// <summary>
    /// 技通浏览记录
    /// </summary>
    public class A1JTLLService : BaseDao<mes_pdm_jstz_yd>
    {
        private UserUtilService _uservice;
        public A1JTLLService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public override IEnumerable<mes_pdm_jstz_yd> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt= new StringBuilder();
                sql.Append("select ta.jcbh,ta.ydr,ta.ydsj,ta.zbid,ta.usercode,ta.jtid,");
                sql.Append("tb.jcmc,tb.jcms,tb.scry,tb.scsj,tb.yxqx1,tb.yxqx2,tb.fp_sj as fpsj,tb.fpr,tb.wjfl,tb.wjlj,tb.jtly ");
                sql.Append($"from mes_pdm_jstz_yd ta,zxjc_t_jstc tb where ta.jtid = tb.jtid and ta.ydrid = {_uservice.CurrentUser.id} ");
                sql_cnt.Append($"select count(*) from mes_pdm_jstz_yd ta,zxjc_t_jstc tb where ta.jtid = tb.jtid and ta.ydrid = {_uservice.CurrentUser.id} ");

                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
                //前端排序
                if (parm.orderbyexp != null && !string.IsNullOrWhiteSpace(parm.orderbyexp))
                {
                    sql.Append(parm.orderbyexp);
                }
                else
                {
                    if (parm.default_order_colname != null && !string.IsNullOrEmpty(parm.default_order_colname))
                    {
                        sql.Append($" order by {parm.default_order_colname} desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<mes_pdm_jstz_yd,zxjc_t_jstc, mes_pdm_jstz_yd>(OraPager(sql.ToString()), (ta, tb) =>
                    {
                        ta.jtid= tb.jtid;
                        ta.jstcinfo= tb;
                        return ta;
                    }, parm.sqlparam,splitOn: "jtid");
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
