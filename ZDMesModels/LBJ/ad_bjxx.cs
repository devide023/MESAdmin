using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 报警信息表
    /// </summary>
    public class ad_bjxx
    {
        /// <summary>
        /// ID主键
        /// </summary>
        public string autoid { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }

        /// <summary>
        /// 报警主体 岗位号、设备号
        /// </summary>
        public string bjzt { get; set; }

        /// <summary>
        /// 质量异常(Y/N)
        /// </summary>
        public string zlyc { get; set; }

        /// <summary>
        /// 物料异常(Y/N)
        /// </summary>
        public string wlyc { get; set; }

        /// <summary>
        /// 设备异常(Y/N)
        /// </summary>
        public string sbyc { get; set; }

        /// <summary>
        /// 其他异常(Y/N)
        /// </summary>
        public string qtyc { get; set; }

        /// <summary>
        /// 刀具异常(Y/N)
        /// </summary>
        public string djyc { get; set; }

        /// <summary>
        /// 夹具异常(Y/N)
        /// </summary>
        public string jjyc { get; set; }

        /// <summary>
        /// 故障代码
        /// </summary>
        public string gzdm { get; set; }

        /// <summary>
        /// 故障信息
        /// </summary>
        public string gzxx { get; set; }

        /// <summary>
        /// 删除标志(Y/N)
        /// </summary>
        public string scbj { get; set; }

        /// <summary>
        /// 质量异常开始时间
        /// </summary>
        public string zlyc_kssj { get; set; }

        /// <summary>
        /// 设备异常开始时间
        /// </summary>
        public string sbyc_kssj { get; set; }

        /// <summary>
        /// 物料异常开始时间
        /// </summary>
        public string wlyc_kssj { get; set; }

        /// <summary>
        /// 其他异常开始时间
        /// </summary>
        public string qtyc_kssj { get; set; }

        /// <summary>
        /// 刀具异常开始时间
        /// </summary>
        public string djyc_kssj { get; set; }

        /// <summary>
        /// 夹具异常开始时间
        /// </summary>
        public string jjyc_kssj { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        public string lrsj { get; set; }
    }

    public class ad_bjxx_mapper : ClassMapper<ad_bjxx>
    {
        public ad_bjxx_mapper()
        {
            AutoMap();
        }
    }
}
