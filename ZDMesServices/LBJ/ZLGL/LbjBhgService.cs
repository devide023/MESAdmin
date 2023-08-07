using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.ZLGL
{
    /// <summary>
    /// 不合格件
    /// </summary>
    public class LbjBhgService : BaseDao<sc_zxjc_bhg>
    {
        public LbjBhgService(string constr) : base(constr)
        {
        }

        public override IEnumerable<sc_zxjc_bhg> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select vin, ztbm, scx, work_flow, kczt, gzxx, smsj, smry, cksj, ckry, clfs, cljg, clsj, clr, cpmc, scxzx from sc_zxjc_bhg where scx in('J503','J504','J505','J507','J512') ");
                sql_cnt.Append($"select count(*) from sc_zxjc_bhg where scx in('J503','J504','J505','J507','J512') ");

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
                        sql.Append(" order by smsj desc ");
                    }
                }

                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<sc_zxjc_bhg>(OraPager(sql.ToString()), parm.sqlparam);
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
