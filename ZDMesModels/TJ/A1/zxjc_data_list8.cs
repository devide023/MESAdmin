using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_data_list8
    {
        /// <summary>
        /// GUID 主键
        /// </summary>
        public string guid { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string engineno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 计划单号
        /// </summary>
        public string billno { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 装配岗位
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 进站时间
        /// </summary>
        public DateTime jcsj { get; set; }
        /// <summary>
        /// 出站时间
        /// </summary>
        public DateTime czsj { get; set; }
        /// <summary>
        /// 装配时间 秒
        /// </summary>
        public int zpsj { get; set; }
        /// <summary>
        /// 装配人员
        /// </summary>
        public string jcry { get; set; }
        /// <summary>
        /// 装配结果
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 夹具号
        /// </summary>
        public string jjh { get; set; }
        /// <summary>
        /// 夹臂号
        /// </summary>
        public string jbh { get; set; }
        /// <summary>
        /// 返修标志 没用
        /// </summary>
        public string fxflg { get; set; }
        /// <summary>
        /// 检测值 没用
        /// </summary>
        public string jcz { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 设备编号(资产号)? 没用
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备类型? 没用
        /// </summary>
        public string sblx { get; set; }
        /// <summary>
        /// 最小值? 没用
        /// </summary>
        public string gymin { get; set; }
        /// <summary>
        /// 最大值? 没用
        /// </summary>
        public string gymax { get; set; }
        /// <summary>
        /// 标准值? 没用
        /// </summary>
        public string gybz { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public string scbz { get; set; }
        /// <summary>
        /// 装配明细
        /// </summary>
        public List<zxjc_data_detail_mx> zpmxlist { get; set; }
    }

    public class zxjc_data_list8_mapper : ClassMapper<zxjc_data_list8>
    {
        public zxjc_data_list8_mapper()
        {
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.billno).Column("bill_no");
            Map(t => t.orderno).Column("order_no");
            Map(t => t.fxflg).Column("fx_flg");
            Map(t => t.gymin).Column("gy_min");
            Map(t => t.gymax).Column("gy_max");
            Map(t => t.gybz).Column("gy_bz");
            AutoMap();
        }
    }
}
