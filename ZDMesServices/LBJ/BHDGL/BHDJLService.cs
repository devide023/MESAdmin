using Autofac.Extras.DynamicProxy;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.BHDGL
{
    public class BHDJLService:BaseDao<lbj_qms_4mbhd>
    {
        public BHDJLService(string constr):base(constr)
        {

        }

        public override IEnumerable<lbj_qms_4mbhd> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, bmbm, scx, scxzx, cjr, cjrmc, cjsj, jt, cpxh, cpmc, bhbw, gzxx, fsddbh, fxddbh, zssl, yzpcl, r, j, l, f, h, c, tsff, qrff, djffry, djffrymc, cxtzry, cxtzrymc, qtbz, czygsdd, czyscsj, czypdjg, czyczr, czyczrmc, czyczsj, scbzgsdd, scbzscsj, scbzpdjg, scbzczr, scbzczrmc, scbzczsj, xcxjgsdd, xcxjscsj, xcxjpdjg, xcxjczr, xcxjczrmc, xcxjczsj, rwzt, djffryqr, cxtzryqr, gcdm, gwh, trig_type as trigtype, change_type as changetype,(select scxzxmc FROM base_scxxx_jj where scx = lbj_qms_4mbhd.scx and scxzx = lbj_qms_4mbhd.scxzx and rownum =1) as scxzxmc from lbj_qms_4mbhd where 1=1 ");
                sql_cnt.Append($"select count(id) from lbj_qms_4mbhd where 1=1 ");

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
                        sql.Append(" order by rwzt asc,trig_type desc,cjsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<lbj_qms_4mbhd>(OraPager(sql.ToString()), parm.sqlparam);
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
