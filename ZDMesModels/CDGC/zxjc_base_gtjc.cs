using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    /// <summary>
    /// 缸体检测数据基础数据
    /// </summary>
    public class zxjc_base_gtjc
    {
        /// <summary>
        ///检测基础数据id
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///产品类型
        ///</summary>
        public string cplx { get; set; }
        /// <summary>
        ///模号
        ///</summary>
        public string mh { get; set; }
        /// <summary>
        ///产品方位
        ///</summary>
        public string cpfw { get; set; }
        /// <summary>
        ///图号
        ///</summary>
        public string th { get; set; }
        /// <summary>
        ///孔系名称
        ///</summary>
        public string kxmc { get; set; }
        /// <summary>
        ///孔径尺寸
        ///</summary>
        public string kjcc { get; set; }
        /// <summary>
        ///孔径上限
        ///</summary>
        public string kjccsx { get; set; }
        /// <summary>
        ///孔径下限
        ///</summary>
        public string kjccxx { get; set; }
        /// <summary>
        ///深度面距
        ///</summary>
        public string sdmj { get; set; }
        /// <summary>
        ///深度面距上限
        ///</summary>
        public string sdmjsx { get; set; }
        /// <summary>
        ///深度面距下限
        ///</summary>
        public string sdmjxx { get; set; }
        /// <summary>
        /// 孔径输入类型
        /// </summary>
        public string kjtype { get; set; }
        /// <summary>
        /// 深度、面距输入类型
        /// </summary>
        public string sdtype { get; set; }
        /// <summary>
        /// 孔径展示值
        /// </summary>
        public string kjzsz { get; set; }
        /// <summary>
        /// 深度、面距展示值
        /// </summary>
        public string sdzsz { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int seq { get; set; }

    }

    public class zxjc_base_gtjc_mapper : ClassMapper<zxjc_base_gtjc>
    {
        public zxjc_base_gtjc_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.sdmjsx).Column("sdmj_sx");
            Map(t => t.sdmjxx).Column("sdmj_xx");
            Map(t => t.kjccxx).Column("kjcc_xx");
            Map(t => t.kjccsx).Column("kjcc_sx");
            AutoMap();
        }
    }
}
