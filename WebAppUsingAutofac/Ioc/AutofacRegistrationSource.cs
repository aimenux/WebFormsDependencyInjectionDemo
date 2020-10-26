using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac.Builder;
using Autofac.Core;

namespace WebAppUsingAutofac.Ioc
{
    public class AutofacRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var serviceType = (service as IServiceWithType)?.ServiceType;
            if (serviceType?.Namespace != null && serviceType.Namespace.StartsWith("ASP", true, CultureInfo.InvariantCulture))
            {
                return new[]
                {
                    RegistrationBuilder.ForType(serviceType).CreateRegistration()
                };
            }

            return Enumerable.Empty<IComponentRegistration>();
        }

        public bool IsAdapterForIndividualComponents => true;
    }
}