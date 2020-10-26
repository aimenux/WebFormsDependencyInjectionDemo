using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppUsingServiceCollection.Ioc
{
    public class ServiceCollectionProvider : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceCollectionProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, serviceType);
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
    }
}