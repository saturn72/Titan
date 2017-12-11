using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Saturn72.Core.Services.Events;
using Titan.Common.Domain.Monitor;
using Titan.Framework.Lifetime.Events;
using Titan.Framework.Monitors;
using Titan.Services.Monitor;

namespace Calculator.Test.Framework.Monitors
{
    public class RestMonitor : IMonitor,
        IEventSubscriber<OnBeforeTestContextExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepExecutionEndEvent>
    {
        private static readonly string ServerUri = "http://localhost:9000/";
        private static readonly string CalculatorResource = "api/calculator";

        private static IEnumerable<Expression> _beforeAddExpressions;
        private readonly IMonitorResultService _monitorService;

        public RestMonitor(IMonitorResultService monitorService)
        {
            _monitorService = monitorService;
        }

        public void HandleEvent(OnBeforeTestContextExecutionStartEvent eventMessage)
        {
            if (eventMessage.TestContext.Name == "Add")
                _beforeAddExpressions = GetAllExpressions();
        }

        public void HandleEvent(OnTestContextStepExecutionEndEvent eventMessage)
        {
            var actualExpressions = GetAllExpressions();

            var actualCount = actualExpressions.Count();
            var expectedCount = _beforeAddExpressions.Count() + 1;

            var monitorResult = new MonitorResult
            {
                Actual = actualCount.ToString(),
                ActualType = actualCount.GetType().FullName,
                Expected = expectedCount.ToString(),
                ExpectedType = expectedCount.GetType().FullName,
                ComparisonTypeCode = ComparisonType.Equality.Code
            };
            _monitorService.AddMonitorResult(monitorResult);
        }

        public int Index => 1;

        private IEnumerable<Expression> GetAllExpressions()
        {
            var req = (IRestRequest) new RestRequest(CalculatorResource, Method.GET);
            var response = GetRestClient().Execute<List<Expression>>(req);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("Failed to get rest data");
            return response.Data;
        }


        private IRestClient GetRestClient()
        {
            return new RestClient(ServerUri);
        }
    }
}