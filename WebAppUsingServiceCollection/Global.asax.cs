using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using WebAppUsingServiceCollection.Ioc;

namespace WebAppUsingServiceCollection
{
    public class Global : HttpApplication
    {
        private const IocContainerType ContainerType = IocContainerType.ServiceCollection;

        public void Application_Start(object sender, EventArgs e)
        {
            ConfigureIoc();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void ConfigureIoc()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IDummyService, DummyService>();
            var serviceProvider = collection.BuildServiceProvider();
            HttpRuntime.WebObjectActivator = IocFactory.CreateServiceProvider(ContainerType, serviceProvider);
        }
    }
}