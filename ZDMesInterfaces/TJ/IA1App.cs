using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 统计无纸化App项目接口
    /// </summary>
    public interface IA1App
    {
        /// <summary>
        /// 获取检测基础信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<zxjc_jclx> Get_JCLX(sys_jclx_form parm);
        /// <summary>
        /// 获取检测项目
        /// </summary>
        /// <param name="jclx"></param>
        /// <returns></returns>
        IEnumerable<zxjc_jcjcxx> Get_JCXM(sys_jcmx_form parm);
        /// <summary>
        /// 根据查询条件获取检测单
        /// </summary>
        /// <param name="bil"></param>
        /// <returns></returns>
        IEnumerable<zxjc_jcbill> Get_JCBills(zxjc_jcbill bil);
        /// <summary>
        /// 保存检测明细
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Save_App_JCMX(sys_app_jc_form form);
        /// <summary>
        /// 监督确认检测项列表
        /// </summary>
        /// <param name="bil"></param>
        /// <returns></returns>
        IEnumerable<zxjc_jcjcxx> Get_JDQR_JcxList(string billid);
        /// <summary>
        /// 单据监督确认
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        bool JcBill_JDQR(sys_jcbill_jdqr parm);
        /// <summary>
        /// 根据设备编号获取检测项目
        /// </summary>
        /// <param name="zcbh"></param>
        /// <returns></returns>
        IEnumerable<zxjc_jcjcxx> Get_Jcxm_ByZcbh(sys_app_dj_form form);
    }
}
