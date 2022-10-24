using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.BaseInfo
{
    public class A1BaseInfoService : OracleBaseFixture, IA1BaseInfo
    {
        public A1BaseInfoService(string constr):base(constr)
        {

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
                sql.Append("select gwh,gwmc FROM base_gwzd order by to_number(gwh) asc");
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
                sql.Append("select distinct ztbm from ztbm_new where jx = :jxno ");
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
    }
}
