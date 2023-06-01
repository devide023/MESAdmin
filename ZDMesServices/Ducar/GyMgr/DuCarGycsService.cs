﻿using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGycsService : BaseDao<base_gycs>,IBatAtachValue<base_gycs>
    {
        private UserUtilService _uservice;
        public DuCarGycsService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public override IEnumerable<base_gycs> GetList(sys_page parm, out int resultcount)
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

                sql.Append("select * from (");
                sql.Append(sql_main);
                sql.Append(" ) base_gycs where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) base_gycs where 1=1 ");
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
                    var q = db.Query<base_gycs>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<base_gycs> BatSetValue(List<base_gycs> list)
        {
            try
            {
                list.ForEach(t =>
                {
                    t.lrr = _uservice.CurrentUser.name;
                    t.shr= _uservice.CurrentUser.name;
                    t.shsj = DateTime.Now;
                    t.shbz = "Y";
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
