using System;
using System.Linq;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Events;
using Saturn72.Core.Services.Extensibility;
using Saturn72.Core.Services.Impl.Events;
using Saturn72.Core.Services.Impl.Extensibility;
using Saturn72.Core.Services.Impl.Logging;

namespace Titan.Framework.SystemTests
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int RegistrationOrder => 100;

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                RegisterPubSubComponents(reg, typeFinder);
                reg.RegisterType<PluginService, IPluginService>(LifeCycle.PerDependency);
                reg.RegisterType<PluginManager, IPluginManager>(LifeCycle.PerDependency);
                reg.RegisterType<MemoryLogger, ILogger>(LifeCycle.PerDependency);
            };
        }

        private void RegisterPubSubComponents(IIocRegistrator reg, ITypeFinder typeFinder)
        {
            reg.RegisterType<EventPublisher, IEventPublisher>(LifeCycle.SingleInstance);
            reg.RegisterType<SubscriptionService, ISubscriptionService>(LifeCycle.SingleInstance);

            RegisterSubscribersByType(reg, typeFinder, typeof(IEventSubscriber<>));
            RegisterSubscribersByType(reg, typeFinder, typeof(IEventAsyncSubscriber<>));
        }

        private void RegisterSubscribersByType(IIocRegistrator registrator, ITypeFinder typeFinder,
            Type subscriberType)
        {
            var consumerTypes = typeFinder.FindClassesOfType(subscriberType).ToArray();
            foreach (var consumer in consumerTypes)
            {
                var services = consumer.FindInterfaces((type, criteria) =>
                {
                    var isMatch = type.IsGenericType &&
                                  ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                    return isMatch;
                }, subscriberType);

                registrator.RegisterType(consumer, services, LifeCycle.PerLifetime);
            }
        }
    }
}