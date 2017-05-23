﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Extensions;
using Titan.Framework.Exceptions;
using Titan.Framework.Lifetime.Interceptors;
using Titan.Framework.Runtime;

namespace Titan.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int RegistrationOrder => 100;

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                RegisterInterceptors(reg, typeFinder, config);

                /*
                 * 
            reg.RegisterInstance(new TestContextStepInterceptor());
            reg.RegisterType<NgPump, IPumpCommander>(LifeCycle.PerDependency,
                interceptorTypes: new[] {typeof(TestContextStepInterceptor)});


            reg.RegisterInstance(new TestStepPartInterceptor());
            reg.RegisterDelegate(resolver =>
                {
                    var tc = TestSuiteContext.Instance.CurrentTestContext();
                    var labManager = resolver.Resolve<ILabManager>();
                    return labManager.GetPumpCommander(tc.ExecutionId, tc.Tags).UiCommander;
                }, LifeCycle.PerLifetime,
                new[] {typeof(TestStepPartInterceptor)});*/
            };
        }

        private void RegisterInterceptors(IIocRegistrator reg, ITypeFinder typeFinder, Saturn72Config config)
        {
            reg.RegisterInstance(new TestContextInterceptor());
            FindTypesByMethodReturnValueAndRegisterInterceptor<IEnumerable<ExecutionResult>, TestContextInterceptor>(
                reg, typeFinder);

            reg.RegisterInstance(new TestContextStepInterceptor());
            FindTypesByMethodReturnValueAndRegisterInterceptor<ExecutionResult, TestContextStepInterceptor>(reg,
                typeFinder);
        }

        private void FindTypesByMethodReturnValueAndRegisterInterceptor<TReturned, TInterceptor>(IIocRegistrator reg,
            ITypeFinder typeFinder)
            where TInterceptor : IInterceptor
        {
            var testLogicMethodInfos = typeFinder.FindMethodsOfReturnType<TReturned>();

            ValidateThatAllMethodsAreVirtualOrImplementInterface(testLogicMethodInfos);
            var declarTypes = testLogicMethodInfos.Select(bi => bi.DeclaringType).Distinct();
            declarTypes.ForEachItem(dt => reg.RegisterType(dt, LifeCycle.PerDependency, new[] {typeof(TInterceptor)}));
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
    }
}