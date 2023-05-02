using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.DuCar
{
    /// <summary>
    /// 点检接口
    /// </summary>
    public interface IDuCarDjgw
    {
        /// <summary>
        /// 生产点检编号
        /// </summary>
        /// <returns></returns>
        string Create_DJNo();
    }
}
