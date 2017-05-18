using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepExecutionEndEvent : EventBase
    {
        public OnTestContextStepExecutionEndEvent(TestContextStep testContextStep)
        {
            TestContextStep = testContextStep;
        }

        public TestContextStep TestContextStep { get; }
    }
}