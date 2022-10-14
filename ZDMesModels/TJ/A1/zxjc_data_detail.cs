using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_data_detail
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string engineno { get; set; }
        /// <summary>
        /// 螺栓计数
        /// </summary>
        public string lsjs { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 计划单号
        /// </summary>
        public string billno { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime jcsj { get; set; }
        /// <summary>
        /// 录入人? 没用
        /// </summary>
        public string jcry { get; set; }
        /// <summary>
        /// 结果（合格OK，不合格NOK）
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 没用
        /// </summary>
        public int channel { get; set; }
        /// <summary>
        /// 程序号
        /// </summary>
        public string program { get; set; }
        /// <summary>
        /// 没用
        /// </summary>
        public int cycle { get; set; }
        /// <summary>
        /// 扭力 压力 加油量 泄漏值
        /// </summary>
        public string data1 { get; set; }
        /// <summary>
        /// 角度 深度 加油类型
        /// </summary>
        public string data2 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public string data3 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public string data4 { get; set; }
        /// <summary>
        /// 没用
        /// </summary>
        public int dup { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string sblx { get; set; }
        /// <summary>
        /// PCS_STATIONINOUT_LOG.ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public string scbz { get; set; }
    }

    public class zxjc_data_detail_mapper : ClassMapper<zxjc_data_detail>
    {
        public zxjc_data_detail_mapper()
        {
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.billno).Column("bill_no");
            Map(t => t.orderno).Column("order_no");
            AutoMap();
        }
    }
}
