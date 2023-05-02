using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_gylx
    {

        public string rid { get; set; }
        public string rid2 { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 右连接机型
        /// </summary>
        public string jxno2 { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 右连接状态码
        /// </summary>
        public string statusno2 { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 是否装配
        /// </summary>
        public string sfzp { get; set; }
        /// <summary>
        /// 右连接字段
        /// </summary>
        public string sfzp2 { get; set; }
        /// <summary>
        /// 装配顺序
        /// </summary>
        public int zpsx { get; set; }
        /// <summary>
        /// 是否免检(每台检 N， 首检S，免检Y)
        /// </summary>
        public string mj { get; set; }
        /// <summary>
        /// 互锁标志
        /// </summary>
        public string fsbz { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 复检编号
        /// </summary>
        public string fjbh { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 审核人
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? shsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 互锁人
        /// </summary>
        public string fsr { get; set; }
        /// <summary>
        /// 互锁时间
        /// </summary>
        public DateTime? fssj { get; set; }
        /// <summary>
        /// 自关联
        /// </summary>
        public zxjc_gylx zxjcgylx { get; set; }
    }

    public class zxjc_gylx_mapper : ClassMapper<zxjc_gylx>
    {
        public zxjc_gylx_mapper()
        {
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.jxno).Key(KeyType.Assigned);
            Map(t => t.rid).Ignore();
            Map(t => t.rid2).Ignore();
            Map(t => t.gwmc).Ignore();
            Map(t => t.jxno2).Ignore();
            Map(t => t.statusno2).Ignore();
            Map(t => t.sfzp2).Ignore();
            Map(t => t.zxjcgylx).Ignore();
            AutoMap();
        }
    }
}
