using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    public interface IJTFPSCX
    {
        /// <summary>
        /// 是否可以删除已分配到生产线的技通
        /// </summary>
        /// <param name="jcbh"></param>
        /// <returns></returns>
        bool CanRemove(string jcbh);
        /// <summary>
        /// 技术通知阅读
        /// </summary>
        /// <param name="jcbh"></param>
        /// <returns></returns>
        bool Read_PDM_JSTZ(string jcbh);
        /// <summary>
        /// 获取PDM技术通知
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> Get_PDM_JSTZ_List(sys_page parm, out int resultcount);
    }
}
