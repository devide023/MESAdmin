using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_search
    {
        public string coltype { get; set; } = "string";
        public string left { get; set; }
        public string right { get; set; }
        public string colname { get; set; }
        public string oper { get; set; } = "like";
        public string value { get; set; } = string.Empty;
        public List<string> values { get; set; } = new List<string>();
        public string logic { get; set; } = string.Empty;
    }
}
