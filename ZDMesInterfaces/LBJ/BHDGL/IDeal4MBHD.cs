using System.Collections.Generic;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.BHDGL
{
    /// <summary>
    /// 4M变化点处理
    /// </summary>
    public interface IDeal4MBHD
    {
        /// <summary>
        /// 操作员变化点列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<lbj_qms_4mbhd> Get_Czy_BHD_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 保存操作员变化点处理信息
        /// </summary>
        /// <returns></returns>
        bool Save_Czy_BHD_Deal(lbj_qms_4mbhd entity);
        /// <summary>
        /// 生产班组变化点列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<lbj_qms_4mbhd> Get_Scbz_BHD_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 保存生产班组变化点信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Save_Scbz_BHD_Deal(lbj_qms_4mbhd entity);
        /// <summary>
        /// 现场巡检变化点列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<lbj_qms_4mbhd> Get_Xcxj_BHD_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 保存现场巡检变化点信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Save_Xcxj_BHD_Deal(lbj_qms_4mbhd entity);
    }
}
