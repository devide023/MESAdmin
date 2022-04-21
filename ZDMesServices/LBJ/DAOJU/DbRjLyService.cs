using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjLyService:BaseDao<base_dbrjzx>,IDaoJu
    {
        public DbRjLyService(string constr) :base(constr)
        {

        }

        public override IEnumerable<base_dbrjzx> GetList(sys_page parm, out int resultcount)
        {
            using (IDbConnection db = new OracleConnection(ConString))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select t1.id,t1.gcdm, t1.dbh, t1.scx, t1.sbbh, t1.rjlx, t1.rjbzsm, t1.rjazsm, t1.rjdqsm, t1.rjazjgs, t1.dqjgs, t1.dblysj, t1.dblyr, t1.rjlysj, t1.rjlyr, t1.rjrmcs, t1.rjzhrmsj, t2.dbmc, t2.dblx, t3.rjmc,t3.cpzt");
                sql.Append(" from BASE_DBRJZX t1, base_dbxx t2, (select ta.rjlx, ta.rjmc, ta.rjbzsm, tb.cpzt FROM   base_rjxx ta, base_dbrjgx tb where  ta.rjlx = tb.djlx(+)) t3 ");
                sql.Append(" where  t1.dbh = t2.dbh(+) ");
                sql.Append(" and    t1.rjlx = t3.rjlx(+) ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append("select count(t1.gcdm) from BASE_DBRJZX t1,base_dbxx t2,(select ta.rjlx, ta.rjmc, ta.rjbzsm, tb.cpzt FROM   base_rjxx ta, base_dbrjgx tb where  ta.rjlx = tb.djlx(+)) t3 ");
                sql_cnt.Append(" where  t1.dbh = t2.dbh(+) ");
                sql_cnt.Append(" and    t1.rjlx = t3.rjlx(+) ");

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
                }
                var q = db.Query<base_dbrjzx>(OraPager(sql.ToString()), parm.sqlparam);
                resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                return q;
            }
        }

        public IEnumerable<sys_dbrjly> DbRjGxList(string dbh)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" select t1.rjid, t1.cpzt, t2.rjlx, t2.rjmc, t2.rjbzsm ");
                    sql.Append(" from BASE_DBRJGX t1, base_rjxx t2");
                    sql.Append(" where  t1.rjid = t2.id");
                    sql.Append(" and t1.dbh = :dbh ");
                    sql.Append(" and    not exists(select * from base_dbrjzx where rjid = t1.rjid)");
                    return db.Query<sys_dbrjly>(sql.ToString(),new { dbh = dbh});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbrjzx> DbRjZxList(string dbh)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" select id,rjid,gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjzhrmsj ");
                    sql.Append(" from BASE_DBRJZX ");
                    sql.Append(" where dbh = :dbh order by rjlx asc");
                    return db.Query<base_dbrjzx>(sql.ToString(), new { dbh = dbh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 初次领用
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override int Add(IEnumerable<base_dbrjzx> entitys)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    db.Open();
                    using (var trans = db.BeginTransaction())
                    {
                        foreach (var item in entitys)
                        {
                            var sfcz = db.ExecuteScalar<int>("select count(dbh) from base_dbrjzx where dbh = :dbh and rjid = :rjid", new { dbh = item.dbh,rjid=item.rjid });
                            if(sfcz > 0)
                            {
                                db.Execute("delete from base_dbrjzx where dbh = :dbh and rjid = :rjid ", new { dbh = item.dbh, rjid = item.rjid }, trans);
                            }
                            DB.Insert<base_dbrjzx>(item, trans);
                            db.Execute("update BASE_DBXX set dbzt = '使用中' where dbh = :dbh", new { dbh = item.dbh }, trans);
                            DB.Insert<base_dbrjzx_ls>(new base_dbrjzx_ls()
                            {
                                rjid = item.rjid,
                                gcdm = item.gcdm,
                                scx = item.scx,
                                dbh = item.dbh,
                                sbbh = item.sbbh,
                                djlx = item.rjlx,
                                djbzsm = item.rjbzsm,
                                djazsm = item.rjazsm,
                                djdqsm = item.rjdqsm,
                                djazjgs = item.rjazjgs,
                                dqjgs = item.dqjgs,
                                dblysj = item.dblysj,
                                dblyr = item.dblyr,
                                rjlyr = item.rjlyr,
                                rjlysj = item.rjlysj,
                                rjrmcs = item.rjrmcs,
                                rjzhrmsj = item.rjzhrmsj
                            },trans);
                        }
                        trans.Commit();
                    }
                    return entitys.Count();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Del(IEnumerable<base_dbrjzx> entitys)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    db.Open();
                    using (var trans = db.BeginTransaction())
                    {
                        foreach (var item in entitys)
                        {
                           var ret = DB.Delete<base_dbrjzx>(item, trans);
                            db.Execute("update BASE_DBXX set dbzt = '空闲中' where dbh = :dbh", new { dbh = item.dbh }, trans);
                        }
                        trans.Commit();
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SetDbBF(string dbh)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetRjSm(List<int> ids)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update BASE_DBRJZX ");
                    sql.Append(" set    rjrmcs = nvl(rjrmcs,0) + 1, ");
                    sql.Append("        rjzhrmsj = sysdate ");
                    sql.Append(" where  id in :id");
                    return db.Execute(sql.ToString(), new { id = ids })>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 换刀领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool DbRjLy(dbrjly_form form)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    db.Open();
                    using (var trans = db.BeginTransaction())
                    {
                        var zxlist = db.Query<base_dbrjzx>("select id from base_dbrjzx where sbbh=:sbbh and scx=:scx", new { sbbh = form.sbbh,scx=form.scx }).Select(t=>t.id);
                        foreach (var item in form.olddbrjzx)
                        {
                            int smxh = 0;
                            db.Execute("update base_dbrjzx set scx=:scx,sbbh=:sbbh,dbh=:dbh,rjazsm=:rjazsm,rjdqsm=:rjdqsm where id=:id", new {
                                id=item.id,
                                scx = form.newscx,
                                sbbh = form.newsbbh,
                                dbh = form.newdbh,
                                rjazsm = item.rjbzsm - smxh,
                                rjdqsm = item.rjbzsm - smxh
                            }, trans);
                        }
                        //未选择项
                        var dellist = zxlist.Except(form.olddbrjzx.Select(t => t.id));
                        foreach (var item in dellist)
                        {
                            db.Execute("delete from base_dbrjzx where id = :id", new { id = item }, trans);
                        }
                        foreach (var item in form.dbrjzx)
                        {
                            var sfcz = db.ExecuteScalar<int>("select count(dbh) from base_dbrjzx where dbh = :dbh and rjid = :rjid", new { dbh = item.dbh, rjid = item.rjid });
                            if (sfcz == 0)
                            {
                                DB.Insert<base_dbrjzx>(item, trans);
                                db.Execute("update BASE_DBXX set dbzt = '使用中' where dbh = :dbh", new { dbh = item.dbh }, trans);
                                DB.Insert<base_dbrjzx_ls>(new base_dbrjzx_ls()
                                {
                                    rjid  = item.rjid,
                                    gcdm = item.gcdm,
                                    scx = item.scx,
                                    dbh = item.dbh,
                                    sbbh = item.sbbh,
                                    djlx = item.rjlx,
                                    djbzsm = item.rjbzsm,
                                    djazsm = item.rjazsm,
                                    djdqsm = item.rjdqsm,
                                    djazjgs = item.rjazjgs,
                                    dqjgs = item.dqjgs,
                                    dblysj = item.dblysj,
                                    dblyr = item.dblyr,
                                    rjlyr = item.rjlyr,
                                    rjlysj = item.rjlysj,
                                    rjrmcs = item.rjrmcs,
                                    rjzhrmsj = item.rjzhrmsj
                                }, trans);
                            }
                        }
                        trans.Commit();
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> GetDbxxBySbbh(string sbbh)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" select t1.dblx, dbmc, t1.dbh ");
                    sql.Append(" FROM   base_dbxx t1, base_dbrjzx t2");
                    sql.Append(" where  t2.dbh = t1.dbh");
                    sql.Append(" and    t2.sbbh = :sbbh ");
                    return db.Query<base_dbxx>(sql.ToString(), new { sbbh = sbbh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_cnc> Get_CnC_By_Scx(string scx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct t2.sbbh, t2.sbmc ");
                sql.Append(" FROM   base_dbrjzx t1, base_cnc t2 ");
                sql.Append(" where  t1.sbbh = t2.sbbh ");
                sql.Append(" and    t1.scx = :scx ");
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    return db.Query<base_cnc>(sql.ToString(), new { scx = scx });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
