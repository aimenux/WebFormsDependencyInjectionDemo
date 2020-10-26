using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppUsingServiceCollection.Startup))]
namespace WebAppUsingServiceCollection
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
