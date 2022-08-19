using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.App
{
    public class AppMenuService : BaseDao<App_Menu>
    {
        public AppMenuService(string constr) : base(constr)
        {
        }
        public override bool Del(IEnumerable<App_Menu> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var menuids = entitys.Select(t => t.id);
                                db.Execute("delete from app_menu where id in :menuid", new { menuid = menuids }, trans);
                                db.Execute("delete FROM app_role_menu where menuid in :menuid", new { menuid = menuids }, trans);
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
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
