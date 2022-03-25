using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModules.LBJ
{
    public class BaseinfoModule: Autofac.Module
    {
        public string Constr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var AssServices = Assembly.Load("ZDMesServices");
            builder.RegisterAssemblyTypes(AssServices).Where(t=>t.FullName.StartsWith("ZDMesServices.Common")).WithParameter("constr", Constr).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(AssServices).Where(t=>t.FullName.StartsWith("ZDMesServices.LBJ")).WithParameter("constr", Constr).AsImplementedInterfaces();
            var baseinfo_module_config = new ConfigurationBuilder();
            baseinfo_module_config.AddJsonFile("LBJ_baseinfo_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(baseinfo_module_config.Build());
            builder.RegisterModule(module);
        }
    }
}
