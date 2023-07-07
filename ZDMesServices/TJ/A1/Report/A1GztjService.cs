using Aspose.Cells;
using Aspose.Cells.Drawing;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using static Slapper.AutoMapper;

namespace ZDMesServices.TJ.A1.Report
{
    public class A1GztjService : OracleBaseFixture, IA1Report
    {
        public A1GztjService(string constr) : base(constr)
        {
        }

        public IEnumerable<report_djjl> Get_DJJLB(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                if (scx.Count() > 0)
                {
                    sqlwhere.Append("  and ta.scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0)
                {
                    sqlwhere.Append("and  ta.jcrq >=  trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if (rq2.Count() > 0)
                {
                    sqlwhere.Append("and  ta.jcrq <= trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                //
                sql.Append(" select rownum as rowno,t.* from (");
                sql.Append(" select ta.jcrq as rq,ta.scx,(select gwmc FROM base_gwzd where scx = ta.scx and gwh = tb.gwh and rownum = 1 ) as gwmc, tb.jclb,tb.jcyq,tb.jcjg,tc.jcd,tc.xh,'' as bz FROM zxjc_jcbill ta,zxjc_jcmx tb,zxjc_jcjcxx tc ");
                sql.Append("where ta.id = tb.jcjg_id ");
                sql.Append(" and tb.jcxid = tc.id(+) ");
                sql.Append(sqlwhere);
                sql.Append(" order by ta.jclx asc,tc.xh asc");
                sql.Append(" ) t ");
                //
                sql_cnt.Append("select count(*) from zxjc_jcbill ta,zxjc_jcmx tb ");
                sql_cnt.Append("where ta.id = tb.jcjg_id ");
                sql_cnt.Append(sqlwhere);
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<report_djjl>(OraPager(sql.ToString()), dp);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), dp);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
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
                sql.Append("select (select scxmc FROM  tj_base_scxxx where scx = t1.scx) as scx,t1.engine_no as vin,t1.jx_no as jxno,t2.jcjg as gjjg,t2.jcsj as gjsj,t2.gjcs,(select user_name FROM zxjc_ryxx where user_code = t2.jcry and scx=t1.scx and rownum =1) as gjry,t1.jcjg as sjjg,t1.lrsj as sjsj,(select user_name FROM zxjc_ryxx where user_code = t1.lrr and scx=t1.scx and rownum =1) as sjry,t1.ycyy as yy,t1.clyj FROM ");
                sql.Append(" ( ");
                sql.Append(" select id, gcdm, scx, gwh, jx_no, engine_no, smj, jcz, jcjg, ycyy, clyj, lrsj, lrr from zxjc_sjjcjl where 1=1 ");
                
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                if (scx.Count() > 0)
                {
                    sqlwhere.Append("  and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0 ) {
                    sqlwhere.Append("and  lrsj >=  trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if(rq2.Count() > 0)
                {
                    sqlwhere.Append("and  lrsj <= trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                sql.Append(sqlwhere);
                sql.Append("  )  t1,");
                sql.Append("  (");
                sql.Append("  select ta.scx,ta.engine_no,to_date(to_char(tb.jcsj,'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd hh24:mi:ss') as jcsj,ta.jcry,ta.jcjg,(select data1 FROM zxjc_data_detail where engine_no = ta.engine_no and gwh = '470' and rownum =1 ) as gjcs FROM zxjc_data_list8 ta,");
                sql.Append(" (select engine_no,max(jcsj) jcsj from zxjc_data_list8 where gwh = '470' group by engine_no ) tb");
                sql.Append(" where ta.engine_no = tb.engine_no and ta.gwh='470' ");
                sql.Append(" ) t2 ");
                sql.Append(" where t1.scx = t2.scx and t1.engine_no = t2.engine_no order by t2.jcsj desc ");
                //
                sql_cnt.Append("select count(*) from zxjc_sjjcjl where 1=1 ");
                sql_cnt.Append(sqlwhere);
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
                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var orderno = parm.search_condition.Where(t => t.colname == "orderno" && !string.IsNullOrEmpty(t.value));
                var jhh = parm.search_condition.Where(t => t.colname == "jjh" && !string.IsNullOrEmpty(t.value));
                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);
                
                sql.Append(" select rownum as rowno, t1.jclb, t1.jcyq, t1.xh, t1.order_no as orderno, t1.jx_no as jxno, t1.status_no as statusno, t1.zpjhh, t1.firstvin,t1.firstjcjg, t2.lastvin,t2.lastjcjg,t1.dxpd ");
                sqltemp.Append(" FROM (select tb.jclx, tb.jclb, tb.jcyq,tb.xh, tb.jcjg as firstjcjg,tb.jcz,ta.zpjhh, ta.order_no, ta.jx_no, ta.status_no,ta.engine_no as firstvin,ta.gwh,ta.scx,ta.jcjg as dxpd  ");
                sqltemp.Append(" FROM zxjc_jcjg ta,ZXJC_JCMX tb where ta.id = tb.jcjg_id and ta.smjbs = 'S' ");                
                if (scx.Count() > 0)
                {
                    sqlwhere.Append(" and ta.scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0) {
                    sqlwhere.Append(" and ta.lrsj >= trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if (rq2.Count() > 0) {
                    sqlwhere.Append(" and ta.lrsj <= trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }
                if (jclx.Count() > 0)
                {
                    sqlwhere.Append(" and  ta.jclx = :jclx ");
                    dp.Add(":jclx", jclx.First().value);
                }
                if (orderno.Count() > 0)
                {
                    sqlwhere.Append(" and ta.order_no = :orderno ");
                    dp.Add(":orderno", orderno.First().value);
                }
                if (jhh.Count() > 0)
                {
                    sqlwhere.Append(" and ta.zpjjh = :jhh ");
                    dp.Add(":jhh", jhh.First().value);
                }
                sqltemp.Append(sqlwhere);
                sqltemp.Append(" ) t1, (select tb.jclx, tb.jclb, tb.jcyq,tb.xh, tb.jcjg as lastjcjg,tb.jcz,ta.zpjhh, ta.order_no, ta.jx_no, ta.status_no,ta.engine_no as lastvin,ta.gwh,ta.scx ");
                sqltemp.Append(" FROM zxjc_jcjg ta,ZXJC_JCMX tb where ta.id = tb.jcjg_id and ta.smjbs = 'M'  ");
                sqltemp.Append(sqlwhere);
                sqltemp.Append(") t2 ");
                sqltemp.Append(" where  t1.order_no = t2.order_no(+)  ");
                sqltemp.Append(" and t1.jclx = t2.jclx(+) ");
                sqltemp.Append(" and t1.jclb = t2.jclb(+) ");
                sqltemp.Append(" and t1.jcyq = t2.jcyq(+) ");
                sqltemp.Append(" and t1.xh = t2.xh(+) ");
                sqltemp.Append(" and t1.scx = t2.scx(+) ");
                sqltemp.Append(" and t1.jx_no = t2.jx_no(+) ");
                sqltemp.Append(" and t1.status_no = t2.status_no(+) ");
                sqltemp.Append(" and t1.zpjhh = t2.zpjhh(+) ");
                sqltemp.Append(" and t1.gwh = t2.gwh(+) ");
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

        public IEnumerable<report_mjghjl> Get_MjGhjl(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlwhere = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();

                var rq1 = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                var rq2 = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                var scx = parm.search_condition.Where(t => t.colname == "scx" && !string.IsNullOrEmpty(t.value));

                DynamicParameters dp = new DynamicParameters();
                dp.Add(":pageindex", parm.pageindex);
                dp.Add(":pagesize", parm.pagesize);

                if (scx.Count() > 0)
                {
                    sqlwhere.Append("  and scx = :scx ");
                    dp.Add(":scx", scx.First().value);
                }
                if (rq1.Count() > 0)
                {
                    sqlwhere.Append("and  lrsj >=  trunc(:rq1) ");
                    dp.Add(":rq1", Convert.ToDateTime(rq1.First().value));
                }
                if (rq2.Count() > 0)
                {
                    sqlwhere.Append("and  lrsj <= trunc(:rq2)+1 ");
                    dp.Add(":rq2", Convert.ToDateTime(rq2.First().value));
                }

                sql.Append("select rownum as rowno, (select scxmc ");
                sql.Append("FROM   tj_base_scxxx  ");
                sql.Append(" where  scx = t1.scx  ");
                sql.Append("and rownum = 1) as scx, (select gwmc  ");
                sql.Append("FROM   base_gwzd  ");
                sql.Append("where  gwh = t1.gwh  ");
                sql.Append("and rownum = 1) as gwmc, t1.engine_no as ksjh, t2.engine_no as jsjh,t1.mjghsj as rq, t1.lrr as gwry, '' as xjqr, '' as bz  ");
                sql.Append("FROM(select scx, gwh, order_no, engine_no, lrr, max(mjghsj) as mjghsj ");
                sql.Append("from zxjc_mjghjl  ");
                sql.Append("where start_or_end = 'S'  ");
                sql.Append(sqlwhere);
                sql.Append("group  by scx, gwh, order_no, engine_no, lrr, mjghsj) t1, (select scx, gwh, order_no, engine_no, lrr, max(mjghsj) as mjghsj  ");
                sql.Append("from zxjc_mjghjl  ");
                sql.Append("where start_or_end = 'E'  ");
                sql.Append(sqlwhere);
                sql.Append("group  by scx, gwh, order_no, engine_no, lrr, mjghsj) t2  ");
                sql.Append("where  t1.scx = t2.scx(+)  ");
                sql.Append("and t1.gwh = t2.gwh(+)  ");
                sql.Append("and t1.order_no = t2.order_no(+)  ");
                //
                sql_cnt.Append("select count(*) from ( ");
                sql_cnt.Append("select scx, gwh, order_no, engine_no, lrr, max(mjghsj) as mjghsj from zxjc_mjghjl where start_or_end = 'S' ");
                sql_cnt.Append(sqlwhere);
                sql_cnt.Append(" group  by scx, gwh, order_no, engine_no, lrr, mjghsj");
                sql_cnt.Append(" ) ");

                IEnumerable<report_mjghjl> jclist = new List<report_mjghjl>();
                using (var db = new OracleConnection(ConString))
                {
                    jclist = db.Query<report_mjghjl>(OraPager(sql.ToString()), dp);
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
