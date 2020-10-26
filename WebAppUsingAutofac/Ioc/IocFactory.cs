using System;
using Autofac;

namespace WebAppUsingAutofac.Ioc
{
    public static class IocFactory
    {
        public static IServiceProvider CreateServiceProvider(IocContainerType iocType, IContainer container)
        {
            switch (iocType)
            {
                case IocContainerType.Autofac:
                    return new AutofacServiceProvider(container);
                case IocContainerType.AutofacPerRequest:
                    return new AutofacPerRequestServiceProvider(container);
                default:
                    throw new ArgumentOutOfRangeException(nameof(iocType));
            }
        }
    }

    public enum IocContainerType
    {
        Autofac,
        AutofacPerRequest
    }
}