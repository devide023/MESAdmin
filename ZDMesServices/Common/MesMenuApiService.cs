using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
namespace ZDMesServices.Common
{
    public class MesMenuApiService: OracleBaseFixture, IMesMenuApi
    {
        public MesMenuApiService(string constr):base(constr)
        {

        }

        public void Update_Mes_Api(sys_menu_api entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id FROM mes_menu_entity where configpath = :filename ");
                StringBuilder tsql = new StringBuilder();
                tsql.Append("insert into mes_menu_api ");
                tsql.Append(" (menuid, api) ");
                tsql.Append(" select :menuid, :api ");
                tsql.Append(" from dual ");
                tsql.Append(" where not exists(select * ");
                tsql.Append("         from mes_menu_api ");
                tsql.Append("         where menuid = :menuid ");
                tsql.Append("         and    api = :api)");
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query(sql.ToString(), new { filename = entity.filename });
                    if (q.Count() > 0)
                    {
                        List<dynamic> data = new List<dynamic>();
                        var menuid = q.First().ID;
                        foreach (var item in entity.apis)
                        {
                            data.Add(new { menuid = menuid, api = item });
                        }
                        db.Execute(tsql.ToString(), data);
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
