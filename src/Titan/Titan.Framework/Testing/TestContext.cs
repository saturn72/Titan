
using System;
using System.Collections.Generic;

namespace Titan.Framework.Testing
{
    public sealed class TestContext : ITestableAudit
    {
        #region ctor

        public TestContext(string name, string parameters, IEnumerable<string> tags, TestSuiteContext testSuiteContext)
        {
            _parameters = parameters;
            _name = name;
            _testSuiteContext = testSuiteContext;
            _executionId = Guid.NewGuid().ToString();
            _createdOnUtc = DateTime.UtcNow;
            _tags = tags;
            testSuiteContext.TestContexts.Add(this);
        }

        #endregion

        public string Parameters
        {
            get { return _parameters; }
        }

        #region Properties

        public IEnumerable<string> Tags
        {
            get { return _tags; }
        }

        public string Name
        {
            get { return _name; }
        }

        public TestSuiteContext TestSuiteContext
        {
            get { return _testSuiteContext; }
        }

        public string ExecutionId
        {
            get { return _executionId; }
        }

        public ICollection<TestContextStep> TestContextSteps
        {
            get { return _testContextSteps ?? (_testContextSteps = new List<TestContextStep>()); }
        }

        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public DateTime ExecutionEndedOnUtc { get; internal set; }

        public DateTime CreatedOnUtc
        {
            get { return _createdOnUtc; }
        }

        public DateTime DisposedOnUtc { get; internal set; }
        public Exception Exception { get; internal set; }

        #endregion

        #region Fields

        private readonly DateTime _createdOnUtc;
        private readonly string _executionId;
        private readonly string _name;
        private readonly TestSuiteContext _testSuiteContext;
        private ICollection<TestContextStep> _testContextSteps;
        private readonly IEnumerable<string> _tags;
        private readonly string _parameters;

        #endregion
    }
}