using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using DapperExtensions;
using DapperExtensions.Predicate;
namespace ZDMesServices.LBJ.ImportData
{
    public class ImportDataService<T> : OracleBaseFixture, IImportData<T> where T : class, new()
    {
        public ImportDataService(string constr) : base(constr)
        {

        }
        public List<IPredicate> whereexp { get; set; }
        public virtual sys_import_result<T> NewImportData(List<T> data)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    sys_import_result<T> result = new sys_import_result<T>();
                    List<T> oklist = new List<T>();
                    List<T> repeatlist = new List<T>();
                    PredicateGroup pg = new PredicateGroup();
                    pg.Operator = GroupOperator.And;
                    pg.Predicates = whereexp;

                    foreach (var item in data)
                    {
                        pg.Predicates.Clear();
                        var q = Db.GetList<T>(pg);
                        if (q.Count() > 0)
                        {
                            repeatlist.Add(item);
                        }
                        else
                        {
                            Db.Insert<T>(item);
                            oklist.Add(item);
                        }
                    }
                    result.oklist = oklist;
                    result.repeatlist = repeatlist;
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        public virtual sys_import_result<T> ReaplaceImportData(List<T> data)
        {
            try
            {
                sys_import_result<T> result = new sys_import_result<T>();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual sys_import_result<T> ZhImportData(List<T> data)
        {
            try
            {
                sys_import_result<T> result = new sys_import_result<T>();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
