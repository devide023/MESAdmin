using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.ZLGL
{
    public interface ILBJProductCheck
    {
        /// <summary>
        /// 获取产品型号检查项目
        /// </summary>
        /// <param name="cpxh"></param>
        /// <returns></returns>
        IEnumerable<zxjc_base_check> GetCheckItemsByCpxh(string cpxh);
        /// <summary>
        /// 保存检测项数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool Save_CheckBill_Data(sys_checkbill_form form);
        /// <summary>
        /// 关键字获取产品型号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<string> GetCpxhByKey(string key);
        /// <summary>
        /// 获取检测单据信息
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        zxjc_check_bill Get_BillInfo_ById(string billid);
    }
}
