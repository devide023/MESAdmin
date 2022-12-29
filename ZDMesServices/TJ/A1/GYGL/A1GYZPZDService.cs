using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1.GYZD;

namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1GYZPZDService:BaseDao<zxjc_t_dzgy>, IBatAtachValue<zxjc_t_dzgy>
    {
        public A1GYZPZDService(string constr) : base(constr)
        {

        }

        public override IEnumerable<zxjc_t_dzgy> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='装配指导' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='装配指导' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
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
                foreach (var item in list)
                {
                    item.scry = item.lrr;
                    item.scsj = DateTime.Now;
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
