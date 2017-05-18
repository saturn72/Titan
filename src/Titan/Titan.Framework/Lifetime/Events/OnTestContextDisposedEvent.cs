using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextDisposedEvent : EventBase
    {
        public OnTestContextDisposedEvent(TestContext testContext)
        {
            TestContext = testContext;
        }

        public TestContext TestContext { get; private set; }
    }
}