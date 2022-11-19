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
using System.Data;

namespace ZDMesServices.CDGC.JJBGL
{
    public class GTJJBService : BaseDao<zxjc_gtjjb_bill>, IGtjjb
    {
        public GTJJBService(string constr) : base(constr)
        {

        }

        public override bool Del(IEnumerable<zxjc_gtjjb_bill> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from zxjc_gtjjb_bill where id = :id");
            StringBuilder sql1 = new StringBuilder();
            sql1.Append("delete FROM zxjc_gtjjb_bill_detail where billid = :id");
            StringBuilder sql2 = new StringBuilder();
            sql2.Append("delete FROM zxjc_gtjjb_gfmx where detailid in (selecc id from zxjc_gtjjb_bill_detail where billid = :id )");
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
                StringBuilder sql = new StringBuilder();
                sql.Append("select cpmc from zxjc_cpb where cpfl = '缸体'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString());
                }
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
                sql.Append("select ta.rq, ta.bc, ta.jbr, ta.dbzz, ta.slry, ta.mcry, ta.jyry, ta.zlqk, ta.sbqk, ta.qtqk, ta.lrr, ta.lrsj, tb.billid,tb.id, tb.cpmc, tb.sbcmpyl, tb.dbmpsl, tb.hcsl, tb.trjgs, tb.gfsl, tb.lfsl, tb.hgsl, tb.dbmpyl ");
                sql.Append(" FROM   zxjc_gtjjb_bill ta, zxjc_gtjjb_bill_detail tb ");
                sql.Append(" where  ta.id = tb.billid ");
                sql.Append(" and    trunc(ta.rq) = trunc(to_date(:rq, 'yyyy-MM-dd HH24:mi:ss')) ");
                sql.Append(" and    ta.bc = :bc");
                StringBuilder sqlgfmx = new StringBuilder();
                sqlgfmx.Append("select id,detailid,vin,yx from zxjc_gtjjb_gfmx where detailid = :mxid ");
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

                    var retlist = q.OrderBy(t=>t.id);
                    foreach (var item in retlist)
                    {
                        foreach (var sitem in item.mxlist)
                        {   
                            sitem.gfmxlist = db.Query<zxjc_gtjjb_gfmx>(sqlgfmx.ToString(), new { mxid = sitem.id }).ToList();
                        }
                    }
                    return retlist;
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
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append(" insert into zxjc_gtjjb_bill_detail ");
                sqlmx.Append(" (billid, cpmc, sbcmpyl, dbmpsl, hcsl, trjgs, gfsl, lfsl, hgsl, dbmpyl)");
                sqlmx.Append(" values");
                sqlmx.Append(" (:billid, :cpmc, :sbcmpyl, :dbmpsl, :hcsl, :trjgs, :gfsl, :lfsl, :hgsl, :dbmpyl) returning id into :id");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                bill.rq = bill.rq?.Date;
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
                                    var mxids = db.Query<string>("select id from zxjc_gtjjb_bill_detail where billid =:billid", new { billid = billid });
                                    db.Execute("delete FROM zxjc_gtjjb_bill_detail where billid =:billid", new { billid = billid },trans);
                                    db.Execute("delete FROM zxjc_gtjjb_gfmx where detailid in :mxids", new { mxids = mxids.ToList() }, trans);
                                }
                                foreach (var item in mx.ToList<zxjc_gtjjb_bill_detail>())
                                {
                                    item.billid = billid;                                    
                                    item.trjgs = item.sbcmpyl + item.dbmpsl + item.hcsl - item.dbmpyl;
                                    item.hgsl = item.trjgs - item.gfsl - item.lfsl;
                                    DynamicParameters p = new DynamicParameters();
                                    p.Add(":billid", item.billid, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":cpmc", item.cpmc, DbType.String, ParameterDirection.Input);
                                    p.Add(":sbcmpyl", item.sbcmpyl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":dbmpsl", item.dbmpsl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":hcsl", item.hcsl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":trjgs", item.trjgs, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":gfsl", item.gfsl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":lfsl", item.lfsl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":hgsl", item.hgsl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":dbmpyl", item.dbmpyl, DbType.Int32, ParameterDirection.Input);
                                    p.Add(":id", null, DbType.Int32, ParameterDirection.Output);
                                    db.Execute(sqlmx.ToString(), p, trans);
                                    int mxid = p.Get<int>(":id");
                                    //var mxid = Db.Insert<zxjc_gtjjb_bill_detail>(item, trans);
                                    foreach (var sitem in item.gfmxlist)
                                    {
                                        sitem.detailid = mxid;
                                        Db.Insert<zxjc_gtjjb_gfmx>(sitem, trans);
                                    }
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
