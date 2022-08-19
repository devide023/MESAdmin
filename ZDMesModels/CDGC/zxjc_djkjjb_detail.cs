using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    /// <summary>
    /// 机加明细
    /// </summary>
    public class zxjc_djkjjb_detail
    {
        /// <summary>
        ///ID
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///单据编号，zxjc_djkjjb_bill
        ///</summary>
        public int billid { get; set; }
        /// <summary>
        ///产品名称
        ///</summary>
        public string cpmc { get; set; }
        /// <summary>
        ///库存数量
        ///</summary>
        public int kcsl { get; set; }
        /// <summary>
        ///加工数
        ///</summary>
        public int jgsl { get; set; }
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
        ///库存剩余数量
        ///</summary>
        public int kcsysl { get; set; }

    }

    public class zxjc_djkjjb_detail_mapper : ClassMapper<zxjc_djkjjb_detail>
    {
        public zxjc_djkjjb_detail_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
