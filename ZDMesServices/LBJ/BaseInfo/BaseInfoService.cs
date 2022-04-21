using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesModels.LBJ;
using DapperExtensions;
using DapperExtensions.Predicate;
using ZDMesModels;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace ZDMesServices.LBJ.BaseInfo
{
    public class BaseInfoService : OracleBaseFixture, IBaseInfo
    {
        public BaseInfoService(string constr):base(constr)
        {

        }

        public IEnumerable<base_ftpfilepath> FtpConfig()
        {
            try
            {
                return DB.GetList<base_ftpfilepath>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> GetDbInfo()
        {
            try
            {
               return DB.GetList<base_dbxx>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gcxx> GetGCXX()
        {
            try
            {
               return DB.GetList<base_gcxx>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<base_gwzd> GetGwZd()
        {
            try
            {
                return DB.GetList<base_gwzd>().OrderBy(t => t.gwh);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_rjxx> GetRjInfo()
        {
            try
            {
                return DB.GetList<base_rjxx>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_scxxx> GetScxXX(string gcdm)
        {
            try
            {
                if (string.IsNullOrEmpty(gcdm))
                {
                    return DB.GetList<base_scxxx>().OrderBy(t => t.scx);
                }
                else
                {
                    return DB.GetList<base_scxxx>(Predicates.Field<base_scxxx>(t=>t.gcdm, Operator.Eq, gcdm)).OrderBy(t=>t.scx);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_ryxx> GetUserCode(string key)
        {
            try
            {
               var pre = Predicates.Field<zxjc_ryxx>(t => t.username, Operator.Like, key);
               return DB.GetList<zxjc_ryxx>(pre);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_cnc> Get_CNC_List()
        {
            try
            {
                return DB.GetList<base_cnc>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<base_cnc> Get_FreeCNC_List()
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select t.sbbh, sbmc ");
                    sql.Append(" from BASE_CNC t");
                    sql.Append(" where not exists(select * FROM base_dbrjzx where sbbh = t.sbbh)");
                    return db.Query<base_cnc>(sql.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 空闲刀柄列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<base_dbxx> Get_FreeDb()
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select gcdm, dbh, dbmc, dblx, cgsj ");
                    sql.Append(" from base_dbxx t ");
                    sql.Append(" where t.dbzt = '空闲中' ");
                    sql.Append(" and    not exists(select * FROM base_dbrjzx where dbh = t.dbh)");
                    return db.Query<base_dbxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
