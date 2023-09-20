using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels.LBJ;
using ZDMesServices;
using ZDMesServices.LBJ.App;
using ZDMesServices.LBJ.ImportData;

namespace ZDMesModules.LBJ
{
    public class LbjServicesModule : Autofac.Module
    {
        public string LBJConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(BaseDao<>)).Named("lbjdboperate", typeof(ZDMesInterfaces.Common.IDbOperate<>)).WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterGeneric(typeof(ImportDataService<>)).Named("lbjimport", typeof(ZDMesInterfaces.LBJ.ImportData.IImportData<>)).WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            //
            builder.RegisterType<ZDMesServices.LBJ.App.AppMenuService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.App.AppRoleService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.BaseInfo.BaseInfoService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.BatGL.LbjBatVinService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.BHDGL.BHDJLService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.BHDGL.BHDXXService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.BHDGL.Lbj4MBhdDealService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.CheckData.CheckDataService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.CheckData.Form_Check_Service>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.CPGL.LBJGdxxService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.DaoBinService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.DbRjGxService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.DbRjLyService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.DjReportService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.LBJDbrjLyNewService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.LbjRmLsService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.RenJuService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DAOJU.RjSmXhService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DZWD.AuditService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DZWD.JTFPService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DZWD.JTGLService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.DZWD.TSTCService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.GWMgr.GwZdService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.GYGL.DZGYService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.GYGL.DZGYSPService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.GYGL.LBJGYLXService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.JHGL.ZpJhService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.OEE.LbjOeeDataService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.OEE.LBJOEEService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.OEE.LbjScxOeeService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.RyMgr.RYJCService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.RyMgr.RYJNService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.RyMgr.RYJXService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.RyMgr.RyxxService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SBMgr.SbxxService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SBWB.GWSBService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SBWB.WbXxService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SBWB.WbZqService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.Setting.LbjSettingService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.CNC_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.GJ_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.HG_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.QX_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.SPC_HJ_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.SPC_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SJCJ.YLJ_SJCJService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.SMJJK.SmjjkService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.YCGL.TcglService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.ZLGL.LbjBhgService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.ZLGL.LBJ_BaseCheckService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.ZLGL.LBJ_CheckBillService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.ZLGL.LBJ_CheckImgService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
            builder.RegisterType<ZDMesServices.LBJ.ZLGL.LbjCheckService>().WithParameter("constr", LBJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors().PreserveExistingDefaults();
        }
    }
}
