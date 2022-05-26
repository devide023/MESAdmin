using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_oper_log
    {
        public int id { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string lx { get; set; }
        /// <summary>
        /// 原数据
        /// </summary>
        public string olddata { get; set; }
        /// <summary>
        /// 新数据
        /// </summary>
        public string newdata { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string czr { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public int czrid { get; set; }
        /// <summary>
        /// 路由路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        public string route { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuname { get; set; }
    }
    public class mes_oper_log_mapper : ClassMapper<mes_oper_log>
    {
        public mes_oper_log_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.route).Ignore();
            Map(t => t.menuname).Ignore();
            AutoMap();
        }
    }
}
