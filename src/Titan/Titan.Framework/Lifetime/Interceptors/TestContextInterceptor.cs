using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Castle.DynamicProxy;
using Saturn72.Extensions;
using Titan.Framework.Exceptions;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Interceptors
{
    public class TestContextInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Exception innerException = null;
            var parameters = InvocationUtil.ExtractMethodParameters(invocation);
            var tc = StartTestExecution(invocation.Method, invocation.Arguments);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                innerException = ex;
            }
            finally
            {
                if (invocation.Method.ReturnType != typeof(void) && invocation.ReturnValue.IsNull() || innerException.NotNull())
                {
                    var exMsg = "Fail to execution test method {0} with parameters {1}".AsFormat(invocation.Method.Name, parameters);
                    tc.Exception = innerException.IsNull()
                        ? new AutomationException(exMsg)
                        : new AutomationException(exMsg, innerException);
                }
                EndTestContextExecution(tc);
                //wait for async events to finish processing ==> 
                //TODO: more generic : wait to all threads to fold with time out
                Thread.Sleep(100);
                if (tc.Exception.NotNull())
                    throw tc.Exception;
            }
        }

        private static void EndTestContextExecution(TestContext testContext)
        {
            TestLifetimePublisher.EndTestContextExecution(testContext);
            TestLifetimePublisher.DisposeTestContext(testContext);
        }

        private static TestContext StartTestExecution(MethodInfo methodInfo, IEnumerable<object> parameters)
        {
            var testSuiteContext = TestSuiteContext.Instance;
            if (testSuiteContext.ExecutionStartedOnUtc == default(DateTime))
                TestLifetimePublisher.StartTestSuiteContextExecution();

            //TODO: get test tags (categories) using methodInfo
            var tc = TestLifetimePublisher.CreateTestContext(methodInfo, parameters, null, testSuiteContext);
            TestLifetimePublisher.StartTestContextExecution(tc);
            return tc;
        }
    }
}