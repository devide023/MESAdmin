using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
namespace ZDMesServices.CDGC.JJBGL
{
    /// <summary>
    /// 电机壳交接班服务类
    /// </summary>
    public class DJKJJBService:BaseDao<zxjc_djkjjb_bill>, IDjkjjb
    {
        public DJKJJBService(string constr) : base(constr)
        {

        }
        public override bool Del(IEnumerable<zxjc_djkjjb_bill> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from zxjc_djkjjb_bill where id = :id");
            StringBuilder sql1 = new StringBuilder();
            sql1.Append("delete from zxjc_djkjjb_detail where billid = :id");
            StringBuilder sql2 = new StringBuilder();
            sql2.Append("delete from zxjc_djkjjb_hx_detail where billid = :id");

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
                                db.Execute(sql.ToString(), new { id = item.id }, trans);
                                db.Execute(sql1.ToString(), new { id = item.id }, trans);
                                db.Execute(sql2.ToString(), new { id = item.id }, trans);
                            }
                            trans.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return false;
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

        public override IEnumerable<zxjc_djkjjb_bill> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                parm.default_order_colname = "rq";
              return base.GetList(parm,out resultcount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public zxjc_djkjjb_bill Get_Djkjjb_Bill_ByBc(string rq, string bc)
        {
            try
            {
                rq = rq.Replace("T", " ");
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, rq, bc, jbr, zlqk, sbqk, qtqk, lrr, lrsj, hxry from zxjc_djkjjb_bill where trunc(rq) = trunc(to_date(:rq,'yyyy-MM-dd HH24:mi:ss')) and bc = :bc ");
                StringBuilder sqljj = new StringBuilder();
                sqljj.Append("select id, billid, cpmc, kcsl, jgsl, gfsl, lfsl, hgsl, kcsysl FROM zxjc_djkjjb_detail where  billid = :billid ");
                StringBuilder sqlhx = new StringBuilder();
                sqlhx.Append("select id, billid, xmmc, trjgsl, dpssl, gfsl, lfsl, hgsl FROM   zxjc_djkjjb_hx_detail  where  billid = :billid ");
                using (var db = new OracleConnection(ConString))
                {
                    zxjc_djkjjb_bill bill = new zxjc_djkjjb_bill();
                    var q = db.Query<zxjc_djkjjb_bill>(sql.ToString(), new { rq = rq, bc = bc });
                    if (q.Count() > 0)
                    {
                        bill = q.First();
                        bill.rq = bill.rq?.Date;
                        var jjmx = db.Query<zxjc_djkjjb_detail>(sqljj.ToString(), new { billid = bill.id });
                        bill.djkjjbdetail = jjmx.ToList();
                        var hxmx = db.Query<zxjc_djkjjb_hx_detail>(sqlhx.ToString(), new { billid = bill.id });
                        bill.djkjjbdetailhx = hxmx.ToList();
                    }
                    return bill;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Djkjjb(zxjc_djkjjb_bill bill, List<zxjc_djkjjb_detail> jjmx, List<zxjc_djkjjb_hx_detail> hxmx)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                StringBuilder sql = new StringBuilder();
                                sql.Append("select id, rq, bc, jbr, zlqk, sbqk, qtqk, lrr, lrsj, hxry ");
                                sql.Append(" FROM   zxjc_djkjjb_bill where bc=:bc and trunc(rq) = trunc(:rq) ");
                                //
                                var q = db.Query<zxjc_djkjjb_bill>(sql.ToString(), new { bc = bill.bc, rq = bill.rq });
                                int billid = 0;
                                if (q.Count() == 0)
                                {
                                    bill.rq = bill.rq?.Date;
                                    billid = Db.Insert<zxjc_djkjjb_bill>(bill, trans);
                                }
                                else
                                {
                                    var obj = q.OrderBy(t => t.id).First();
                                    bill.id = obj.id;
                                    db.Execute("update zxjc_djkjjb_bill set rq = trunc(:rq),bc=:bc,jbr=:jbr,zlqk=:zlqk,sbqk=:sbqk,hxry=:hxry where id = :id", bill, trans);
                                }
                                sql.Clear();
                                sql.Append("select count(id) FROM ZXJC_DJKJJB_DETAIL where billid=:billid");
                                var qjjmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = bill.id });
                                if(qjjmx > 0)
                                {
                                    db.Execute("delete from zxjc_djkjjb_detail where billid= :billid", new { billid = bill.id }, trans);
                                }
                                //机加明细
                                foreach (var item in jjmx)
                                {
                                    item.billid = bill.id;
                                    item.hgsl = item.jgsl - item.gfsl - item.lfsl;
                                    item.kcsysl = item.kcsl - item.jgsl;
                                    Db.Insert<zxjc_djkjjb_detail>(item, trans);
                                }
                                sql.Clear();
                                sql.Append("select count(id) FROM zxjc_djkjjb_hx_detail where billid=:billid");
                                var qhxmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = bill.id });
                                if (qhxmx > 0)
                                {
                                    db.Execute("delete from zxjc_djkjjb_hx_detail where billid= :billid", new { billid = bill.id }, trans);
                                }
                                //后序明细
                                foreach (var item in hxmx)
                                {
                                    item.billid = bill.id;
                                    item.hgsl = item.trjgsl - item.dpssl - item.gfsl - item.lfsl;
                                    Db.Insert<zxjc_djkjjb_hx_detail>(item, trans);
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
