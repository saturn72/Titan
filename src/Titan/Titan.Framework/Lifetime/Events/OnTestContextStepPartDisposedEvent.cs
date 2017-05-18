using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepPartDisposedEvent : EventBase
    {
        public OnTestContextStepPartDisposedEvent(TestContextStepPart testContextStepPart)
        {
            TestContextStepPart = testContextStepPart;
        }

        public TestContextStepPart TestContextStepPart { get; }
    }
}