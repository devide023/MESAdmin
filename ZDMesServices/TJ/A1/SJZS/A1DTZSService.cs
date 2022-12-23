using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.SJZS
{
    /// <summary>
    /// 单台追溯
    /// </summary>
    public class A1DTZSService:BaseDao<zxjc_data_list8>, IA1ZPMX
    {
        public A1DTZSService(string constr) :base(constr)
        {

        }

        public override IEnumerable<zxjc_data_list8> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select engine_no as engineno, status_no as statusno, bill_no as billno, order_no as orderno, gwh, to_char(jcsj, 'yyyy-mm-dd hh24:mi:ss') as jcsj, ");
                sql.Append(" decode((select count(*) from zxjc_ryxx where user_code=zxjc_data_list8.jcry),0,jcry,(select user_name from zxjc_ryxx where user_code=zxjc_data_list8.jcry and rownum = 1))as jcry, jcjg, fx_flg as fxflg, jcz, gcdm, scx, sbbh, sblx, gy_min as gymin, gy_max as gymax, gy_bz as gybz, jx_no as jxno from zxjc_data_list8 where 1=1 ");
                sql_cnt.Append($"select count(*) from zxjc_data_list8 where 1=1 ");

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
                        sql.Append(" order by jcsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_data_list8>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_data_detail> Get_Detail(zxjc_data_list8 parm)
        {
            try
            {
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append(" select guid, gcdm, scx, engine_no as engineno, lsjs, status_no as statusno, bill_no as billno, order_no as orderno, gwh, jcsj,");
                sqlmx.Append(" decode((select count(*) from zxjc_ryxx where user_code=ZXJC_DATA_DETAIL.jcry),0,jcry,(select user_name from zxjc_ryxx where user_code=ZXJC_DATA_DETAIL.jcry and rownum = 1))as jcry, jcjg, channel, program, cycle, data1, data2, data3, data4, dup, sbbh, sblx, resultid, scbz, ycyy");
                sqlmx.Append(" from ZXJC_DATA_DETAIL where engine_no = :jh and gwh = :gwh and sbbh = :sbbh ");
                sqlmx.Append(" order by jcsj desc ");
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_data_detail>(sqlmx.ToString(), new { jh = parm.engineno, gwh = parm.gwh, sbbh = parm.sbbh });
                    return q;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_data_detail_mx> Get_ZPMX_List(zxjc_data_list8 parm)
        {
            try
            {
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append(" select guid, gcdm, scx, engine_no as engineno, lsjs, status_no as statusno, bill_no as billno, order_no as orderno, gwh, jcsj,");
                sqlmx.Append(" decode((select count(*) from zxjc_ryxx where user_code=ZXJC_DATA_DETAIL_MX.jcry),0,jcry,(select user_name from zxjc_ryxx where user_code=ZXJC_DATA_DETAIL_MX.jcry and rownum = 1))as jcry, jcjg, channel, program, cycle, data1, data2, data3, data4, dup, sbbh, sblx, resultid, scbz, ycyy");
                sqlmx.Append(" from ZXJC_DATA_DETAIL_MX where engine_no = :jh and gwh = :gwh and sbbh = :sbbh ");
                sqlmx.Append(" order by jcsj desc ");

                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_data_detail_mx>(sqlmx.ToString(), new { jh = parm.engineno, gwh = parm.gwh, sbbh = parm.sbbh });
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
