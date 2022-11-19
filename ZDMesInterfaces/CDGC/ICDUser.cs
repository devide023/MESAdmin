using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;
namespace ZDMesInterfaces.CDGC
{
    public interface ICDUser
    {
        IEnumerable<zxjc_ryxx> Get_RYXX_List();
    }
}
