using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels.LBJ;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using static Slapper.AutoMapper;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.DAOJU
{
    public class LBJDbrjLyNewService : OracleBaseFixture, IDBRJZX
    {
        private UserUtilService _uservice;
        public LBJDbrjLyNewService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public IEnumerable<base_dbrjzx> Get_DbRjZx_List(sys_dbrjgx_form form)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select gcdm, dbh, scx, sbbh, rjlx, rjbzsm, rjazsm, rjdqsm, rjazjgs, dqjgs, dblysj, dblyr, rjlysj, rjlyr, rjrmcs, rjzhrmsj, id, rjid, cxz, gwh, rjrmr, rjwz, scxzx ");
                sql.Append(" FROM base_dbrjzx where 1=1 ");
                StringBuilder sqldbxx = new StringBuilder();
                sqldbxx.Append("select dblx,dbmc,dbh FROM base_dbxx where dbh = :dbh ");
                StringBuilder sqlrjxx = new StringBuilder();
                sqlrjxx.Append("select rjlx, rjmc, rjbzsm, jgwz, bz as rjxxbz FROM base_rjxx where id = :rjid");
                StringBuilder sqlsbxx = new StringBuilder();
                sqlsbxx.Append("select sbbh,sbmc,gwh,scx,scxzx FROM  base_sbxx where sbbh = :sbbh ");
                using (var db = new OracleConnection(ConString))
                {
                    DynamicParameters p = new DynamicParameters();
                    if (!string.IsNullOrEmpty(form.scx))
                    {
                        sql.Append(" and scx = :scx  ");
                        p.Add(":scx", form.scx);
                    }
                    if (!string.IsNullOrEmpty(form.scxzx))
                    {
                        sql.Append(" and scxzx = :scxzx  ");
                        p.Add(":scxzx", form.scxzx);
                    }
                    if (!string.IsNullOrEmpty(form.sbbh))
                    {
                        sql.Append(" and sbbh = :sbbh ");
                        p.Add(":sbbh", form.sbbh);
                    }
                    var q = db.Query<base_dbrjzx>(sql.ToString(), p).OrderBy(t=>t.scx).ThenBy(t=>t.scxzx).ThenBy(t=>t.sbbh).ThenBy(t=>t.dbh);
                    foreach (var item in q)
                    {
                        item.baserjxx = db.Query<base_rjxx>(sqlrjxx.ToString(), new { rjid = item.rjid }).FirstOrDefault();
                        item.basedbxx = db.Query<base_dbxx>(sqldbxx.ToString(), new { dbh = item.dbh }).FirstOrDefault();
                        item.basesbxx = db.Query<base_sbxx>(sqlsbxx.ToString(), new { sbbh = item.sbbh }).FirstOrDefault();
                    }
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_DbRjZx_New(sys_save_dbrjzx_form form)
        {
            try
            {   
                //1、保存刀柄刃具关系表
                StringBuilder sql_dbrjgx = new StringBuilder();
                sql_dbrjgx.Append(" insert into BASE_DBRJGX(gcdm, cpzt, dbh, dblx, rjid, djlx, jgwz) ");
                sql_dbrjgx.Append(" select '9902',:cpzt,:dbh,(select dblx from base_dbxx where dbh = :dbh),:rjid,(select rjlx from base_rjxx where id = :rjid), ");
                sql_dbrjgx.Append(" (select jgwz from base_rjxx where id = :rjid ) from dual where not exists( ");
                sql_dbrjgx.Append(" select * FROM BASE_DBRJGX where cpzt = :cpzt and dbh = :dbh and djlx = (select rjlx from base_rjxx where id = :rjid ) ");
                sql_dbrjgx.Append(" )  ");
                //2、寿命消耗
                StringBuilder sql_smxh = new StringBuilder();
                sql_smxh.Append(" insert into base_rjsmxh");
                sql_smxh.Append(" (gcdm, rjlx, cpzt, scx, sbbh, mjxhsm)");
                sql_smxh.Append(" select '9902', :rjlx, :cpzt, :scx, :sbbh, 1  ");
                sql_smxh.Append(" from dual where not exists ");
                sql_smxh.Append(" (select * ");
                sql_smxh.Append(" from base_rjsmxh ");
                sql_smxh.Append(" where scx = :scx ");
                sql_smxh.Append(" and    cpzt = :cpzt");
                sql_smxh.Append(" and    rjlx = :rjlx");
                sql_smxh.Append(" and    sbbh = :sbbh) ");
                //3、保存刀柄刃具在线表
                StringBuilder sql_dbrjzx = new StringBuilder();
                sql_dbrjzx.Append(" insert into base_dbrjzx ");
                sql_dbrjzx.Append(" (gcdm, dbh, scx, sbbh, rjlx, rjbzsm,rjazsm, rjdqsm, dblysj, dblyr, rjlysj, rjlyr, rjid, gwh, rjwz, scxzx) ");
                sql_dbrjzx.Append(" values ");
                sql_dbrjzx.Append(" ('9902', :dbh, :scx, :sbbh, :rjlx, :rjbzsm,:rjbzsm, :rjdqsm, sysdate, :dblyr, sysdate, :dblyr, :rjid, (select gwh FROM base_sbxx where sbbh = :sbbh),(select jgwz FROM base_rjxx where id = :rjid and rownum=1), :scxzx) ");
                //4、保存刀柄刃具流水表
                StringBuilder sql_dbrjzx_ls = new StringBuilder();
                sql_dbrjzx_ls.Append(" insert into base_dbrjzx_ls ");
                sql_dbrjzx_ls.Append(" (gcdm, scx, dbh, sbbh, djlx, djbzsm,djazsm, djdqsm, dblysj, dblyr, rjlysj, rjlyr, rjid, gwh, lrsj, scxzx) ");
                sql_dbrjzx_ls.Append(" values ");
                sql_dbrjzx_ls.Append(" ('9902', :scx, :dbh, :sbbh, :rjlx, :rjbzsm,:rjbzsm, :rjdqsm, sysdate, :dblyr, sysdate, :dblyr, :rjid, (select gwh FROM base_sbxx where sbbh = :sbbh and rownum=1), sysdate, :scxzx)  ");
                //5、查询历史寿命
                StringBuilder sql_lssmcx = new StringBuilder();
                sql_lssmcx.Append("select rjdqsm FROM base_dbrj_lssm where smlx = 1 and scx = :scx and scxzx = :scxzx and sbbh = :sbbh and dbh = :dbh and rjid = :rjid order by id desc ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in form.dbrjzx)
                                {
                                    //查询卸载时的寿命
                                    var sfcz_rjlssm = db.Query<int>(sql_lssmcx.ToString(), new {scx=item.scx,scxzx = item.scxzx,sbbh = item.sbbh,dbh=item.dbh,rjid= item.rjid });
                                    if (sfcz_rjlssm.Count() > 0)
                                    {
                                        item.rjdqsm = sfcz_rjlssm.First();
                                    }
                                    db.Execute(sql_dbrjgx.ToString(), new { cpzt = form.wlbm, dbh = item.dbh, rjid = item.rjid }, trans);
                                    db.Execute(sql_smxh.ToString(), new { cpzt = form.wlbm, scx = item.scx, rjlx = item.rjlx, sbbh = item.sbbh },trans);
                                    db.Execute(sql_dbrjzx.ToString(), new { dbh = item.dbh, scx = item.scx, sbbh = item.sbbh, rjlx = item.rjlx, rjbzsm = item.rjbzsm, rjdqsm = item.rjdqsm, dblyr = _uservice.CurrentUser.name, rjid = item.rjid, scxzx = item.scxzx }, trans);
                                    db.Execute(sql_dbrjzx_ls.ToString(), new { dbh = item.dbh, scx = item.scx, sbbh = item.sbbh, rjlx = item.rjlx, rjbzsm = item.rjbzsm, rjdqsm = item.rjdqsm, dblyr = _uservice.CurrentUser.name, rjid = item.rjid, scxzx = item.scxzx }, trans);
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
    }
}
