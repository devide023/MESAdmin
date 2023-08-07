using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.LBJ;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.ZLGL
{
    public class LBJ_BaseCheckService : BaseDao<zxjc_base_check>,IBatAtachValue<zxjc_base_check>
    {
        private UserUtilService u;
        public LBJ_BaseCheckService(string constr) : base(constr)
        {
            u = new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_base_check> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();

                sql.Append($"select id, cpfw, cpxh, th, jcxm, jcpc, jcgj, jcz, jcxx, jcsx, srlx, seq, lrr, lrsj, isfx, scbz from zxjc_base_check where scbz='N' ");
                sql_cnt.Append($"select count(*) from zxjc_base_check where scbz='N' ");

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
                    var q = db.Query<zxjc_base_check>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<zxjc_base_check> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_base_check set scbz='Y' where scbz='N' and id in :ids ");
                using (var db = new  OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { ids = entitys.Select(t => t.id).ToList() }) > 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_base_check> BatSetValue(List<zxjc_base_check> list)
        {
            try
            {
                int i = 1;
                list.ForEach(t =>
                {
                    if (t.seq == 0)
                    {
                        t.seq = i++;
                    }
                    t.lrr = u.CurrentUser.name;
                });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
