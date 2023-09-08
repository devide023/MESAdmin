using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.ZSKJ;
using ZDMesServices.ZSKJ;
namespace ZDMesModules.LBJ
{
    public class ZSKJModule:Autofac.Module
    {
        public string ZSKJConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterType<ZSKJServices>().As<IZSKJ>().WithParameter("constr",ZSKJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
        }
    }
}
