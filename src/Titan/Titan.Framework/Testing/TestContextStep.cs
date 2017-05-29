using System;
using System.Collections.Generic;
using System.Reflection;
using Titan.Framework.Runtime;

namespace Titan.Framework.Testing
{
    public sealed class TestContextStep : ITestableAudit
    {
        private ICollection<TestContextStepPart> _testContextStepParts;

        public TestContextStep(TestContext testContext, MethodInfo methodInfo, object[] parameters)
        {
            Name = methodInfo.Name;
            TestContext = testContext;
            ExecutionId = Guid.NewGuid().ToString();
            CreatedOnUtc = DateTime.UtcNow;
            Parameters = parameters;
            MethodInfo = methodInfo;
        }

        public string Name { get; }

        public TestContext TestContext { get; }

        public string ExecutionId { get; }

        public ICollection<TestContextStepPart> TestContextStepParts
        {
            get { return _testContextStepParts ?? (_testContextStepParts = new List<TestContextStepPart>()); }
        }

        public ExecutionResult ExecutionResult { get; internal set; }

        public object[] Parameters { get; }

        public MethodInfo MethodInfo { get; }

        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public DateTime ExecutionEndedOnUtc { get; internal set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime DisposedOnUtc { get; internal set; }
        public Exception Exception { get; internal set; }
    }
}