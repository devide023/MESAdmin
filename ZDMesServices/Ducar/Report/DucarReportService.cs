using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Net.Http.Headers;

namespace ZDMesServices.Ducar.Report
{
    public class DucarReportService : OracleBaseFixture, IDuCarReport
    {
        public DucarReportService(string constr) : base(constr)
        {
        }

        public IEnumerable<ducar_report_dtzs> Get_Engine_No_Data(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                DynamicParameters p = new DynamicParameters();
                p.Add(":pageindex", parm.pageindex);
                p.Add(":pagesize", parm.pagesize);
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select engine_no, status_no, bill_no, order_no, gwh, jcsj, czsj, zpsj, jcry, jcjg, jjh, jbh, fx_flg, jcz, gcdm, scx, sbbh, sblx, jx_no from zxjc_data_list where 1=1 ");
                sql_cnt.Append("select count(*) from zxjc_data_list where 1=1 ");
                if(parm != null && parm.search_condition.Count>0) {
                    int i = 0;
                    foreach (var item in parm.search_condition)
                    {
                        if (!string.IsNullOrEmpty(item.value))
                        {
                            sql.Append($" and {item.colname} = :{item.colname}{i} ");
                            sql_cnt.Append($" and {item.colname} = :{item.colname}{i} ");
                            p.Add($":{item.colname}{i}", item.value);
                        }
                        if (item.values.Count > 0)
                        {
                            sql.Append($" and {item.colname} in :{item.colname}{i} ");
                            sql_cnt.Append($" and {item.colname} in :{item.colname}{i} ");
                            p.Add($":{item.colname}{i}", item.values);
                        }
                        i++;
                    }
                }
                sql.Append(" order by gwh asc ");
                using (var db = new OracleConnection(ConString))
                {
                    var qs = db.Query<ducar_report_dtzs>(OraPager(sql.ToString()), p);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), p);
                    return qs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ducar_report_fault> Get_Fault_Static(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                List<ducar_report_fault> list = new List<ducar_report_fault>();
                StringBuilder sqljx = new StringBuilder();
                sqljx.Append("select distinct ta.engine_no, ta.engine_type_no as jx_no FROM  barcode_print ta, zxjc_data_list tb ");
                sqljx.Append(" where  ta.engine_no = tb.engine_no and  ta.print_time between trunc(:rq1) and trunc(:rq2) + 1");
                StringBuilder sqlztbm = new StringBuilder();
                sqlztbm.Append("select distinct ta.engine_no, ta.status_no FROM  barcode_print ta, zxjc_data_list tb ");
                sqlztbm.Append(" where  ta.engine_no = tb.engine_no and  ta.print_time between trunc(:rq1) and trunc(:rq2) + 1");
                //一次合格数量
                StringBuilder sqlfirst = new StringBuilder();
                sqlfirst.Append("select count(*) FROM  (select distinct tb.engine_no FROM   barcode_print ta, zxjc_data_list tb where  ta.engine_no = tb.engine_no  and   tb.jcjg <> '合格' and ta.engine_type_no = :jxno and   ta.print_time between trunc(:rq1) and trunc(:rq2) + 1)");
                StringBuilder sql_ztbm_first = new StringBuilder();
                sql_ztbm_first.Append("select count(*) FROM  (select distinct tb.engine_no FROM   barcode_print ta, zxjc_data_list tb where  ta.engine_no = tb.engine_no  and   tb.jcjg <> '合格' and ta.status_no = :ztbm and   ta.print_time between trunc(:rq1) and trunc(:rq2) + 1)");

                //合格数量
                StringBuilder sqlhgsl = new StringBuilder();
                sqlhgsl.Append("select count(*) FROM  (select distinct tb.engine_no FROM   barcode_print ta, zxjc_data_list tb where  ta.engine_no = tb.engine_no  and   tb.jcjg = '合格' and ta.engine_type_no = :jxno and   ta.print_time between trunc(:rq1) and trunc(:rq2) + 1)");
                StringBuilder sql_ztbm_hgsl = new StringBuilder();
                sql_ztbm_hgsl.Append("select count(*) FROM  (select distinct tb.engine_no FROM   barcode_print ta, zxjc_data_list tb where  ta.engine_no = tb.engine_no  and   tb.jcjg = '合格' and ta.status_no = :ztbm and   ta.print_time between trunc(:rq1) and trunc(:rq2) + 1)");
                //返修明细
                StringBuilder sqlmx= new StringBuilder();
                sqlmx.Append("select t1.fault_no, (select fault_name FROM  zxjc_fault where fault_no = t1.fault_no and rownum = 1) as fault_name, count(t1.fault_no) as gzsl FROM (select tb.fault_no FROM barcode_print ta, zxjc_gwzd_fxmx tb  where  ta.engine_no = tb.engine_no  and ta.print_time between trunc(:rq1) and trunc(:rq2) + 1 and ta.engine_type_no = :jxno) t1 group  by fault_no");
                StringBuilder sql_ztbm_fxmx = new StringBuilder();
                sql_ztbm_fxmx.Append("select t1.fault_no, (select fault_name FROM  zxjc_fault where fault_no = t1.fault_no and rownum = 1) as fault_name, count(t1.fault_no) as gzsl FROM (select tb.fault_no FROM barcode_print ta, zxjc_gwzd_fxmx tb  where  ta.engine_no = tb.engine_no  and ta.print_time between trunc(:rq1) and trunc(:rq2) + 1 and ta.status_no = :ztbm) t1 group  by fault_no");
                DynamicParameters p = new DynamicParameters();
                p.Add(":pageindex", parm.pageindex);
                p.Add(":pagesize", parm.pagesize);
                //
                DateTime ksrq = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
                DateTime jsrq = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                var v = parm.search_condition.Where(t => t.colname == "kssj" && !string.IsNullOrEmpty(t.value));
                if (v.Count() > 0)
                {
                    ksrq = Convert.ToDateTime(v.First().value);
                }
                v = parm.search_condition.Where(t => t.colname == "jssj" && !string.IsNullOrEmpty(t.value));
                if (v.Count() > 0)
                {
                    jsrq = Convert.ToDateTime(v.First().value);
                }
                using (var db = new OracleConnection(ConString))
                {
                    var jxtj = parm.search_condition.Where(t => t.colname == "tjfs" && t.value == "1");
                    var zttj = parm.search_condition.Where(t => t.colname == "tjfs" && t.value == "2");
                    //按机型统计
                    if (jxtj.Count() > 0)
                    {
                        //上线刻字数量
                        var kzlist = db.Query<ducar_report_item>(sqljx.ToString(), new { rq1=ksrq,rq2=jsrq});
                        var jxnos = kzlist.GroupBy(t => t.jx_no);
                        foreach (var jxno in jxnos)
                        {
                            //检测数量
                            var jcsl = kzlist.Where(t => t.jx_no == jxno.Key).Count();
                            //机型一次合格数量
                            var ychgsl = jcsl - db.ExecuteScalar<int>(sqlfirst.ToString(), new { jxno = jxno.Key, rq1 = ksrq, rq2 = jsrq });
                            //合格数量
                            var hgsl = db.ExecuteScalar<int>(sqlhgsl.ToString(), new { jxno = jxno.Key, rq1 = ksrq, rq2 = jsrq });
                            //一次合格率
                            double hgl = 0;
                            if (jcsl > 0)
                            {
                                hgl = Math.Round(Convert.ToDouble(ychgsl) / Convert.ToDouble(jcsl), 4) * 100;
                            }
                            list.Add(new ducar_report_fault()
                            {
                                jx_no = jxno.Key,
                                jcsl = jcsl,
                                firsthgsl = ychgsl,
                                hgsl = hgsl,
                                hgl = hgl,
                                fxmxlist = db.Query<ducar_fxmx_item>(sqlmx.ToString(), new { jxno = jxno.Key, rq1 = ksrq, rq2 = jsrq }).ToList()
                            }) ;
                        }
                    }
                    //按状态统计
                    if (zttj.Count() > 0)
                    {
                        //上线刻字数量
                        var kzlist = db.Query<ducar_report_item>(sqlztbm.ToString(), new { rq1 = ksrq, rq2 = jsrq });
                        var ztbms = kzlist.GroupBy(t => t.status_no);
                        foreach (var ztbm in ztbms)
                        {
                            //检测数量
                            var jcsl = kzlist.Where(t => t.status_no == ztbm.Key).Count();
                            //机型一次合格数量
                            var ychgsl = jcsl - db.ExecuteScalar<int>(sql_ztbm_first.ToString(), new { ztbm = ztbm.Key, rq1 = ksrq, rq2 = jsrq });
                            //合格数量
                            var hgsl = db.ExecuteScalar<int>(sql_ztbm_hgsl .ToString(), new { ztbm = ztbm.Key, rq1 = ksrq, rq2 = jsrq });
                            //一次合格率
                            double hgl = 0;
                            if (jcsl > 0)
                            {
                                hgl = Math.Round(Convert.ToDouble(ychgsl) / Convert.ToDouble(jcsl), 4) * 100;
                            }
                            list.Add(new ducar_report_fault()
                            {
                                status_no = ztbm.Key,
                                jcsl = jcsl,
                                firsthgsl = ychgsl,
                                hgsl = hgsl,
                                hgl = hgl,
                                fxmxlist = db.Query<ducar_fxmx_item>(sql_ztbm_fxmx.ToString(), new { ztbm = ztbm.Key, rq1 = ksrq, rq2 = jsrq }).ToList()
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_data_list> Get_Gwzs_Data(sys_page parm, out int resultcount)
        {
            try
            {
                List<zxjc_data_list> list = new List<zxjc_data_list>();
                resultcount = 0;
                DynamicParameters p = new DynamicParameters();
                p.Add(":pageindex", parm.pageindex);
                p.Add(":pagesize", parm.pagesize);

                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select engine_no, status_no, bill_no, order_no, gwh,(select gwmc from base_gwzd where gwh=zxjc_data_list.gwh and scx = zxjc_data_list.scx and rownum = 1) as gwmc,jcsj, czsj, zpsj, jcry, jcjg, jjh, jbh, fx_flg, jcz, gcdm, scx, sbbh, sblx, gy_min, gy_max, gy_bz, id, scbz, jx_no, bzxx, csjgwh, pkid from zxjc_data_list where 1=1 ");
                sql_cnt.Append("select count(*) from zxjc_data_list where 1=1 ");
                if (parm != null && parm.search_condition.Count > 0)
                {
                    int i = 0;
                    foreach (var item in parm.search_condition)
                    {
                        if (!string.IsNullOrEmpty(item.value))
                        {
                            if (item.colname == "kssj" )
                            {
                                sql.Append($" and jcsj >= :jcsj1 ");
                                sql_cnt.Append($" and jcsj >= :jcsj1 ");
                                p.Add(":jcsj1", Convert.ToDateTime(item.value));
                            }
                            else if (item.colname == "jssj" )
                            {
                                sql.Append($" and jcsj <= :jcsj2 ");
                                sql_cnt.Append($" and jcsj <= :jcsj2 ");
                                p.Add(":jcsj2", Convert.ToDateTime(item.value));
                            }
                            else { 
                            sql.Append($" and {item.colname} = :{item.colname}{i} ");
                            sql_cnt.Append($" and {item.colname} = :{item.colname}{i} ");
                            p.Add($":{item.colname}{i}", item.value);
                            }
                        }
                        if (item.values.Count > 0)
                        {
                            sql.Append($" and {item.colname} in :{item.colname}{i} ");
                            sql_cnt.Append($" and {item.colname} in :{item.colname}{i} ");
                            p.Add($":{item.colname}{i}", item.values);
                        }
                        i++;
                    }
                }
                sql.Append(" order by gwh asc,jcsj desc ");
                using (var db = new OracleConnection(ConString))
                {
                    var qs = db.Query<zxjc_data_list>(sql.ToString(), p);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), p);
                    return qs;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
