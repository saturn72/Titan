using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestSuiteCreatedEvent : EventBase
    {
        public OnTestSuiteCreatedEvent(TestSuiteContext testSuiteContext)
        {
            TestSuiteContext = testSuiteContext;
        }

        public TestSuiteContext TestSuiteContext { get; }
    }
}