using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjLyService : BaseDao<base_dbrjzx>, IDaoJu, IImportData<base_dbrjzx>
    {
        public DbRjLyService(string constr) : base(constr)
        {
        }
        
        /// <summary>
        /// 插入刀柄刃具在线记录
        /// </summary>
        private StringBuilder Insert_RjZX_Sql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_dbrjzx ");
                sql.Append(" (gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjid, cxz, gwh,rjwz) ");
                sql.Append(" values  ");
                sql.Append(" (:gcdm, :dbh, :scx, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs, :rjid, :cxz, :gwh,(select jgwz FROM base_rjxx where id = :rjid and rownum <2)) ");
                return sql;
            }
        }
        /// <summary>
        /// 插入刀柄刃具流水记录
        /// </summary>
        private StringBuilder Insert_RjZX_Ls_Sql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_dbrjzx_ls ");
                sql.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm, djazsm, djdqsm, djazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs,rjzhrmsj, rjid, cxz, gwh) ");
                sql.Append(" values ");
                sql.Append("(:gcdm, :scx, :dbh, :sbbh, :rjlx, :rjbzsm, :rjazsm, :rjdqsm, :rjazjgs, :dqjgs, :dblysj, :dblyr, :rjlysj, :rjlyr, :rjrmcs,:rjzhrmsj, :rjid, :cxz, :gwh) ");
                return sql;
            }
        }
        //插入变化点信息
        private StringBuilder Inser_BHD_Sql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into lbj_qms_4mbhd ");
                sql.Append(" (id, scx, cjrmc, cjsj, jt, rwzt,djffrymc,gzxx,f,gcdm, gwh, trig_type, change_type)");
                sql.Append(" values ");
                sql.Append("(:id, :scx, :cjrmc, sysdate, :sbbh, '00',:djffr,:gzxx,:f,'9902', :gwh, 1, 2)");
                return sql;
            }
        }
        private mes_user_entity UserInfo
        {
            get
            {
                using (var db = new OracleConnection(ConString))
                {
                    string dlrsql = "select id, code, name from mes_user_entity where token = :token ";
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    var dlrq = db.Query<mes_user_entity>(dlrsql, new { token = token });
                    mes_user_entity uinfo = new mes_user_entity();
                    if (dlrq.Count() > 0)
                    {
                        uinfo = dlrq.First();
                    }
                    return uinfo;
                }
            }
        }
        //查询刃具对应关系
        private StringBuilder SelectRjGx_Sql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select ta.id, ta.dbh,ta.dblx, ta.cpzt, ta.djlx, ta.rjid,tb.id as tbrjid, tb.rjmc, tb.rjbzsm, tb.bz as rjxxbz ");
                sql.Append(" FROM   base_dbrjgx ta, base_rjxx tb ");
                sql.Append(" where  ta.rjid = tb.id ");
                sql.Append(" and    ta.id = :id ");
                return sql;
            }
        }
        //查询更换刀具的历史加工数，当前寿命
        private StringBuilder Select_His_Sql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx_ls where id = ");
                sql.Append(" (select max(id) FROM base_dbrjzx_ls where scx = :scx and dbh = :dbh and rjid = :rjid) ");
                return sql;
            }
        }
        private string SBBH_GWH_Sql
        {
            get
            {
                return "select gwh FROM base_sbxx where sbbh = :sbbh";
            }
        }
        //变化点是否为创建状态
        private string BHD_Sfcl_Sql
        {
            get
            {
                return "select id from lbj_qms_4mbhd where jt=:sbbh and rwzt = '00' order by cjsj desc";
            }
        }
        public string Append_BHD_Sql
        {
            get
            {
                return "update lbj_qms_4mbhd set gzxx = nvl(gzxx,' ') || :gzxx|| ',' where id = :id";
            }
        }
        
        public override IEnumerable<base_dbrjzx> GetList(sys_page parm, out int resultcount)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ta.gcdm, ta.dbh, ta.scx, ta.sbbh, ta.rjlx, ta.rjbzsm, ta.rjazsm, ta.rjdqsm, ta.rjazjgs, ta.dqjgs, ta.dblysj, ta.dblyr, ta.rjlysj, ta.rjlyr,ta.rjrmr,ta.rjrmcs, ta.rjzhrmsj, ta.id, ta.rjid, ta.cxz,round((ta.rjdqsm / ta.rjbzsm) * 100, 2) as rjzt,ta.rjwz,");
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
                    try
                    {
                        db.Open();
                        InitDB(db);
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
                string token = ZDToolHelper.TokenHelper.GetToken;
                StringBuilder sql = new StringBuilder();
                sql.Append("update BASE_DBRJZX ");
                sql.Append(" set    rjbzsm = :bzsm,rjrmcs = nvl(rjrmcs,0) + 1, ");
                sql.Append("        rjzhrmsj = sysdate,rjdqsm = 0,cxz=0,rjrmr = :rjrmr ");
                sql.Append(" where  id = :id");
                //查询刀柄刃具在线
                StringBuilder zxcxsql = new StringBuilder();
                zxcxsql.Append("select * from BASE_DBRJZX where id = :id ");
                //更新流水表刃磨数据
                StringBuilder update_ls_sql = new StringBuilder();
                update_ls_sql.Append(" update base_dbrjzx_ls set rjrmcs = nvl(rjrmcs,0)+1,rjzhrmsj=sysdate,djdqsm =0,cxz=0,rjrmr=:rjrmr where id =:lsid ");
                //设备编号查询变化点表状态
                StringBuilder qbhdzt =new StringBuilder();
                qbhdzt.Append("select id from lbj_qms_4mbhd where jt=:sbbh and rwzt = '00' order by cjsj desc ");
                //原有变化点追加信息
                StringBuilder zjbhdsql = new StringBuilder();
                zjbhdsql.Append("update lbj_qms_4mbhd set gzxx = nvl(gzxx,' ') || :gzxx|| ',' where id = :id");
                //刃磨流水
                StringBuilder rmlssql = new StringBuilder();
                rmlssql.Append("insert into zxjc_rjrm_ls (rjzxid,scx,dbh,sbbh,bzsm,dqsm,rjid, rjlx, rmr, rmrid) values (:rjzxid,:scx,:dbh,:sbbh,:bzsm,:dqsm,:rjid, :rjlx, :rmr, :rmrid) ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var _u = UserInfo;
                                foreach (var item in ids)
                                {
                                    int bzsm = 4000;
                                    var rjxxobj = db.Query<base_rjxx>("select ta.* from base_rjxx ta, base_dbrjzx tb where  ta.id = tb.rjid and tb.id = :id", new { id = item }).FirstOrDefault();
                                    var zxobj = db.Query<base_dbrjzx>(zxcxsql.ToString(), new { id = item }).FirstOrDefault();
                                    var hisobj = db.Query<base_dbrjzx_ls>(Select_His_Sql.ToString(), new {scx = zxobj.scx, dbh =zxobj.dbh,rjid = zxobj.rjid}).FirstOrDefault();
                                    if (rjxxobj != null)
                                    {
                                        bzsm = rjxxobj.rjbzsm;
                                        var ret = db.Execute(sql.ToString(), new { id = item, bzsm = bzsm, rjrmr = _u.name }, trans);
                                        db.Execute(update_ls_sql.ToString(),new { lsid = hisobj.id,rjrmr = _u.name }, trans);
                                        var iscntquery = db.Query<string>(qbhdzt.ToString(), new { sbbh = zxobj.sbbh });
                                        if (iscntquery.Count() == 0)
                                        {
                                            //var bhdid = db.Query<string>("select f_base_getno('BHD', 'MES', 8) from dual").First();
                                            db.Execute(Inser_BHD_Sql.ToString(), new {
                                                id = Guid.NewGuid().ToString().Replace("-",""),
                                                scx = zxobj.scx,
                                                cjrmc = _u.name,
                                                djffr = _u.name,
                                                sbbh = zxobj.sbbh,
                                                gzxx = $"刃具刃磨:{zxobj.rjlx}",
                                                f = "0007",
                                                gwh = zxobj.gwh
                                            }, trans);
                                        }
                                        else
                                        {
                                            db.Execute(zjbhdsql.ToString(), new { gzxx = zxobj.rjlx, id = iscntquery.First() },trans);
                                        }
                                        db.Execute(rmlssql.ToString(), new {rjzxid=zxobj.id, scx = zxobj.scx, dbh = zxobj.dbh, sbbh = zxobj.sbbh, bzsm=zxobj.rjbzsm, dqsm = zxobj.rjdqsm, rjid = zxobj.rjid, rjlx = zxobj.rjlx, rmr = _u.name, rmrid = _u.id }, trans);
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
        /// <summary>
        /// 刀柄刃具领用 2022-06-03
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool DaoBinRenJuLy(dbrjlyform form)
        {
            try
            {
                string gwhsql = "select gwh FROM base_sbxx where sbbh = :sbbh";
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in form.dbrjids)
                                {
                                    //查询刀柄刃具对应关系
                                    var gxb = db.Query<base_dbrjgx, base_rjxx, base_dbrjgx>(SelectRjGx_Sql.ToString(), (ta, tb) =>
                                    {
                                        tb.id = ta.rjid;
                                        ta.baserjxx = tb;
                                        return ta;
                                    }, new { id = item }, splitOn: "tbrjid").FirstOrDefault();
                                    //查询刃具历史信息
                                    var lsquery = db.Query<base_dbrjzx_ls>(Select_His_Sql.ToString(), new { scx = form.scx, dbh = gxb.dbh, rjid = gxb.rjid });
                                    var gwh = db.ExecuteScalar<string>(gwhsql, new { sbbh = form.sbbh });
                                    base_dbrjzx zxobj = new base_dbrjzx();
                                    zxobj.gcdm = form.gcdm;
                                    zxobj.scx = form.scx;
                                    zxobj.sbbh = form.sbbh;
                                    zxobj.dbh = gxb.dbh;
                                    zxobj.dblx = gxb.dblx;
                                    zxobj.rjid = gxb.rjid;
                                    zxobj.rjlx = gxb.djlx;
                                    zxobj.rjlyr = form.lyr;
                                    zxobj.dblyr = form.lyr;
                                    zxobj.rjlysj = DateTime.Now;
                                    zxobj.dblysj = DateTime.Now;
                                    zxobj.rjbzsm = gxb.baserjxx.rjbzsm;
                                    zxobj.rjazsm = gxb.baserjxx.rjbzsm;
                                    zxobj.rjdqsm = 0;
                                    zxobj.rjazjgs = 0;
                                    zxobj.dqjgs = lsquery.Count() > 0 ? lsquery.FirstOrDefault().dqjgs : 0;
                                    zxobj.rjrmcs = lsquery.Count() > 0 ? lsquery.FirstOrDefault().rjrmcs : 0;
                                    zxobj.rjzhrmsj = lsquery.Count() > 0 ? lsquery.FirstOrDefault().rjzhrmsj : Convert.ToDateTime(null);
                                    zxobj.gwh = lsquery.Count() > 0 ? lsquery.FirstOrDefault().gwh : gwh;
                                    zxobj.cxz = lsquery.Count() > 0 ? lsquery.FirstOrDefault().cxz : 0;
                                    db.Execute(Insert_RjZX_Sql.ToString(), zxobj, trans);
                                    db.Execute(Insert_RjZX_Ls_Sql.ToString(), zxobj, trans);
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
                sql.Append("delete from BASE_DBRJZX where id in :id");
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

        public IEnumerable<base_dbrjzx> GetRjZxByDbh(string dbh,string scx,string sbbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                DynamicParameters p = new DynamicParameters();
                sql.Append("select * from base_dbrjzx where 1=1 ");
                if (!string.IsNullOrEmpty(scx))
                {
                    sql.Append(" and scx = :scx ");
                    p.Add(":scx", scx, DbType.String, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(sbbh))
                {
                    sql.Append(" and sbbh = :sbbh ");
                    p.Add(":sbbh", sbbh, DbType.String, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(dbh))
                {
                    sql.Append(" and dbh like :dbh ");
                    p.Add(":dbh", dbh+"%", DbType.String, ParameterDirection.Input);
                }
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<base_dbrjzx>(sql.ToString(), p);
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
        /// <summary>
        /// 刀柄刃具更换，旧刀换新刀，换到同一台设备
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ZxRjChange(List<base_dbrjzx> list)
        {
            try
            {
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
                                    db.Execute(Insert_RjZX_Ls_Sql.ToString(), currentobj, trans);
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
                                            var lsquery = db.Query<base_dbrjzx_ls>(Select_His_Sql.ToString(), new { scx = dqzxobj.scx, dbh = dqzxobj.dbh, rjid = sitem.rjid }).FirstOrDefault();
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
                                            db.Execute(Insert_RjZX_Sql.ToString(), obj, trans);
                                            db.Execute(Insert_RjZX_Ls_Sql.ToString(), obj, trans);
                                            //db.Execute(Inser_BHD_Sql.ToString(), new { 
                                            //    id = Guid.NewGuid().ToString().Replace("-", ""),
                                            //    scx = obj.scx,
                                            //    cjrmc = obj.dblyr,
                                            //    djffr = UserInfo.name,
                                            //    sbbh = obj.sbbh,
                                            //    gzxx=$"原刃具:{currentobj.rjlx}更换为:{obj.rjlx}",
                                            //    f= "0006",
                                            //    gwh = obj.gwh
                                            //}, trans);
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

        public bool Save_DbRjZx_Change(sys_dbrj_bgly_form form)
        {
            try
            {    
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in form.dbrjgxid)
                                {
                                    //查询刀柄刃具对应关系
                                    var gxb = db.Query<base_dbrjgx, base_rjxx, base_dbrjgx>(SelectRjGx_Sql.ToString(), (ta, tb) =>
                                    {
                                        tb.id = ta.rjid;
                                        ta.baserjxx = tb;
                                        return ta;
                                    }, new { id = item }, splitOn: "tbrjid").FirstOrDefault();
                                    //查询刃具历史信息
                                    var lsquery = db.Query<base_dbrjzx_ls>(Select_His_Sql.ToString(), new { scx = form.scx, dbh = gxb.dbh, rjid = gxb.rjid });
                                    var gwh = db.ExecuteScalar<string>(SBBH_GWH_Sql, new { sbbh = form.sbbh });
                                    base_dbrjzx zxobj = new base_dbrjzx();
                                    zxobj.gcdm = form.gcdm;
                                    zxobj.scx = form.scx;
                                    zxobj.sbbh = form.sbbh;
                                    zxobj.dbh = gxb.dbh;
                                    zxobj.dblx = gxb.dblx;
                                    zxobj.rjid = gxb.rjid;
                                    zxobj.rjlx = gxb.djlx;
                                    zxobj.rjlyr = form.lyr;
                                    zxobj.dblyr = form.lyr;
                                    zxobj.rjlysj = DateTime.Now;
                                    zxobj.dblysj = DateTime.Now;
                                    zxobj.rjbzsm = gxb.baserjxx.rjbzsm;
                                    zxobj.rjazsm = gxb.baserjxx.rjbzsm;
                                    zxobj.rjdqsm = lsquery.Count() > 0 ? lsquery.FirstOrDefault().djdqsm : 0;
                                    zxobj.rjazjgs = 0;
                                    zxobj.dqjgs = lsquery.Count() > 0 ? lsquery.FirstOrDefault().dqjgs : 0;
                                    zxobj.rjrmcs = lsquery.Count() > 0 ? lsquery.FirstOrDefault().rjrmcs : 0;
                                    zxobj.rjzhrmsj = lsquery.Count() > 0 ? lsquery.FirstOrDefault().rjzhrmsj : Convert.ToDateTime(null);
                                    zxobj.gwh = gwh;
                                    zxobj.cxz = lsquery.Count() > 0 ? lsquery.FirstOrDefault().cxz : 0;
                                    db.Execute(Insert_RjZX_Sql.ToString(), zxobj, trans);
                                    db.Execute(Insert_RjZX_Ls_Sql.ToString(), zxobj, trans);
                                    //db.Execute(Inser_BHD_Sql.ToString(), new
                                    //{
                                    //    id = Guid.NewGuid().ToString().Replace("-", ""),
                                    //    scx = zxobj.scx,
                                    //    cjrmc = zxobj.dblyr,
                                    //    djffr = UserInfo.name,
                                    //    sbbh = zxobj.sbbh,
                                    //    gzxx = $"领用刃具:{zxobj.rjlx}",
                                    //    f = "0006",
                                    //    gwh = zxobj.gwh
                                    //}, trans);
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

        public IEnumerable<base_dbrjzx> Search_DbRjZx(sys_dbrjzx_form form)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.gcdm, ta.dbh, ta.scx, (select scxmc ");
                sql.Append(" FROM   base_scxxx ");
                sql.Append(" where  scx = ta.scx ");
                sql.Append(" and    rownum < 2) as scxmc, ta.rjlx, ta.rjbzsm, ta.rjazsm, ta.rjdqsm, ta.rjazjgs, ta.dqjgs, ta.dblysj, ta.dblyr, ta.rjlysj, ta.rjlyr, ta.rjrmcs, ta.rjzhrmsj, ta.id, ta.cxz, ta.gwh,ta.rjwz, ");
                sql.Append(" ta.sbbh, tb.sbmc, tb.sblx, tb.ljlx, ");
                sql.Append(" ta.dbh, tc.dbmc, tc.dblx, tc.bz ");
                sql.Append(" FROM   base_dbrjzx ta, base_sbxx tb, base_dbxx tc ");
                sql.Append(" where  ta.sbbh = tb.sbbh(+) ");
                sql.Append(" and    ta.dbh = tc.dbh(+) ");
                if (!string.IsNullOrEmpty(form.scx))
                {
                    sql.Append(" and ta.scx = :scx ");
                    p.Add(":scx", form.scx, DbType.String, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(form.sbbh))
                {
                    sql.Append(" and ta.sbbh = :sbbh ");
                    p.Add(":sbbh", form.sbbh, DbType.String, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(form.dbh))
                {
                    sql.Append(" and ta.dbh = :dbh ");
                    p.Add(":dbh", form.dbh, DbType.String, ParameterDirection.Input);
                }
                using (var db = new OracleConnection(ConString))
                {
                   var q = db.Query<base_dbrjzx, base_sbxx,base_dbxx, base_dbrjzx>(sql.ToString(),(ta, tb,tc) =>
                    {
                        ta.basesbxx = tb;
                        ta.basedbxx = tc;
                        ta.dbh = tc.dbh;
                        ta.sbbh = tb.sbbh;
                        return ta;
                    },p,splitOn: "sbbh,dbh");

                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool OldToNew(List<base_dbrjzx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_dbrjzx set rjbzsm=(select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid),rjdqsm = 0,rjlysj=sysdate,rjrmcs=0,rjzhrmsj=null,cxz=0 where id in :id");
                //原记录
                StringBuilder oldsql = new StringBuilder();
                oldsql.Append("select * from base_dbrjzx where id in :id");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                string token = ZDToolHelper.TokenHelper.GetToken;
                                var _u = UserInfo;
                                var ids = entitys.Select(t => t.id).ToList();
                                var oldlist = db.Query<base_dbrjzx>(oldsql.ToString(), new { id = ids}).ToList();
                                var ret = db.Execute(sql.ToString(), new { id = ids }, trans);
                                foreach (var item in oldlist)
                                {
                                    //变化点是否处理
                                    var bhdsfcl = db.Query<string>(BHD_Sfcl_Sql, new { sbbh = item.sbbh });
                                    //未处理时在原变化点累加
                                    if (bhdsfcl.Count() > 0)
                                    {
                                        db.Execute(Append_BHD_Sql, new { id = bhdsfcl.First(),gzxx=item.rjlx });
                                    }
                                    else
                                    {
                                        db.Execute(Inser_BHD_Sql.ToString(), new
                                        {
                                            id = Guid.NewGuid().ToString().Replace("-", ""),
                                            scx = item.scx,
                                            cjrmc = _u.name,
                                            djffr = _u.name,
                                            sbbh = item.sbbh,
                                            gzxx = $"刃具更换:{item.rjlx}",
                                            f = "0007",
                                            gwh = item.gwh
                                        });
                                    }
                                    db.Execute(Insert_RjZX_Ls_Sql.ToString(), new
                                    {
                                        gcdm = item.gcdm,
                                        scx = item.scx,
                                        dbh=item.dbh,
                                        sbbh=item.sbbh,
                                        rjlx=item.rjlx,
                                        rjbzsm=item.rjbzsm,
                                        rjazsm=0,
                                        rjdqsm = 0,
                                        rjazjgs = item.rjazjgs,
                                        dqjgs = item.dqjgs,
                                        dblysj = item.dblysj,
                                        dblyr = item.dblyr,
                                        rjlysj = DateTime.Now,
                                        rjlyr = item.rjlyr,
                                        rjrmcs = 0,
                                        rjzhrmsj = Convert.ToDateTime(null),
                                        rjid = item.rjid,
                                        cxz=0,
                                        gwh = item.gwh
                                    }, trans) ;

                                    //db.Execute(Inser_BHD_Sql.ToString(), new
                                    //{
                                    //    id = Guid.NewGuid().ToString().Replace("-", ""),
                                    //    scx = item.scx,
                                    //    cjrmc = item.dblyr,
                                    //    djffr = UserInfo.name,
                                    //    sbbh = item.sbbh,
                                    //    gzxx = $"以旧换新刃具类型:{item.rjlx}",
                                    //    f = "0006",
                                    //    gwh = item.gwh
                                    //}, trans);
                                }
                                trans.Commit();
                                return ret > 0;
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

        public bool GxSmFromBase(List<base_dbrjzx> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update base_dbrjzx set rjbzsm = (select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid),");
            sql.Append(" rjwz = (select jgwz FROM base_rjxx where id = base_dbrjzx.rjid),");
            sql.Append(" cxz = case when rjdqsm/(select rjbzsm FROM base_rjxx where id = base_dbrjzxr.jid) < 0.95 then 0 ");
            sql.Append(" when rjdqsm/(select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid) >=0.95 and rjdqsm/(select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid) <= 1 then 1 ");
            sql.Append(" when rjdqsm/(select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid) >1 then 2 end ");
            sql.Append(" where id = :id ");
            StringBuilder sqlls = new StringBuilder();
            sqlls.Append("update base_dbrjzx_ls set djbzsm = (select rjbzsm FROM base_rjxx where id = base_dbrjzx_ls.rjid)");
            sqlls.Append(" where id = (select max(id) from base_dbrjzx_ls where rjid = :rjid) ");
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

                                db.Execute(sqlls.ToString(), item, trans);
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
        
        public bool GxSm(List<base_dbrjzx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_dbrjzx set rjbzsm = :rjbzsm, ");
                sql.Append(" cxz = case when rjdqsm/:rjbzsm < 0.95 then 0 ");
                sql.Append(" when rjdqsm/:rjbzsm >=0.95 and rjdqsm/:rjbzsm <= 1 then 1 ");
                sql.Append(" when rjdqsm/:rjbzsm >1 then 2 end ");
                sql.Append(" where id = :id ");
                //更新刃具基础表标准寿命
                StringBuilder sqlrjxx = new StringBuilder();
                sqlrjxx.Append("update base_rjxx set rjbzsm = :rjbzsm where id = :rjid");
                //更新流水表
                StringBuilder sqlls = new StringBuilder();
                sqlls.Append("update base_dbrjzx_ls set djbzsm = :rjbzsm where id = ");
                sqlls.Append("(select max(id) FROM base_dbrjzx_ls where scx = :scx and sbbh = :sbbh and dbh = :dbh and rjid = :rjid)");
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
                                    db.Execute(sqlrjxx.ToString(), item, trans);
                                    db.Execute(sqlls.ToString(), item, trans);
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

        public IEnumerable<zxjc_rjrm_ls> View_RmMx(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select rjid,scx,dbh,(select dblx from base_dbxx where dbh = zxjc_rjrm_ls.dbh and rownum =1) as dblx,sbbh,scx,bzsm,dqsm, rjlx, rmsj, rmr from ZXJC_RJRM_LS where rjid = :id order by rmsj desc");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_rjrm_ls>(sql.ToString(), new { id = id });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool LXDHJ(List<base_dbrjzx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_dbrjzx set rjbzsm=(select rjbzsm FROM base_rjxx where id = base_dbrjzx.rjid),rjdqsm = 0,rjlysj=sysdate,rjrmcs=0,rjzhrmsj=null,cxz=0 where id in :id");
                //原记录
                StringBuilder oldsql = new StringBuilder();
                oldsql.Append("select * from base_dbrjzx where id in :id");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                string token = ZDToolHelper.TokenHelper.GetToken;
                                var _u = UserInfo;
                                var ids = entitys.Select(t => t.id).ToList();
                                var oldlist = db.Query<base_dbrjzx>(oldsql.ToString(), new { id = ids }).ToList();
                                var ret = db.Execute(sql.ToString(), new { id = ids }, trans);
                                foreach (var item in oldlist)
                                {
                                    //变化点是否处理
                                    var bhdsfcl = db.Query<string>(BHD_Sfcl_Sql, new { sbbh = item.sbbh });
                                    //未处理时在原变化点累加
                                    if (bhdsfcl.Count() > 0)
                                    {
                                        db.Execute(Append_BHD_Sql, new { id = bhdsfcl.First(), gzxx = item.rjlx });
                                    }
                                    else
                                    {
                                        db.Execute(Inser_BHD_Sql.ToString(), new
                                        {
                                            id = Guid.NewGuid().ToString().Replace("-", ""),
                                            scx = item.scx,
                                            cjrmc = _u.name,
                                            djffr = _u.name,
                                            sbbh = item.sbbh,
                                            gzxx = $"刃具角度更换:{item.rjlx}",
                                            f = "0007",
                                            gwh = item.gwh
                                        });
                                    }
                                    db.Execute(Insert_RjZX_Ls_Sql.ToString(), new
                                    {
                                        gcdm = item.gcdm,
                                        scx = item.scx,
                                        dbh = item.dbh,
                                        sbbh = item.sbbh,
                                        rjlx = item.rjlx,
                                        rjbzsm = item.rjbzsm,
                                        rjazsm = 0,
                                        rjdqsm = 0,
                                        rjazjgs = item.rjazjgs,
                                        dqjgs = item.dqjgs,
                                        dblysj = item.dblysj,
                                        dblyr = item.dblyr,
                                        rjlysj = DateTime.Now,
                                        rjlyr = item.rjlyr,
                                        rjrmcs = 0,
                                        rjzhrmsj = Convert.ToDateTime(null),
                                        rjid = item.rjid,
                                        cxz = 0,
                                        gwh = item.gwh
                                    }, trans);
                                }
                                trans.Commit();
                                return ret > 0;
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
    }
}
