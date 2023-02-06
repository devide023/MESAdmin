using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.JJBGL
{
    public class CDGC_GTTF_Service : BaseDao<zxjc_gtjjb_gfmx>
    {
        public CDGC_GTTF_Service(string constr) : base(constr)
        {

        }

        public override IEnumerable<zxjc_gtjjb_gfmx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();


                sql.Append($"select id, detailid, (select cpmc from zxjc_gtjjb_bill_detail where id = zxjc_gtjjb_gfmx.detailid and rownum = 1) as cpmc, vin, yx, jth, lrr,tfr,tfsj from zxjc_gtjjb_gfmx where 1=1 ");
                sql_cnt.Append($"select count(detailid) from zxjc_gtjjb_gfmx where 1=1 ");

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
                        sql.Append(" order by lrsj desc nulls last ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_gtjjb_gfmx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<zxjc_gtjjb_gfmx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_gtjjb_gfmx set yx = :yx,jth = :jth,tfr=:tfr,tfsj=sysdate where id =:id ");
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql.ToString(), new { yx = item.yx, jth = item.jth, tfr = item.tfr, id = item.id },trans);
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
    }
}
