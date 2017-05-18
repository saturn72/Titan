using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepExecutionStartEvent : EventBase
    {
        public OnTestContextStepExecutionStartEvent(TestContextStep testContextStep)
        {
            TestContextStep = testContextStep;
        }

        public TestContextStep TestContextStep { get; }
    }
}