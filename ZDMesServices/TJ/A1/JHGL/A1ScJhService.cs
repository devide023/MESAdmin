using Aspose.Cells;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.JHGL
{
    public class A1ScJhService:BaseDao<tj_pp_sc_zpjh>
    {
        public A1ScJhService(string constr):base(constr)
        {
            
        }

        public override IEnumerable<tj_pp_sc_zpjh> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder gylxsql= new StringBuilder();
                gylxsql.Append("select count(*) FROM mes_zxjc_gylx where scx = :scx and status_no =:ztbm and jx_no = :jxno ");

                StringBuilder gylxsql1 = new StringBuilder();
                gylxsql1.Append("select count(*) FROM mes_zxjc_gylx where scx = :scx and status_no is null and jx_no = :jxno ");

                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select zpjhh, order_no, scx, xh, zpx, scxh, scbc, scsl, scsj, zpsl, zpsj, sxsl, hgsl, fxsl, wxssl, bz, ztbm, jx, status_flag,sap_zt, csbm, (select name1 FROM T_TJ_SD_KHDAXX where kunnr = tj_pp_sc_zpjh.csbm and rownum=1) as khmc, xsbz, jtdh, jssj, jhh, cqyy ");
                sql.Append(" from tj_pp_sc_zpjh where  STATUS_FLAG = '1' ");
                sql_cnt.Append($" select count(zpjhh) from tj_pp_sc_zpjh where STATUS_FLAG = '1'  ");

                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
                else
                {
                    sql.Append("  and scbc = '白班' ");
                    sql.Append(" and zpsj between to_date(to_char(trunc(sysdate),'yyyy-mm-dd')||' 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date(to_char(trunc(sysdate),'yyyy-mm-dd')||' 23:59:59','yyyy-mm-dd hh24:mi:ss')");
                    sql_cnt.Append(" and scbc = '白班' ");
                    sql_cnt.Append(" and zpsj between to_date(to_char(trunc(sysdate),'yyyy-mm-dd')||' 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date(to_char(trunc(sysdate),'yyyy-mm-dd')||' 23:59:59','yyyy-mm-dd hh24:mi:ss') ");
                    
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
                        sql.Append($" order by zpsj desc nulls last "); 
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<tj_pp_sc_zpjh>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gylx_jx_qty = db.ExecuteScalar<int>(gylxsql1.ToString(), new { scx = item.zpx, jxno = item.jx });
                        item.gylx_ztbm_qty = db.ExecuteScalar<int>(gylxsql.ToString(), new { scx = item.zpx, ztbm=item.ztbm, jxno = item.jx });
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
    }
}
