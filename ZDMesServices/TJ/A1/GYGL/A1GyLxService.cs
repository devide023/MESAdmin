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
    public class A1GyLxService:BaseDao<mes_zxjc_gylx>,IBatAtachValue<mes_zxjc_gylx>,IA1GYLX
    {
        public A1GyLxService(string constr):base(constr)
        {

        }

        public override int Add(IEnumerable<mes_zxjc_gylx> entitys, out IEnumerable<mes_zxjc_gylx> noklist)
        {
            try
            {
                List<mes_zxjc_gylx> postdata = new List<mes_zxjc_gylx>();
                List<mes_zxjc_gylx> repeatdata = new List<mes_zxjc_gylx>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM  mes_zxjc_gylx where scx = :scx and jx_no = :jxno and gwh = :gwh and nvl(status_no,'_') = :statusno ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, jxno = item.jxno, gwh = item.gwh, statusno = string.IsNullOrEmpty(item.statusno)?"_": item.statusno });
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
                     i.jxno = i.jxno?.Trim();
                     i.statusno = i.statusno?.Trim();
                 });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExsit(string jxno)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(1) from mes_zxjc_gylx where  jx_no = :jxno and status_no is null");
                using (var db = new OracleConnection(ConString))
                {
                   var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jxno = jxno });
                    return cnt > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExsit(string jxno, string statusno)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(1) from mes_zxjc_gylx where jx_no = :jxno and status_no = :statusno ");
                using (var db = new OracleConnection(ConString))
                {
                    var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jxno = jxno,statusno = statusno });
                    return cnt > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
