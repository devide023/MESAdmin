using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;

namespace ZDMesServices.CDGC.JJBGL
{
    public class GTJJBService : BaseDao<zxjc_gtjjb_bill>, IGtjjb
    {
        public GTJJBService(string constr) : base(constr)
        {

        }

        public override IEnumerable<zxjc_gtjjb_bill> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, rq, bc, jbr, dbzz, slry, mcry, jyry, zlqk, sbqk, qtqk, lrr, lrsj ");
                sql.Append(" FROM   zxjc_gtjjb_bill ");
                sql.Append(" where  1=1 ");

                StringBuilder sql_detail = new StringBuilder();
                sql_detail.Append("select id, billid, cpmc, sbcmpyl, dbmpsl,hcsl, trjgs, gfsl, lfsl, hgsl, dbmpyl  from zxjc_gtjjb_bill_detail where billid = :billid ");

                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(id) from zxjc_gtjjb_bill where 1=1 ");
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
                    else
                    {
                        sql.Append($" order by rq desc,bc asc ");
                    }
                }
                var _dic = new Dictionary<int, zxjc_gtjjb_bill>();
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_gtjjb_bill>(OraPager(sql.ToString()),parm.sqlparam);
                    /*foreach (var item in q)
                    {
                        item.mxlist = db.Query<zxjc_gtjjb_bill_detail>(sql_detail.ToString(), new { billid = item.id }).ToList();
                    }*/
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<dynamic> Get_CpList()
        {
            try
            {
                List<dynamic> list = new List<dynamic>();
                list.Add("1.4T CC");
                list.Add("1.5L DC");
                list.Add("1.5T AA");
                list.Add("1.2T EL");

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_gtjjb_bill> Get_Gtjjb_List_ByBc(string rq, string bc)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.id, ta.rq, ta.bc, ta.jbr, ta.dbzz, ta.slry, ta.mcry, ta.jyry, ta.zlqk, ta.sbqk, ta.qtqk, ta.lrr, ta.lrsj, tb.billid, tb.cpmc, tb.sbcmpyl, tb.dbmpsl, tb.hcsl, tb.trjgs, tb.gfsl, tb.lfsl, tb.hgsl, tb.dbmpyl ");
                sql.Append(" FROM   zxjc_gtjjb_bill ta, zxjc_gtjjb_bill_detail tb ");
                sql.Append(" where  ta.id = tb.billid ");
                sql.Append(" and    trunc(ta.rq) = trunc(to_date(:rq, 'yyyy-MM-dd HH24:mi:ss')) ");
                sql.Append(" and    ta.bc = :bc");

                using (var db = new OracleConnection(ConString))
                {
                    var _dic = new Dictionary<int, zxjc_gtjjb_bill>();
                    var q = db.Query<zxjc_gtjjb_bill, zxjc_gtjjb_bill_detail, zxjc_gtjjb_bill>(sql.ToString(), (ta, tb) =>
                    {
                        zxjc_gtjjb_bill entity = new zxjc_gtjjb_bill();
                        if(!_dic.TryGetValue(tb.billid,out entity))
                        {
                            ta.id = tb.billid;
                            entity = ta;
                            entity.mxlist = new List<zxjc_gtjjb_bill_detail>();
                            _dic.Add(tb.billid, ta);
                        }
                        entity.mxlist.Add(tb);
                        return entity;
                    }, new { rq = rq, bc = bc }, splitOn: "billid");

                    return q.OrderBy(t=>t.id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Gtjjb(zxjc_gtjjb_bill bill)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, rq, bc, jbr, dbzz, slry, mcry, jyry, zlqk, sbqk, qtqk, lrr, lrsj ");
                sql.Append(" from ZXJC_GTJJB_BILL ");
                sql.Append(" where trunc(rq) = trunc(:rq) and bc = :bc");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                bill.rq = bill.rq.Date;
                                int size = bill.mxlist.Count();
                                zxjc_gtjjb_bill_detail[] mx = new zxjc_gtjjb_bill_detail[size];
                                bill.mxlist.CopyTo(mx);
                                bill.mxlist = null;
                                var q = db.Query<zxjc_gtjjb_bill>(sql.ToString(), new { rq = bill.rq, bc = bill.bc });
                                int billid = 0;
                                if (q.Count() == 0)
                                {
                                    billid = Db.Insert<zxjc_gtjjb_bill>(bill, trans);
                                }
                                else
                                {
                                    var obj = q.OrderBy(t => t.id).First();
                                    billid = obj.id;
                                    bill.id = obj.id;
                                    db.Execute("update zxjc_gtjjb_bill set rq=:rq,bc=:bc,jbr=:jbr,dbzz=:dbzz,slry=:slry, mcry =:mcry, jyry=:jyry, zlqk =:zlqk, sbqk = :sbqk, qtqk = :qtqk where id = :id", bill,trans);
                                }
                                int sfmx = db.ExecuteScalar<int>("select count(id) FROM zxjc_gtjjb_bill_detail where billid =:billid ", new { billid = billid });
                                if (sfmx > 0)
                                {
                                    db.Execute("delete FROM zxjc_gtjjb_bill_detail where billid =:billid", new { billid = billid },trans);
                                }
                                foreach (var item in mx.ToList<zxjc_gtjjb_bill_detail>())
                                {
                                    item.billid = billid;
                                    Db.Insert<zxjc_gtjjb_bill_detail>(item, trans);
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
