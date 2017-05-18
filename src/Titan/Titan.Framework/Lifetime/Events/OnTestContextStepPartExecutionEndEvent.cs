using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepPartExecutionEndEvent : EventBase
    {
        public OnTestContextStepPartExecutionEndEvent(TestContextStepPart testContextStepPart)
        {
            TestContextStepPart = testContextStepPart;
        }

        public TestContextStepPart TestContextStepPart { get; }
    }
}