using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.TJ.A1;
namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 技通分配组别设置
    /// </summary>
    public interface IJTFPRY
    {
        /// <summary>
        /// 获取当前登录用户所在组别能看到的技术通知
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstcfp> Get_User_JSTZFp(IUser user);
        /// <summary>
        /// 获取所有技通分配组别
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstcfp_group> Get_All_Group();
    }
}
