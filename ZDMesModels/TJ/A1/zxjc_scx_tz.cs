using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_scx_tz
    {
        /// <summary>
        /// id号，自动生成
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 通知信息
        /// </summary>
        public string tzxx { get; set; }
        /// <summary>
        /// 通知分类 不用
        /// </summary>
        public string tzfl { get; set; }
        /// <summary>
        /// 发出部门 不用
        /// </summary>
        public string fcbm { get; set; }
        /// <summary>
        /// 发出人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 发出日期
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 下班时间 不用
        /// </summary>
        public DateTime xbsj { get; set; }
    }

    public class zxjc_scx_tz_mapper : ClassMapper<zxjc_scx_tz>
    {
        public zxjc_scx_tz_mapper()
        {
            AutoMap();
        }
    }
}
