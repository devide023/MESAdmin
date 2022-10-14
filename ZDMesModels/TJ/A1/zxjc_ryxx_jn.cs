using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;

namespace ZDMesModels.TJ.A1
{
    public class zxjc_ryxx_jn
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 适应生产线
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 人员帐号
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 技能编号
        /// </summary>
        public string jnbh { get; set; }
        /// <summary>
        /// 技能信息
        /// </summary>
        public string jnxx { get; set; }
        /// <summary>
        /// 适应岗位
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public string sfhg { get; set; } = "Y";
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr
        {
            get;set;
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }=DateTime.Now;
        /// <summary>
        /// 技能分类（装配 测试 校验 安全 其他）
        /// </summary>
        public string jnfl { get; set; }
        /// <summary>
        /// 技能时间
        /// </summary>
        public DateTime jnsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 技能熟练度
        /// </summary>
        public int jnsld { get; set; } = 1;
    }

    public class zxjc_ryxx_jn_mapper : ClassMapper<zxjc_ryxx_jn>
    {
        public zxjc_ryxx_jn_mapper()
        {
            Map(t => t.jnbh).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Ignore();
            AutoMap();
        }
    }
}
