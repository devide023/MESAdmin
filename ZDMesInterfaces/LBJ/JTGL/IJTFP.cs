using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.JTGL
{
    public interface IJTFP
    {
        /// <summary>
        /// 获取未分配技术通知
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> Get_WFP_List();
        /// <summary>
        /// 更新技通分配状态
        /// </summary>
        /// <param name="jtids"></param>
        /// <returns></returns>
        bool Update_JtFpZt(List<string> jtids);
        /// <summary>
        /// 取消分配
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool CancleFP(List<zxjc_t_jstc> entitys);
    }
}
