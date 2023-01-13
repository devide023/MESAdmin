using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1GyLxService:BaseDao<mes_zxjc_gylx>,IBatAtachValue<mes_zxjc_gylx>
    {
        public A1GyLxService(string constr):base(constr)
        {

        }

        public override bool Modify(IEnumerable<mes_zxjc_gylx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update mes_zxjc_gylx set jx_no =:jxno, status_no=:statusno, gwh=:gwh, zpsx=:zpsx, mj=:mj, fsbz=:fsbz, sfzp=:sfzp,bz=:bz where rowid = :rid");
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

        public override bool Del(IEnumerable<mes_zxjc_gylx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from mes_zxjc_gylx where rowid in :rids");
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

        public List<mes_zxjc_gylx> BatSetValue(List<mes_zxjc_gylx> list)
        {
            try
            {
                list.ForEach(i =>
                 {
                     i.jxno = i.jxno.Trim();
                     i.statusno = i.statusno.Trim();
                 });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
