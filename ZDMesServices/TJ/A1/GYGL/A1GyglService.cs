using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;

namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1GyglService : OracleBaseFixture, IA1GYGL
    {
        public A1GyglService(string constr) : base(constr)
        {
        }

        public IEnumerable<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> Get_Czgc_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='操作规程' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='操作规程' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 工艺视频
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        public IEnumerable<ZDMesModels.TJ.A1.zxjc_t_dzgy> Get_GySpList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='视频' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='视频' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<ZDMesModels.TJ.A1.zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy> Get_Lswt_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='历史问题' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='历史问题' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ZDMesModels.TJ.A1.MDS.zxjc_t_dzgy> Get_Mds_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='MDS表' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='MDS表' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<ZDMesModels.TJ.A1.MDS.zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy> Get_ZpzdList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='装配指导' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='装配指导' ");

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
                        sql.Append($" order by jx_no asc, to_number(gwh) asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
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
