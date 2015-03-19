using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PStudio.WXPlatform.CMSWeb.Startup))]
namespace PStudio.WXPlatform.CMSWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
