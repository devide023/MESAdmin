using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class sys_checkbill_form
    {
        public int id { get; set; }
        public string scx { get; set; }
        public string bmmc { get; set; }
        public DateTime rq { get; set; }
        public string bc { get; set; }
        public string cpxh { get; set; }
        public string cpmc { get; set; }
        public string gxmc { get; set; }
        public string khmc { get; set; }
        public string jcjg { get; set; }
        public string bz { get; set; }
        public string vin { get; set; }
        public string smjbs { get; set; }
        public string jjh { get; set; }
        public string xgr { get; set; }
        public string lrr { get; set; }

        public List<zxjc_check_bill_detail> BillDetails { get; set; }
    }
}
