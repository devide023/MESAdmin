using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.JTGL;
using ZDMesModels.LBJ;
using Dapper;
using ZDMesModels;

namespace ZDMesServices.LBJ.DZWD
{
    public class JTFPService:BaseDao<zxjc_t_jstcfp>, IJTFP
    {
        public JTFPService(string constr):base(constr)
        {

        }

        public override IEnumerable<zxjc_t_jstcfp> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append("select count(id) from ZXJC_T_JSTCFP t where 1=1 ");
                StringBuilder sql = new StringBuilder();
                sql.Append("select t.id, t.jtid, t4.name as jtmc,t4.url as wjlj, t.gcdm, t1.gcmc, t.scx, t2.scxmc, t.gwh, t3.gwmc, t.status_no as statusno, t.bz, t.lrr1, t.lrsj1 ");
                sql.Append(" from ZXJC_T_JSTCFP t, base_gcxx t1, base_scxxx t2, base_gwzd t3,zsdl_jstz@pdm_99 t4 ");
                sql.Append(" where  t.gcdm = t1.gcdm(+) ");
                sql.Append(" and    t.scx = t2.scx(+) ");
                sql.Append(" and    t.gwh = t3.gwh(+) ");
                sql.Append(" and    t.jtid = t4.no(+) ");
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
                var q = Db.Connection.Query<zxjc_t_jstcfp>(OraPager(sql.ToString()),parm.sqlparam);
                resultcount = Db.Connection.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                return q;
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
                sql1.Append("select jtid, jcmc, wjlj, yxqx1, yxqx2, 1 as type,fp_flg as fpflg from zxjc_t_jstc where fp_flg = 'N' ");
                var list = Db.Connection.Query<zxjc_t_jstc>(sql1.ToString()).ToList();
                var list1 = Db.Connection.Query<zxjc_t_jstc>(sql.ToString());
                list.AddRange(list1);
                return list.Where(t=>t.fpflg=="N");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
