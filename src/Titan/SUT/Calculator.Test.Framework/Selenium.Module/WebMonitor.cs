using Saturn72.Core.Services.Events;
using Titan.Framework.Lifetime.Events;

namespace Calculator.Test.Framework.Selenium.Module
{
    public class WebMonitor:IEventSubscriber<OnTestContextStepExecutionEndEvent>
    {
        public void HandleEvent(OnTestContextStepExecutionEndEvent eventMessage)
        {
            var options = eventMessage.TestContextStep.Parameters;
            throw new System.NotImplementedException();
        }
    }
}
