using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;
using ZDMesServices.LBJ.ImportData;
using DapperExtensions;
using DapperExtensions.Predicate;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DaoBinService:BaseDao<base_dbxx>
    {
        public DaoBinService(string constr) :base(constr)
        {
            
        }

        public override int Add(IEnumerable<base_dbxx> entitys,out IEnumerable<base_dbxx> noklist)
        {
            int cnt = 0;
            List<base_dbxx>  repeatlist = new List<base_dbxx>();
            using (var db = new OracleConnection(ConString))
            {
                InitDB(db);
                try
                {
                    foreach (var item in entitys)
                    {
                       var q = Db.GetList<base_dbxx>(Predicates.Field<base_dbxx>(t => t.dbh, Operator.Eq, item.dbh));
                        if(q.Count() == 0)
                        {
                            Db.Insert<base_dbxx>(item);
                            cnt++;
                        }
                        else
                        {
                            repeatlist.Add(item);
                        }
                    }
                    noklist = repeatlist;
                    return cnt;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    db.Close();
                    Db.Dispose();
                }
            }  
        }
    }
}
