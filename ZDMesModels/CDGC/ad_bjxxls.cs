using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class ad_bjxxls
    {
        /// <summary>
        ///ID 主键
        ///</summary>
        public string guid { get; set; }
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///生产线
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///报警主体
        ///</summary>
        public string bjzt { get; set; }
        public int bjlx { get; set; }
        public string bjdm { get; set; }
        public string bjxx { get; set; }
        public DateTime? kssj { get; set; }
        public DateTime? jssj { get; set; }
        public int bjsj { get; set; }
        public DateTime? lrsj { get; set; }
        public string lrr { get; set; }
        public string bhlrr { get; set; }
        public string bjlxdesc { get; set; }
        public string bjdj { get; set; }
    }

    public class ad_bjxxls_mapper : ClassMapper<ad_bjxxls>
    {
        public ad_bjxxls_mapper()
        {
            AutoMap();
        }
    }
}
