using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.RyMgr
{
    public class DuCarJxglService : BaseDao<sc_jxgl>,IBatAtachValue<sc_jxgl>
    {
        private UserUtilService _uservice;
        public DuCarJxglService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public List<sc_jxgl> BatSetValue(List<sc_jxgl> list)
        {
            try
            {
                list.ForEach(t => t.lrr = _uservice.CurrentUser.name);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IEnumerable<sc_jxgl> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql_main.Append($"select ta.id,ta.user_code as usercode,tb.user_name as username,");
                sql_main.Append(" ta.scx, ta.gwh,tc.gwmc, ta.wtd, ta.khyy, ta.lx, ta.jlje, ta.cfje, ta.lrr, ta.lrsj, ta.fsrq, ta.bz from sc_jxgl ta,zxjc_ryxx tb,base_gwzd tc  ");
                sql_main.Append(" where ta.user_code = tb.user_code(+) ");
                sql_main.Append(" and ta.scx = tb.scx(+) ");
                sql_main.Append(" and ta.scx = tc.scx(+) ");
                sql_main.Append(" and ta.gwh = tc.gwh(+) ");
                //
                sql.Append("select * from (");
                sql.Append(sql_main);
                sql.Append(") sc_jxgl where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) sc_jxgl where 1=1 ");

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
                    if (parm.default_order_colname != null && !string.IsNullOrEmpty(parm.default_order_colname))
                    {
                        sql.Append($" order by {parm.default_order_colname} desc ");
                    }
                    else
                    {
                        sql.Append($" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<sc_jxgl>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<sc_jxgl> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from sc_jxgl where id in :ids");
                using (var db = new OracleConnection(ConString))
                {
                    var ids = entitys.Select(t => t.id).ToArray();
                    var cnt = db.Execute(sql.ToString(), new { ids = ids });
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
