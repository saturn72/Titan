using System;
using System.Linq;
using Castle.DynamicProxy;
using Saturn72.Extensions;
using Titan.Framework.Exceptions;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Interceptors
{
    public class TestStepPartInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var parameters = InvocationUtil.ExtractMethodParameters(invocation);

            var tcsp = StartTestContextStepPartExecution(invocation.Method.Name, parameters);
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

        private TestContextStepPart StartTestContextStepPartExecution(string name, string parameters)
        {
            var step = TestSuiteContext.Instance.TestContexts.Last().TestContextSteps.Last();

            var tcsp = TestLifetimeManager.CreateTestContextStepPart(name, step, parameters);
            TestLifetimeManager.StartTestContextStepPartExecution(tcsp);
            return tcsp;
        }

        private void EndTestContextStepPartExecution(TestContextStepPart stepPart)
        {
            TestLifetimeManager.EndTestContextStepPartExecution(stepPart);
            TestLifetimeManager.DisposeTestContextStepPart(stepPart);
        }
    }
}