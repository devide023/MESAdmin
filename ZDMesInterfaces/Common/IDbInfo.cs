using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    public interface IDbInfo
    {
        /// <summary>
        /// 关键过滤表明
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetTable(string key);
        /// <summary>
        /// 表名获取该表列信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        IEnumerable<sys_field_info> GetColInfoByTable(string tablename);
    }
}
