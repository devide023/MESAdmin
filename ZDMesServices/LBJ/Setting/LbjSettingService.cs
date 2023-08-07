using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.Setting
{
    public class LbjSettingService : BaseDao<base_param>
    {
        public LbjSettingService(string constr) : base(constr)
        {
        }

        public override bool Modify(IEnumerable<base_param> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_param set scx=:scx,scxzx=:scxzx,param_key = :paramkey, param_value = :paramvalue,bz=:bz where rowid = :rid ");
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

        public override IEnumerable<base_param> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select rowid as rid,gcdm,scx,scxzx,param_key as paramkey,param_value as paramvalue,bz  FROM base_param where 1=1 ");
                sql_cnt.Append($"select count(*) from base_param where 1=1 ");

                StringBuilder sqlscxzx = new StringBuilder();
                sqlscxzx.Append("select scx, scxmc, scxzx, scxzxmc FROM base_scxxx_jj");

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
                    var scxzxlist = db.Query<base_scxxx_jj>(sqlscxzx.ToString());
                    var q = db.Query<base_param>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.scxzxs = scxzxlist.Where(t=>t.scx == item.scx).Select(t=>new option_list() {label=t.scxzxmc,value=t.scxzx }).ToList();
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
    }
}
