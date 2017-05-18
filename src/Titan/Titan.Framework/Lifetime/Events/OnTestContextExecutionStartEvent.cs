using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextExecutionStartEvent : EventBase
    {
        public OnTestContextExecutionStartEvent(TestContext testContext)
        {
            TestContext = testContext;
        }

        public TestContext TestContext { get; }
    }
}