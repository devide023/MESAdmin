using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_search_result:sys_result
    {
        public int resultcount { get; set; } = 0;
        public new IEnumerable<dynamic> list { get; set; }
    }
}
