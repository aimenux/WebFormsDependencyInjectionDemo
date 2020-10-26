using System;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;

namespace WebAppUsingAutofac.Ioc
{
    public class AutofacPerRequestServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        public AutofacPerRequestServiceProvider(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            var lifetimeScope = CreateLifetimeScope() ?? _container;

            if (lifetimeScope.IsRegistered(serviceType))
            {
                return lifetimeScope.Resolve(serviceType);
            }

            return Activator.CreateInstance(serviceType,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null,
                null,
                null);
        }

        private ILifetimeScope CreateLifetimeScope()
        {
            var currentHttpContext = HttpContext.Current;
            if (currentHttpContext == null) return null;

            var lifetimeScope = (ILifetimeScope) currentHttpContext.Items[typeof(ILifetimeScope)];
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

            lifetimeScope = _container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            currentHttpContext.Items.Add(typeof(ILifetimeScope), lifetimeScope);
            currentHttpContext.ApplicationInstance.RequestCompleted += CleanScope;
            return lifetimeScope;
        }
    }
}