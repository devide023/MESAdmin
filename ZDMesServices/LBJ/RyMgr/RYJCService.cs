using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace ZDMesServices.LBJ.RyMgr
{
    public class RYJCService:BaseDao<zxjc_jcgl>
    {
        public RYJCService(string constr):base(constr)
        {

        }
        public override int Add(IEnumerable<zxjc_jcgl> entitys)
        {
            int index = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into zxjc_jcgl ");
            sql.Append(" (gcdm, user_code, scx, bzxx, gwh, jcxx, jcly, sl, jcje, fsrq, khr, khbm, lx, bz, lrr, lrsj) ");
            sql.Append(" values ");
            sql.Append(" (:gcdm, :usercode, :scx, :bzxx, :gwh, :jcxx, :jcly, :sl, :jcje, :fsrq, :khr, :khbm, :lx, :bz, :lrr, :lrsj) ");
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    int cnt = db.Execute(sql.ToString(), item, trans);
                                    if (cnt > 0)
                                    {
                                        index++;
                                    }
                                }
                                trans.Commit();
                                return index;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override IEnumerable<zxjc_jcgl> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select gcdm, user_code as usercode, (select user_name ");
                sql.Append(" FROM   zxjc_ryxx ");
                sql.Append(" where  user_code = ZXJC_JCGL.user_code ");
                sql.Append(" and    rownum < 2) as username, scx, bzxx, gwh, jcxx, jcly, sl, jcje, fsrq, khr, khbm, lx, bz, lrr, lrsj, id from zxjc_jcgl where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_jcgl where 1=1 ");
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
                        sql.Append($" order by lrsj desc ");
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
