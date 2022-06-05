using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.SBWB
{
    /// <summary>
    /// 维保周期
    /// </summary>
    public interface ISbWbZq
    {
        IEnumerable<base_sbwb_ls> WbZqList();
    }
}
