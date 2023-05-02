using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.SJBL;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.CPGL
{
    public class LBJGdxxService: BaseDao<zxjc_gdxxb>,IZxjcData
    {
        public LBJGdxxService(string constr):base(constr)
        {

        }
        public override IEnumerable<zxjc_gdxxb> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select gcdm, scx, gwh, engine_no, order_no, status_no, lrsj, isgdxx, autoid, ishege, gwxh ");
                sql.Append("  from zxjc_gdxxb where gwh = 'OP10' ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_gdxxb where gwh = 'OP10' ");
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
                    else
                    {
                        sql.Append($" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_gdxxb>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_data_list> Get_Lbj_Zxjc_Data_List(sys_page parm)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select jcz, scx, gwh, order_no, vin_jj, ztbm, jcry, jcjg, jcsj FROM   zxjc_data_list ");
                sql.Append(" where 1=1 ");
                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
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
                    else
                    {
                        sql.Append($" order by jcsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_data_list>(sql.ToString(), parm.sqlparam);
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
