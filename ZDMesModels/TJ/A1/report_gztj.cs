using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 故障统计
    /// </summary>
    public class report_gztj
    {
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 检测数量
        /// </summary>
        public int jcsl { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        public int hgsl { get; set; }
        /// <summary>
        /// 不合格数量
        /// </summary>
        public int ngsl { get; set; }
        /// <summary>
        /// 合格率
        /// </summary>
        public double hgl { get; set; }
        /// <summary>
        /// 故障明细
        /// </summary>
        public List<report_gzlx_item> children { get; set; }
    }
    /// <summary>
    /// 检测数据项目
    /// </summary>
    public class report_jc_data_item
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// Vin
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
    }
    public class report_gxmx_data_item
    {
        public string faultno { get; set; }
        public string faultname { get; set; }
        public string jxno { get; set; }
        public string statusno { get; set; }
    }
    /// <summary>
    /// 故障类型统计
    /// </summary>
    public class report_gzlx_item
    {
        /// <summary>
        /// 故障编号
        /// </summary>
        public string faultno { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        public string faultname { get; set; }
        /// <summary>
        /// 发生次数
        /// </summary>
        public int sl { get; set; }
    }
}
