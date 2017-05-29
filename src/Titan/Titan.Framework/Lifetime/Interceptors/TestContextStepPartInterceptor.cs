using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Saturn72.Extensions;
using Titan.Framework.Exceptions;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Interceptors
{
    public class TestContextStepPartInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var parameters = InvocationUtil.ExtractMethodParameters(invocation);

            var tcsp = StartTestContextStepPartExecution(invocation.Method, invocation.Arguments);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                tcsp.Exception = ex;
            }
            tcsp.Result = invocation.ReturnValue;

            EndTestContextStepPartExecution(tcsp);
            if (tcsp.Exception.NotNull()) //|| tcsp.Result.IsNull())
                HandleError(tcsp);
        }

        private void HandleError(TestContextStepPart stepPart)
        {
            var ex = new AutomationException(
                "failure while running {0} with parameters: [{1}]".AsFormat(stepPart.Name, stepPart.Parameters),
                stepPart.Exception);
            stepPart.Exception = ex;
            throw ex;
        }

        private static TestContextStepPart StartTestContextStepPartExecution(MethodInfo methodInfo, IEnumerable<object> parameters)
        {
            var step = TestSuiteContext.Instance.TestContexts.Last().TestContextSteps.Last();

            var tcsp = TestLifetimePublisher.CreateTestContextStepPart(step, methodInfo,parameters);
            TestLifetimePublisher.StartTestContextStepPartExecution(tcsp);
            return tcsp;
        }

        private void EndTestContextStepPartExecution(TestContextStepPart stepPart)
        {
            TestLifetimePublisher.EndTestContextStepPartExecution(stepPart);
            TestLifetimePublisher.DisposeTestContextStepPart(stepPart);
        }
    }
}