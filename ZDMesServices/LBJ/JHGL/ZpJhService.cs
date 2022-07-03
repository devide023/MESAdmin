using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.JHGL
{
    public class ZpJhService :BaseDao<pp_zpjh>
    {
        public ZpJhService(string constr):base(constr)
        {

        }

        public override IEnumerable<pp_zpjh> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select gcdm, zpjhh, order_no as orderno, scx, xh, scddlx, scsl, scsj, jhsj, ztbm, jx, first_flag as firstflag, scbz, zt, sapzt, khdm, khpch, jhh, xshh, xsbz, xssx, cpyhjg, yhph, lrr, lrsj, sxsl, xxsl, sx_sj as sxsj, xx_sj as xxsj, mp_fhsl as mpfhsl, scbm, bc, shbj, shr, shsj, fshr, fshsj, jgrq, ztprog, zequp, zmould, zemold, zname, zrbh, scsl1, pcbj, sapbj, ycsl, bcp_sl as bcpsl, order_no_dl as ordernodl, teco_sj as teco_sj ");
                sql.Append(" from pp_zpjh where scx in ('J503','J505','J507') ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from pp_zpjh where scx in ('J503','J505','J507') ");
                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
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
                        sql.Append($" order by jhsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<pp_zpjh>(OraPager(sql.ToString()), parm.sqlparam);
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
