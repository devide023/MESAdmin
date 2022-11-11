using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_t_dzgy
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 工艺文件ID（GUID）
        /// </summary>
        public string gyid { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 工艺文件编号（调服务获取）
        /// </summary>
        public string gybh { get; set; }
        /// <summary>
        /// 工艺文件名称（调服务获取）
        /// </summary>
        public string gymc { get; set; }
        /// <summary>
        /// 工艺文件描述
        /// </summary>
        public string gyms { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string wjlj { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string jwdx { get; set; }
        /// <summary>
        /// 上传用户名
        /// </summary>
        public string scry { get; set; }
        /// <summary>
        /// 上传电脑名
        /// </summary>
        public string scpc { get; set; }
        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime scsj { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string bbbh { get; set; }
        /// <summary>
        /// 工艺类型
        /// </summary>
        public string gylx { get; set; } = "工艺";
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 状态
        /// </summary>
        public List<dynamic> statusno_list { get; set; }
    }

    public class zxjc_t_dzgy_mapper : ClassMapper<zxjc_t_dzgy>
    {
        public zxjc_t_dzgy_mapper()
        {
            Map(t => t.gyid).Key(KeyType.Assigned);
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.statusno_list).Ignore();
            AutoMap();
        }
    }
}
