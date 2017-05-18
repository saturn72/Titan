using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepPartCreatedEvent : EventBase
    {
        public OnTestContextStepPartCreatedEvent(TestContextStepPart testContextStepPart)
        {
            TestContextStepPart = testContextStepPart;
        }

        public TestContextStepPart TestContextStepPart { get; }
    }
}