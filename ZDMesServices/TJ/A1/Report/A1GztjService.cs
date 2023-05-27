using Aspose.Cells.Drawing;
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

namespace ZDMesServices.TJ.A1.Report
{
    public class A1GztjService : OracleBaseFixture, IA1Report
    {
        public A1GztjService(string constr) : base(constr)
        {
        }

        public IEnumerable<report_fxjlb> Get_FXJLB(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                DynamicParameters dp = new DynamicParameters();
                sql_cnt.Append("select count(*) from zxjc_gwzd_fxmx where 1=1 ");
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                if (scx.Count() > 0)
                {
                    sqlwhere.Append("  and ta.scx = :scx ");
                    sql_cnt.Append(" and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0)
                {
                    sqlwhere.Append("and  ta.jcsj >=  trunc(:rq1) ");
                    sql_cnt.Append(" and jcsj >=  trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if (rq2.Count() > 0)
                {
                    sqlwhere.Append("and  ta.jcsj <= trunc(:rq2)+1 ");
                    sql_cnt.Append(" and jcsj <=  trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                sql.Append(" select ta.engine_no as vin, (select scxmc ");
                sql.Append(" FROM   tj_base_scxxx ");
                sql.Append(" where  scx = ta.scx) as scx, ta.gwh, td.jx as jxno, ta.status_no, ta.gzxx,ta.yyfx,ta.sjbjbm as wlmc,ta.sjbjcs as gys,ta.clfs,(select user_name from zxjc_ryxx where user_code = ta.fxr) as fxr, ta.jcbj, tc.user_name as jcry, ta.jcsj,tb.fault_fl as fxlx,tb.fault_name as faultname  ");
                sql.Append(" FROM zxjc_gwzd_fxmx ta, zxjc_fault tb, zxjc_ryxx tc,tj_pp_sc_zpjh td  ");
                sql.Append(" where  ta.fault_no = tb.fault_no(+)  ");
                sql.Append(" and ta.jcry = tc.user_code(+) ");
                sql.Append(" and ta.order_no = td.order_no(+) ");
                sql.Append(sqlwhere);
                sql.Append(" order by ta.jcsj desc ");
                //
                IEnumerable <report_fxjlb> jclist = new List<report_fxjlb>();
                using (var db = new OracleConnection(ConString))
                {
                    jclist = db.Query<report_fxjlb>(OraPager(sql.ToString()), dp);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), dp);
                    return jclist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<report_gsjdbsy> Get_GSJDBSY(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select t1.*,t2.sjjg,t2.sjry,t2.sjsj,t2.yy,t2.clyj FROM ");
                sql.Append(" ( ");
                sql.Append(" select (select scxmc FROM tj_base_scxxx where scx = ta.scx ) as scx, ta.engine_no as vin,ta.jx_no as jxno,ta.status_no as ztbm,to_date(to_char(ta.jcsj, 'yyyy-mm-dd hh24:mi:ss'), 'yyyy-mm-dd hh24:mi:ss') as gjsj, jcjg as gjjg,tb.user_name as gjry FROM zxjc_data_list8 ta,zxjc_ryxx tb where ta.jcry = tb.user_code and ta.gwh in ");
                sql.Append(" (select distinct gwh FROM base_gwzd where work_flow = 24 ) ");
                //
                sql_cnt.Append("select count(*) from zxjc_data_list8 where gwh in (select distinct gwh FROM base_gwzd where work_flow = 24) ");
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                if (scx.Count() > 0)
                {
                    sqlwhere.Append("  and ta.scx = :scx ");
                    sql_cnt.Append(" and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0 ) {
                    sqlwhere.Append("and  ta.jcsj >=  trunc(:rq1) ");
                    sql_cnt.Append(" and jcsj >=  trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if(rq2.Count() > 0)
                {
                    sqlwhere.Append("and  ta.jcsj <= trunc(:rq2)+1 ");
                    sql_cnt.Append(" and jcsj <=  trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                sql.Append(sqlwhere);
                sql.Append("  )  t1,");
                sql.Append("  (");
                sql.Append("  select aa.*,bb.gzxx as yy, bb.clfs as clyj FROM ");
                sql.Append(" (select ta.engine_no as vin, ta.jcjg as sjjg, tb.user_name as sjry, to_date(to_char(ta.jcsj, 'yyyy-mm-dd hh24:mi:ss'), 'yyyy-mm-dd hh24:mi:ss') as sjsj FROM zxjc_data_list8 ta, zxjc_ryxx tb where ta.jcry = tb.user_code and ta.gwh in (select distinct gwh FROM base_gwzd where gwmc like '%水检%' ) ");
                sql.Append(sqlwhere);
                sql.Append(" ) aa,zxjc_gwzd_fxmx bb where aa.vin = bb.engine_no(+) ");
                sql.Append(" ) t2 ");
                sql.Append(" where t1.vin = t2.vin(+) order by t1.gjsj desc ");
                IEnumerable<report_gsjdbsy> jclist = new List<report_gsjdbsy>();
                using (var db = new OracleConnection(ConString) )
                {
                    jclist = db.Query<report_gsjdbsy>( OraPager(sql.ToString()), dp);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), dp);
                    return jclist;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<report_gztj> Get_GzTj(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct t1.*, t2.gwh, t2.jcjg ");
                sql.Append("FROM(select tb.jx, tb.ztbm, tb.zpx, ta.vin ");
                sql.Append("from barcode_print ta, tj_pp_sc_zpjh tb ");
                sql.Append(" where ta.order_no = tb.order_no ");
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value)); 
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                //故障明细统计
                StringBuilder sql_gzmx = new StringBuilder();
                sql_gzmx.Append("select t1.*, t2.write_req as jxno ");
                sql_gzmx.Append("FROM (select bb.print_time,aa.engine_no, aa.fault_no as faultno, aa.status_no as statusno, (select fault_name ");
                sql_gzmx.Append("  FROM   zxjc_fault ");
                sql_gzmx.Append("  where  fault_no = ");
                sql_gzmx.Append("         aa.fault_no ");
                sql_gzmx.Append("  and    rownum = 1) as faultname ");
                sql_gzmx.Append(" FROM zxjc_gwzd_fxmx aa,barcode_print bb where aa.engine_no = bb.vin  ");
                
                DynamicParameters dp = new DynamicParameters();
                if (rq1.Count() > 0)
                {
                    sql.Append("and  ta.print_time >=  trunc(:rq1) ");
                    sql_gzmx.Append(" and bb.print_time >= trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if (rq2.Count() > 0)
                {
                    sql.Append("and  ta.print_time <= trunc(:rq2) ");
                    sql_gzmx.Append(" and bb.print_time <= trunc(:rq2) + 1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                if (scx.Count() > 0)
                {
                    sql.Append("and    tb.zpx = :scx ");
                    sql_gzmx.Append(" and aa.scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                sql.Append(" ) t1, zxjc_data_list8 t2 ");
                sql.Append("where t1.vin = t2.engine_no ");
                //
                
                sql_gzmx.Append(" ) t1, barcode_print t2 ");
                sql_gzmx.Append(" where t1.engine_no = t2.vin ");
                IEnumerable <report_jc_data_item> jclist = new List<report_jc_data_item>();
                List< report_gztj > result_list = new List<report_gztj>();
                using (var db = new  OracleConnection(ConString))
                {
                    jclist = db.Query<report_jc_data_item>(sql.ToString(), dp);
                    var jxlist = jclist.GroupBy(t => t.jx);
                    foreach (var jx in jxlist)
                    {
                        var jcsl = jclist.Where(t => t.jx == jx.Key).Select(t => t.vin).Distinct().Count();
                        var bhgsl = jclist.Where(t => t.jx == jx.Key && t.jcjg == "不合格").Select(t =>  t.vin).Distinct().Count();
                        double hgl = 0;
                        if (jcsl > 0)
                        {
                            hgl = Math.Round(Convert.ToDouble(Convert.ToDouble(jcsl - bhgsl) / Convert.ToDouble(jcsl)) * 100, 2);
                        }
                        var gzmxlist = db.Query<report_gxmx_data_item>(sql_gzmx.ToString(), dp);
                        var gzgroup = gzmxlist.Where(t => t.jxno == jx.Key).GroupBy(t => new { t.faultno, t.faultname });
                        List<report_gzlx_item> gzlxtj = new List<report_gzlx_item>();
                        foreach (var gz in gzgroup)
                        {
                            gzlxtj.Add(new report_gzlx_item()
                            {
                                faultno = gz.Key.faultno,
                                faultname = gz.Key.faultname,
                                sl = gzmxlist.Where(t=>t.jxno == jx.Key && t.faultno == gz.Key.faultno).Count(),
                            });
                        }
                        result_list.Add(new report_gztj()
                        {
                            jxno = jx.Key,
                            jcsl = jcsl,
                            ngsl = bhgsl,
                            hgsl = jcsl - bhgsl,
                            hgl = hgl,
                            children = gzlxtj
                        });
                    }
                }
                resultcount = result_list.Count;
                return result_list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<report_jcjg> Get_JCJG(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqltemp = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                var jclx = parm.search_condition.Where(t => t.colname == "jclx" && !string.IsNullOrEmpty(t.value));
                var orderno = parm.search_condition.Where(t => t.colname == "orderno" && !string.IsNullOrEmpty(t.value));
                var jhh = parm.search_condition.Where(t => t.colname == "jjh" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                
                sql.Append(" select rownum as rowno, ta.jclb, ta.jcyq, ta.xh, td.orderno, td.jxno, td.statusno, td.zpjhh, td.firstvin, td.firstjcjg, td.lastvin, td.lastjcjg ");
                sqltemp.Append(" FROM(select jclx, jclb, jcyq, xh  ");
                sqltemp.Append(" FROM   zxjc_jcjcxx where 1=1 ");                
                if (scx.Count() > 0)
                {
                    sqlwhere.Append(" and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (jclx.Count() > 0)
                {
                    sqltemp.Append(" and  jclx = :jclx ");
                    dp.Add(":jclx", jclx.First().value);
                }
                sqltemp.Append(sqlwhere);
                sqltemp.Append(" ) ta, (select tb.jclx, tb.jclb, tb.jcyq, tb.jcjg as firstjcjg, tb.zpjhh, tb.orderno, tb.jxno, tb.statusno, tb.firstvin, tc.lastvin, tc.jcjg as lastjcjg  ");
                sqltemp.Append(" FROM(select jclx, jclb, jcyq, jcjg, zpjhh, order_no as orderno, jx_no as jxno, status_no as statusno, engine_no as firstvin  ");
                sqltemp.Append(" from zxjc_jcjg t  ");
                sqltemp.Append(" where smjbs = 'S' ");
               
                if (orderno.Count() > 0)
                {
                    sqlwhere.Append(" and order_no = :orderno ");
                    dp.Add(":orderno", orderno.First().value);
                }
                if (jhh.Count() > 0)
                {
                    sqlwhere.Append(" and zpjjh = :jhh ");
                    dp.Add(":jhh", jhh.First().value);
                }
                sqltemp.Append(sqlwhere);
                sqltemp.Append(") tb, ");
                sqltemp.Append(" (select jclx, jclb, jcyq, jcjg, zpjhh, order_no as orderno, jx_no as jxno, status_no as statusno, engine_no as lastvin  ");
                sqltemp.Append(" from zxjc_jcjg t  ");
                sqltemp.Append(" where smjbs = 'M' ");
                sqltemp.Append(sqlwhere);
                sqltemp.Append(" ) tc  ");
                sqltemp.Append(" where  tb.orderno = tc.orderno(+)  ");
                sqltemp.Append(" and tb.zpjhh = tc.zpjhh(+)  ");
                sqltemp.Append(" and tb.jclx = tc.jclx(+)  ");
                sqltemp.Append(" and tb.jclb = tc.jclb(+) ");
                sqltemp.Append(" and tb.jcyq = tc.jcyq(+) ");
                sqltemp.Append(" ) td  ");
                sqltemp.Append(" where  ta.jclx = td.jclx(+)  ");
                sqltemp.Append(" and ta.jclb = td.jclb(+)  ");
                sqltemp.Append(" and ta.jcyq = td.jcyq(+)  ");
                //
                sql.Append(sqltemp);
                sql_cnt.Append("select count(*) ");
                sql_cnt.Append(sqltemp);
                IEnumerable<report_jcjg> jclist = new List<report_jcjg>();
                using (var db = new OracleConnection(ConString))
                {
                    jclist = db.Query<report_jcjg>(OraPager(sql.ToString()), dp);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), dp);
                    return jclist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<report_jcmx> Get_JCMX(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select * from (select rownum as rowno, jclb, xh, jcyq, jcjg from ZXJC_JCMX where 1= 1");
                sql_cnt.Append("select count(*) from ZXJC_JCMX where 1= 1 ");
                var jclx = parm.search_condition.Where(t => t.colname == "jclx" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);

                if (scx.Count() > 0)
                {
                    sqlwhere.Append(" and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (jclx.Count() > 0)
                {
                    sqlwhere.Append(" and jclx = :jclx ");
                    dp.Add(":jclx", jclx.First().value);
                }
                sql.Append(sqlwhere);
                sql.Append(") order by rowno asc,xh asc ");
                sql_cnt.Append(sqlwhere);
                IEnumerable<report_jcmx> jclist = new List<report_jcmx>();
                using (var db = new OracleConnection(ConString))
                {
                    jclist = db.Query<report_jcmx>(OraPager(sql.ToString()), dp);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), dp);
                    return jclist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
