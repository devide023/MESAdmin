using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class pcs_plcdata_state
    {
        public string id { get; set; }
        public string plcip { get; set; }
        public string dbnum { get; set; }
        public string plcdata { get; set; }
        public DateTime time { get; set; }
        public string commcationdatatype { get; set; }
        public string gwh { get; set; }
        public string jjh { get; set; }
        public string commcationstatus { get; set; }
        public string commcationrresult { get; set; }
        public string gcdm { get; set; }
        public string scx { get; set; }
    }
}
