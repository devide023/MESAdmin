using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesInterfaces.DuCar
{
    /// <summary>
    /// 看护岗位
    /// </summary>
    public interface IDuCarKhGw
    {
        /// <summary>
        /// 获取人工岗位号所看护岗位
        /// </summary>
        /// <param name="gwh"></param>
        /// <returns></returns>
        IEnumerable<base_gwzd> Get_Khgw_List(string scx,string gwh);
    }
}
