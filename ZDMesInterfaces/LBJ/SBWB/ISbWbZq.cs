using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.SBWB
{
    /// <summary>
    /// 维保周期
    /// </summary>
    public interface ISbWbZq
    {
        IEnumerable<base_sbwb_ls> WbZqList();
        /// <summary>
        /// 所有维保数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_sbwb> WbXxList(base_sbwb item);
        /// <summary>
        /// 生产线+子线获取维保信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IEnumerable<base_sbwb> ScxZxWbXxList(base_sbwb item);
        /// <summary>
        /// 保存设备维保（2023-07-25）
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool SaveSbWbInfo(List<base_sbwb_ls> entitys);
    }
}
