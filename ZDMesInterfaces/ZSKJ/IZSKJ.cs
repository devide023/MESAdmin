using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.ZSKJ
{
    public interface IZSKJ
    {
        IEnumerable<string> Get_CXDH_List();
        IEnumerable<string> Get_Jcsx_By_CXDH(string cxdh);
    }
}
