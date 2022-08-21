using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class zxjc_gtjjb_bill_detail
    {
        /// <summary>
        ///ID
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///单据id,zxjc_gtjjb_bill.id
        ///</summary>
        public int billid { get; set; }
        /// <summary>
        ///产品名称
        ///</summary>
        public string cpmc { get; set; }
        /// <summary>
        ///上班次毛坯余量
        ///</summary>
        public int sbcmpyl { get; set; }
        /// <summary>
        ///当班毛坯数量
        ///</summary>
        public int dbmpsl { get; set; }
        /// <summary>
        ///换产数量
        ///</summary>
        public int hcsl { get; set; }
        /// <summary>
        ///投入加工数
        ///</summary>
        public int trjgs { get; set; }
        /// <summary>
        ///工废数
        ///</summary>
        public int gfsl { get; set; }
        /// <summary>
        ///料废数
        ///</summary>
        public int lfsl { get; set; }
        /// <summary>
        ///合格数
        ///</summary>
        public int hgsl { get; set; }
        /// <summary>
        ///当班毛胚余量
        ///</summary>
        public int dbmpyl { get; set; }
    }

    public class zxjc_gtjjb_bill_detail_mapper:ClassMapper<zxjc_gtjjb_bill_detail>
    {
        public zxjc_gtjjb_bill_detail_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
