using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.Common
{
    public class OperateLogService : BaseDao<mes_oper_log>
    {
        private string _constr = string.Empty;
        private IUser _user;
        public OperateLogService(string constr, IUser user) :base(constr)
        {
            _constr = constr;
            _user = user;
        }

        public override IEnumerable<mes_oper_log> GetList(sys_page parm,out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.*, tc.name as menuname ");
                sql.Append(" FROM   mes_oper_log ta, (select menuid, ('/api' || trim(api)) as api");
                sql.Append("          FROM   mes_menu_api) tb, mes_menu_entity tc");
                sql.Append(" where ta.path = tb.api(+)");
                sql.Append(" and tb.menuid = tc.id(+)");
                sql.Append(" and ta.czrid = :czrid ");
                sql.Append(" order by ta.czrq desc ");
                using (var db = new OracleConnection(ConString))
                {
                    parm.sqlparam.Add(":czrid", _user.CurrentUser().id);
                    resultcount = db.ExecuteScalar<int>("select count(id) from mes_oper_log where czrid = :czrid ", parm.sqlparam);
                    return db.Query<mes_oper_log>(OraPager(sql.ToString()), parm.sqlparam);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
