using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_result
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<dynamic> list { get; set; } = new List<dynamic>();
        public List<dynamic> noklist { get; set; } = new List<dynamic>();
    }
}
