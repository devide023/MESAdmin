using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_jcgl
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 人员帐号
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 班组（白班/夜班）
        /// </summary>
        public string bzxx { get; set; } = "白班";
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 考核类型
        /// </summary>
        public string lx { get; set; }
        /// <summary>
        /// 奖惩明细信息
        /// </summary>
        public string jcxx { get; set; }
        /// <summary>
        /// 奖惩来源
        /// </summary>
        public string jclr { get; set; } = "奖惩通报";
        /// <summary>
        /// 发生产品数量
        /// </summary>
        public int sl { get; set; } = 1;
        /// <summary>
        /// 奖励金额
        /// </summary>
        public int jcje { get; set; }
        /// <summary>
        /// 发生日期
        /// </summary>
        public DateTime? fsrq { get; set; }
        /// <summary>
        /// 考核人
        /// </summary>
        public string khr { get; set; }
        /// <summary>
        /// 考核部门
        /// </summary>
        public string khbm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 删除标志
        /// </summary>
        public string scbz { get; set; } = "N";
    }
    public class zxjc_jcgl_mapper : ClassMapper<zxjc_jcgl>
    {
        public zxjc_jcgl_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Ignore();
            AutoMap();
        }
    }
}
