using System;
using System.Collections.Generic;
using Calculator.Test.Framework.Commanders;
using Calculator.Test.Framework.SystemBlockOptions;
using Saturn72.Core.Services.Events;
using Titan.Common.Domain.Monitor;
using Titan.Framework.Lifetime.Events;
using Titan.Framework.Monitors;
using Titan.Services.Monitor;

namespace Calculator.Test.Framework.Monitors
{
    public class WebMonitor : IMonitor,
        IEventSubscriber<OnTestContextStepExecutionEndEvent>
    {
        private const string AddExpectedResultFormat = "{0} + {1} = {2}";
        private static IDictionary<string, Action<OnTestContextStepExecutionEndEvent>> _handlers;
        private readonly ISeleniumCommander _commander;
        private readonly IMonitorService _monitorService;

        public WebMonitor(ISeleniumCommander commander, IMonitorService monitorService)
        {
            _commander = commander;
            _monitorService = monitorService;
        }

        public void HandleEvent(OnTestContextStepExecutionEndEvent eventMessage)
        {
            Action<OnTestContextStepExecutionEndEvent> handler;
            if (GetHandlers().TryGetValue(eventMessage.TestContextStep.Name, out handler))
                handler(eventMessage);
        }

        private IDictionary<string, Action<OnTestContextStepExecutionEndEvent>> GetHandlers()
        {
            return _handlers ?? (_handlers =
                       new Dictionary<string, Action<OnTestContextStepExecutionEndEvent>>(StringComparer
                           .OrdinalIgnoreCase)
                       {
                           {"add", AddHandler}
                       });
        }

        private void AddHandler(OnTestContextStepExecutionEndEvent message)
        {
            var options = message.TestContextStep.Parameters[0] as AddOptions;
            var expectedResult = string.Format(AddExpectedResultFormat, options.X, options.Y, options.X + options.Y);
            var actualResult = _commander.GetElementText("add-result");

            var monitorResult = new MonitorResult
            {
                Actual = actualResult,
                Expected = expectedResult,
                ComparisonTypeCode = ComparisonType.Equality.Code
            };
            _monitorService.AddMonitorResult(monitorResult);
        }

        public int Index { get { return 100; } }
    }
}