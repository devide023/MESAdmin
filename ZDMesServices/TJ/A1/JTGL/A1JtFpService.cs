using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtFpService:BaseDao<zxjc_t_jstcfp>,IJTFP
    {
        public A1JtFpService(string constr):base(constr)
        {

        }

        public List<zxjc_t_jstcfp> IsDistribute(List<zxjc_t_jstcfp> list)
        {
            List<zxjc_t_jstcfp> retlist = new List<zxjc_t_jstcfp>();    
               StringBuilder sql = new StringBuilder();
            sql.Append("select count(id) from ZXJC_T_JSTCFP where jtid = :jtid and gwh=:gwh and jx_no=:jxno and status_no=:statusno");
            using (var db = new OracleConnection(ConString))
            {
                foreach (var item in list)
                {
                   var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jtid = item.jtid, gwh=item.gwh, jxno = item.jxno, statusno = item.statusno });
                    if (cnt > 0)
                    {
                        retlist.Add(item);
                    }    
                }
            }
            return retlist;
        }

        public List<zxjc_t_jstc_scx> IsJtToScx(List<zxjc_t_jstc_scx> list)
        {
            List<zxjc_t_jstc_scx> retlist = new List<zxjc_t_jstc_scx>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(id) from zxjc_t_jstc_scx where jcbh=:jcbh and scx=:scx");
            using (var db = new OracleConnection(ConString))
            {
                foreach (var item in list)
                {
                    var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jcbh = item.jcbh, scx=item.scx });
                    if (cnt > 0)
                    {
                        retlist.Add(item);
                    }
                }
            }
            return retlist;
        }

        public bool Jstz_To_Scx(List<zxjc_t_jstc_scx> list)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in list)
                                {
                                    Db.Insert<zxjc_t_jstc_scx>(item, trans);
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
            finally
            {
                Db?.Dispose();
            }
        }
    }
}
