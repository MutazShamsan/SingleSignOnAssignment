using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSO.Web.App.Startup))]
namespace SSO.Web.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
