using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ZDMesInterfaces;
using ZDMesInterfaces.Common;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Autofac.Configuration;

namespace ZDMesModules.TJ
{
    public class BaseinfoModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {     // Add the configuration to the ConfigurationBuilder.
            var baseinfo_module_config = new ConfigurationBuilder();
            baseinfo_module_config.AddJsonFile("TJ_baseinfo_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(baseinfo_module_config.Build());
            builder.RegisterModule(module);
        }
    }
}
