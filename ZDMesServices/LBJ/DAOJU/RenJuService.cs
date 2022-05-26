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
namespace ZDMesServices.LBJ.DAOJU
{
    public class RenJuService:BaseDao<base_rjxx>, IImportData<base_rjxx>
    {
        public RenJuService(string constr):base(constr)
        {

        }

        public sys_import_result<base_rjxx> NewImportData(List<base_rjxx> data)
        {
            try
            {
                sys_import_result<base_rjxx> ret = new sys_import_result<base_rjxx>();
                List<base_rjxx> oklist = new List<base_rjxx>();
                List<base_rjxx> repeatlist = new List<base_rjxx>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) from BASE_RJXX where rjlx = :rjlx");
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    foreach (var item in data)
                    {
                        var sfcz = db.ExecuteScalar<int>(sql.ToString(), new { rjlx = item.rjlx });
                        if (sfcz == 0)
                        {
                            Db.Insert<base_rjxx>(item);
                            oklist.Add(item);
                        }
                        else
                        {
                            repeatlist.Add(item);
                        }
                    }
                    ret.oklist = oklist;
                    ret.repeatlist = repeatlist;
                    return ret;
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

        public sys_import_result<base_rjxx> ReaplaceImportData(List<base_rjxx> data)
        {
            try
            {

                sys_import_result<base_rjxx> ret = new sys_import_result<base_rjxx>();


                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_import_result<base_rjxx> ZhImportData(List<base_rjxx> data)
        {
            try
            {

                sys_import_result<base_rjxx> ret = new sys_import_result<base_rjxx>();


                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
