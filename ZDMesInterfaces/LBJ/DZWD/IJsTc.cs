using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.DZWD
{
    public interface IJsTc
    {
        /// <summary>
        /// 设置技通未分配状态为分配状态
        /// </summary>
        /// <param name="jtids"></param>
        /// <returns></returns>
        bool Update_FpFlag(List<string> jtids);
        /// <summary>
        /// 技术通知能否可以删除
        /// </summary>
        /// <param name="jtid"></param>
        /// <returns></returns>
        bool CanDel(string jtid);
        /// <summary>
        /// 发送到用户的技术通知
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> My_Doc_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 分配明细
        /// </summary>
        /// <param name="jtid"></param>
        /// <returns></returns>
        string Fp_Detail(string jtid);
    }
}
