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
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGWList();
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

    }
}
