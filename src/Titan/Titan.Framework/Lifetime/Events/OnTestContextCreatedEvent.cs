using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextCreatedEvent : EventBase
    {
        public OnTestContextCreatedEvent(TestContext testContext)
        {
            TestContext = testContext;
        }

        public TestContext TestContext { get; }
    }
}