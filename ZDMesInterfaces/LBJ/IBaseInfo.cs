using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ
{
    /// <summary>
    /// 基础信息接口
    /// </summary>
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
        /// 生产线-子线信息
        /// </summary>
        /// <param name="gcdm"></param>
        /// <returns></returns>
        IEnumerable<base_scxxx_jj> Get_ScxXX_JJ(string scx);
        /// <summary>
        /// 岗位信息
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGwXX(string scx);
        IEnumerable<base_gwzd> GetGwListByScx(string scx);
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
        /// 关键字过滤刃具信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<base_rjxx> GetRjInfoByKey(string key);
        /// <summary>
        /// 未使用刃具信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<dynamic> Get_UnUse_RjInfo(List<string> dbh);
        /// <summary>
        /// 获取刀柄基础信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> GetDbInfo();
        /// <summary>
        /// 关键字获取刀柄信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> GetDbInfo_By_Key(string key);
        /// <summary>
        /// 未使用刀柄信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> Get_UnUse_DbInfo();
        /// <summary>
        /// 未使用刀柄信息,换刀
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> Get_UnUseDbList();
        /// <summary>
        /// 未使用刀柄，刃具信息树形
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbxx> UnUse_DbRj_Tree();
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_sbxx> Get_SBXX_List();
        
        /// <summary>
        /// 获取物料编码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<base_wlxx> WLBM_By_Key(string key);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> UserList();
        /// <summary>
        /// 人员信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> RyxxList();
    }
}
