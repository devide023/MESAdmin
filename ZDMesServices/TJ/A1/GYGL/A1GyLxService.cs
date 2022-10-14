using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1GyLxService:BaseDao<zxjc_gylx>
    {
        public A1GyLxService(string constr):base(constr)
        {

        }

        public override IEnumerable<zxjc_gylx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select rowid as rid, gcdm, scx, jx_no as jxno, status_no as statusno, gwh,");
                sql.Append(" (select gwmc from base_gwzd where gwh = zxjc_gylx.gwh and rownum < 2) as gwmc,zpsx, mj, fsbz, shbz, sfzp, fjbh, bz, lrr, lrsj from zxjc_gylx where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_gylx where 1=1 ");
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
                        sql.Append(" order by jx_no asc,zpsx asc nulls last");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_gylx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<zxjc_gylx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_gylx set jx_no =:jxno, status_no=:statusno, gwh=:gwh, zpsx=:zpsx, mj=:mj, fsbz=:fsbz, sfzp=:sfzp,bz=:bz where rowid = :rid");
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

        public override bool Del(IEnumerable<zxjc_gylx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_gylx where rowid in :rids");
                var rids = entitys.Select(t => t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rids = rids }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
