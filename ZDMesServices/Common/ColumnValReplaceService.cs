using Dapper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesServices.TJ.Common;
using ZDToolHelper;

namespace ZDMesServices.Common
{
    public class ColumnValReplaceService : OracleBaseFixture, IColumnValueReplace
    {
        public ColumnValReplaceService(string constr) : base(constr)
        {
        }

        public int Replace_Column_Value(sys_colval_replace parm)
        {
            try
            {
                var sqlinfo = ColValReplaceHelper.Get_Replace_Exp(parm);
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query(sqlinfo.sql, sqlinfo.select_param);
                    if (list.Count() > 0)
                    {
                        var ids = list.Select(t => t.RID).ToList();
                        sqlinfo.update_param.Add(":rid", ids);
                        var cnt = db.Execute(sqlinfo.updatesql, sqlinfo.update_param);
                        return cnt;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
