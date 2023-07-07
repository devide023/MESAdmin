using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.WLMgr
{
    public class DuCarGwbjService : BaseDao<base_gwbj>
    {
        public DuCarGwbjService(string constr) : base(constr)
        {
        }

        public override bool Modify(IEnumerable<base_gwbj> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_gwbj set scx=:scx,gwh=:gwh,lxpd=:lxpd,wlbm=:wlbm,dxsl=:dxsl,gwpb=:gwpb,wlsx=:wlsx,bz=:bz,jx_no=:jxno where rowid = :rid ");
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
        public override bool Del(IEnumerable<base_gwbj> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from base_gwbj where rowid in :rids ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rids = entitys.Select(t => t.rid).ToList() }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IEnumerable<base_gwbj> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select rowid as rid,gcdm, scx, gwh,(select gwmc from base_gwzd where gwh = base_gwbj.gwh and scx = base_gwbj.scx and rownum = 1) as gwmc,");
                sql.Append("wlbm,(select wlmc from base_wlxx where wlbm = base_gwbj.wlbm and rownum = 1 ) as wlmc,");
                sql.Append("lxpd, lxlx, dxsl, gwpb, qwwbm, wlsx, zcqld, psfs, gdbh, gdc, zgkc,");
                sql.Append("sfdy, bz, lrr, lrsj, jx_no as jxno, gzzx, sfznlc from base_gwbj where 1=1 ");
                sql_cnt.Append($"select count(*) from base_gwbj where 1=1 ");
                //
                StringBuilder sqlscxgwh = new StringBuilder();
                sqlscxgwh.Append("select gwh,gwmc from base_gwzd where scx=:scx");
                //
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
                    var qs = db.Query<base_gwbj>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var q in qs)
                    {
                        var query = db.Query<base_gwzd>(sqlscxgwh.ToString(), new { scx = q.scx });
                        q.gwhs = query.Select(t => new sys_option_item { value = t.gwh, label = t.gwmc }).OrderBy(t=>t.value).ToList();
                    }
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
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
