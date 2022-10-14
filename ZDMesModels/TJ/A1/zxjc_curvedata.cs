using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_curvedata
    {
        /// <summary>
        /// GUID 主键
        /// </summary>
        public string guid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string engineno { get; set; }
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
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime jcsj { get; set; }
        /// <summary>
        /// 录入人 没用
        /// </summary>
        public string jcry { get; set; }
        /// <summary>
        /// 程序号
        /// </summary>
        public string program { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string sblx { get; set; }
        /// <summary>
        /// 压力/扭力采集点数
        /// </summary>
        public int torquepoints { get; set; }
        /// <summary>
        /// 深度/角度采集点数
        /// </summary>
        public int anglepoints { get; set; }
        /// <summary>
        /// 两个压力/扭力点的时间间隔(单位s)
        /// </summary>
        public int ttimeinterval { get; set; }
        /// <summary>
        /// 两个深度/角度点的时间间隔(单位s)
        /// </summary>
        public int atimeinterval { get; set; }
        /// <summary>
        /// 压力/扭力系数
        /// </summary>
        public int tdataratio { get; set; }
        /// <summary>
        /// 深度/角度系数
        /// </summary>
        public int adataratio { get; set; }
        /// <summary>
        /// 压力/扭力曲线数据
        /// </summary>
        public byte[] tlist { get; set; }
        /// <summary>
        /// 深度/角度曲线数据
        /// </summary>
        public byte[] alist { get; set; }
        /// <summary>
        /// 拧紧结果
        /// </summary>
        public string resultid { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public string scbz { get; set; }
    }

    public class zxjc_curvedata_mapper : ClassMapper<zxjc_curvedata>
    {
        public zxjc_curvedata_mapper()
        {
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.billno).Column("bill_no");
            Map(t => t.orderno).Column("order_no");
            Map(t => t.torquepoints).Column("torque_points");
            Map(t => t.anglepoints).Column("angle_points");
            Map(t => t.ttimeinterval).Column("t_time_interval");
            Map(t => t.atimeinterval).Column("a_time_interval");
            Map(t => t.tdataratio).Column("t_data_ratio");
            Map(t => t.adataratio).Column("a_data_ratio");
            Map(t => t.tlist).Column("t_list");
            Map(t => t.alist).Column("a_list");
            AutoMap();
        }
    }
}
