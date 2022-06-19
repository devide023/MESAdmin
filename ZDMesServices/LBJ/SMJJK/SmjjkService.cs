using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.SMJJK
{
    public class SmjjkService : BaseDao<zxjc_smls>
    {
        public SmjjkService(string constr) : base(constr)
        {

        }

        public override IEnumerable<zxjc_smls> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql_cnt = new StringBuilder();
                    sql_cnt.Append("select count(*) from ZXJC_SMLS t where 1=1 ");
                    StringBuilder sql = new StringBuilder();
                    sql.Append("SELECT rowid as rid ,gcdm, scx, status_no as statusno,(select wlmc from base_wlxx where wlbm = ZXJC_SMLS.status_no and rownum < 2) as wlmc, engine_no as engineno, order_no as orderno, bc,smjbs, cpzt, jczpdz, zpjcjg, jcr, jcsj, szbjg, szbry, szbjcsj, lrsj, wcsj, scbz,'N' as sfbh");
                    sql.Append(" FROM ZXJC_SMLS where 1=1 ");
                  
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
                    try
                    {
                        var q = db.Query<zxjc_smls>(OraPager(sql.ToString()), parm.sqlparam);
                        resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                        return q;
                    }
                    catch (OracleException)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<zxjc_smls> entitys)
        {
            try
            {
                List<zxjc_smls> oklist = new List<zxjc_smls>();
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_smls set jczpdz = :jczpdz where rowid = :rid");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        int ret = db.Execute(sql.ToString(), item);
                        if (ret > 0)
                        {
                            oklist.Add(item);
                        }
                    }
                }
                return oklist.Count == entitys.Count();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
