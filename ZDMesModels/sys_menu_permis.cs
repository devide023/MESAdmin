using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_menu_permis
    {
        public List<string> funs { get; set; } = new List<string>();
        public List<string> editfields { get; set; } = new List<string>();
        public List<string> hidefields { get; set; } = new List<string>();
        public List<string> batbtns { get; set; } = new List<string>();
    }
}
