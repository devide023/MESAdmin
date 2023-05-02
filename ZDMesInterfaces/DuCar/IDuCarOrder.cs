using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;
namespace ZDMesInterfaces.DuCar
{
    public interface IDuCarOrder
    {
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        pp_zpjh Get_OrdrInfo(string orderno);
        /// <summary>
        /// 订单是否已校验
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        bool IsOrderJy(string orderno);
        /// <summary>
        /// 已齐套
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        sys_order_jy_result YQT(string orderno);
        /// <summary>
        /// 未齐套
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        sys_order_jy_result WQT(string orderno);
        /// <summary>
        /// 是否有工艺路线
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        sys_order_jy_result HasGylx(string orderno);
        /// <summary>
        /// BOM校验
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        sys_order_jy_result HasBOM(string orderno);
        sys_order_jy_result Set_OrderJy_Gylx(string orderno);
        sys_order_jy_result Set_OrderJy_BOM(string orderno);
    }
}
