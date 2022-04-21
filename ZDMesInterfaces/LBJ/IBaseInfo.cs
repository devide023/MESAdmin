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
        /// <summary>
        /// 获取刃具信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_rjxx> GetRjInfo();
        /// <summary>
        /// 获取刀柄基础信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> GetDbInfo();
        /// <summary>
        /// 获取空闲刀柄列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> Get_FreeDb();
        /// <summary>
        /// 获取数控车床设备列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_cnc> Get_CNC_List();
        /// <summary>
        /// 获取设备未绑定CNC列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_cnc> Get_FreeCNC_List();
        /// <summary>
        /// ftp配置信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_ftpfilepath> FtpConfig();
    }
}
