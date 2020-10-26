using System;
using System.Reflection;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppUsingServiceCollection.Ioc
{
    public class ServiceCollectionPerRequestProvider : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceCollectionPerRequestProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var lifetimeScope = CreateServiceScope();
                var serviceProvider = lifetimeScope?.ServiceProvider ?? _serviceProvider;
                return ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, serviceType);
            }
            catch (InvalidOperationException)
            {
                return Activator.CreateInstance(serviceType,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                    null,
                    null,
                    null);
            }
        }

        private IServiceScope CreateServiceScope()
        {
            var currentHttpContext = HttpContext.Current;
            if (currentHttpContext == null) return null;

            var lifetimeScope = (IServiceScope)currentHttpContext.Items[typeof(IServiceScope)];
            if (lifetimeScope != null)
            {
                return lifetimeScope;
            }

            void CleanScope(object sender, EventArgs args)
            {
                var innerLifetimeScope = lifetimeScope;
                if (!(sender is HttpApplication application)) return;
                application.RequestCompleted -= CleanScope;
                innerLifetimeScope.Dispose();
            }

            lifetimeScope = _serviceProvider.CreateScope();
            currentHttpContext.Items.Add(typeof(IServiceScope), lifetimeScope);
            currentHttpContext.ApplicationInstance.RequestCompleted += CleanScope;
            return lifetimeScope;
        }
    }
}