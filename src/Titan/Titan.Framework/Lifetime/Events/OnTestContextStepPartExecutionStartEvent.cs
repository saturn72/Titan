using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepPartExecutionStartEvent : EventBase
    {
        public OnTestContextStepPartExecutionStartEvent(TestContextStepPart testContextStepPart)
        {
            TestContextStepPart = testContextStepPart;
        }

        public TestContextStepPart TestContextStepPart { get; }
    }
}