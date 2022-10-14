using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.JCGL
{
    public class A1JcGlService:BaseDao<zxjc_jcgl>
    {
        public A1JcGlService(string constr):base(constr)
        {

        }
        public override int Add(IEnumerable<zxjc_jcgl> entitys)
        {
            foreach (var item in entitys)
            {
                if(item.lx == "奖励")
                {
                    item.jcje = Math.Abs(item.jcje);
                }
                else if(item.lx == "惩罚")
                {
                    item.jcje = -1 * Math.Abs(item.jcje);
                }
            }
            return base.Add(entitys);
        }
        public override bool Modify(IEnumerable<zxjc_jcgl> entitys)
        {
            foreach (var item in entitys)
            {
                if (item.lx == "奖励")
                {
                    item.jcje = Math.Abs(item.jcje);
                }
                else if(item.lx == "惩罚")
                {
                    item.jcje = -1 * Math.Abs(item.jcje);
                }
            }
            return base.Modify(entitys);
        }
        public override IEnumerable<zxjc_jcgl> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select id, gcdm, user_code as usercode,(select user_name from zxjc_ryxx where user_code =zxjc_jcgl.user_code and rownum < 2 ) as username, scx, bzxx, gwh, jcxx, jclr, sl, jcje, fsrq, khr, khbm, lx, bz, lrr, lrsj, scbz from zxjc_jcgl where scbz='N' ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_jcgl where scbz='N' ");
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
                        sql.Append(" order by lrsj desc,user_code asc nulls last");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_jcgl>(OraPager(sql.ToString()), parm.sqlparam);
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
