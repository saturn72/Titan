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
        IEventSubscriber<OnBeforeTestContextStepPartExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepExecutionEndEvent>
    {
        private static readonly string ServerUri = "http://localhost:9000/";
        private static readonly string CalculatorResource = "api/calculator";

        private static IEnumerable<Expression> _beforeAddExpressions;
        private readonly IMonitorService _monitorService;

        public RestMonitor(IMonitorService monitorService)
        {
            _monitorService = monitorService;
        }

        public void HandleEvent(OnBeforeTestContextStepPartExecutionStartEvent eventMessage)
        {
            if (eventMessage.TestContextStepPart.Name == "Add")
                _beforeAddExpressions = GetAllExpressions();
        }

        public void HandleEvent(OnTestContextStepExecutionEndEvent eventMessage)
        {
            var actualExpressions = GetAllExpressions();

            var monitorResult = new MonitorResult
            {
                Actual = JsonConvert.SerializeObject(actualExpressions),
                ActualType = actualExpressions.GetType().FullName,
                Expected = JsonConvert.SerializeObject(_beforeAddExpressions),
                ExpectedType = _beforeAddExpressions.GetType().FullName,
                ComparisonTypeCode = ComparisonType.ExpectedIsSubsetOfActual.Code
            };
            _monitorService.AddMonitorResult(monitorResult);

            var actualCount = actualExpressions.Count();
            var expectedCount = _beforeAddExpressions.Count() + 1;

            monitorResult = new MonitorResult
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