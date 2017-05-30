using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnBeforeTestContextStepPartExecutionStartEvent : EventBase
    {
        public OnBeforeTestContextStepPartExecutionStartEvent(TestContextStepPart testContextStepPart)
        {
            TestContextStepPart = testContextStepPart;
        }

        public TestContextStepPart TestContextStepPart { get; }
    }
}