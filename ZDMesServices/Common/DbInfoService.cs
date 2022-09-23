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
    public class DbInfoService: OracleBaseFixture,IDbInfo
    {
        public DbInfoService(string constr) : base(constr)
        {

        }

        public IEnumerable<sys_field_info> GetColInfoByTable(string tablename)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT lower(replace(tb.COLUMN_NAME, '_', '')) as prop, decode(instr(tb.COLUMN_NAME, '_'), 0, '', lower(tb.COLUMN_NAME)) as dbprop, ta.COMMENTS as label,case ");
            sql.Append(" when instr(tb.DATA_TYPE, 'VARCHAR') > 0 then  ");
            sql.Append("  'string' ");
            sql.Append(" when instr(tb.DATA_TYPE, 'DATE') > 0 then ");
            sql.Append("  'date' ");
            sql.Append(" when instr(tb.DATA_TYPE, 'NUMBER') > 0 then ");
            sql.Append("  'int' ");
            sql.Append(" end as coltype ");
            sql.Append(" FROM   USER_TAB_COLUMNS tb, USER_COL_COMMENTS ta ");
            sql.Append(" WHERE  tb.TABLE_NAME = :tablename ");
            sql.Append(" AND    tb.TABLE_NAME = ta.TABLE_NAME ");
            sql.Append(" AND    tb.COLUMN_NAME = ta.COLUMN_NAME ");
            sql.Append(" order  by tb.column_id");
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<sys_field_info>(sql.ToString(), new { tablename = tablename.ToUpper() });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<dynamic> GetTable(string key)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT lower(ta.TABLE_NAME) as table_name,tb.COMMENTS ");
            sql.Append(" FROM   user_tables ta, user_tab_comments tb");
            sql.Append(" WHERE  ta.TABLE_NAME = tb.TABLE_NAME");
            sql.Append(" and ta.table_name like :key ");
            sql.Append(" ORDER  BY ta.TABLE_NAME");
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<dynamic>(sql.ToString(), new { key = "%" + key.ToUpper() + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
