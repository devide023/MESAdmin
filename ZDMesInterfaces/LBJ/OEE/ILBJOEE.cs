using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.OEE
{
    public interface ILBJOEE
    {
        /// <summary>
        /// 获取生产线OEE数据模板
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        zxjc_scx_oee Get_OEEDataByScx(string scx);
        /// <summary>
        /// OEE模板是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsOEETemplateExist(base_template_scx_oee entity);
        /// <summary>
        /// 生产线OEE是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsScxOEEExist(zxjc_scx_oee entity);
    }
}
