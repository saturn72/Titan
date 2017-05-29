using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Saturn72.Extensions;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Interceptors
{
    public class TestContextStepInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var testStepName = invocation.Method.Name;
            var tc = StartTestContextStepExecution(testStepName, invocation.Arguments, invocation.Method);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                tc.Exception = ex;
            }

            tc.ExecutionResult = invocation.ReturnValue as ExecutionResult;

            EndTestContextStepExecution(tc);

            if (tc.ExecutionResult.IsNull() || tc.Exception.NotNull())
                HandleErrors(tc);
        }

        private void HandleErrors(TestContextStep tc)
        {
            var tcException = tc.Exception;
            var methodName = tc.Name;
            var methodParameters = tc.Parameters;


            if (tcException.NotNull())
            {
                var innerExMessage = tcException.InnerException != null
                    ? "\nInnerException summary: " + tcException.AsString()
                    : "";

                var exMsg = "Exception was throws while executing method \'{0}\' with parameters [{1}]. {2}"
                    .AsFormat(methodName, methodParameters, innerExMessage);

                throw new AutomationException(exMsg, tcException);
            }
            if (tc.ExecutionResult.HasExecutionErrors)
            {
                var exeSummary = string.Join("\n\t", tc.ExecutionResult.ExecutionSummary());
                var exMsg = "FAILURE while executing method \'{0}\' with parameters [{1}]. \nEXECUTION SUMMERY:\n{2}"
                    .AsFormat(methodName, methodParameters, exeSummary);

                throw new AutomationException(exMsg);
            }
            if (tc.ExecutionResult.HasValidationErrors)
            {
                var validSummary = string.Join("\n\t", tc.ExecutionResult.ValidationSummary());
                var exMsg = "FAILURE while validating method \'{0}\' with parameters [{1}]. \nVALIDATION SUMMERY:\n{2}"
                    .AsFormat(methodName, methodParameters, validSummary);

                throw new AutomationException(exMsg);
            }
        }

        private TestContextStep StartTestContextStepExecution(string testStepName, object[] parameters, MethodInfo methodInfo)
        {
            var testContext = TestSuiteContext.Instance.TestContexts.Last();
            Guard.NotNull(testContext);

            var tc = TestLifetimePublisher.CreateTestContextStep(testContext, parameters, methodInfo);
            TestLifetimePublisher.StartTestContextStepExecution(tc);
            return tc;
        }

        private void EndTestContextStepExecution(TestContextStep step)
        {
            TestLifetimePublisher.EndTestContextStepExecution(step);
            TestLifetimePublisher.DisposeTestContextStep(step);
        }
    }
}