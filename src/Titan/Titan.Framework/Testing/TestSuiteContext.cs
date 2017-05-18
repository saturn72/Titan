using System;
using System.Collections.Generic;

namespace Titan.Framework.Testing
{
    public class TestSuiteContext : ITestableAudit
    {
        #region Fields

        private readonly DateTime _createdOnUtc;
        private readonly string _testSuiteId;
        private ICollection<TestContext> _testContexts;

        #endregion

        #region ctor

        private TestSuiteContext(string testSuiteId)
        {
            _createdOnUtc = DateTime.UtcNow;
            _testSuiteId = testSuiteId;
        }

        #endregion

        public static TestSuiteContext Instance { get; private set; }

        public string TestSuiteId
        {
            get { return _testSuiteId; }
        }

        public virtual ICollection<TestContext> TestContexts
        {
            get { return _testContexts ?? (_testContexts = new List<TestContext>()); }
        }

        public DateTime ExecutionEndedOnUtc { get; internal set; }

        public DateTime CreatedOnUtc
        {
            get { return _createdOnUtc; }
        }

        public DateTime DisposedOnUtc { get; private set; }
        public DateTime ExecutionStartedOnUtc { get; internal set; }

        public static void Create()
        {
            if (Instance != null)
                throw new InvalidOperationException("TestSuiteContext can be created only once.");
            Instance = new TestSuiteContext(Guid.NewGuid().ToString());
        }

        public void Terminate()
        {
            DisposedOnUtc = DateTime.UtcNow;
        }
    }
}