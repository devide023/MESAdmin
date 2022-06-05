using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DJGW;
using ZDMesModels.LBJ;
using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.DJXX
{
    public class DJGWService:BaseDao<zxjc_djgw>, IDjGw
    {
        public DJGWService(string constr):base(constr)
        {

        }

        public bool IsExistDjNo(string djno)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    int ret = db.ExecuteScalar<int>("select count(gcdm) from ZXJC_DJGW where djno = :djno ", new { djno = djno });
                    return ret > 0;
                }   
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int MaxDjNo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    return db.ExecuteScalar<int>("select count(djno) from ZXJC_DJGW");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
