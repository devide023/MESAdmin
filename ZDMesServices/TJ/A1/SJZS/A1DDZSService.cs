using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.SJZS
{
    /// <summary>
    /// 订单追溯
    /// </summary>
    public class A1DDZSService:OracleBaseFixture, IA1DDZS
    {
        public A1DDZSService(string constr):base(constr)
        {

        }

        public IEnumerable<barcode_print> Get_VinList(sys_page parm, out int resultcount)
        {
            try
            {

                resultcount = 0;

                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select vin, to_char(print_time,'yyyy-mm-dd hh24:mi:ss') as print_time, zpjh, order_no, ztbm, write_req, seq_no, printer, status_flag ");
                sql.Append(" from barcode_print where 1=1 ");
                sql_cnt.Append($"select count(*) from barcode_print where 1=1 ");

                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
                else
                {
                    sql.Append(" and order_no in (select order_no FROM tj_pp_sc_zpjh where trunc(zpsj) = trunc(sysdate) and zpx ='1' ) ");
                    sql_cnt.Append(" and order_no in (select order_no FROM tj_pp_sc_zpjh where trunc(zpsj) = trunc(sysdate) and zpx ='1' ) ");
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
                        sql.Append(" order by vin desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<barcode_print>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
