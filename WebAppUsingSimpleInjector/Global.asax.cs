using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using Domain;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;

namespace WebAppUsingSimpleInjector
{
    public class Global : HttpApplication
    {
        private static readonly Container Container = new Container();

        public static void InitializeHandler(IHttpHandler handler)
        {
            var handlerType = handler is Page
                ? handler.GetType().BaseType
                : handler.GetType();
            if (handlerType == null) return;
            
            var instance = Container.GetRegistration(handlerType, true);
            instance?.Registration.InitializeInstance(handler);
        }

        public void Application_Start(object sender, EventArgs e)
        {
            ConfigureIoc();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void ConfigureIoc()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            Container.Options.PropertySelectionBehavior = new ImportAttributePropertySelectionBehavior();
            Container.Register<IDummyService, DummyService>(Lifestyle.Singleton);
            RegisterWebPages(Container);
            //Container.Verify();
        }

        private static void RegisterWebPages(Container container)
        {
            var pageTypes =
                from assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                where !assembly.IsDynamic
                where !assembly.GlobalAssemblyCache
                from type in assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(Page))
                where !type.IsAbstract && !type.IsGenericType
                select type;

            const string justification = "ASP.NET creates and disposes page classes for us.";

            foreach (var type in pageTypes)
            {
                var reg = Lifestyle.Transient.CreateRegistration(type, container);
                reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, justification);
                container.AddRegistration(type, reg);
            }
        }

        private class ImportAttributePropertySelectionBehavior : IPropertySelectionBehavior
        {
            public bool SelectProperty(Type implementationType, PropertyInfo property) 
            {
                return typeof(Page).IsAssignableFrom(implementationType)
                       && property.GetCustomAttributes(typeof(ImportAttribute), true).Length > 0;
            }
        }
    }
}