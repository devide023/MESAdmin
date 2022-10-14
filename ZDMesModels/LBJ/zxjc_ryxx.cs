using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_ryxx
    {
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///课线信息
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///人员账号
        ///</summary>
        public string usercode { get; set; }
        /// <summary>
        ///人员姓名
        ///</summary>
        public string username { get; set; }
        /// <summary>
        ///密码
        ///</summary>
        public string password { get; set; }
        /// <summary>
        ///员工类型
        ///</summary>
        public string rylx { get; set; }
        /// <summary>
        ///岗位信息
        ///</summary>
        public string gwh { get; set; }
        /// <summary>
        ///班组信息
        ///</summary>
        public string bzxx { get; set; }
        /// <summary>
        ///合格上岗
        ///</summary>
        public string hgsg { get; set; }
        /// <summary>
        ///出生日期
        ///</summary>
        public DateTime csrq { get; set; }
        /// <summary>
        ///入司日期
        ///</summary>
        public DateTime rsrq { get; set; }
        /// <summary>
        ///加密号
        ///</summary>
        public string jmh { get; set; }
        /// <summary>
        ///人员性别
        ///</summary>
        public string ryxb { get; set; }
        /// <summary>
        ///相片名称
        ///</summary>
        public string xpmc { get; set; }
        /// <summary>
        ///离入职信息
        ///</summary>
        public string scbz { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfz { get; set; }
        /// <summary>
        /// 岗位选项
        /// </summary>
        public List<sys_column_options> gwhoptions { get; set; }
    }

    public class zxjc_ryxx_mapper : ClassMapper<zxjc_ryxx>
    {
        public zxjc_ryxx_mapper()
        {
            Map(t => t.usercode).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Column("user_name");
            Map(t => t.password).Column("pass_word");
            Map(t => t.gwhoptions).Ignore();
            AutoMap();
        }
    }
}
