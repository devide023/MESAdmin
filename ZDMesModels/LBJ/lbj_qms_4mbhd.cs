using DapperExtensions.Mapper;
using System;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 变化点记录表
    ///</summary>
    public class lbj_qms_4mbhd
    {
        /// <summary>
        ///  ID
        ///</summary>
        public string id { get; set; } = string.Empty;
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; } = string.Empty;
        /// <summary>
        /// 生产线 
        ///</summary>
        public string scx { get; set; } = string.Empty;
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; } = string.Empty;
        /// <summary>
        /// 创建人 
        ///</summary>
        public string cjr { get; set; } = string.Empty;
        /// <summary>
        /// 创建人 
        ///</summary>
        public string cjrmc { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间 
        ///</summary>
        public DateTime? cjsj { get; set; }
        /// <summary>
        /// 机台 
        ///</summary>
        public string jt { get; set; } = string.Empty;
        /// <summary>
        /// 产品型号 
        ///</summary>
        public string cpxh { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称 
        ///</summary>
        public string cpmc { get; set; } = string.Empty;
        /// <summary>
        /// 变化部位 
        ///</summary>
        public string bhbw { get; set; } = string.Empty;
        /// <summary>
        /// 故障现象 
        ///</summary>
        public string gzxx { get; set; } = string.Empty;
        /// <summary>
        /// 发生断点编号 
        ///</summary>
        public string fsddbh { get; set; } = string.Empty;
        /// <summary>
        /// 发现断点编号 
        ///</summary>
        public string fxddbh { get; set; } = string.Empty;
        /// <summary>
        /// 追溯数量 
        ///</summary>
        public decimal? zssl { get; set; } = 0;
        /// <summary>
        /// 已制品处理 
        ///</summary>
        public string yzpcl { get; set; } = string.Empty;
        /// <summary>
        /// 调试方法 
        ///</summary>
        public string tsff { get; set; } = string.Empty;
        /// <summary>
        /// 确认方法 
        ///</summary>
        public string qrff { get; set; } = string.Empty;
        /// <summary>
        /// 刀具发放人员 
        ///</summary>
        public string djffry { get; set; } = string.Empty;
        /// <summary>
        /// 刀具发放人员 
        ///</summary>
        public string djffrymc { get; set; } = string.Empty;
        /// <summary>
        /// 程序调整人员 
        ///</summary>
        public string cxtzry { get; set; } = string.Empty;
        /// <summary>
        /// 程序调整人员 
        ///</summary>
        public string cxtzrymc { get; set; } = string.Empty;
        /// <summary>
        /// 维修设备人员 
        ///</summary>
        public string wxsbry { get; set; } = string.Empty;
        /// <summary>
        /// 维修设备人员 
        ///</summary>
        public string wxsbrymc { get; set; } = string.Empty;
        /// <summary>
        /// 其他 
        ///</summary>
        public string qtbz { get; set; } = string.Empty;
        /// <summary>
        /// 改善断点 
        ///</summary>
        public string czygsdd { get; set; } = string.Empty;
        /// <summary>
        /// 实测数据 
        ///</summary>
        public string czyscsj { get; set; } = string.Empty;
        /// <summary>
        /// 判定结果 
        ///</summary>
        public string czypdjg { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string czyczr { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string czyczrmc { get; set; } = string.Empty;
        /// <summary>
        /// 确认时间 
        ///</summary>
        public DateTime? czyczsj { get; set; }
        /// <summary>
        /// 改善断点 
        ///</summary>
        public string scbzgsdd { get; set; } = string.Empty;
        /// <summary>
        /// 实测数据 
        ///</summary>
        public string scbzscsj { get; set; } = string.Empty;
        /// <summary>
        /// 判定结果 
        ///</summary>
        public string scbzpdjg { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string scbzczr { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string scbzczrmc { get; set; } = string.Empty;
        /// <summary>
        /// 确认时间 
        ///</summary>
        public DateTime? scbzczsj { get; set; }
        /// <summary>
        /// 改善断点 
        ///</summary>
        public string xcxjgsdd { get; set; } = string.Empty;
        /// <summary>
        /// 实测数据 
        ///</summary>
        public string xcxjscsj { get; set; } = string.Empty;
        /// <summary>
        /// 判定结果 
        ///</summary>
        public string xcxjpdjg { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string xcxjczr { get; set; } = string.Empty;
        /// <summary>
        /// 确认人员 
        ///</summary>
        public string xcxjczrmc { get; set; } = string.Empty;
        /// <summary>
        /// 确认时间 
        ///</summary>
        public DateTime? xcxjczsj { get; set; }
        /// <summary>
        /// 任务状态 
        ///</summary>
        public string rwzt { get; set; } = string.Empty;
        /// <summary>
        /// 操作业务 
        ///</summary>
        public string deal { get; set; } = string.Empty;
        /// <summary>
        /// 刀具发放确认 
        ///</summary>
        public string djffryqr { get; set; } = string.Empty;
        /// <summary>
        /// 程序调试确认 
        ///</summary>
        public string cxtzryqr { get; set; } = string.Empty;
        /// <summary>
        /// 维修设备确认 
        ///</summary>
        public string wxsbryqr { get; set; } = string.Empty;
        /// <summary>
        /// 触发类型
        /// </summary>
        public string trigtype { get; set; } = string.Empty;
        /// <summary>
        /// 变化类型
        /// </summary>
        public string changetype { get; set; } = string.Empty;
        /// <summary>
        /// 生产线子线
        /// </summary>
        public string scxzx { get; set; } = string.Empty;
        /// <summary>
        /// 子线名称
        /// </summary>
        public string scxzxmc { get; set; } = string.Empty;

        public string r { get; set; } = string.Empty;
        public string j { get; set; } = string.Empty;
        public string l { get; set; } = string.Empty;
        public string f { get; set; } = string.Empty;
        public string h { get; set; } = string.Empty;
        public string c { get; set; } = string.Empty;

    }
    public class lbj_qms_4mbhd_mapper : ClassMapper<lbj_qms_4mbhd>
    {
        public lbj_qms_4mbhd_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.trigtype).Column("trig_type");
            Map(t => t.changetype).Column("change_type");
            Map(t => t.scxzxmc).Ignore();
            AutoMap();
        }
    }
}
