using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.ZLGL
{
    public interface ILBJCheckImage
    {
        /// <summary>
        /// 根据产品类型、产品面向查找检查图片项，返回图片地址列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<string> GetCheckImages(zxjc_check_image parm);
    }
}
