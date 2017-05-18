using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepCreatedEvent : EventBase
    {
        public OnTestContextStepCreatedEvent(TestContextStep testContextStep)
        {
            TestContextStep = testContextStep;
        }

        public TestContextStep TestContextStep { get; }
    }
}