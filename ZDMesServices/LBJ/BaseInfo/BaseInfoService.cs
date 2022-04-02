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

namespace ZDMesServices.LBJ.BaseInfo
{
    public class BaseInfoService : OracleBaseFixture, IBaseInfo
    {
        public BaseInfoService(string constr):base(constr)
        {

        }
        public IEnumerable<base_gcxx> GetGCXX()
        {
            try
            {
               return DB.GetList<base_gcxx>();
            }
            catch (Exception)
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

        public IEnumerable<base_scxxx> GetScxXX(string gcdm)
        {
            try
            {
                if (string.IsNullOrEmpty(gcdm))
                {
                    return DB.GetList<base_scxxx>();
                }
                else
                {
                    return DB.GetList<base_scxxx>(Predicates.Field<base_scxxx>(t=>t.gcdm, Operator.Eq, gcdm));
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
               return Db.GetList<zxjc_ryxx>(pre);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
