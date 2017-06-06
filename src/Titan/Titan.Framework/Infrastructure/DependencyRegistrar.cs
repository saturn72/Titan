using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Events;
using Saturn72.Core.Services.Extensibility;
using Saturn72.Core.Services.Impl.Events;
using Saturn72.Core.Services.Impl.Extensibility;
using Saturn72.Core.Services.Impl.Logging;
using Saturn72.Extensions;
using Titan.Data.Repositories;
using Titan.Framework.Commanders;
using Titan.Framework.Exceptions;
using Titan.Framework.Lifetime.Interceptors;
using Titan.Framework.Runtime;
using Titan.Services.Data;
using Titan.Services.Monitor;

namespace Titan.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int RegistrationOrder => 100;

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                RegisterInterceptors(reg, typeFinder);
                RegisterServices(reg, typeFinder);
                RegisterRepositories(reg, typeFinder);
                reg.RegisterType<MemoryLogger, ILogger>(LifeCycle.PerDependency);
            };
        }

        private void RegisterRepositories(IIocRegistrator reg, ITypeFinder typeFinder)
        {
            reg.RegisterType<MonitorResultMemoryRepository, IMonitorResultRepository>(LifeCycle.PerDependency);
        }

        private void RegisterServices(IIocRegistrator reg, ITypeFinder typeFinder)
        {
            //Extensibility
            RegisterPubSubComponents(reg, typeFinder);
            reg.RegisterType<PluginService, IPluginService>(LifeCycle.PerDependency);
            reg.RegisterType<PluginManager, IPluginManager>(LifeCycle.PerDependency);

            //Monitoring
            reg.RegisterType<MonitorResultService, IMonitorResultService>(LifeCycle.PerDependency);
        }

        private void RegisterInterceptors(IIocRegistrator reg, ITypeFinder typeFinder)
        {
            reg.RegisterInstance(new TestContextInterceptor());
            FindTypesByMethodReturnValueAndRegisterInterceptor<IEnumerable<ExecutionResult>, TestContextInterceptor>(
                reg, typeFinder);

            reg.RegisterInstance(new TestContextStepInterceptor());
            FindTypesByMethodReturnValueAndRegisterInterceptor<ExecutionResult, TestContextStepInterceptor>(reg,
                typeFinder);

            RegisterCommanders(reg, typeFinder);
        }

        private void RegisterCommanders(IIocRegistrator reg, ITypeFinder typeFinder)
        {
            reg.RegisterInstance(new TestContextStepPartInterceptor());

            var commanderTypes = typeFinder.FindClassesOfType<ICommander>();
            if (!commanderTypes.Any())
                throw new AutomationException("Failed to find commander implementors. Please register commanders");

            foreach (var ct in commanderTypes)
            {
                var firstLevelCommanderInterfaces = GetFirstLevelInterfaces(ct)
                    .Where(t => typeof(ICommander).IsAssignableFrom(t));

                firstLevelCommanderInterfaces.ForEachItem(flci =>
                    reg.RegisterType(ct, flci, LifeCycle.PerDependency,
                        interceptorTypes: new[] {typeof(TestContextStepPartInterceptor)}));
            }
        }

        private static IEnumerable<Type> GetFirstLevelInterfaces(Type type)
        {
            var allInterfaces = type.GetInterfaces();
            return allInterfaces.Except
                (allInterfaces.SelectMany(t => t.GetInterfaces()));
        }

        private void FindTypesByMethodReturnValueAndRegisterInterceptor<TReturned, TInterceptor>(IIocRegistrator reg,
            ITypeFinder typeFinder)
            where TInterceptor : IInterceptor
        {
            var methodInfos = GetMethodInfos<TReturned>(typeFinder);
            if (!methodInfos.Any())
                throw new AutomationException(
                    "Failed to find test logic component. Please validate you have instance of an object with methods that retuns the type " +
                    typeof(TReturned));

            ValidateThatAllMethodsAreVirtualOrImplementInterface(methodInfos);
            var declarTypes = methodInfos.Select(bi => bi.DeclaringType).Distinct();
            var interceptorTypes = new[] {typeof(TInterceptor)};

            declarTypes.ForEachItem(dt =>
            {
                var firstLevelInterfaces = GetFirstLevelInterfaces(dt).ToArray();
                if (firstLevelInterfaces.Any())
                    foreach (var fli in firstLevelInterfaces)
                        reg.RegisterType(dt, fli, LifeCycle.PerDependency, null, interceptorTypes);
                else
                    reg.RegisterType(dt, LifeCycle.PerDependency, interceptorTypes);
            });
        }

        private static IEnumerable<MethodInfo> GetMethodInfos<TReturned>(ITypeFinder typeFinder)
        {
            var methodInfos = typeFinder.FindMethodsOfReturnType<TReturned>().ToList();
            //remove interception from get accessor od properties
            var propGetters = new List<MethodInfo>();
            foreach (var mi in methodInfos)
                if (mi.DeclaringType.GetProperties().Any(p => p.GetGetMethod() == mi))
                    propGetters.Add(mi);
            propGetters.ForEach(pg => methodInfos.Remove(pg));
            return methodInfos;
        }

        private void ValidateThatAllMethodsAreVirtualOrImplementInterface(IEnumerable<MethodInfo> methodInfos)
        {
            var sb = new StringBuilder();

            foreach (var mi in methodInfos)
            {
                if (mi.IsVirtual)
                    continue;

                var dt = mi.DeclaringType;
                var dtInterfaces = dt.GetInterfaces();
                if (dtInterfaces.Any(dti => dti.GetMethods().Any(im => IsSameMethodSignature(im, mi))))
                    continue;
                sb.AppendLine(mi.DeclaringType + "." + mi.Name);
            }

            if (sb.Length > 0)
                throw new AutomationException(
                    "Interceptor registration failure. Interception can only work on virtual or interface implementation.\nPlease refactor the following types: " +
                    sb);
        }

        private bool IsSameMethodSignature(MethodInfo left, MethodInfo right)
        {
            var sameSignatureFunc = new Func<ParameterInfo[], ParameterInfo[], bool>((l, r) =>
            {
                if (l.Length != r.Length)
                    return false;

                for (var i = 0; i < l.Length; i++)
                {
                    var leftParam = l[i];
                    var rightParam = r[i];
                    if (leftParam.ParameterType == rightParam.ParameterType &&
                        leftParam.Position == rightParam.Position)
                        continue;
                    return false;
                }
                return true;
            });

            return left.Name == right.Name && left.ReturnType == right.ReturnType
                   && sameSignatureFunc(left.GetParameters(), right.GetParameters());
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
                                  ((Type) criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                    return isMatch;
                }, subscriberType);

                registrator.RegisterType(consumer, services, LifeCycle.PerLifetime);
            }
        }
    }
}