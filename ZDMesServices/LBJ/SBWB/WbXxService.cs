using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ZDMesInterfaces.LBJ.SBWB;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.SBWB
{
    public class WbXxService:BaseDao<base_sbwb>
    {
        private ISBGW _sbgw;
        public WbXxService(string constr, ISBGW sbgw) :base(constr)
        {
            _sbgw = sbgw;
        }
        public override int Add(IEnumerable<base_sbwb> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        item.gwh = _sbgw.GetGWH_By_Sbbh(item.sbbh);
                        item.autoid = Guid.NewGuid().ToString();
                    }
                    return base.Add(entitys);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IEnumerable<base_sbwb> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select autoid, gcdm, wbsh, wbxx, bz, lrr, lrsj, scbz,scx,sbbh ");
                sql.Append(" from base_sbwb ");
                sql.Append(" where 1 = 1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from base_sbwb  where 1=1 ");
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
                        sql.Append($" order by scx asc,sbbh asc,wbsh asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<base_sbwb>(OraPager(sql.ToString()), parm.sqlparam);
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
