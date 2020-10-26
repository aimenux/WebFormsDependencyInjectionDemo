using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppUsingSimpleInjector.Startup))]
namespace WebAppUsingSimpleInjector
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
