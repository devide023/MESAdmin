using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.SJZS
{
    /// <summary>
    /// 曲轴窜动检测
    /// </summary>
    public class A1QzcdjcService : BaseDao<zxjc_qzcdjc>
    {
        public A1QzcdjcService(string constr) : base(constr)
        {
        }

        public override IEnumerable<zxjc_qzcdjc> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql= new StringBuilder();
                StringBuilder sql_cnt= new StringBuilder();
                sql.Append($"select id, vin, a, b, c, d, e,(select user_name FROM zxjc_ryxx where user_code = zxjc_qzcdjc.lrr and rownum=1) as lrr, lrsj, gcdm, scx, gwh, jcjg from zxjc_qzcdjc where 1=1 ");
                sql_cnt.Append($"select count(id) from zxjc_qzcdjc where 1=1 ");

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
                    var q = db.Query<zxjc_qzcdjc>(OraPager(sql.ToString()), parm.sqlparam);
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
