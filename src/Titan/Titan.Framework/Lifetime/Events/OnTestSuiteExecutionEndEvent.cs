using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestSuiteExecutionEndEvent : EventBase
    {
        public OnTestSuiteExecutionEndEvent(TestSuiteContext testSuiteContext)
        {
            TestSuiteContext = testSuiteContext;
        }

        public TestSuiteContext TestSuiteContext { get; }
    }
}