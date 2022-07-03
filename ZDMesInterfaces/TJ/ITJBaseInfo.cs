using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ;
namespace ZDMesInterfaces.TJ
{
    public interface ITJBaseInfo
    {
        /// <summary>
        /// 获取生产线信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<tj_base_scxxx> GetScxXx();
        /// <summary>
        /// 获取改为站点（工序站点）
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_gxzd> GetGWZD();
    }
}
