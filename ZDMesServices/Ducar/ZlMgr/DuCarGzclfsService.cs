using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarGzclfsService : BaseDao<zxjc_fault_clfs>, IDuCarFault_CLFS, IBatAtachValue<zxjc_fault_clfs>
    {
        private UserUtilService _uservice;
        public DuCarGzclfsService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_fault_clfs> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql_main.Append("select rowid as rid, gcdm, scx, gwh, ");
                sql_main.Append(" (select gwmc FROM base_gwzd where gwh = zxjc_fault_clfs.gwh and scx =zxjc_fault_clfs.scx and rownum = 1 ) as gwmc, fault_no as faultno,");
                sql_main.Append(" (select fault_name FROM zxjc_fault where fault_no = zxjc_fault_clfs.fault_no and scx = zxjc_fault_clfs.scx and rownum = 1 ) as faultname,");
                sql_main.Append(" hand_no as handno, hand_name as handname, remark, lrr, lrsj from zxjc_fault_clfs ");
                //
                sql.Append("select * from (");
                sql.Append(sql_main);
                sql.Append(") zxjc_fault_clfs where 1=1 ");
                sql_cnt.Append("select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_fault_clfs where 1=1 ");
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
                    else
                    {
                        sql.Append($" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var q = db.Query<zxjc_fault_clfs>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gwhs = gwzdlist.Where(t => t.scx == item.scx).Select(t=>new sys_options_list() {label=t.gwmc,value=t.gwh }).OrderBy(t => t.value).ToList();
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

        public override bool Del(IEnumerable<zxjc_fault_clfs> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete FROM zxjc_fault_clfs where rowid in :rids");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rids = entitys.Select(t => t.rid).ToList() })>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Modify(IEnumerable<zxjc_fault_clfs> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_fault_clfs ");
                sql.Append(" set scx = :scx,");
                sql.Append("        gwh = :gwh, ");
                sql.Append("        fault_no = :faultno, ");
                sql.Append("        hand_no = :handno, ");
                sql.Append("        hand_name = :handname, ");
                sql.Append("        remark = :remark ");
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
        public List<zxjc_fault_clfs> BatSetValue(List<zxjc_fault_clfs> list)
        {
            try
            {
                list.ForEach(t =>
                {
                    t.handno = Create_CLFS_No();
                    t.faultno = GetFaultNoByName(t.faultname);
                    t.lrr = _uservice.CurrentUser.name;
                });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Create_CLFS_No()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_handno.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "CL" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetFaultNoByName(string faultname)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select fault_no from zxjc_fault where fault_name = :faultname");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(), new { faultname = faultname }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
