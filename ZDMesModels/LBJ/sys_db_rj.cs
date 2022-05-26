using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刀柄刃具对应关系，一个刀柄号对应多个刃具
    /// </summary>
    public class sys_db_rj_gx
    {
        public string value { get; set; }
        public string label { get; set; }
        public List<dynamic> children { get; set; }

    }
}
