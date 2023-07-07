using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// App检测单据
    /// </summary>
    public class zxjc_jcbill
    {
        public string id { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string jclx { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime? jcrq { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string zcbh { get; set; }
        /// <summary>
        /// 设备管理员
        /// </summary>
        public string sbgly { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 监督确认人
        /// </summary>
        public string jdqr { get; set; }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? jdrqsj { get; set; }
        /// <summary>
        /// 检测明细
        /// </summary>
        public IEnumerable<zxjc_jcmx> jcmxlist { get; set; }
    }

    public class zxjc_jcbill_mapper : ClassMapper<zxjc_jcbill>
    {
        public zxjc_jcbill_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.jcmxlist).Ignore();
            AutoMap();
        }
    }
}
