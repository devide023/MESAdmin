using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_check_bill_detail
    {
        public int id { get; set; }
        public int billid { get; set; }
        public int checkid { get; set; }
        public string checkval { get; set; }
        public string val1 { get; set; }
        public string val2 { get; set; }

        public zxjc_check_bill CheckBill { get; set; }
        public zxjc_base_check CheckItem { get; set; }
    }

    public class zxjc_check_bill_detail_mapper:ClassMapper<zxjc_check_bill_detail>
    {
        public zxjc_check_bill_detail_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.CheckBill).Ignore();
            Map(t => t.CheckItem).Ignore();
            AutoMap();
        }
    }
}
