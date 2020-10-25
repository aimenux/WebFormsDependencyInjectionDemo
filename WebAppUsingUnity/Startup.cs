using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppUsingUnity.Startup))]
namespace WebAppUsingUnity
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
