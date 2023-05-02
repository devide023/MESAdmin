using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor;
using ZDMesInterceptor.LBJ;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesServices.Common;
using ZDMesServices.LBJ.ImportData;

namespace ZDMesModules.DUCAR
{
    public class DucarModule: Autofac.Module
    {
        public string DucarConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var AssServices = Assembly.Load("ZDMesServices");
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.Common")).WithParameter("constr", DucarConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.Ducar")).WithParameter("constr", DucarConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            var AssInterfaces = Assembly.Load("ZDMesInterfaces");
            builder.RegisterAssemblyTypes(AssInterfaces).Where(t => t.FullName.StartsWith("ZDMesInterfaces")).PreserveExistingDefaults();
            //builder.RegisterType<UserUtilService>().WithParameter("constr", TJConstr).SingleInstance();
            builder.RegisterGeneric(typeof(ImportDataService<>)).Named("ducarimp", typeof(IImportData<>)).WithParameter("constr", DucarConstr).PropertiesAutowired().WithProperty("Import_Config_File_Path", "~/Ducar_Import_Config.json").AsImplementedInterfaces().EnableInterfaceInterceptors();
            //builder.RegisterType<Form_Check_Service>().Named("cdgcformcheck",typeof(IFormCheck));
            builder.RegisterType<VerifyService>().As<ITemplateVerify>().As<IRequireVerify>()
                .PropertiesAutowired().WithProperty("VerifyConfigPath", "~/DucarVerfyConfig.json")
                .AsImplementedInterfaces().EnableInterfaceInterceptors();
            //注册拦截器
            builder.Register(c => new CUDLogger(DucarConstr));
            builder.Register(c => new ImportLog(DucarConstr));
            builder.Register(c => new UserLog(DucarConstr));
            var module_config = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(module_config.Build());
            builder.RegisterModule(module);
        }
    }
}
