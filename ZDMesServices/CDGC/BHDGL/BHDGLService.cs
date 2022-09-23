using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace ZDMesServices.CDGC.BHDGL
{
    public class BHDGLService:BaseDao<base_bhdxx>
    {
        public BHDGLService(string constr) : base(constr)
        {

        }

        public override IEnumerable<base_bhdxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select rowid as rid, gcdm, bhdbh, bhdnr, bhddj, sbfs, sbtj FROM base_bhdxx where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append("select count(*) from base_bhdxx where 1=1 ");
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
                    var q = db.Query<base_bhdxx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<base_bhdxx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_bhdxx set gcdm=:gcdm, bhdbh=:bhdbh, bhdnr=:bhdnr, bhddj=:bhddj, sbfs=:sbfs, sbtj=:sbtj where rowid = :rid");
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

        public override bool Del(IEnumerable<base_bhdxx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from  base_bhdxx where rowid = :rid");
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
                                    db.Execute(sql.ToString(), new { rid = item.rid }, trans);
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
