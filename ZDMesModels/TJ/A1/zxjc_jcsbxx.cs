using DapperExtensions.Mapper;
using System;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 检测资产信息表
    /// </summary>
    public class zxjc_jcsbxx
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 资产编号
        /// </summary>
        public string sbbh { get; set; }
        public string sblx { get; set; }
        public string sbmc { get; set; }
        public string jcid { get; set; }
        /// <summary>
        /// 检测表单
        /// </summary>
        public string jcbd { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; } = DateTime.Now;
    }

    public class zxjc_jcsbxx_mapper : ClassMapper<zxjc_jcsbxx>
    {
        public zxjc_jcsbxx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.jcbd).Ignore();
            AutoMap();
        }
    }
}
