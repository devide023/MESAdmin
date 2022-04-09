using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.SBWB
{
    public interface ISbWbZq
    {
        IEnumerable<base_sbwb_ls> WbZqList();
    }
}
