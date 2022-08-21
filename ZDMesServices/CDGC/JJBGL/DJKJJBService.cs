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

        public override IEnumerable<zxjc_djkjjb_bill> GetList(sys_page parm, out int resultcount)
        {
            try
            {
              return base.GetList(parm,out resultcount);
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
                                    bill.rq = bill.rq.Date;
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
                                var qjjmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = billid });
                                if(qjjmx > 0)
                                {
                                    db.Execute("delete from zxjc_djkjjb_detail where billid= :billid", new { billid = billid }, trans);
                                }
                                //机加明细
                                foreach (var item in jjmx)
                                {
                                    item.billid = billid;
                                    Db.Insert<zxjc_djkjjb_detail>(item, trans);
                                }
                                sql.Clear();
                                sql.Append("select count(id) FROM zxjc_djkjjb_hx_detail where billid=:billid");
                                var qhxmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = billid });
                                if (qhxmx > 0)
                                {
                                    db.Execute("delete from zxjc_djkjjb_hx_detail where billid= :billid", new { billid = billid }, trans);
                                }
                                //后序明细
                                foreach (var item in hxmx)
                                {
                                    item.billid = billid;
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
