﻿using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    public class DuCarSbxxService : BaseDao<base_sbxx>
    {
        public DuCarSbxxService(string constr) : base(constr)
        {
        }

        public override IEnumerable<base_sbxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.sbbh, ta.sbmc, ta.gcdm, ta.scx, ta.gwh,tb.gwmc, ta.sblx, ta.txfs, ta.ip, ta.port, ta.plcdbnumread, ta.plcdbnumwrite, ta.plcsbxh, ta.sfky, ta.sflj, ta.bz, ta.lrr, ta.lrsj, ta.com, ta.iscxh, ta.ishxsj, ta.ljlx, ta.mq_qname, ta.sbcj, ta.sbxh, ta.disable, ta.zcbh from base_sbxx ta, base_gwzd tb ");
                sql_main.Append(" where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) base_sbxx where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) base_sbxx where 1=1 ");

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
                    var q = db.Query<base_sbxx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<base_sbxx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_sbxx set zcbh = :zcbh,sbxh=:sbxh,ljlx=:ljlx,bz=:bz,ishxsj=:ishxsj where sbbh = :sbbh");
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
                                    db.Execute(sql.ToString(), item,trans);
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
