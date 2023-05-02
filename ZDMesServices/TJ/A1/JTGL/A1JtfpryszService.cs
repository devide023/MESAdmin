using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtfpryszService : BaseDao<zxjc_t_jstcfp_ry>
    {
        public A1JtfpryszService(string constr) : base(constr)
        {
        }
        public override IEnumerable<zxjc_t_jstcfp_ry> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql= new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, usercode,(select name from mes_user_entity where code = zxjc_t_jstcfp_ry.usercode and rownum = 1) as username, lrr, lrsj, zbid from zxjc_t_jstcfp_ry where 1=1 ");
                sql_cnt.Append($"select count(*) from zxjc_t_jstcfp_ry where 1=1 ");

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
                    else
                    {
                        sql.Append($" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstcfp_ry>(OraPager(sql.ToString()), parm.sqlparam);
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
