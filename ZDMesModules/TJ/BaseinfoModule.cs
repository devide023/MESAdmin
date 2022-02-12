using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ZDMesInterfaces;
using ZDMesModels.TJ;
using ZDMesInterfaces.DB;
using ZDMesInterfaces.Common;
using ZDMesServices.Common;
using System.Reflection;

namespace ZDMesModules.TJ
{
    public class BaseinfoModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {         
            builder.RegisterType<DbOperateService>().As<IDbConn>().AsImplementedInterfaces();
            builder.RegisterType<DbOperateService>().As<IDbOperate>().AsImplementedInterfaces();
        }
    }
}
