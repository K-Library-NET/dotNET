using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WXStudio.ConfigMgt.Web.Startup))]
namespace WXStudio.ConfigMgt.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
