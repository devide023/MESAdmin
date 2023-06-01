using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarDjxxService : BaseDao<zxjc_djgw>,IDuCarDjgw, IBatAtachValue<zxjc_djgw>
    {
        private UserUtilService _uservice;
        public DuCarDjxxService(string constr) : base(constr)
        {
            _uservice= new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_djgw> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.gcdm, ta.scx, ta.gwh,tb.gwmc, ta.jx_no as jxno, ta.status_no as statusno, ta.djno, ta.djxx, ta.scbz, ta.lrr, ta.lrsj, ta.djlx from zxjc_djgw ta, base_gwzd tb ");
                sql_main.Append(" where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_djgw where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_djgw where 1=1 ");

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
                    var q = db.Query<zxjc_djgw>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_djgw> BatSetValue(List<zxjc_djgw> list)
        {
            try
            {
                list.ForEach(t =>
                {
                    t.djno = Create_DJNo();
                    t.lrr = _uservice.CurrentUser.name;
                });
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Create_DJNo()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_djno.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "DJ" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
