using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
namespace ZDMesInterfaces.TJ
{
    public interface IA1BaseInfo
    {
        IEnumerable<dynamic> Get_All_ScxList();
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGWList();
        /// <summary>
        /// 获取生产线岗位
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGWList(string scx);
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_sbxx> GetSbXxList();
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> GetRyXxList();

        IEnumerable<dynamic> GetJxNoByKey(string key);
        /// <summary>
        /// 机型编码获取状态编码
        /// </summary>
        /// <param name="jxno"></param>
        /// <returns></returns>
        IEnumerable<string> GetZtBMByJxNo(string jxno);
        /// <summary>
        /// 关键字获取故障信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<zxjc_fault> GetFaultNoByKey(string key);
        /// <summary>
        /// 获取装配类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetZPLXList();

    }
}
