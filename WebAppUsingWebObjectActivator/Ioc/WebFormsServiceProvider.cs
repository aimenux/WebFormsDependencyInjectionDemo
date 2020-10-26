using System;
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
            return Container.Resolve(serviceType);
        }
    }
}