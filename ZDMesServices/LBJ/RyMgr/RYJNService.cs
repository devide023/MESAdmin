using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
using ZDMesInterfaces.LBJ.RyMgr;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.RyMgr
{
    public class RYJNService:BaseDao<zxjc_ryxx_jn>,IRyJn
    {
        public RYJNService(string constr):base(constr)
        {

        }

        public override int Add(IEnumerable<zxjc_ryxx_jn> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) FROM zxjc_ryxx where user_code = :usercode");
            using (var db = new OracleConnection(ConString))
            {
                InitDB(db);
                try
                {
                    List<dynamic> oklist = new List<dynamic>();
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { usercode = item.usercode });
                        if (cnt > 0)
                        {
                            var isok = Db.Insert<zxjc_ryxx_jn>(item);
                            oklist.Add(isok);
                        }
                    }
                    return oklist.Count;
                }
                finally
                {
                    Db.Dispose();
                }
            }
            
        }

        public override IEnumerable<zxjc_ryxx_jn> GetList(sys_page parm, out int resultcount)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select t.gcdm, t.user_code as usercode, (select user_name ");
            sql.Append(" from zxjc_ryxx ");
            sql.Append(" where user_code = t.user_code ");
            sql.Append(" and    rownum < 2) as username, t.jnbh, t.jnxx, t.scx, t.gwh, t.sfhg, t.lrr, t.lrsj, t.jnfl, t.jnsj, t.jnsld ");
            sql.Append(" from   ZXJC_RYXX_JN t where 1=1 ");
            StringBuilder sql_cnt = new StringBuilder();
            sql_cnt.Append("select count(*) from zxjc_ryxx_jn where 1=1 ");
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
            }
            using (var db = new OracleConnection(ConString))
            {
                var q = db.Query<zxjc_ryxx_jn>(OraPager(sql.ToString()), parm.sqlparam);
                resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                return q;
            }  
            
        }

        public int MaxJnNo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {

                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(jnbh) from ZXJC_RYXX_JN");
                    return db.ExecuteScalar<int>(sql.ToString());
                }  
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExistJnNo(string jnno)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(jnbh) from ZXJC_RYXX_JN where jnbh = :jnno");
                    var ret = db.ExecuteScalar<int>(sql.ToString(), new { jnno = jnno });
                    return ret > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
