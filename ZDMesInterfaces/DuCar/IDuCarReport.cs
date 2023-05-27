using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesInterfaces.DuCar
{
    public interface IDuCarReport
    {
        /// <summary>
        /// 单台数据追溯
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<ducar_report_dtzs> Get_Engine_No_Data(sys_page parm,out int resultcount);
        /// <summary>
        /// 岗位数据 
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_data_list> Get_Gwzs_Data(sys_page parm,out int resultcount);
        /// <summary>
        /// 故障统计
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<ducar_report_fault> Get_Fault_Static(sys_page parm, out int resultcount);
    }
}
