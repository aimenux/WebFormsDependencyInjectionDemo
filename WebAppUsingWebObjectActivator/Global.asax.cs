using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppUsingWebObjectActivator.Ioc;

namespace WebAppUsingWebObjectActivator
{
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            ConfigureIoc();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void ConfigureIoc()
        {
            HttpRuntime.WebObjectActivator = new WebFormsServiceProvider();
        }
    }
}