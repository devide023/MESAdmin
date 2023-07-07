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

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarDzgyService : BaseDao<zxjc_t_dzgy>,IBatAtachValue<zxjc_t_dzgy>
    {
        private UserUtilService _userservice;
        public DuCarDzgyService(string constr) : base(constr)
        {
            _userservice = new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_t_dzgy> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.gyid, ta.gybh, ta.gymc, ta.gyms, ta.gcdm, ta.scx, ta.gwh,tb.gwmc, ta.jx_no as jxno, ta.status_no as statusno, ta.wjlj, ta.jwdx, ta.scry, ta.scpc, ta.scsj, ta.bbbh from zxjc_t_dzgy ta, base_gwzd tb ");
                sql_main.Append(" where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_t_dzgy where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_t_dzgy where 1=1 ");
                //
                StringBuilder sqlgwh = new StringBuilder();
                sqlgwh.Append("select scx, gwh, gwmc FROM base_gwzd order by scx asc");
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
                }

                using (var db = new OracleConnection(ConString))
                {
                    var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var q = db.Query<zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gwhs = gwzdlist.Where(t => t.scx == item.scx).Select(t => new sys_options_list() { label = t.gwmc, value = t.gwh }).OrderBy(t => t.value).ToList();
                    }
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_t_dzgy> BatSetValue(List<zxjc_t_dzgy> list)
        {
            try
            {
                list.ForEach(t => t.scry = _userservice.CurrentUser.name);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
