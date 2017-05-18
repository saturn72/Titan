using System;

namespace Titan.Framework.Testing
{
    public sealed class TestContextStepPart : ITestableAudit
    {
        private readonly DateTime _createdOnUtc;
        private readonly string _executionId;
        private readonly string _name;
        private readonly string _parameters;
        private readonly TestContextStep _testContextStepStep;

        public TestContextStepPart(string name, TestContextStep testContextStepStep, string parameters)
        {
            _name = name;
            _testContextStepStep = testContextStepStep;
            _executionId = Guid.NewGuid().ToString();
            _createdOnUtc = DateTime.UtcNow;
            _parameters = parameters;
        }

        public string Name
        {
            get { return _name; }
        }

        public TestContextStep TestContextStep
        {
            get { return _testContextStepStep; }
        }

        public string ExecutionId
        {
            get { return _executionId; }
        }
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
        public object Result { get; internal set; }
    }
}