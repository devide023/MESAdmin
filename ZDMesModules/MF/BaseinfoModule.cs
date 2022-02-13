using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModules.MF
{
    public class BaseinfoModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseinfo_module_config = new ConfigurationBuilder();
            baseinfo_module_config.AddJsonFile("MF_baseinfo_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(baseinfo_module_config.Build());
            builder.RegisterModule(module);
        }
    }
}
