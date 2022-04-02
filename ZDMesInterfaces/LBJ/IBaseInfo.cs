using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ
{
    public interface IBaseInfo
    {
        /// <summary>
        /// 工厂信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_gcxx> GetGCXX();
        /// <summary>
        /// 生产线信息
        /// </summary>
        /// <param name="gcdm"></param>
        /// <returns></returns>
        IEnumerable<base_scxxx> GetScxXX(string gcdm);
        /// <summary>
        /// 岗位站点信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGwZd();
        /// <summary>
        /// 查询员工编码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> GetUserCode(string key);
    }
}
