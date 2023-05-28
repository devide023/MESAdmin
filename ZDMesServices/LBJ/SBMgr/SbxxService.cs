using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.SBMgr
{
    public class SbxxService:BaseDao<base_sbxx>
    {
        public SbxxService(string constr):base(constr)
        {

        }
        public override IEnumerable<base_sbxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select rowid as rid, sbbh, sbmc, gcdm, scx, gwh, sblx, ljlx, txfs, ip, port, com, sfky, sflj, bz, lrr, lrsj, glgwh, refresh_time, glgwxh, sbczl, sbyz, sbzj from base_sbxx where 1=1 ");
                sql_cnt.Append($"select count(*) from base_sbxx where 1=1 ");

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
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<base_sbxx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Modify(IEnumerable<base_sbxx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_sbxx set sbczl=:sbczl,sbyz=:sbyz,sbzj=:sbzj where rowid = :rid");
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
