using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Autofac.Configuration;
using ZDMesInterfaces;
using ZDMesInterfaces.Common;
using Autofac.Extras.DynamicProxy;

namespace ZDMesModules.TJ
{
    public class BaseinfoModule:Autofac.Module
    {
        public string TJConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var AssServices = Assembly.Load("ZDMesServices");
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.TJ")).WithParameter("constr", TJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();

            var AssInterfaces = Assembly.Load("ZDMesInterfaces");
            builder.RegisterAssemblyTypes(AssInterfaces).Where(t=>t.FullName.StartsWith("ZDMesInterfaces.TJ")).PreserveExistingDefaults();

            var baseinfo_module_config = new ConfigurationBuilder();
            baseinfo_module_config.AddJsonFile("TJ_baseinfo_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(baseinfo_module_config.Build());
            builder.RegisterModule(module);
        }
    }
}
