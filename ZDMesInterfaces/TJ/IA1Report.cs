using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 无纸化报表接口
    /// </summary>
    public interface IA1Report
    {
        /// <summary>
        /// 故障统计报表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_gztj> Get_GzTj(sys_page parm, out int resultcount);
        /// <summary>
        /// 干水检对比试验
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_gsjdbsy> Get_GSJDBSY(sys_page parm, out int resultcount);
        /// <summary>
        /// 返修记录表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_fxjlb> Get_FXJLB(sys_page parm, out int resultcount);
        /// <summary>
        /// 检测结果
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_jcjg> Get_JCJG(sys_page parm, out int resultcount);
        /// <summary>
        /// 检测明细
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_jcmx> Get_JCMX(sys_page parm, out int resultcount);
        /// <summary>
        /// 毛巾更换记录
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_mjghjl> Get_MjGhjl(sys_page parm, out int resultcount);
        /// <summary>
        /// 点检记录表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<report_djjl> Get_DJJLB(sys_page parm, out int resultcount);
    }
}
