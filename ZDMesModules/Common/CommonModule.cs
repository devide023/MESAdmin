using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModules.Common
{
    public class CommonModule:Autofac.Module
    {
        public string Constr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ZDMesServices.Common.ColumnValReplaceService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.DbInfoService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.FtpConfigService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.MesMenuApiService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.MesMenuService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.MesRoleService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.MesSeqService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.MesUserService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.PageConfigService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterType<ZDMesServices.Common.UserUtilService>().WithParameter("constr", Constr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
        }
    }
}
