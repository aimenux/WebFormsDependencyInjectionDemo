using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Domain;
using WebAppUsingAutofac.Ioc;

namespace WebAppUsingAutofac
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
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AutofacRegistrationSource());
            builder.RegisterType<DummyService>().As<IDummyService>().SingleInstance();
            var container = builder.Build();
            HttpRuntime.WebObjectActivator = new AutofacServiceProvider(container);
        }
    }
}