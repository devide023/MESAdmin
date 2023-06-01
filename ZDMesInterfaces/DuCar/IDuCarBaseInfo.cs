using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesInterfaces.DuCar
{
    public interface IDuCarBaseInfo
    {
        /// <summary>
        /// 获取生产线信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_scxxx> Get_All_ScxList();
        IEnumerable<base_gwzd> GetGWList();
        /// <summary>
        /// 获取生产线岗位
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGWList(string scx);
        /// <summary>
        /// 获取生产线人员岗位
        /// </summary>
        /// <param name="scx"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        IEnumerable<base_gwzd> GetGWByRyList(string scx,string usercode);
        /// <summary>
        /// 所有设备信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_sbxx> GetSbXxList();
        /// <summary>
        /// 获取岗位设备信息
        /// </summary>
        /// <param name="gwh"></param>
        /// <returns></returns>
        IEnumerable<base_sbxx> GetSbXxByGwbh(string gwh);
        /// <summary>
        /// 人员信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> GetRyXxList();
        /// <summary>
        /// 人员信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> GetRyXxByKey(string key);
        /// <summary>
        /// 生产线人员信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<zxjc_ryxx> GetScxRyXxByKey(string scx,string key);
        /// <summary>
        /// 通过岗位编号查找设备列表
        /// </summary>
        /// <param name="gwh"></param>
        /// <returns></returns>
        IEnumerable<base_sbxx> GetSbbhsByGwh(string scx,string gwh);
        /// <summary>
        /// 故障名称获取故障编号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<zxjc_fault> Get_FaultNo_By_Key(string key);
        /// <summary>
        /// 订单号建议输入
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<pp_zpjh> Get_OrderNo_By_Key(string key);
        /// <summary>
        /// 关键字过滤机型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<string> Get_JxNo_ByCode(string key);
        /// <summary>
        /// 机型编码查状态编码
        /// </summary>
        /// <param name="jxno"></param>
        /// <returns></returns>
        IEnumerable<string> Get_Ztbm_ByJxno(string jxno);
        /// <summary>
        /// 关键字获取物料信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<base_wlxx> Get_WlxxByKey(string key);
    }
}
