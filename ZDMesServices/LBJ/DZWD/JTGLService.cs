using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesInterfaces.LBJ.DZWD;
using ZDMesModels.LBJ;
using Dapper;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using ZDMesInterfaces.Common;

namespace ZDMesServices.LBJ.DZWD
{
    public class JTGLService:BaseDao<zxjc_t_jstc>,IJsTc
    {
        private IUser _user;
        public JTGLService(string constr, IUser user) :base(constr)
        {
            _user = user;
        }
        public override IEnumerable<zxjc_t_jstc> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, scx, shbz, shr, shsj, ver from zxjc_t_jstc where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from zxjc_t_jstc where 1=1 ");
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
                        sql.Append(" order by yxqx1 desc nulls last,jcbh desc,ver desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstc>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q; 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetVer(string wjbh)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select nvl(max(ver),0)+1 as ver FROM zxjc_t_jstc where jcbh = :jcbh");
            using (var db = new OracleConnection(ConString))
            {
               return db.ExecuteScalar<int>(sql.ToString(), new { jcbh = wjbh });
            }
        }
        public override int Add(IEnumerable<zxjc_t_jstc> entitys)
        {
            foreach (var item in entitys)
            {
                item.ver = GetVer(item.jcbh.Trim());
            }
            return base.Add(entitys);
        }
        public bool CanDel(string jtid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(*) from zxjc_t_jstc where jtid = :jtid and (fp_flg='Y' or shbz='Y')");
                    //判断是否为审核或已分配
                    int cnt = db.ExecuteScalar<int>(sql.ToString(), new { jtid = jtid });
                    if (cnt == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                }
            catch (Exception)
            {

                throw;
            }
        }

        public string Fp_Detail(string jtid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select tb.scxmc FROM  zxjc_t_jstcfp ta, base_scxxx tb where  ta.scx = tb.scx and ta.jtid = :jtid ");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("select tb.name FROM  zxjc_t_jstc_user ta, mes_user_entity tb where  ta.userid = tb.id and  ta.jtid = :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    var list1 = db.Query<string>(sql.ToString(), new { jtid = jtid });
                    var list2 = db.Query<string>(sql1.ToString(), new { jtid = jtid });
                    string ret = string.Empty;
                    foreach (var item in list1)
                    {
                        ret = ret + item + ",";
                    }
                    foreach (var item in list2)
                    {
                        ret = ret + item + ",";
                    }
                    if (ret.Length > 0)
                    {
                        ret = ret.Remove(ret.Length - 1, 1);
                    }
                    return ret;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_t_jstc> My_Doc_List(sys_page parm, out int resultcount)
        {
            try
            {
                var userid = _user.CurrentUser().id;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append(" select ta.jtid, ta.jcbh, ta.jcmc, ta.jcms, ta.wjlj, ta.jwdx, ta.scry, ta.scpc, ta.scsj, ta.yxqx1, ta.yxqx2, ta.gcdm, ta.fp_flg as fpflg, ta.fp_sj as fpsj, ta.fpr, ta.wjfl, ta.scx, ta.shbz, ta.shr, ta.shsj ");
                sql.Append(" FROM   zxjc_t_jstc ta, zxjc_t_jstc_user tb ");
                sql.Append(" where  ta.jtid = tb.jtid ");
                sql.Append($" and  tb.userid = {userid} ");
                sql_cnt.Append(" select count(*) from zxjc_t_jstc ta,zxjc_t_jstc_user tb where  ta.jtid = tb.jtid ");
                sql_cnt.Append($" and tb.userid = {userid} ");

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
                        sql.Append(" order by ta.fp_sj desc nulls last ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstc>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update_FpFlag(List<string> jtids)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                var token = ZDToolHelper.TokenHelper.GetToken;
                string usql = "select id, code, name from mes_user_entity where token = :token";
                sql.Append("update  zxjc_t_jstc set fp_flg = 'Y',fp_sj=sysdate,fpr=:fpr where fp_flg='N' and jtid in :jtid");
                using (var db = new OracleConnection(ConString))
                {
                    mes_user_entity uinfo = new mes_user_entity();
                    var q = db.Query<mes_user_entity>(usql, new { token = token});
                    if (q.Count() > 0)
                    {
                        uinfo = q.First();
                    }
                    var cnt = db.Execute(sql.ToString(), new { jtid = jtids, fpr = uinfo.name });
                    return cnt > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
