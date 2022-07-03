using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.JTGL;
using ZDMesModels.LBJ;
using Dapper;
using ZDMesModels;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.DZWD
{
    public class JTFPService:BaseDao<zxjc_t_jstcfp>, IJTFP
    {
        public JTFPService(string constr):base(constr)
        {

        }
        public override int Add(IEnumerable<zxjc_t_jstcfp> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into zxjc_t_jstc_user(userid,jtid) values (:userid,:jtid) ");
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    using (var trans = db.BeginTransaction())
                    {
                        try
                        {
                            Db.Insert<zxjc_t_jstcfp>(entitys, trans);
                            foreach (var item in entitys)
                            {
                                foreach (var sitem in item.rylist)
                                {
                                    db.Execute(sql.ToString(), new {userid=sitem,jtid = item.jtid }, trans);
                                }
                            }
                            trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                    
                }
            }
            finally
            {
                Db.Dispose();
            }
        }

        public bool CancleFP(List<zxjc_t_jstc> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from ZXJC_T_JSTC_USER where jtid in :jtids");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("delete from  zxjc_t_jstcfp where jtid in :jtids");
                StringBuilder sql2 = new StringBuilder();
                sql2.Append("update ZXJC_T_JSTC set fp_flg = 'N',fp_sj=null,fpr=null where jtid in :jtids");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        var ids = entitys.Select(t => t.jtid);
                        db.Open();
                        using (var tran = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute(sql.ToString(), new { jtids = ids }, tran);
                                db.Execute(sql1.ToString(), new { jtids = ids }, tran);
                                db.Execute(sql2.ToString(), new { jtids = ids }, tran);
                                tran.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                tran.Rollback();
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

        public override IEnumerable<zxjc_t_jstcfp> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append("select count(id) from ZXJC_T_JSTCFP t where 1=1 ");
                StringBuilder sql = new StringBuilder();
                sql.Append("select t.id, t.jtid, t2.jcbh, t2.jcmc as jtmc,t2.wjlj as wjlj, t.gcdm,(select gcmc from base_gcxx where gcdm = t.gcdm) as gcmc, t.scx,(select scxmc FROM  base_scxxx where scx = t.scx)  as scxmc, t.gwh,(select gwmc FROM  base_gwzd where scx = t.scx and gwh = t.gwh and rownum < 2) as gwmc, t.status_no as statusno, t.bz, t.lrr1, t.lrsj1 ");
                sql.Append(" from ZXJC_T_JSTCFP t, zxjc_t_jstc t2 ");
                sql.Append(" where  t.jtid = t2.jtid ");
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
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstcfp>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_t_jstc> Get_WFP_List()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select * from (select no as jtid, name as jcmc, url as wjlj, trunc(sysdate) as yxqx1, to_date(EXPIRE_DATE, 'yyyy-mm-dd') as yxqx2, 0 as type,(select case when count(id) > 0 then 'Y' else 'N' end ");
                sql.Append(" FROM   zxjc_t_jstcfp ");
                sql.Append(" where  jtid = t.no ) as  fpflg ");
                sql.Append(" from zsdl_jstz @pdm_99 t");
                sql.Append(" where t.release_dept like '零部件%'");
                sql.Append(" and    lx = 'JSTZ'");
                sql.Append(" and    status <> '已作废'");
                sql.Append(" and    to_date(expire_date, 'yyyy-mm-dd') >= sysdate) order by yxqx2 asc");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("select jtid,jcbh, jcmc,jcms, wjlj, yxqx1, yxqx2, 1 as type,fp_flg as fpflg from zxjc_t_jstc where shbz='Y' and fp_flg = 'N' ");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<zxjc_t_jstc>(sql1.ToString()).ToList();
                    //var list1 = db.Query<zxjc_t_jstc>(sql.ToString());
                    //list.AddRange(list1);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update_JtFpZt(List<string> jtids)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_t_jstc set fp_flg='N',fp_sj=null,fpr=null where fp_flg='Y' and jtid in :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    var cnt = db.Execute(sql.ToString(), new { jtid = jtids });
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
