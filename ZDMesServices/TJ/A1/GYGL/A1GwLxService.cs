using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.GYGL
{
    public  class A1GwLxService:BaseDao<base_gwzx_gwlx>
    {
        public A1GwLxService(string constr):base(constr)
        {

        }

        public override bool Modify(IEnumerable<base_gwzx_gwlx> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update base_gwzx_gwlx set jx_no=:jxno,status_no=:statusno,gwh=:gwh,gwmc=:gwmc,bz=:bz where rowid = :rid");
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

        public override bool Del(IEnumerable<base_gwzx_gwlx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete FROM base_gwzx_gwlx where rowid in :rid");
                var rids = entitys.Select(t => t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rid = rids }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
