using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_gwzd_fxmx
    {
        /// <summary>
        /// 返修ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 机号（完整）
        /// </summary>
        public string engineno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 故障现象
        /// </summary>
        public string gzxx { get; set; }
        /// <summary>
        /// 检测部件
        /// </summary>
        public string jcbj { get; set; }
        /// <summary>
        /// 建议处理岗位
        /// </summary>
        public string jyclgw { get; set; }
        /// <summary>
        /// 故障录入人
        /// </summary>
        public string jcry { get; set; }
        /// <summary>
        /// 故障录入时间
        /// </summary>
        public DateTime? jcsj { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public string clfs { get; set; }
        /// <summary>
        /// 实际处理岗位
        /// </summary>
        public string sjclgw { get; set; }
        /// <summary>
        /// 涉及部件
        /// </summary>
        public string sjbjbm { get; set; }
        /// <summary>
        /// 涉及厂商
        /// </summary>
        public string sjbjcs { get; set; }
        /// <summary>
        /// 原因分析
        /// </summary>
        public string yyfx { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string cljg { get; set; }
        /// <summary>
        /// 返修机标志
        /// </summary>
        public string fxjflg { get; set; }
        /// <summary>
        /// 处理录入人
        /// </summary>
        public string fxr { get; set; }
        /// <summary>
        /// 处理录入时间
        /// </summary>
        public DateTime? fxsj { get; set; }
        /// <summary>
        /// 闭环标志
        /// </summary>
        public string bhbz { get; set; }
        /// <summary>
        /// 闭环录入人
        /// </summary>
        public string bhr { get; set; }
        /// <summary>
        /// 闭环录入时间
        /// </summary>
        public DateTime? bhsj { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public string faultno { get; set; }
        /// <summary>
        /// 标注坐标
        /// </summary>
        public string handno { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string tpzb { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string tpname { get; set; }
    }

    public class zxjc_gwzd_fxmx_mapper : ClassMapper<zxjc_gwzd_fxmx>
    {
        public zxjc_gwzd_fxmx_mapper()
        {
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.orderno).Column("order_no");
            Map(t => t.fxjflg).Column("fxj_flg");
            Map(t => t.faultno).Column("fault_no");
            Map(t => t.handno).Column("hand_no");
            Map(t => t.gwmc).Ignore();
            AutoMap();
        }
    }
}
