using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class zxjc_gtjc_detail
    {
        /// <summary>
        ///
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///检测单id,ZXJC_GTJC_BILL.id
        ///</summary>
        public int billid { get; set; }
        /// <summary>
        ///检测id
        ///</summary>
        public int jcid { get; set; }
        /// <summary>
        ///产品方位
        ///</summary>
        public string cpfw { get; set; }
        /// <summary>
        ///孔系名称
        ///</summary>
        public string kxmc { get; set; }
        /// <summary>
        ///孔系尺寸
        ///</summary>
        public string kxcc { get; set; }
        /// <summary>
        ///深度面距
        ///</summary>
        public string sdmj { get; set; }
        /// <summary>
        ///孔径值
        ///</summary>
        public string kjval { get; set; }
        /// <summary>
        ///深度面距值
        ///</summary>
        public string sdmjval { get; set; }
        /// <summary>
        ///检测结果
        ///</summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 孔径类型，radio\input
        /// </summary>
        public string kjtype { get; set; }
        /// <summary>
        /// 深度类型，input\none\T
        /// </summary>
        public string sdtype { get; set; }
        /// <summary>
        /// 缸体检测基础信息
        /// </summary>
        public zxjc_base_gtjc basegtjc { get; set; }
        /// <summary>
        /// 孔径值是否合格
        /// </summary>
        public bool kjzsfhg { get; set; } = true;
        /// <summary>
        /// 深度值是否合格
        /// </summary>

        public bool sdzsfhg { get; set; } = true;

    }

    public class zxjc_gtjc_detail_mapper : ClassMapper<zxjc_gtjc_detail>
    {
        public zxjc_gtjc_detail_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.kjtype).Ignore();
            Map(t => t.sdtype).Ignore();
            Map(t => t.basegtjc).Ignore();
            Map(t=>t.kjzsfhg).Ignore();
            Map(t=>t.sdzsfhg).Ignore();
            AutoMap();
        }
    }
}
