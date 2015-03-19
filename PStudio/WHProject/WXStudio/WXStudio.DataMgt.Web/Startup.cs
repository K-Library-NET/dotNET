using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WXStudio.DataMgt.Web.Startup))]
namespace WXStudio.DataMgt.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            StartUpCodeFirstDbMigration();
        }

        private static void StartUpCodeFirstDbMigration()
        {
            LogHelper.Info("Start Code First Upgrade Database to latest......");
            try
            {
                System.Data.Entity.Database.SetInitializer(
                    new System.Data.Entity.MigrateDatabaseToLatestVersion<WXStudio.EFModel.Entities.WXPstudioDbContext,
                    WXStudio.EFModel.Entities.Migrations.Configuration>());
                //using (WXStudio.EFModel.Entities.WXPstudioDbContext context = new WXStudio.EFModel.Entities.WXPstudioDbContext())
                //{
                //    if (context.CoreConfigs.Count() > 0)
                //    {
                //        var temp = context.CoreConfigs.First();
                //    }
                //}
            }
            catch (System.Exception e)
            {
                LogHelper.Fatal("Code First代码更新数据库时发生致命错误！", e);
                return;
            }
            LogHelper.Info("Code First Upgrade Database OK!");
        }
    }
}
