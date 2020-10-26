using System;

namespace WebAppUsingServiceCollection.Ioc
{
    public static class IocFactory
    {
        public static IServiceProvider CreateServiceProvider(IocContainerType iocType, IServiceProvider serviceProvider)
        {
            switch (iocType)
            {
                case IocContainerType.ServiceCollection:
                    return new ServiceCollectionProvider(serviceProvider);
                case IocContainerType.ServiceCollectionPerRequest:
                    return new ServiceCollectionPerRequestProvider(serviceProvider);
                default:
                    throw new ArgumentOutOfRangeException(nameof(iocType));
            }
        }
    }

    public enum IocContainerType
    {
        ServiceCollection,
        ServiceCollectionPerRequest
    }
}