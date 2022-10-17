using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.ZLGL
{
    public class A1ClFsService:BaseDao<zxjc_fault_clfs>
    {
        public A1ClFsService(string constr):base(constr)
        {

        }

        public override bool Del(IEnumerable<zxjc_fault_clfs> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_fault_clfs where rowid in :rids");
                var ids = entitys.Select(t=>t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rids = ids })>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<zxjc_fault_clfs> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update zxjc_fault_clfs set gwh=:gwh,fault_no=:faultno,hand_no=:handno,hand_name=:handname,remark=:remark where rowid = :rid");
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
                catch (Exception ex)
                {
                    throw;
                }    
                finally
                {
                    db.Close();
                }
            }
        }
    }
}
