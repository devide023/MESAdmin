using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarGzjcxxService : BaseDao<zxjc_fault>,IBatAtachValue<zxjc_fault>
    {
        public DuCarGzjcxxService(string constr) : base(constr)
        {
        }
        public override IEnumerable<zxjc_fault> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.rowid as rid, ta.gcdm, ta.scx, ta.gwh,tb.gwmc, ta.fault_no as faultno, ta.fault_name as faultname, ta.fault_fl as faultfl, ta.jx_no as jxno, ta.status_no as statusno, ta.gwh_bz as gwhbz,tc.gwmc as clgwmc, ta.bz, ta.lrr, ta.lrsj, ta.tpname from zxjc_fault ta, base_gwzd tb,base_gwzd tc ");
                sql_main.Append(" where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql_main.Append(" and ta.gwh_bz = tc.gwh(+) and ta.scx = tc.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_fault where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_fault where 1=1 ");
                //
                StringBuilder sqlgwh = new StringBuilder();
                sqlgwh.Append("select scx, gwh, gwmc FROM base_gwzd order by scx asc");

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
                }

                using (var db = new OracleConnection(ConString))
                {
                    var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var q = db.Query<zxjc_fault>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gwhs = gwzdlist.Where(t => t.scx == item.scx).Select(t => new sys_options_list() { label = t.gwmc, value = t.gwh }).OrderBy(t => t.value).ToList();
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
        
        public override bool Modify(IEnumerable<zxjc_fault> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" update zxjc_fault ");
                sql.Append(" set scx = :scx,  ");
                sql.Append("        gwh = :gwh, ");
                sql.Append("        fault_no = :faultno, ");
                sql.Append("        fault_name = :faultname, ");
                sql.Append("        fault_fl = :faultfl, ");
                sql.Append("        jx_no = :jxno, ");
                sql.Append("        status_no = :statusno, ");
                sql.Append("        gwh_bz = :gwhbz, ");
                sql.Append("        bz = :bz ");
                sql.Append(" where  rowid = :rid ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql.ToString(), item, trans);
                                }
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Create_FaultNo(string faultname)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select fault_no from zxjc_fault where fault_name = :faultname");
                using (var db = new OracleConnection(ConString))
                {
                    var sfcz = db.Query<string>(sql.ToString(), new { faultname = faultname });
                    if (sfcz.Count() == 0)
                    {
                        sql.Clear();
                        sql.Append("select SEQ_MES_FAULTNO.nextval FROM dual");
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        return "GZ" + no.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        return sfcz.First().ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<zxjc_fault> Create_FaultNo_List(List<zxjc_fault> list)
        {
            try
            {
                var names = list.Select(t => t.faultname).Distinct();
                foreach (var item in names)
                {
                    var defaultno = Create_FaultNo(item);
                    list.Where(t => t.faultname == item).ToList().ForEach(t => t.faultno = defaultno);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_fault> BatSetValue(List<zxjc_fault> list)
        {
            try
            {
                return Create_FaultNo_List(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
