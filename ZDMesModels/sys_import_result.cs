using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_import_result<T> where T:class,new()
    {
        public List<T> oklist { get; set; }
        public List<T> repeatlist { get; set; }
    }
}
