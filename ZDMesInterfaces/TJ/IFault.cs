using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
namespace ZDMesInterfaces.TJ
{
    public interface IFault
    {
        /// <summary>
        /// 故障名称生成故障代码，相同故障名称，相同故障代码
        /// </summary>
        /// <param name="faultname"></param>
        /// <returns></returns>
        /// 
        string Create_FaultNo(string faultname);
        List<zxjc_fault> Create_FaultNo_List(List<zxjc_fault> list);
    }
}
