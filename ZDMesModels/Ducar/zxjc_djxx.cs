using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    public class zxjc_djxx
    {
        /// <summary>
        /// 唯一ID(GUID)
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jx_no { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string status_no { get; set; }
        /// <summary>
        /// 点检编号
        /// </summary>
        public string djno { get; set; }
        /// <summary>
        /// 点检内容
        /// </summary>
        public string djxx { get; set; }
        /// <summary>
        /// 点检结果(Y/N)
        /// </summary>
        public string djjg { get; set; }
        /// <summary>
        /// 点检备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 点检人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 点检时间
        /// </summary>
        public DateTime? lrsj { get; set; }
    }

    public class zxjc_djxx_mapper : ClassMapper<zxjc_djxx>
    {
        public zxjc_djxx_mapper()
        {
            Map(t => t.gwmc).Ignore();
            AutoMap();
        }
    }
}
