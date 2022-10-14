using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.CDGC
{
    public class zxjc_ryxx
    {
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; } = "9902";
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
        public string password { get; set; } = "123456";
        /// <summary>
        ///员工类型
        ///</summary>
        public string rylx { get; set; } = "操作工";
        /// <summary>
        /// 默认岗位
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        ///班组信息
        ///</summary>
        public string bzxx { get; set; } = "白班";
        /// <summary>
        ///合格上岗
        ///</summary>
        public string hgsg { get; set; } = "Y";
        /// <summary>
        ///出生日期
        ///</summary>
        public DateTime? csrq { get; set; }
        /// <summary>
        ///入司日期
        ///</summary>
        public DateTime? rsrq { get; set; }
        /// <summary>
        ///人员性别
        ///</summary>
        public string ryxb { get; set; } = "男";
        /// <summary>
        ///相片名称
        ///</summary>
        public string xpmc { get; set; }
        /// <summary>
        ///离入职信息
        ///</summary>
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
            AutoMap();
        }
    }
}
