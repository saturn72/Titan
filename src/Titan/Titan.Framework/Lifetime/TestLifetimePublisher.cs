
using System;
using System.Collections.Generic;
using System.Reflection;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Services.Events;
using Titan.Framework.Lifetime.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime
{
    public sealed class TestLifetimePublisher
    {
        private static readonly IEventPublisher EventPublisher = AppEngine.Current.Resolve<IEventPublisher>();

        #region testSuiteContext

        internal static void CreateTestSuiteContext()
        {
            TestSuiteContext.Create();
            EventPublisher.Publish(new OnTestSuiteCreatedEvent(TestSuiteContext.Instance));
        }

        internal static void BeforeTestSuiteContextExecution()
        {
            EventPublisher.Publish(new OnBeforeTestSuiteExecutionStartEvent(TestSuiteContext.Instance));
        }

        internal static void StartTestSuiteContextExecution()
        {
            TestSuiteContext.Instance.ExecutionStartedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestSuiteExecutionStartedEvent(TestSuiteContext.Instance));
        }

        internal static void EndTestSuiteContextExecution()
        {
            TestSuiteContext.Instance.ExecutionEndedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestSuiteExecutionEndEvent(TestSuiteContext.Instance));
        }

        internal static void DisposeTestSuiteContext()
        {
            TestSuiteContext.Instance.Terminate();
            EventPublisher.Publish(new OnTestSuiteDisposedEvent(TestSuiteContext.Instance));
        }

        #endregion

        #region TestContext

        internal static TestContext CreateTestContext(MethodInfo methodInfo, IEnumerable<object> parameters, string[] tags,
            TestSuiteContext testSuiteContext)
        {
            var tc = new TestContext(methodInfo, parameters, tags, testSuiteContext);
            EventPublisher.Publish(new OnTestContextCreatedEvent(tc));
            return tc;
        }

        internal static void BeforeStartTestContextExecution(TestContext testContext)
        {
            EventPublisher.Publish(new OnBeforeTestContextExecutionStartEvent(testContext));
        }

        internal static void StartTestContextExecution(TestContext testContext)
        {
            testContext.ExecutionStartedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextExecutionStartEvent(testContext));
        }

        public static void EndTestContextExecution(TestContext testContext)
        {
            testContext.ExecutionEndedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextExecutionEndEvent(testContext));
        }

        internal static void DisposeTestContext(TestContext testContext)
        {
            testContext.DisposedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextDisposedEvent(testContext));
        }

        #endregion

        #region TestContextStep

        internal static TestContextStep CreateTestContextStep(TestContext testContext, object[] parameters, MethodInfo methodInfo)
        {
            var tcs = new TestContextStep(testContext, methodInfo, parameters);
            testContext.TestContextSteps.Add(tcs);

            EventPublisher.Publish(new OnTestContextStepCreatedEvent(tcs));
            return tcs;
        }

        internal static void DisposeTestContextStep(TestContextStep testContextStep)
        {
            testContextStep.DisposedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepDisposedEvent(testContextStep));
        }

        internal static void BeforeStartTestContextStepExecution(TestContextStep testContextStep)
        {
            EventPublisher.Publish(new OnBeforeTestContextStepExecutionStartEvent(testContextStep));
        }

        internal static void StartTestContextStepExecution(TestContextStep testContextStep)
        {
            testContextStep.ExecutionStartedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepExecutionStartEvent(testContextStep));
        }

        internal static void EndTestContextStepExecution(TestContextStep testContextStep)
        {
            testContextStep.ExecutionEndedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepExecutionEndEvent(testContextStep));
        }

        #endregion

        #region TestContextStepPart

        internal static TestContextStepPart CreateTestContextStepPart(TestContextStep step, MethodInfo methodInfo,
            IEnumerable<object> parameters)
        {
            var tcsp = new TestContextStepPart(step,methodInfo, parameters);
            step.TestContextStepParts.Add(tcsp);

            EventPublisher.Publish(new OnTestContextStepPartCreatedEvent(tcsp));
            return tcsp;
        }

        internal static void DisposeTestContextStepPart(TestContextStepPart testContextStepPart)
        {
            testContextStepPart.DisposedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepPartDisposedEvent(testContextStepPart));
        }

        internal static void BeforeStartTestContextStepPartExecution(TestContextStepPart testContextStepPart)
        {
            EventPublisher.Publish(new OnBeforeTestContextStepPartExecutionStartEvent(testContextStepPart));
        }

        internal static void StartTestContextStepPartExecution(TestContextStepPart testContextStepPart)
        {
            testContextStepPart.ExecutionStartedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepPartExecutionStartEvent(testContextStepPart));
        }

        internal static void EndTestContextStepPartExecution(TestContextStepPart testContextStepPart)
        {
            testContextStepPart.ExecutionEndedOnUtc = DateTime.UtcNow;
            EventPublisher.Publish(new OnTestContextStepPartExecutionEndEvent(testContextStepPart));
        }

        #endregion
    }
}