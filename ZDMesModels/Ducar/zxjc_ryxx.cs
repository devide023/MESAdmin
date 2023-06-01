using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_ryxx
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 课线信息，数字如：1线，维护为1
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 人员帐号,只能由6位数字组成
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码,只能由6位数字组成
        /// </summary>
        public string password { get; set; } = "123456";
        /// <summary>
        /// 岗位信息,如：OP10
        /// </summary>
        public string gwh { get; set; }
        public string gwmc { get; set; }
        /// <summary>
        /// 班组信息（白班、中班、夜班）
        /// </summary>
        public string bzxx { get; set; } = "白班";
        /// <summary>
        /// 合格上岗,代表员工经过安全培训等通用培训,具备上岗条件（Y/N）
        /// </summary>
        public string hgsg { get; set; } = "Y";
        /// <summary>
        /// 入司日期
        /// </summary>
        public DateTime? rsrq { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? csrq { get; set; }
        /// <summary>
        /// 加密号，用于打印为二维码后实现扫描登录
        /// </summary>
        public string jmh { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        /// <summary>
        /// 人员性别
        /// </summary>
        public string ryxb { get; set; } = "男";
        /// <summary>
        /// 人员类型
        /// </summary>
        public string rylx { get; set; } = "装配";
        /// <summary>
        /// 相片名称
        /// </summary>
        public string xpmc { get; set; }
        /// <summary>
        /// 离入职信息，离职的账户无效
        /// </summary>
        public string scbz { get; set; } = "N";
    }
    public class zxjc_ryxx_mapper : ClassMapper<zxjc_ryxx>
    {
        public zxjc_ryxx_mapper()
        {
            Map(t => t.usercode).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Column("user_name");
            Map(t => t.password).Column("pass_word");
            Map(t => t.gwmc).Ignore();
            AutoMap();
        }
    }
}
