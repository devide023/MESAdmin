﻿using System;
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
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjLyService:BaseDao<base_dbrjzx>,IDaoJu, IImportData<base_dbrjzx>
    {
        public DbRjLyService(string constr) :base(constr)
        {
        }

        public override IEnumerable<base_dbrjzx> GetList(sys_page parm, out int resultcount)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ta.gcdm, ta.dbh, ta.scx, ta.sbbh, ta.rjlx, ta.rjbzsm, ta.rjazsm, ta.rjdqsm, ta.rjazjgs, ta.dqjgs, ta.dblysj, ta.dblyr, ta.rjlysj, ta.rjlyr, ta.rjrmcs, ta.rjzhrmsj, ta.id, ta.rjid, ta.cxz, ");
            sql.Append(" tb.dbh as basedbh,tb.dblx,tb.dbmc,tb.dbzt,tb.bz as dbxxbz, ");
            sql.Append(" tc.id as rjxxid,tc.rjmc,tc.bz as rjxxbz,");
            sql.Append(" td.sbbh as basesbbh,td.sbmc, td.gwh, td.sblx, td.ljlx, td.txfs, td.ip, td.port, td.sfky, td.sflj, td.bz as sbxxbz,td.glgwh ");
            sql.Append(" from BASE_DBRJZX ta,base_dbxx tb,base_rjxx tc,base_sbxx td ");
            sql.Append(" where  ta.dbh = tb.dbh ");
            sql.Append(" and ta.rjid = tc.id(+) ");
            sql.Append(" and ta.sbbh = td.sbbh ");
            StringBuilder sql_cnt = new StringBuilder();
            sql_cnt.Append("select count(*) from BASE_DBRJZX ta,base_dbxx tb,base_rjxx tc,base_sbxx td ");
            sql_cnt.Append(" where  ta.dbh = tb.dbh ");
            sql_cnt.Append(" and ta.rjid = tc.id(+) ");
            sql_cnt.Append(" and ta.sbbh = td.sbbh ");

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
                sql.Append($" order by ta.scx asc,ta.sbbh asc ");
            }
            using (var db = new OracleConnection(ConString))
            {
                var q = db.Query<base_dbrjzx, base_dbxx, base_rjxx, base_sbxx, base_dbrjzx>(OraPager(sql.ToString()), (ta, tb, tc, td) =>
                    {
                        tb.dbh = ta.dbh;
                        tc.id = ta.rjid;
                        td.sbbh = ta.sbbh;
                        ta.basedbxx = tb;
                        ta.baserjxx = tc;
                        ta.basesbxx = td;
                        return ta;
                    }, parm.sqlparam, splitOn: "basedbh,rjxxid,basesbbh");
                resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                return q;
            }
        }

        public IEnumerable<sys_dbrjly> DbRjGxList(string dbh)
        {
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append(" select t1.rjid, t1.cpzt, t2.rjlx, t2.rjmc, t2.rjbzsm ");
                sql.Append(" from BASE_DBRJGX t1, base_rjxx t2");
                sql.Append(" where  t1.rjid = t2.id");
                sql.Append(" and t1.dbh = :dbh ");
                sql.Append(" and    not exists(select * from base_dbrjzx where rjid = t1.rjid)");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<sys_dbrjly>(sql.ToString(), new { dbh = dbh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<sys_db_rj_gx> DbRjGxList(List<string> dbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select dbh, cpzt, dblx, rjid, djlx ");
                sql.Append(" from base_dbrjgx where dbh in :dbh");
                sql.Append(" group  by dbh, cpzt, dblx, rjid, djlx ");

                StringBuilder sqlmin = new StringBuilder();
                sqlmin.Append("select * from  base_dbrjgx where id =( ");
                sqlmin.Append(" select min(id) FROM  base_dbrjgx where dbh = :dbh and rjid = :rjid) ");

                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<base_dbrjgx>(sql.ToString(), new { dbh = dbh });
                    var retlist = new List<sys_db_rj_gx>();
                    foreach (var item in dbh)
                    {
                        var filteritem = list.Where(t => t.dbh == item);
                        sys_db_rj_gx obj = new sys_db_rj_gx();
                        List<dynamic> childlist = new List<dynamic>();
                        foreach (var sitem in filteritem)
                        {
                            var minobj = db.Query<base_dbrjgx>(sqlmin.ToString(), new { dbh = item, rjid = sitem.rjid }).FirstOrDefault();
                            childlist.Add(new
                            {
                                label = minobj.djlx,
                                value = minobj.id
                            });
                        }
                        obj.children = childlist;
                        obj.value = item;
                        obj.label = list.Where(t => t.dbh == item).FirstOrDefault().dblx;
                        retlist.Add(obj);
                    }
                    return retlist;
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

                StringBuilder sql = new StringBuilder();
                sql.Append(" select id,rjid,gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjzhrmsj ");
                sql.Append(" from BASE_DBRJZX ");
                sql.Append(" where dbh = :dbh order by rjlx asc");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_dbrjzx>(sql.ToString(), new { dbh = dbh });
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
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    db.Open();
                    using (var trans = db.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in entitys)
                            {
                                var ret = Db.Delete<base_dbrjzx>(item, trans);
                                db.Execute("update BASE_DBXX set dbzt = '空闲中' where dbh = :dbh", new { dbh = item.dbh }, trans);
                            }
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
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
                StringBuilder sql = new StringBuilder();
                sql.Append("update BASE_DBRJZX ");
                sql.Append(" set    rjbzsm = :bzsm,rjrmcs = nvl(rjrmcs,0) + 1, ");
                sql.Append("        rjzhrmsj = sysdate ");
                sql.Append(" where  id = :id");
                int okcnt = 0;

                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in ids)
                                {
                                    int bzsm = 4000;
                                    var rjxxobj = db.Query<base_rjxx>("select ta.* from base_rjxx ta, base_dbrjzx tb where  ta.id = tb.rjid and tb.id = :id", new { id = item }).FirstOrDefault();
                                    if (rjxxobj != null)
                                    {
                                        bzsm = rjxxobj.rjbzsm;
                                        var ret = db.Execute(sql.ToString(), new { id = item, bzsm = bzsm }, trans);
                                        if (ret > 0)
                                        {
                                            okcnt++;
                                        }
                                    }
                                }
                                trans.Commit();
                                return okcnt > 0;
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
        /// <summary>
        /// 换刀领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool DbRjLy(dbrjly_form form)
        {
            return true;
        }

        public IEnumerable<base_dbxx> GetDbxxBySbbh(string sbbh)
        {
            try
            {
                
                    return new List<base_dbxx>();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DaoBinRenJuLy(dbrjlyform form)
        {
            try
            {
                var postdata = new List<base_dbrjzx>();
                var chalist = new List<base_dbrjzx>();
                var postdatals = new List<base_dbrjzx_ls>();
                var dblist = new List<base_dbrjzx>();
                StringBuilder sql_del = new StringBuilder();
                sql_del.Append("delete from base_dbrjzx where scx = :scx and sbbh = :sbbh ");
                StringBuilder sql_dygx = new StringBuilder();
                sql_dygx.Append("select * from BASE_DBRJGX where id = :id ");

                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    db.Open();
                    try
                    {
                        foreach (var item in form.dbh)
                        {
                            dblist.Add(new base_dbrjzx()
                            {
                                gcdm = "9902",
                                scx = form.scx,
                                sbbh = form.sbbh,
                                dbh = item,
                                dblyr = form.lyr,
                                dblysj = DateTime.Now,
                            });
                        }
                        var sbxxobj = db.Query<base_sbxx>("select * from base_sbxx where sbbh = :sbbh", new { sbbh = form.sbbh }).FirstOrDefault();
                        foreach (var item in form.rjlx)
                        {
                            var sbbh = item[0].ToString();
                            var gxid = Convert.ToInt32(item[1]);
                            var dygxobj = db.Query<base_dbrjgx>(sql_dygx.ToString(), new { id = gxid }).FirstOrDefault();
                            var rjxxobj = db.Query<base_rjxx>("select * from base_rjxx where id = :rjid", new { rjid = dygxobj.rjid }).FirstOrDefault();
                            postdata.Add(new base_dbrjzx()
                            {
                                gcdm = "9902",
                                scx = form.scx,
                                sbbh = form.sbbh,
                                dbh = dygxobj.dbh,
                                rjlx = dygxobj.djlx,
                                rjid = dygxobj.rjid,
                                rjbzsm = rjxxobj.rjbzsm,
                                rjazsm = rjxxobj.rjbzsm,
                                rjdqsm = 0,
                                dblyr = form.lyr,
                                dblysj = DateTime.Now,
                                rjlyr = form.lyr,
                                rjlysj = DateTime.Now,
                                rjazjgs = 0,
                                rjrmcs = 0,
                                gwh = sbxxobj.gwh
                            });
                            postdatals.Add(new base_dbrjzx_ls()
                            {
                                gcdm = "9902",
                                scx = form.scx,
                                sbbh = form.sbbh,
                                dbh = dygxobj.dbh,
                                djlx = dygxobj.djlx,
                                rjid = dygxobj.rjid,
                                djbzsm = rjxxobj.rjbzsm,
                                djazsm = rjxxobj.rjbzsm,
                                djdqsm = 0,
                                dblyr = form.lyr,
                                dblysj = DateTime.Now,
                                rjlyr = form.lyr,
                                rjlysj = DateTime.Now,
                                djazjgs = 0,
                                rjrmcs = 0,
                                gwh = sbxxobj.gwh
                            });
                        }
                        foreach (var item in dblist)
                        {
                            var q = postdata.Where(t => t.scx == item.scx && t.dbh == item.dbh && t.sbbh == item.sbbh);
                            if (q.Count() == 0)
                            {
                                chalist.Add(item);
                            }
                        }
                        postdata.AddRange(chalist);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute(sql_del.ToString(), new { scx = form.scx, sbbh = form.sbbh, dbh = form.dbh }, trans);
                                Db.Insert<base_dbrjzx>(postdata, trans);
                                Db.Insert<base_dbrjzx_ls>(postdatals, trans);
                                trans.Commit();
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                        return true;
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
                Db.Dispose();
            }
        }
        public IEnumerable<base_dbrjgx> GetRjxxByDbBh(List<string> dbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct gcdm, dbh, cpzt, djlx, rjid, id, dblx from BASE_DBRJGX t where dbh in :dbh");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_dbrjgx>(sql.ToString(), new { dbh = dbh });
                } 
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InstallRjXx(List<base_dbrjzx> zxlist)
        {
            throw new NotImplementedException();
        }

        public bool UnInstallRjXx(List<int> zxids)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update BASE_DBRJZX set rjlx = null,rjbzsm=0,rjazsm=0,rjdqsm=0,rjlyr=null,rjlysj=null,rjid = 0 where id in :id");
                //卸载刃具保存到流水表
                StringBuilder sqlls = new StringBuilder();
                sqlls.Append("insert into base_dbrjzx_ls ");
                sqlls.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm, djazsm, djdqsm, djazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjzhrmsj, rjid, cxz, gwh, lrsj) ");
                sqlls.Append(" select gcdm, scx, dbh, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjzhrmsj, rjid, cxz, gwh, sysdate ");
                sqlls.Append(" from base_dbrjzx ");
                sqlls.Append(" where id in :id");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                //卸载刃具list
                                db.Execute(sqlls.ToString(), new { id = zxids }, trans);
                                db.Execute(sql.ToString(), new { id = zxids }, trans);
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

        public IEnumerable<base_dbrjzx> GetRjZxByDbh(string dbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where dbh like :dbh ");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<base_dbrjzx>(sql.ToString(), new { dbh = dbh + "%" });
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbrjzx> ChooseRjlxByDbh(string dbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select ta.*,(select dblx from base_dbxx where dbh = ta.dbh and rownum < 2) as dblx FROM   base_dbrjzx ta ");
                sql.Append(" where  ta.sbbh in (select sbbh FROM base_dbrjzx ta where ta.dbh = :dbh ) order by ta.sbbh asc,ta.dbh asc ");

                StringBuilder sqlgx = new StringBuilder();
                sqlgx.Append("select ta.* from base_dbrjgx ta where ta.dbh = :dbh");

                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<base_dbrjzx>(sql.ToString(), new { dbh = dbh });
                    foreach (var item in list)
                    {
                        List<base_dbrjgx> dbrjgxlist = new List<base_dbrjgx>();
                        dbrjgxlist = db.Query<base_dbrjgx>(sqlgx.ToString(), new { dbh = item.dbh }).ToList();
                        item.dbrjdygx = dbrjgxlist;
                        if (item.rjid > 0)
                        {
                            item.rjids.Add(item.rjid);
                        }
                    }
                    return list;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 在线刃具安装
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ZxRjInstall(List<base_dbrjzx> list)
        {
            try
            {
                //在线记录
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_dbrjzx ");
                sql.Append(" (gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh) ");
                sql.Append(" values  ");
                sql.Append(" (:gcdm, :dbh, :scx, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh) ");
                //流水记录
                StringBuilder sqlls = new StringBuilder();
                sqlls.Append(" insert into base_dbrjzx_ls ");
                sqlls.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm, djazsm, djdqsm, djazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh) ");
                sqlls.Append(" values ");
                sqlls.Append("(:gcdm, :scx, :dbh, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh) ");
                //删除在线记录
                StringBuilder sqldelzx = new StringBuilder();
                sqldelzx.Append("delete from base_dbrjzx where id = :id ");
                //刃具对应关系
                StringBuilder sqldygx = new StringBuilder();
                sqldygx.Append(" select ta.id, ta.dbh, ta.cpzt, ta.djlx, ta.rjid,tb.id as tbrjid, tb.rjmc, tb.rjbzsm, tb.bz as rjxxbz ");
                sqldygx.Append(" FROM   base_dbrjgx ta, base_rjxx tb ");
                sqldygx.Append(" where  ta.rjid = tb.id ");
                sqldygx.Append(" and    ta.rjid in :rjid ");
                sqldygx.Append(" and    ta.dbh = :dbh ");
                //岗位号
                string gwhsql = "select gwh FROM base_sbxx where sbbh = :sbbh";
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in list)
                                {
                                    var gwh = db.ExecuteScalar<string>(gwhsql, new { sbbh = item.sbbh });
                                    var gxlist = db.Query<base_dbrjgx, base_rjxx, base_dbrjgx>(sqldygx.ToString(), (ta, tb) =>
                                    {
                                        tb.id = ta.rjid;
                                        ta.baserjxx = tb;
                                        return ta;
                                    }, new { dbh = item.dbh, rjid = item.rjids }, splitOn: "tbrjid");
                                    db.Execute(sqldelzx.ToString(), new { id = item.id }, trans);
                                    foreach (var sitem in gxlist)
                                    {
                                        var obj = new
                                        {
                                            gcdm = "9902",
                                            scx = item.scx,
                                            dbh = item.dbh,
                                            sbbh = item.sbbh,
                                            dblyr = item.dblyr,
                                            dblysj = item.dblysj,
                                            rjlyr = item.dblyr,
                                            rjlysj = DateTime.Now,
                                            gwh = gwh,
                                            rjlx = sitem.djlx,
                                            rjbzsm = sitem.baserjxx.rjbzsm,
                                            rjazsm = sitem.baserjxx.rjbzsm,
                                            rjdqsm = 0,
                                            rjazjgs = 0,
                                            dqjgs = 0,
                                            rjid = sitem.rjid,
                                            cxz = 0,
                                            rjrmcs = 0,
                                        };
                                        db.Execute(sql.ToString(), obj, trans);
                                        db.Execute(sqlls.ToString(), obj, trans);
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
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ZxRjChange(List<base_dbrjzx> list)
        {
            try
            {
                //在线记录
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_dbrjzx ");
                sql.Append(" (gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh) ");
                sql.Append(" values  ");
                sql.Append(" (:gcdm, :dbh, :scx, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh) ");
                //流水记录
                StringBuilder sqlls = new StringBuilder();
                sqlls.Append(" insert into base_dbrjzx_ls ");
                sqlls.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm, djazsm, djdqsm, djazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs,rjzhrmsj, rjid, cxz, gwh) ");
                sqlls.Append(" values ");
                sqlls.Append("(:gcdm, :scx, :dbh, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs,:rjzhrmsj, :rjid, :cxz, :gwh) ");
                //删除在线记录
                StringBuilder sqldelzx = new StringBuilder();
                sqldelzx.Append("delete from base_dbrjzx where id = :id ");
                //刃具对应关系
                StringBuilder sqldygx = new StringBuilder();
                sqldygx.Append(" select ta.id, ta.dbh, ta.cpzt, ta.djlx, ta.rjid,tb.id as tbrjid, tb.rjmc, tb.rjbzsm, tb.bz as rjxxbz ");
                sqldygx.Append(" FROM   base_dbrjgx ta, base_rjxx tb ");
                sqldygx.Append(" where  ta.rjid = tb.id ");
                sqldygx.Append(" and    ta.rjid in :rjid ");
                sqldygx.Append(" and    ta.dbh = :dbh ");
                //保存备换刀数据到流水表
                StringBuilder dqrjsql = new StringBuilder();
                dqrjsql.Append("select * from BASE_DBRJZX where id = :id");
                //查询更换刀具的历史加工数，当前寿命
                StringBuilder hissql = new StringBuilder();
                hissql.Append("select * from base_dbrjzx_ls where id = ");
                hissql.Append(" (select max(id) FROM base_dbrjzx_ls where scx = :scx and dbh = :dbh and rjid = :rjid) ");
                //保存变化点信息
                StringBuilder bhdsql = new StringBuilder();
                bhdsql.Append(" insert into lbj_qms_4mbhd ");
                bhdsql.Append(" (id, scx, cjrmc, cjsj, jt, rwzt, gcdm, gwh, trig_type, change_type)");
                bhdsql.Append(" values ");
                bhdsql.Append("(:id, :scx, :cjrmc, sysdate, :sbbh, '创建中', '9902', :gwh, 1, 2)");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in list)
                                {
                                    //获取当前在线刃具数据
                                    var dqzxobj = db.Query<base_dbrjzx>(dqrjsql.ToString(), new { id = item.id }).FirstOrDefault();
                                    var currentobj = new
                                    {
                                        gcdm = dqzxobj.gcdm,
                                        scx = dqzxobj.scx,
                                        dbh = dqzxobj.dbh,
                                        sbbh = dqzxobj.sbbh,
                                        dblyr = dqzxobj.dblyr,
                                        dblysj = dqzxobj.dblysj,
                                        rjlyr = dqzxobj.dblyr,
                                        rjlysj = dqzxobj.rjlysj,
                                        gwh = dqzxobj.gwh,
                                        rjlx = dqzxobj.rjlx,
                                        rjbzsm = dqzxobj.rjbzsm,
                                        rjazsm = dqzxobj.rjbzsm,
                                        rjdqsm = dqzxobj.rjdqsm,
                                        rjazjgs = dqzxobj.rjazjgs,
                                        dqjgs = dqzxobj.dqjgs,
                                        rjid = dqzxobj.rjid,
                                        cxz = dqzxobj.cxz,
                                        rjrmcs = dqzxobj.rjrmcs,
                                        rjzhrmsj = dqzxobj.rjzhrmsj,
                                    };
                                    //保存记录到流水
                                    db.Execute(sqlls.ToString(), currentobj, trans);
                                    IEnumerable<base_dbrjgx> gxlist = new List<base_dbrjgx>();
                                    //更换刃具
                                    if (item.rjids.Count > 0)
                                    {
                                        //查询刀柄刃具对应关系
                                        gxlist = db.Query<base_dbrjgx, base_rjxx, base_dbrjgx>(sqldygx.ToString(), (ta, tb) =>
                                        {
                                            tb.id = ta.rjid;
                                            ta.baserjxx = tb;
                                            return ta;
                                        }, new { dbh = dqzxobj.dbh, rjid = item.rjids }, splitOn: "tbrjid");
                                        //删除记录
                                        db.Execute(sqldelzx.ToString(), new { id = item.id }, trans);
                                        foreach (var sitem in gxlist)
                                        {
                                            //查询刃具历史信息
                                            var lsquery = db.Query<base_dbrjzx_ls>(hissql.ToString(), new { scx = dqzxobj.scx, dbh = dqzxobj.dbh, rjid = sitem.rjid }).FirstOrDefault();
                                            var obj = new
                                            {
                                                gcdm = "9902",
                                                scx = dqzxobj.scx,
                                                dbh = dqzxobj.dbh,
                                                sbbh = dqzxobj.sbbh,
                                                dblyr = dqzxobj.dblyr,
                                                dblysj = dqzxobj.dblysj,
                                                rjlyr = dqzxobj.dblyr,
                                                rjlysj = DateTime.Now,
                                                gwh = dqzxobj.gwh,
                                                rjlx = sitem.djlx,
                                                rjbzsm = sitem.baserjxx.rjbzsm,
                                                rjazsm = lsquery.djazsm,
                                                rjdqsm = lsquery.djdqsm,
                                                rjazjgs = lsquery.djazjgs,
                                                dqjgs = lsquery.dqjgs,
                                                rjid = sitem.rjid,
                                                cxz = lsquery.cxz,
                                                rjrmcs = lsquery.rjrmcs,
                                                rjzhrmsj = lsquery.rjzhrmsj
                                            };
                                            db.Execute(sql.ToString(), obj, trans);
                                            db.Execute(sqlls.ToString(), obj, trans);
                                            db.Execute(bhdsql.ToString(), new { id = Guid.NewGuid().ToString().Replace("-", ""), scx = obj.scx, cjrmc = obj.dblyr, sbbh = obj.sbbh, gwh = obj.gwh }, trans);
                                        }
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
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public sys_import_result<base_dbrjzx> NewImportData(List<base_dbrjzx> data)
        {
            try
            {
                List<base_dbrjzx> oklist = new List<base_dbrjzx>();
                List<base_dbrjzx> repeatlist = new List<base_dbrjzx>();
                //在线记录
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_dbrjzx ");
                sql.Append(" (gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh) ");
                sql.Append(" values  ");
                sql.Append(" (:gcdm, :dbh, :scx, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh) ");
                //流水记录
                StringBuilder sqlls = new StringBuilder();
                sqlls.Append(" insert into base_dbrjzx_ls ");
                sqlls.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm, djazsm, djdqsm, djazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh) ");
                sqlls.Append(" values ");
                sqlls.Append("(:gcdm, :scx, :dbh, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh) ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in data)
                                {
                                    //是否存在
                                    var sfcz = db.ExecuteScalar<int>("select count(*) FROM   base_dbrjzx where  scx = :scx and sbbh = :sbbh and dbh = :dbh and rjlx = :rjlx", new { scx = item.scx, sbbh = item.sbbh, dbh = item.dbh, rjlx = item.rjlx });
                                    if (sfcz == 0)
                                    {
                                        //岗位号
                                        var gwh = db.ExecuteScalar<string>("select gwh FROM base_sbxx where sbbh = :sbbh", new { sbbh = item.sbbh });
                                        item.gwh = gwh;
                                        base_rjxx rjxx = db.Query<base_rjxx>("select * FROM base_rjxx where rjlx = :rjlx", new { rjlx = item.rjlx }).FirstOrDefault();
                                        item.rjid = (int)rjxx?.id;
                                        item.rjbzsm = (int)rjxx?.rjbzsm;
                                        item.rjazsm = item.rjbzsm;
                                        item.rjdqsm = 0;
                                        item.cxz = 0;
                                        var isok = db.Execute(sql.ToString(), item, trans);
                                        db.Execute(sqlls.ToString(), item, trans);
                                        oklist.Add(item);
                                    }
                                    else
                                    {
                                        repeatlist.Add(item);
                                    }
                                }
                                trans.Commit();
                                return new sys_import_result<base_dbrjzx>()
                                {
                                    oklist = oklist,
                                    repeatlist = repeatlist
                                };
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

        public sys_import_result<base_dbrjzx> ReaplaceImportData(List<base_dbrjzx> data)
        {
            throw new NotImplementedException();
        }

        public sys_import_result<base_dbrjzx> ZhImportData(List<base_dbrjzx> data)
        {
            throw new NotImplementedException();
        }
    }
}
