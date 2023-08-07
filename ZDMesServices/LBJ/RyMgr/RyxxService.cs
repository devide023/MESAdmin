using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZDMesInterfaces.LBJ.RyMgr;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.RyMgr
{
    public class RyxxService : BaseDao<zxjc_ryxx>, IRyXx
    {
        public RyxxService(string constr) : base(constr)
        {
        }
        public override IEnumerable<zxjc_ryxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select gcdm, scx,user_code as usercode, user_name as username, pass_word as password,rylx, gwh, bzxx, hgsg, csrq, rsrq, jmh, ryxb, xpmc, scbz, tel,sfz from zxjc_ryxx where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_ryxx where 1=1 ");
                //岗位
                StringBuilder gwzdlist = new StringBuilder();
                gwzdlist.Append("select scx, gwh,gwmc FROM base_gwzd");
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
                        sql.Append($" order by scx asc,gwh asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var scxzxlist = db.Query<base_gwzd>(gwzdlist.ToString());
                    var q = db.Query<zxjc_ryxx>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gwhoptions = scxzxlist.Where(t => t.scx == item.scx).Select(t => new sys_column_options() { label = t.gwmc, value = t.gwh }).ToList();
                    }
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Modify(IEnumerable<zxjc_ryxx> entitys)
        {
            try
            {
                List<bool> result = new List<bool>();
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_ryxx set scx=:scx,user_name = :username,rylx = :rylx,");
                sql.Append(" gwh = :gwh,");
                sql.Append(" bzxx = :bzxx,");
                sql.Append(" hgsg = :hgsg,");
                sql.Append(" csrq = :csrq,");
                sql.Append(" rsrq = :rsrq,");
                sql.Append(" ryxb = :ryxb,");
                sql.Append(" xpmc = :xpmc,");
                sql.Append(" sfz = :sfz,");
                sql.Append(" tel = :tel");
                sql.Append(" where  user_code = :usercode");
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql.ToString(), item, trans);
                                }
                                trans.Commit();
                                return true;
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

        public override bool Del(IEnumerable<zxjc_ryxx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_ryxx where user_code  in :usercode");
                StringBuilder sqljn = new StringBuilder();
                sqljn.Append("delete FROM  zxjc_ryxx_jn  where user_code in :usercode");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var usercodes = entitys.Select(t => t.usercode).ToArray();
                                db.Execute(sql.ToString(), new { usercode = usercodes }, trans);
                                db.Execute(sqljn.ToString(), new { usercode = usercodes }, trans);
                                db.Execute("delete from app_role_user where usercode in :usercode", new { usercode = usercodes }, trans);
                                trans.Commit();
                                return true;
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

        public bool IsExistUserCode(string usercode)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select count(user_code) from ZXJC_RYXX where user_code = :usercode");
                        var ret = db.ExecuteScalar<int>(sql.ToString(), new { usercode = usercode });
                        return ret > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int MaxUserCode()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(user_code) from ZXJC_RYXX");
                    return db.ExecuteScalar<int>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
