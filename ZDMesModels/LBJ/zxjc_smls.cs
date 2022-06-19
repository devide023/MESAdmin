using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 首末件记录表
    /// </summary>
    public class zxjc_smls
    {
        public string rid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string wlmc { get; set; }

        /// <summary>
        /// 产品件号
        /// </summary>
        public string engineno { get; set; }

        /// <summary>
        /// 产品订单号
        /// </summary>
        public string orderno { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public string bc { get; set; }

        /// <summary>
        /// 首末件标识（首件 末件）
        /// </summary>
        public string smjbs { get; set; }

        /// <summary>
        /// 产品状态（待检测、不合格、合格）
        /// </summary>
        public string cpzt { get; set; }

        /// <summary>
        /// 首末件检测照片地址
        /// </summary>
        public string jczpdz { get; set; }

        /// <summary>
        /// 首末件照片检测结果
        /// </summary>
        public string zpjcjg { get; set; }

        /// <summary>
        /// 首末件照片检测人员
        /// </summary>
        public string jcr { get; set; }

        /// <summary>
        /// 首末件检测时间
        /// </summary>
        public DateTime? jcsj { get; set; }

        /// <summary>
        /// 三坐标结果
        /// </summary>
        public string szbjg { get; set; }

        /// <summary>
        /// 三坐标检测人员
        /// </summary>
        public string szbry { get; set; }

        /// <summary>
        /// 三坐标结果时间
        /// </summary>
        public DateTime? szbjcsj { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? wcsj { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public string scbz { get; set; }

        /// <summary>
        /// 是否闭环
        /// </summary>
        public string sfbh { get; set; }
    }

    public class zxjc_smls_mapper : ClassMapper<zxjc_smls>
    {
        public zxjc_smls_mapper()
        {
            Map(t => t.statusno).Column("status_no");
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.orderno).Column("order_no");
            Map(t => t.rid).Ignore();
            Map(t => t.wlmc).Ignore();
            AutoMap();
        }
    }
}
