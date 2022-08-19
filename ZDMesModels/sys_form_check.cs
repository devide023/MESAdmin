using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_form_check
    {
        public string model { get; set; }
        public List<sys_form_field> fields { get; set; }
    }

    public class sys_form_field
    {
        public string colname { get; set; }
        public string collabel { get; set; }
        public string msg { get; set; }
    }
}
