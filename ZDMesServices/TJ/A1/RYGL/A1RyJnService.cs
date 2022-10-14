using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.RYGL
{
    public class A1RyJnService:BaseDao<zxjc_ryxx_jn>,IRYJN
    {
        public A1RyJnService(string constr):base(constr)
        {

        }
        public override IEnumerable<zxjc_ryxx_jn> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select gcdm, user_code as usercode,(select user_name from zxjc_ryxx where user_code =zxjc_ryxx_jn.user_code and rownum < 2 ) as username, jnbh, jnxx, scx, gwh, sfhg, lrr, lrsj, jnfl, jnsj, jnsld");
                sql.Append(" from zxjc_ryxx_jn where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_ryxx_jn where 1=1 ");
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
                        sql.Append(" order by gwh asc,lrsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_ryxx_jn>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override int Add(IEnumerable<zxjc_ryxx_jn> entitys)
        {
            try
            {
                entitys.ToList().ForEach(t => t.jnbh = CreateJnCode());
                return base.Add(entitys);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string CreateJnCode()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_jnbh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "JN" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
