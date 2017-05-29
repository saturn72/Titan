
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Titan.Framework.Testing
{
    public sealed class TestContext : ITestableAudit
    {
        #region ctor

        public TestContext(MethodInfo methodInfo, IEnumerable<object> parameters, IEnumerable<string> tags, TestSuiteContext testSuiteContext)
        {
            MethodInfo = methodInfo;
            Parameters = parameters;
            Name = methodInfo.Name;
            TestSuiteContext = testSuiteContext;
            ExecutionId = Guid.NewGuid().ToString();
            CreatedOnUtc = DateTime.UtcNow;
            Tags = tags;
            testSuiteContext.TestContexts.Add(this);
        }

        #endregion



        #region Properties

        public MethodInfo MethodInfo { get; }

        public IEnumerable<object> Parameters { get; }

        public IEnumerable<string> Tags { get; }

        public string Name { get; }

        public TestSuiteContext TestSuiteContext { get; }

        public string ExecutionId { get; }

        public ICollection<TestContextStep> TestContextSteps
        {
            get { return _testContextSteps ?? (_testContextSteps = new List<TestContextStep>()); }
        }

        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public DateTime ExecutionEndedOnUtc { get; internal set; }
        public DateTime CreatedOnUtc { get; }

        public DateTime DisposedOnUtc { get; internal set; }
        public Exception Exception { get; internal set; }

        #endregion

        #region Fields

        private ICollection<TestContextStep> _testContextSteps;

        #endregion
    }
}