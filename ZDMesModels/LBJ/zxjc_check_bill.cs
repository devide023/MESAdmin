using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_check_bill
    {
        public int id { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string bmmc { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime rq { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        public string bc { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string cpxh { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string cpmc { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string gxmc { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string khmc { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
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
        /// 件号
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// 夹具号
        /// </summary>
        public string jjh { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string xgr { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? xgsj { get; set; }
        /// <summary>
        /// 首末件标识
        /// </summary>
        public string smjbs { get; set; }
        public string xjr { get; set; }
        public DateTime? xjsj { get; set; }
        public string xjjg { get; set; }
        public string shjg { get; set; }
        /// <summary>
        /// 检测项明细
        /// </summary>
        public List<zxjc_check_bill_detail> BillDetails { get; set; }
    }

    public class zxjc_check_bill_mapper:ClassMapper<zxjc_check_bill>
    {
        public zxjc_check_bill_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.BillDetails).Ignore();
            AutoMap();
        }
    }
}
