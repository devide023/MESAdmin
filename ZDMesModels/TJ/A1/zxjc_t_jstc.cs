using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_t_jstc
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 技通ID(GUID)
        /// </summary>
        public string jtid { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 技术通知编号（调用服务获取）
        /// </summary>
        public string jcbh { get; set; }
        /// <summary>
        /// 技术通知名称（调用服务获取）
        /// </summary>
        public string jcmc { get; set; }
        /// <summary>
        /// 文件描述（手输）
        /// </summary>
        public string jcms { get; set; }
        /// <summary>
        /// 文件路径（返回值）
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
        public DateTime? scsj { get; set; }
        /// <summary>
        /// 有效期限-开始
        /// </summary>
        public DateTime? yxqx1 { get; set; }
        /// <summary>
        /// 有效期限-结束
        /// </summary>
        public DateTime? yxqx2 { get; set; }

        /// <summary>
        /// 分配标志
        /// </summary>
        public string fpflg { get; set; } = "N";
        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime? fpsj { get; set; }
        /// <summary>
        /// 分配人
        /// </summary>
        public string fpr { get; set; }
        /// <summary>
        /// 文件分类：技术通知、变更文件、质量事故、安全事故、标准作业培训视频文件、各岗位注意事项
        /// </summary>
        public string wjfl { get; set; } = "技术通知";
        /// <summary>
        /// 技通来源，1：pdm，0：zxjc_t_jstc
        /// </summary>
        public int jtly { get; set; } = 0;
        public string lrr { get; set; }
        public DateTime lrsj { get; set; } =DateTime.Now;
        /// <summary>
        /// 查看数量
        /// </summary>
        public int rcnt { get; set; }
        public string fpscxlist { get; set; }
        /// <summary>
        /// 是否分配到岗位
        /// </summary>
        public string istogwh { get; set; }
        /// <summary>
        /// 多选生产线
        /// </summary>
        public List<string> scxs { get; set; }
        /// <summary>
        /// 阅读人
        /// </summary>
        public string ydrs { get; set; }

    }

    public class zxjc_t_jstc_mapper : ClassMapper<zxjc_t_jstc>
    {
        public zxjc_t_jstc_mapper()
        {
            Map(t => t.jtid).Key(KeyType.Assigned);
            Map(t => t.fpflg).Column("fp_flg");
            Map(t => t.fpsj).Column("fp_sj");
            Map(t => t.rcnt).Ignore();
            Map(t => t.istogwh).Ignore();
            Map(t => t.fpscxlist).Ignore();
            Map(t => t.scxs).Ignore();
            Map(t => t.ydrs).Ignore();
            AutoMap();
        }
    }
}
