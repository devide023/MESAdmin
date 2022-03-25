using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_role_entity
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
        ///角色编码
        ///</summary>
        public string code { get; set; }
        /// <summary>
        ///角色名称
        ///</summary>
        public string name { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public int adduser { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime addtime { get; set; }

    }

    public class mes_role_entity_mapper : ClassMapper<mes_role_entity>
    {
        public mes_role_entity_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
