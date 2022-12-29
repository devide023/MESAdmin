using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesInterfaces.TJ;

namespace ZDMesServices.TJ.A1.ZLGL
{
    public class A1ClFsService:BaseDao<zxjc_fault_clfs>, IBatAtachValue<zxjc_fault_clfs>
    {
        public A1ClFsService(string constr):base(constr)
        {

        }

        public List<zxjc_fault_clfs> BatSetValue(List<zxjc_fault_clfs> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select SEQ_MES_CLFS_NO.nextval from dual");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("select fault_no FROM zxjc_fault where fault_name = :name ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var clfsno = db.ExecuteScalar<int>(sql.ToString());
                        if (string.IsNullOrEmpty(item.faultno))
                        {
                            var q = db.Query<string>(sql1.ToString(), new { name = item.faultname });
                            if (q.Count() > 0)
                            {
                                item.faultno = q.First();
                            }
                        }
                        item.handno = "NO" + clfsno.ToString().PadLeft(8, '0');
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
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
                catch (Exception)
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
