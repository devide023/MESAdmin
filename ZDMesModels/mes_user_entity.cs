using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_user_entity
    {
        /// <summary>
        ///主键
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///状态
        ///</summary>
        public int status { get; set; }
        /// <summary>
        ///用户编码
        ///</summary>
        public string code { get; set; }
        /// <summary>
        ///用户名称
        ///</summary>
        public string name { get; set; }
        /// <summary>
        ///用户密码
        ///</summary>
        public string pwd { get; set; }
        /// <summary>
        ///Token
        ///</summary>
        public string token { get; set; }
        /// <summary>
        ///头像名称
        ///</summary>
        public string headimg { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public int adduser { get; set; }
        public string addusername { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<dynamic> role { get; set; }
    }

    public class mes_user_entity_mapper : ClassMapper<mes_user_entity>
    {
        public mes_user_entity_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.role).Ignore();
            AutoMap();
        }
    }
}
