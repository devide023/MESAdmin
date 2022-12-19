using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class lbj_qms_4mbhd
    {
        /// <summary>
        ///ID
        ///</summary>
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        ///部门编码
        ///</summary>
        public string bmbm { get; set; }
        /// <summary>
        ///部门名称
        ///</summary>
        public string bmmc { get; set; }
        /// <summary>
        ///生产线
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///创建人
        ///</summary>
        public string cjr { get; set; }
        /// <summary>
        ///创建人
        ///</summary>
        public string cjrmc { get; set; }
        /// <summary>
        ///创建时间
        ///</summary>
        public DateTime cjsj { get; set; }
        /// <summary>
        ///机台
        ///</summary>
        public string jt { get; set; }
        /// <summary>
        ///产品型号
        ///</summary>
        public string cpxh { get; set; }
        /// <summary>
        ///产品名称
        ///</summary>
        public string cpmc { get; set; }
        /// <summary>
        ///变化部位
        ///</summary>
        public string bhbw { get; set; }
        /// <summary>
        ///故障现象
        ///</summary>
        public string gzxx { get; set; }
        /// <summary>
        ///发生断点编号
        ///</summary>
        public string fsddbh { get; set; }
        /// <summary>
        ///发现断点编号
        ///</summary>
        public string fxddbh { get; set; }
        /// <summary>
        ///追溯数量
        ///</summary>
        public string zssl { get; set; }
        /// <summary>
        ///已制品处理
        ///</summary>
        public string yzpcl { get; set; }
        /// <summary>
        ///人
        ///</summary>
        public string r { get; set; }
        /// <summary>
        ///机
        ///</summary>
        public string j { get; set; }
        /// <summary>
        ///料
        ///</summary>
        public string l { get; set; }
        /// <summary>
        ///发
        ///</summary>
        public string f { get; set; }
        /// <summary>
        ///环
        ///</summary>
        public string h { get; set; }
        /// <summary>
        ///测
        ///</summary>
        public string c { get; set; }
        /// <summary>
        ///调试方法
        ///</summary>
        public string tsff { get; set; }
        /// <summary>
        ///确认方法
        ///</summary>
        public string qrff { get; set; }
        /// <summary>
        ///刀具发放人员
        ///</summary>
        public string djffry { get; set; }
        /// <summary>
        ///刀具发放人员
        ///</summary>
        public string djffrymc { get; set; }
        /// <summary>
        ///程序调整人员
        ///</summary>
        public string cxtzry { get; set; }
        /// <summary>
        ///程序调整人员
        ///</summary>
        public string cxtzrymc { get; set; }
        /// <summary>
        ///维修设备人员
        ///</summary>
        public string wxsbry { get; set; }
        /// <summary>
        ///维修设备人员
        ///</summary>
        public string wxsbrymc { get; set; }
        /// <summary>
        ///其他
        ///</summary>
        public string qtbz { get; set; }
        /// <summary>
        ///改善断点
        ///</summary>
        public string czygsdd { get; set; }
        /// <summary>
        ///实测数据
        ///</summary>
        public string czyscsj { get; set; }
        /// <summary>
        ///判定结果
        ///</summary>
        public string czypdjg { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string czyczr { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string czyczrmc { get; set; }
        /// <summary>
        ///确认时间
        ///</summary>
        public DateTime czyczsj { get; set; }
        /// <summary>
        ///改善断点
        ///</summary>
        public string scbzgsdd { get; set; }
        /// <summary>
        ///实测数据
        ///</summary>
        public string scbzscsj { get; set; }
        /// <summary>
        ///判定结果
        ///</summary>
        public string scbzpdjg { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string scbzczr { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string scbzczrmc { get; set; }
        /// <summary>
        ///确认时间
        ///</summary>
        public DateTime scbzczsj { get; set; }
        /// <summary>
        ///改善断点
        ///</summary>
        public string xcxjgsdd { get; set; }
        /// <summary>
        ///实测数据
        ///</summary>
        public string xcxjscsj { get; set; }
        /// <summary>
        ///判定结果
        ///</summary>
        public string xcxjpdjg { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string xcxjczr { get; set; }
        /// <summary>
        ///确认人员
        ///</summary>
        public string xcxjczrmc { get; set; }
        /// <summary>
        ///确认时间
        ///</summary>
        public DateTime xcxjczsj { get; set; }
        /// <summary>
        ///任务状态
        ///</summary>
        public int rwzt { get; set; } = 0;
        /// <summary>
        ///操作业务
        ///</summary>
        public string deal { get; set; }
        /// <summary>
        ///审批意见
        ///</summary>
        public string freeuse1 { get; set; }
        /// <summary>
        ///
        ///</summary>
        public string freeuse2 { get; set; }
        /// <summary>
        ///
        ///</summary>
        public string freeuse3 { get; set; }
        /// <summary>
        ///刀具发放确认
        ///</summary>
        public string djffryqr { get; set; }
        /// <summary>
        ///程序调试确认
        ///</summary>
        public string cxtzryqr { get; set; }
        /// <summary>
        ///维修设备确认
        ///</summary>
        public string wxsbryqr { get; set; }
    }

    public class lbj_qms_4mbhd_mapper : AutoClassMapper<lbj_qms_4mbhd>
    {
        public lbj_qms_4mbhd_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}
