using System;
using System.Reflection;
using Domain;
using Unity;

namespace WebAppUsingWebObjectActivator.Ioc
{
    public class WebFormsServiceProvider : IServiceProvider
    {
        private static readonly UnityContainer Container = new UnityContainer();

        static WebFormsServiceProvider()
        {
            Container.RegisterSingleton<IDummyService, DummyService>();
        }

        public object GetService(Type serviceType)
        {
            if (Container.IsRegistered(serviceType))
            {
                return Container.Resolve(serviceType);
            }

            return Activator.CreateInstance(
                serviceType,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null, 
                null, 
                null);
        }
    }
}