using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1GwJxService:BaseDao<base_gwzx_jx>
    {
        public A1GwJxService(string constr):base(constr)
        {

        }
        public override int Add(IEnumerable<base_gwzx_jx> entitys, out IEnumerable<base_gwzx_jx> noklist)
        {
            try
            {
                List<base_gwzx_jx> postdata = new List<base_gwzx_jx>();
                List<base_gwzx_jx> repeatdata = new List<base_gwzx_jx>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM  base_gwzx_jx where scx = :scx and cpjx = :cpjx and gwh = :gwh ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, cpjx = item.cpjx, gwh = item.gwh });
                        if (cnt == 0)
                        {
                            postdata.Add(item);
                        }
                        else
                        {
                            repeatdata.Add(item);
                        }
                    }
                }
                noklist = repeatdata;
                return base.Add(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Modify(IEnumerable<base_gwzx_jx> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update base_gwzx_jx set cpjx=:cpjx,gwh=:gwh,gwmc=:gwmc,bz=:bz where rowid = :rid");
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
        public override bool Del(IEnumerable<base_gwzx_jx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete FROM base_gwzx_jx where rowid in :rid");
                var rids = entitys.Select(t => t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rid = rids })>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
