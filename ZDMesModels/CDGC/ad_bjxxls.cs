using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class ad_bjxxls
    {
        /// <summary>
        ///ID 主键
        ///</summary>
        public string guid { get; set; }
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///生产线
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///报警主体
        ///</summary>
        public string bjzt { get; set; }
        /// <summary>
        ///异常原因（质量异常、物料异常、设备异常、其它异常、AGV故障）
        ///</summary>
        public string ycyy { get; set; }
        /// <summary>
        ///设备编码
        ///</summary>
        public string sbbh { get; set; }
        /// <summary>
        ///故障代码
        ///</summary>
        public string gzdm { get; set; }
        /// <summary>
        ///故障信息
        ///</summary>
        public string gzxx { get; set; }
        /// <summary>
        ///故障等级
        ///</summary>
        public string gzdj { get; set; }
        /// <summary>
        ///异常开始时间
        ///</summary>
        public DateTime kssj { get; set; }
        /// <summary>
        ///异常结束时间
        ///</summary>
        public DateTime? jssj { get; set; }
        /// <summary>
        ///异常时间（秒）
        ///</summary>
        public int ycsj { get; set; }
        /// <summary>
        ///删除标志 （Y/N）
        ///</summary>
        public string scbj { get; set; } = "N";
        /// <summary>
        ///记录日期
        ///</summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///闭环人
        ///</summary>
        public string bhlrr { get; set; }

    }

    public class ad_bjxxls_mapper : ClassMapper<ad_bjxxls>
    {
        public ad_bjxxls_mapper()
        {
            AutoMap();
        }
    }
}
