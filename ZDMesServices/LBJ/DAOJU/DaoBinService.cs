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

namespace ZDMesServices.LBJ.DAOJU
{
    public class DaoBinService:BaseDao<base_dbxx>
    {
        public DaoBinService(string constr) :base(constr)
        {
            
        }

        //public sys_import_result<base_dbxx> NewImportData(List<base_dbxx> data)
        //{
        //    return _import.NewImportData(data);
        //}

        //public sys_import_result<base_dbxx> ReaplaceImportData(List<base_dbxx> data)
        //{
        //    return _import.ReaplaceImportData(data);
        //}

        //public sys_import_result<base_dbxx> ZhImportData(List<base_dbxx> data)
        //{
        //    return _import.ZhImportData(data);
        //}
    }
}
