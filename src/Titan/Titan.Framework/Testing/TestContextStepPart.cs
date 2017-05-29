using System;
using System.Collections.Generic;
using System.Reflection;

namespace Titan.Framework.Testing
{
    public sealed class TestContextStepPart : ITestableAudit
    {
        public TestContextStepPart(TestContextStep testContextStepStep, MethodInfo methodInfo, IEnumerable<object> parameters)
        {
            Name = methodInfo.Name;
            TestContextStep = testContextStepStep;
            ExecutionId = Guid.NewGuid().ToString();
            CreatedOnUtc = DateTime.UtcNow;
            Parameters = parameters;
        }

        public string Name { get; }

        public TestContextStep TestContextStep { get; }

        public string ExecutionId { get; }

        public IEnumerable<object> Parameters { get; }

        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public DateTime ExecutionEndedOnUtc { get; internal set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime DisposedOnUtc { get; internal set; }
        public Exception Exception { get; internal set; }
        public object Result { get; internal set; }
    }
}