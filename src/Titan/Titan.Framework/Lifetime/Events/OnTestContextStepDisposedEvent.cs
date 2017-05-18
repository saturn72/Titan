using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestContextStepDisposedEvent : EventBase
    {
        public OnTestContextStepDisposedEvent(TestContextStep testContextStep)
        {
            TestContextStep = testContextStep;
        }

        public TestContextStep TestContextStep { get; }
    }
}