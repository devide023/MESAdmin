using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_scx_oee
    {
        public string rid { get; set; }
        public string gcdm { get; set; } = "9902";
        public string scx { get; set; }
        public int year
        {
            get
            {
                return rq.Year;
            }
        }
        public int month
        {
            get
            {
                return rq.Month;
            }
        }
        public int day
        {
            get
            {
                return rq.Day;
            }
        }
        /// <summary>
        /// 计划作息时间(小时)
        /// </summary>
        public decimal jhzxsj { get; set; }
        /// <summary>
        /// 早中晚 班前会(小时)
        /// </summary>
        public decimal zzwbqh { get; set; }
        /// <summary>
        /// 早中晚 吃饭(小时)
        /// </summary>
        public decimal zzwcf { get; set; }
        /// <summary>
        /// 早中晚 班中休息(小时)
        /// </summary>
        public decimal zzwbzxx { get; set; }
        /// <summary>
        /// 早中晚 班后5S整理或者设备保养(小时)
        /// </summary>
        public decimal zzwsbby { get; set; }
        /// <summary>
        /// 培训（小时）
        /// </summary>
        public decimal px { get; set; }
        /// <summary>
        /// 休息（小时）
        /// </summary>
        public decimal xx { get; set; }
        /// <summary>
        /// 堵料时间(小时)
        /// </summary>
        public decimal dlsjjam { get; set; }
        /// <summary>
        /// 待料时间(小时)
        /// </summary>
        public decimal dlsjwait { get; set; }
        /// <summary>
        /// 换刀时间(小时)
        /// </summary>
        public decimal hdsj { get; set; }
        /// <summary>
        /// 换型时间(小时)
        /// </summary>
        public decimal hxsj { get; set; }
        /// <summary>
        /// 故障时间(小时)
        /// </summary>
        public decimal gzsj { get; set; }
        /// <summary>
        /// 其他停机时间(小时)
        /// </summary>
        public decimal qttjsj { get; set; }
        /// <summary>
        /// 理论节拍（秒）
        /// </summary>
        public decimal lljp { get; set; }
        /// <summary>
        /// 合格品数量
        /// </summary>
        public decimal hgpsl { get; set; }
        /// <summary>
        /// 不合格品数量
        /// </summary>
        public decimal bhgpsl { get; set; }
        /// <summary>
        /// 目标OEE(%)
        /// </summary>
        public decimal oeetarget { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal sfjs { get; set; }
        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal jhyxsj
        {
            get
            {
                return jhzxsj - jhtjsj;
            }
        }
        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal sjyxsj
        {
            get
            {
                return jhzxsj - tjsj;
            }
        }
        /// <summary>
        /// 计划停机时间
        /// </summary>
        public decimal jhtjsj
        {
            get
            {
                return zzwbqh + zzwcf + zzwbzxx + zzwsbby + px + xx;
            }
        }
        /// <summary>
        /// 非计划停机时间
        /// </summary>
        public decimal fjhtjsj
        {
            get
            {
                return dlsjjam + dlsjwait + hdsj + hxsj + gzsj + qttjsj;
            }
        }
        /// <summary>
        /// 停机时间
        /// </summary>
        public decimal tjsj
        {
            get
            {
                return fjhtjsj + jhtjsj;
            }
        }
        /// <summary>
        /// 加工理论时间（小时）
        /// </summary>
        public decimal jgllsj { get {
                return sjcl * lljp / 60;
            } }
        /// <summary>
        /// 实际产量（总）
        /// </summary>
        public decimal sjcl
        {
            get
            {
                return hgpsl + bhgpsl;
            }
        }
        /// <summary>
        /// JPH（每小时产量）
        /// </summary>
        public decimal jph { get {
                if (sjyxsj > 0)
                {
                    return Math.Round(sjcl / (sjyxsj / 60), 2);
                }
                else {
                    return 0m;
                }
            } }
        /// <summary>
        /// 时间开动率(%)
        /// </summary>
        public decimal sjkdl
        {
            get
            {
                if (jhyxsj > 0)
                {
                    return Math.Round(sjyxsj / jhyxsj, 4) * 100;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 性能开动率(%)
        /// </summary>
        public decimal xnkdl { get {
                if (sjyxsj > 0)
                {
                    return Math.Round(jgllsj / sjyxsj, 4) * 100;
                }
                else
                {
                    return 0;
                }
            } }
        /// <summary>
        /// 合格品率(%)
        /// </summary>
        public decimal hgpl
        {
            get
            {
                if (sjcl != 0)
                {
                    return Math.Round(hgpsl / sjcl, 4) * 100;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 实际OEE(%)
        /// </summary>
        public decimal oeereal
        {
            get
            {
                return Math.Round((hgpl / 100) * (xnkdl / 100) * (sjkdl / 100), 4) * 100;
            }
        }
        /// <summary>
        /// TEEP（设备总生产率）
        /// </summary>
        public decimal teep
        {
            get
            {
                if (jhzxsj > 0)
                {
                    return Math.Round(oeereal * sjyxsj / jhzxsj, 2) ;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime rq { get; set; }
    }

    public class zxjc_scx_oee_mapper : ClassMapper<zxjc_scx_oee>
    {
        public zxjc_scx_oee_mapper()
        {
            Map(t => t.rq).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.dlsjjam).Column("dlsj_jam");
            Map(t => t.dlsjwait).Column("dlsj_wait");
            Map(t => t.oeetarget).Column("oee_target");
            Map(t => t.oeereal).Column("oee_real");
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}
