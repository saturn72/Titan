using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnBeforeTestContextStepExecutionStartEvent : EventBase
    {
        public OnBeforeTestContextStepExecutionStartEvent(TestContextStep testContextStep)
        {
            TestContextStep = testContextStep;
        }

        public TestContextStep TestContextStep { get; }
    }
}