using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.SBWB;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGwzdService : BaseDao<base_gwzd>
    {
        public DuCarGwzdService(string constr) : base(constr)
        {
        }

        public override bool Modify(IEnumerable<base_gwzd> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" update base_gwzd ");
                sql.Append("  set scx = :scx,                  ");
                sql.Append("         gwh = :gwh,               ");
                sql.Append("         gwmc = :gwmc,             ");
                sql.Append("         gwlx = :gwlx,             ");
                sql.Append("         gwfl = :gwfl,             ");
                sql.Append("         gzty = :gzty,             ");
                sql.Append("         bz = :bz,                 ");
                sql.Append("         ip = :ip,                 ");
                sql.Append("         jbfdj = :jbfdj,           ");
                sql.Append("         ishcjy = :ishcjy,         ");
                sql.Append("         work_flow = :workflow,    ");
                sql.Append("         smgw = :smgw             ");
                sql.Append("  where  rowid = :rid              ");

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

        public override IEnumerable<base_gwzd> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql_main.Append($"select ta.gcdm, ta.scx, ta.gwh,tc.gwmc,ta.jx_no as jxno, ta.status_no as statusno, ta.sbbh, tb.sbmc, tb.sblx, ta.sbcxh, ta.gy_min as gymin, ta.gy_max as gymax, ta.gy_bz as gybz, ta.shbz, ta.lrr, ta.lrsj, ta.shr, ta.shsj, ta.mj, ta.parm1, ta.bz, ta.gbh, ta.wzh, ta.cxcs1, ta.cxcs2, ta.iscxh, ta.ishxsj from base_gycs ta ");
                sql_main.Append(" ,base_sbxx tb,base_gwzd tc where ta.sbbh = tb.sbbh(+) ");
                sql_main.Append(" and ta.scx = tb.scx(+) ");
                sql_main.Append(" and ta.scx = tc.scx(+)");
                sql_main.Append(" and ta.gwh = tc.gwh(+)");


                sql.Append(" select rowid as rid, gcdm, scx, gwh, gwmc, gwlx, gwfl, shbz, gzty, bz, lrr, lrsj, shr, shsj, ip, decode( (select count(*) ");
                sql.Append(" FROM   zxjc_gwzd_khxx  ");
                sql.Append(" where  gwh =  ");
                sql.Append("        base_gwzd.gwh  ");
                sql.Append(" and    scx =  ");
                sql.Append("        base_gwzd.scx),0,'N','Y')as iskhgw, bdfdj, jbfdj, bdjj, jbjj, hjkagv, hjkjj, fxgwh, dqjx, jjfx, ishcjy, work_flow as workflow, smgw, atlasmodel, iszdhg, jzpdgw  ");
                sql.Append(" FROM   base_gwzd  ");
                sql.Append(" where 1=1 ");
                sql_cnt.Append($"select count(*) from base_gwzd where 1=1 ");
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
                    var q = db.Query<base_gwzd>(OraPager(sql.ToString()), parm.sqlparam);
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
