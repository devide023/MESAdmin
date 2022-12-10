using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesInterfaces.TJ;
using ZDMesInterfaces.Common;
using Microsoft.Extensions.Primitives;
using ZDMesModels;
using System.Net.Http;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JsTzService:BaseDao<zxjc_t_jstc>, IJTFPSCX
    {
        public A1JsTzService(string constr):base(constr)
        {
        }

        

        public bool CanRemove(string jcbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(jtid) from zxjc_t_jstcfp where jtid = :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.ExecuteScalar<int>(sql.ToString(), new { jtid = jcbh }) == 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_t_jstc> Get_PDM_JSTZ_List(sys_page parm, out int resultcount)
        {
            try
            {
                int uid = 0;
                using (var db = new OracleConnection(ConString))
                {
                    var token = ZDToolHelper.TokenHelper.GetToken;
                    var qu = db.Query<mes_user_entity>("select id,name from mes_user_entity where token = :token", new { token = token });
                    if (qu.Count() > 0)
                    {
                        uid = qu.First().id;
                    }
                    StringBuilder sql_cnt = new StringBuilder();
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select * from ");
                    sql.Append(" (select jtid,jtly,wjfl,no as jcbh,name as jcmc,url as wjlj,creator as scry,");
                    sql.Append(" cjsj as scsj,expire_date as yxqx2,(select lrr from   zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no and  rownum =1) as fpr,");
                    sql.Append(" (select lrsj from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpsj,");
                    sql.Append(" (select fp_flg from zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpflg,");
                    sql.Append(" (select count(jcbh) from   zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no) as sffp ");
                    if (uid != 0)
                    {
                        sql.Append(" ,(select count(*) from mes_pdm_jstz_yd where jcbh = no and ydrid =" + uid + ") as rcnt ");
                    }
                    else
                    {
                        sql.Append(" ,0 as rcnt ");
                    }
                    sql.Append(" FROM v_zxjc_t_jstc) m where (m.fpflg = 'N' or m.sffp=0) ");
                    sql_cnt.Append("select count(m.jcbh) from ( ");
                    sql_cnt.Append(" select jtly,wjfl,no as jcbh,name as jcmc,url as wjlj,creator as scry,cjsj as scsj,expire_date as yxqx2,");
                    sql_cnt.Append(" (select lrr from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum =1) as fpr,");
                    sql_cnt.Append(" (select lrsj from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpsj,");
                    sql_cnt.Append(" (select fp_flg from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpflg,");
                    sql_cnt.Append(" (select count(jcbh) from   zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no) as sffp FROM v_zxjc_t_jstc) m ");
                    sql_cnt.Append(" where (m.fpflg = 'N' or m.sffp=0) ");
                    if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                    {
                        sql.Append(" and " + parm.sqlexp);
                        sql_cnt.Append(" and " + parm.sqlexp);
                    }
                    //前端排序
                    if (parm.orderbyexp != null && !string.IsNullOrWhiteSpace(parm.orderbyexp))
                    {
                        sql.Append(parm.orderbyexp);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(parm.sqlconfig.sql_orderby))
                        {
                            sql.Append("order by m.scsj desc nulls last");
                        }
                    }

                    var q = db.Query<zxjc_t_jstc>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Read_PDM_JSTZ(string jcbh)
        {
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;                
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into mes_pdm_jstz_yd ");
                sql.Append("(jcbh, ydr, ydsj, ydrid) ");
                sql.Append(" select :jcbh, :ydr, sysdate, :ydrid from dual where not exists (select * from mes_pdm_jstz_yd where jcbh = :jcbh and ydrid= :ydrid )");
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<mes_user_entity>("select id,name from mes_user_entity where token = :token", new { token = token });
                    if (q.Count() > 0)
                    {
                        int uid = q.First().id;
                        string uname = q.First().name;
                        db.Execute(sql.ToString(), new { jcbh = jcbh, ydr = uname, ydrid = uid });
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
