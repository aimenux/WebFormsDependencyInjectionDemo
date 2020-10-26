using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppUsingWebObjectActivator.Startup))]
namespace WebAppUsingWebObjectActivator
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
