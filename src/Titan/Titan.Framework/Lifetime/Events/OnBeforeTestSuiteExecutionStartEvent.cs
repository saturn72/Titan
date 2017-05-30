using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnBeforeTestSuiteExecutionStartEvent : EventBase
    {
        public OnBeforeTestSuiteExecutionStartEvent(TestSuiteContext testSuiteContext)
        {
            TestSuiteContext = testSuiteContext;
        }

        public TestSuiteContext TestSuiteContext { get; }
    }
}