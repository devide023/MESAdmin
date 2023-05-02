using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class base_sbxx
    {
        public string sbbh { get; set; }
        public string sbmc { get; set; }
        public string gcdm { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        public string gwmc { get; set; }
        public string sblx { get; set; }
        public string txfs { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public string plcdbnumread { get; set; }
        public string plcdbnumwrite { get; set; }
        public string plcsbxh { get; set; }
        public string sfky { get; set; }
        public string sflj { get; set; }
        public string bz { get; set; }
        public string lrr { get; set; }
        public DateTime? lrsj { get; set; }
        public string com { get; set; }
        public string iscxh { get; set; }
        public string ishxsj { get; set; }
        public string ljlx { get; set; }
        public string sbxh { get; set; }
    }
    public class base_sbxx_mapper : ClassMapper<base_sbxx>
    {
        public base_sbxx_mapper()
        {
            Map(t => t.sbbh).Key(KeyType.Assigned);
            Map(t => t.gwmc).Ignore();
            AutoMap();
        }
    }
}
