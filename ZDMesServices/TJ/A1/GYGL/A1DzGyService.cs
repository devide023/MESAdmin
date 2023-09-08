using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;

namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1DzGyService : BaseDao<zxjc_t_dzgy>, IBatAtachValue<zxjc_t_dzgy>
    {
        public A1DzGyService(string constr) : base(constr)
        {

        }

        public override int Add(IEnumerable<zxjc_t_dzgy> entitys, out IEnumerable<zxjc_t_dzgy> noklist)
        {
            try
            {
                List<zxjc_t_dzgy> postdata = new List<zxjc_t_dzgy>();
                List<zxjc_t_dzgy> repeatdata = new List<zxjc_t_dzgy>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(gyid) FROM  zxjc_t_dzgy where scx = :scx and jx_no = :jxno and gwh = :gwh and gymc = :gymc ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, jxno = item.jxno, gwh = item.gwh, gymc = item.gymc });
                        if (cnt == 0) {
                            postdata.Add(item);
                        }
                        else
                        {
                            repeatdata.Add(item);
                        }
                    }
                }
                noklist = repeatdata;
                return base.Add(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_t_dzgy> BatSetValue(List<zxjc_t_dzgy> list)
        {
            try
            {
                foreach (var item in list)
                {
                    item.scry = item.lrr;
                    item.scsj = DateTime.Now;
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 电子工艺列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        public override IEnumerable<zxjc_t_dzgy> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select gyid, gybh, gymc, gyms, gcdm, scx, gwh, jx_no as jxno, status_no as statusno, wjlj, jwdx, scry, scpc, scsj, bbbh, gylx, lrr, lrsj, bz from zxjc_t_dzgy where gylx='工艺' ");
                sql_cnt.Append($"select count(gyid) from zxjc_t_dzgy where gylx='工艺' ");

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
                    var q = db.Query<zxjc_t_dzgy>(OraPager(sql.ToString()), parm.sqlparam);
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
