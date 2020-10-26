using System;
using System.Reflection;
using Autofac;

namespace WebAppUsingAutofac.Ioc
{
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        public AutofacServiceProvider(IContainer rootContainer)
        {
            _container = rootContainer;
        }

        public object GetService(Type serviceType)
        {
            if (_container.IsRegistered(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return Activator.CreateInstance(serviceType,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null,
                null,
                null);
        }
    }
}