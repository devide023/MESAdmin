using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.BHDGL
{
    public interface IBHD
    {
        /// <summary>
        /// 变化点最大数
        /// </summary>
        /// <returns></returns>
        int Get_Max_BHD();
        /// <summary>
        /// 变化点编号是否存在
        /// </summary>
        /// <param name="bhdno"></param>
        /// <returns></returns>
        bool IsExistBhdNo(string bhdno);
    }
}
