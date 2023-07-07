using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.BaseInfo
{
    public class DuCarBaseInfoService : OracleBaseFixture, IDuCarBaseInfo
    {
        public DuCarBaseInfoService(string constr) : base(constr)
        {
        }

        public IEnumerable<base_gwzd> GetGWByRyList(string scx, string usercode)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select gwh, gwmc FROM base_gwzd where scx = :scx and gwh in (");
                sql.Append("select gwh FROM zxjc_ryxx where user_code = :usercode ");
                sql.Append(" union ");
                sql.Append(" select gwh FROM zxjc_ryxx_jn where user_code = :usercode and scx = :scx ");
                sql.Append(")");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_gwzd>(sql.ToString(), new { scx = scx,usercode = usercode });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gwzd> GetGWList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select gwh, gwmc FROM base_gwzd");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_gwzd>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gwzd> GetGWList(string scx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select gwh, gwmc FROM base_gwzd where gzty = 'N' and scx = :scx");
                StringBuilder sqlall = new StringBuilder();
                sqlall.Append("select gwh, gwmc FROM base_gwzd where gzty = 'N' ");
                using (var db = new OracleConnection(ConString))
                {
                    if (!string.IsNullOrEmpty(scx))
                    {
                        return db.Query<base_gwzd>(sql.ToString(), new { scx = scx });
                    }
                    else
                    {
                        return db.Query<base_gwzd>(sqlall.ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_ryxx> GetRyXxByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select user_code as usercode, user_name as username FROM zxjc_ryxx where user_code like :key or user_name like :key");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_ryxx>(sql.ToString(), new { key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_ryxx> GetRyXxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select user_code as usercode, user_name as username FROM zxjc_ryxx");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_ryxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_sbxx> GetSbbhsByGwh(string scx,string gwh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select sbbh,sbmc FROM base_sbxx where sfky='Y' and scx = :scx and gwh=:gwh order by sbbh asc ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbxx>(sql.ToString(), new { scx = scx, gwh = gwh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_sbxx> GetSbXxByGwbh(string gwh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select sbbh, sbmc FROM base_sbxx where gwh = :gwh");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbxx>(sql.ToString(), new { gwh=gwh});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_sbxx> GetSbXxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select sbbh, sbmc FROM base_scxxx");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_ryxx> GetScxRyXxByKey(string scx,string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select user_code as usercode, user_name as username FROM zxjc_ryxx where scx = :scx and (user_code like :key or user_name like :key )");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_ryxx>(sql.ToString(), new { scx = scx, key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_scxxx> Get_All_ScxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select scx, scxmc FROM base_scxxx");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_scxxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_fault> Get_FaultNo_By_Key(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select fault_no as faultno,fault_name as faultname from ZXJC_FAULT where (fault_no like :key or fault_name like :key)");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_fault>(sql.ToString(), new { key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> Get_JxNo_ByCode(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * FROM ");
                sql.Append(" (select distinct jx FROM pp_zpjh where jx like :key )");
                sql.Append(" where rownum < 10 order by jx");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(), new { key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<pp_zpjh> Get_OrderNo_By_Key(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.order_no,ta.scx FROM pp_zpjh ta,zxjc_order_jy tb where ta.order_no = tb.order_no and tb.qdjy = 'Y' and tb.gdbomjy = 'Y' and tb.gylxjy = 'Y' and ta.order_no like :key");
                using (var db = new OracleConnection(ConString))
                {
                   return db.Query<pp_zpjh>(sql.ToString(), new { key = "%" + key.ToUpper() + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_wlxx> Get_WlxxByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select wlbm,wlmc,cpjx,wlzt,wlz FROM base_wlxx where (wlmc like :key or wlbm like :key) and rownum < 20 ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_wlxx>(sql.ToString(), new { key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> Get_Ztbm_ByJxno(string jxno)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select distinct replace(ztbm,'\n','') FROM pp_zpjh where jx like :jxno ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(), new { jxno = "%" + jxno + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
