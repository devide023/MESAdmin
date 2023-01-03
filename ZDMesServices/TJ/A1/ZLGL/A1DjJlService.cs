using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesModels.TJ.A1;
using ZDMesModels;

namespace ZDMesServices.TJ.A1.ZLGL
{
    public class A1DjJlService:BaseDao<zxjc_djxx>
    {
        public A1DjJlService(string constr):base(constr)
        {

        }
        public override IEnumerable<zxjc_djxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, djno, djxx, djjg, bz, decode(instr(lrr,'01'),0,lrr,(select user_name from zxjc_ryxx where user_code = lrr and rownum = 1 )) as lrr, lrsj from zxjc_djxx where 1=1 ");
                sql_cnt.Append($"select count(*) from zxjc_djxx where 1=1 ");

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
                    var q = db.Query<zxjc_djxx>(OraPager(sql.ToString()), parm.sqlparam);
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
