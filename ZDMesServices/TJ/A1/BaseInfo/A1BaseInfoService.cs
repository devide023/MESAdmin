using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesInterfaces.Common;
using ZDMesModels;
using System.Runtime.InteropServices;

namespace ZDMesServices.TJ.A1.BaseInfo
{
    public class A1BaseInfoService : OracleBaseFixture, IA1BaseInfo, IEntityDetail<string>
    {
        public A1BaseInfoService(string constr):base(constr)
        {

        }
        public virtual IEnumerable<string> Details(params object[] parm)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cpbm from ztbm_new where jx = :jxno ");
                using (var db = new OracleConnection(ConString))
                {
                    if (parm.Length > 0)
                    {
                        return db.Query<string>(sql.ToString(), new { jxno = parm[0].ToString() });
                    }
                    else
                    {
                        return new List<string>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_fault> GetFaultNoByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct fault_no as faultno, fault_name as faultname FROM zxjc_fault where fault_no like :key or fault_name like :key");
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

        public IEnumerable<base_gwzd> GetGWList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct gwh,gwmc FROM base_gwzd order by to_number(REGEXP_REPLACE(gwh, '-', '.')) asc");
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
                sql.Append("select distinct gwh,gwmc FROM base_gwzd where scx = :scx order by to_number(REGEXP_REPLACE(gwh, '-', '.')) asc");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_gwzd>(sql.ToString(), new {scx = scx});
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<dynamic> GetJcLxByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select scx as \"scx\", jclx as \"jclx\" FROM zxjc_jclx where scbz = 'N' and jclx like :key ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<dynamic>(sql.ToString(), new { key = "%" + key + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<dynamic> GetJxNoByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct ccbm as \"ccbm\",ccmc as \"ccmc\" from V_JXXX where ccbm like :key ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<dynamic>(sql.ToString(), new { key = "%" + key.Trim().ToUpper() + "%" });
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
                sql.Append("select user_code as usercode,user_name as username FROM zxjc_ryxx where scbz = 'N' ");
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

        public IEnumerable<base_sbxx> GetSbXxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select sbbh,sbmc,gwh,sblx FROM base_sbxx");
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

        public IEnumerable<option_list> GetWlbmByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select wlbm,wlmc FROM base_wlxx where wlbm like :key or wlmc like :key and rownum < 100 ");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query(sql.ToString(), new { key = "%" + key.Trim() + "%" }).Select(t => new option_list() { label = t.WLMC, value = t.WLBM });
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<dynamic> GetZPLXList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select lx as \"lx\",mc as \"mc\" FROM bom_zplx ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<dynamic>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> GetZtBMByJxNo(string jxno)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cpbm from ztbm_new where jx = :jxno ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(),new { jxno=jxno});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_jclx> GetZxjcLx()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select scx,jclx FROM zxjc_jclx where scbz='N' order by jclx ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_jclx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<dynamic> Get_All_ScxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select scx as \"scx\",scxmc as \"scxmc\" FROM   tj_base_scxxx where scxmc is not null");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<dynamic>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
