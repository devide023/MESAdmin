using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    /// <summary>
    /// 电机壳交接班，后序明细
    /// </summary>
    public class zxjc_djkjjb_hx_detail
    {
        /// <summary>
        ///ID
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///单据id,zxjc_djkjjb_bill.id
        ///</summary>
        public int billid { get; set; }
        /// <summary>
        ///项目名称
        ///</summary>
        public string xmmc { get; set; }
        /// <summary>
        ///投入加工数
        ///</summary>
        public int trjgsl { get; set; }
        /// <summary>
        ///待评审数量
        ///</summary>
        public int dpssl { get; set; }
        /// <summary>
        ///工废数量
        ///</summary>
        public int gfsl { get; set; }
        /// <summary>
        ///料废数量
        ///</summary>
        public int lfsl { get; set; }
        /// <summary>
        ///合格数量
        ///</summary>
        public int hgsl { get; set; }

    }
    
    public class zxjc_djkjjb_hx_detail_mapper:ClassMapper<zxjc_djkjjb_hx_detail>
    {
        public zxjc_djkjjb_hx_detail_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
