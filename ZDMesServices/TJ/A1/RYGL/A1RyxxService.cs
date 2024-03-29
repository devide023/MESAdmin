﻿using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.RYGL
{
    public class A1RyxxService : BaseDao<zxjc_ryxx>, IRYGL, IBatAtachValue<zxjc_ryxx>
    {
        public A1RyxxService(string constr) : base(constr)
        {

        }

        public List<zxjc_ryxx> BatSetValue(List<zxjc_ryxx> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_rybh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        var ucode = item.scx.PadLeft(2, '0') + no.ToString().PadLeft(4, '0');
                        item.usercode = ucode;
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override IEnumerable<zxjc_ryxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder scxxx = new StringBuilder();
                scxxx.Append("select scxmc FROM tj_base_scxxx where scx in :bzxx ");

                StringBuilder sql = new StringBuilder();
                sql.Append($"select user_code as usercode, user_name as username, pass_word as password, gcdm, scx, gwh, bzxx, hgsg, rsrq, csrq, jmh, ryxb, rylx, tel,xl, xpmc, scbz, lzrq from zxjc_ryxx where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_ryxx where 1=1 ");
                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
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
                        sql.Append($" order by scbz asc,user_code asc nulls last ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var isbzxx = parm.sqlparam.ParameterNames.Where(t => t.Contains("bzxx"));
                    //班组信息特殊处理
                    if (isbzxx.Count() > 0)
                    {
                        var fname = isbzxx.First();
                        var bzxxs = parm.sqlparam.Get<List<string>>(fname);
                        var bzs = db.Query<string>(scxxx.ToString(), new { bzxx = bzxxs });
                        parm.sqlparam.Add(fname, bzs);
                    }
                    var q = db.Query<zxjc_ryxx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CreateUserCode()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_rybh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "01" + no.ToString().PadLeft(4, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
