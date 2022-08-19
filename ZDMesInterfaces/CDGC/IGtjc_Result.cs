using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    /// <summary>
    /// 缸体检测数据
    /// </summary>
    public interface IGtjc_Result
    {
        /// <summary>
        /// 保存缸体检测单据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        bool Update_Gtjc_Bill(zxjc_gtjc_bill bill);
        /// <summary>
        /// 保存缸体检测数据
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        bool Save_Gtjc_CheckData(zxjc_gtjc_bill bill);
        /// <summary>
        /// 生成4台机台单据id
        /// </summary>
        /// <param name="th"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        List<int> Create_Bill_Ids(string cplx,DateTime rq);
        /// <summary>
        /// 二维码查检测数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IEnumerable<zxjc_gtjc_detail> Get_CheckData_Vin(string rq,string code,string cplx);


    }
}
