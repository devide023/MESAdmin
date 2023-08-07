using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.GYGL
{
    public class LBJGYLXService:BaseDao<zxjc_gylx>
    {
        public LBJGYLXService(string constr):base(constr)
        {

        }

        public override IEnumerable<zxjc_gylx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select rowid as rid,scx, status_no as statusno, gwh, zpsx, mj, fsbz, shbz, sfzp, fjbh, bz,lrr,lrsj from zxjc_gylx where 1=1 ");
                sql_cnt.Append($"select count(*) from zxjc_gylx where 1=1 ");

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
                sql.Append("update zxjc_gylx set scx=:scx, status_no=:statusno, gwh=:gwh,zpsx=:zpsx, mj=:mj, fsbz=:fsbz, shbz=:shbz, sfzp=:sfzp, bz=:bz where rowid = :rid ");
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
    }
}
