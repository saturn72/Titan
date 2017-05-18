using System;
using System.Collections.Generic;
using Titan.Framework.Runtime;

namespace Titan.Framework.Testing
{
    public sealed class TestContextStep : ITestableAudit
    {
        private readonly DateTime _createdOnUtc;
        private readonly string _executionId;
        private readonly string _name;
        private readonly string _parameters;
        private readonly TestContext _testContext;
        private ICollection<TestContextStepPart> _testContextStepParts;

        public TestContextStep(string name, TestContext testContext, string parameters)
        {
            _name = name;
            _testContext = testContext;
            _executionId = Guid.NewGuid().ToString();
            _createdOnUtc = DateTime.UtcNow;
            _parameters = parameters;
        }

        public string Name
        {
            get { return _name; }
        }

        public TestContext TestContext
        {
            get { return _testContext; }
        }

        public string ExecutionId
        {
            get { return _executionId; }
        }

        public ICollection<TestContextStepPart> TestContextStepParts
        {
            get { return _testContextStepParts ?? (_testContextStepParts = new List<TestContextStepPart>()); }
        }

        public ExecutionResult ExecutionResult { get; internal set; }

        public string Parameters
        {
            get { return _parameters; }
        }

        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public DateTime ExecutionEndedOnUtc { get; internal set; }

        public DateTime CreatedOnUtc
        {
            get { return _createdOnUtc; }
        }

        public DateTime DisposedOnUtc { get; internal set; }
        public Exception Exception { get; internal set; }
    }
}