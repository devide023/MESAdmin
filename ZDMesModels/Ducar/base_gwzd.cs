﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class base_gwzd
    {
        public string rid { get; set; }
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
        /// 岗位分类（部装、装配、测试、返工）
        /// </summary>
        public string gwlx { get; set; }
        /// <summary>
        /// 岗位分类（人工、自动）
        /// </summary>
        public string gwfl { get; set; }
        /// <summary>
        /// 审核标志（Y已审核 N未审核）
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 故障停用（Y停用 N启用）
        /// </summary>
        public string gzty { get; set; } = "N";
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
        public DateTime? shsj { get; set; }
        /// <summary>
        /// PCS人工岗位PCIP
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 看护岗位
        /// </summary>
        public string khgw { get; set; }
        /// <summary>
        /// 返修岗位号
        /// </summary>
        public string fxgwh { get; set; }
        /// <summary>
        /// 当前机型平台
        /// </summary>
        public string dqjx { get; set; }
    }
    public class base_gwzd_mapper : ClassMapper<base_gwzd>
    {
        public base_gwzd_mapper()
        {
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}
