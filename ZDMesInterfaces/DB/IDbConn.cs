using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.DB
{
    public interface IDbConn
    {
        string DbConnStr { get; set; }
        void InitDbConn();
        void InitDbConn(String ConnStr);
    }
}
