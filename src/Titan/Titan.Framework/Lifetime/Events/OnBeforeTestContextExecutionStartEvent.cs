using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnBeforeTestContextExecutionStartEvent : EventBase
    {
        public OnBeforeTestContextExecutionStartEvent(TestContext testContext)
        {
            TestContext = testContext;
        }

        public TestContext TestContext { get; }
    }
}