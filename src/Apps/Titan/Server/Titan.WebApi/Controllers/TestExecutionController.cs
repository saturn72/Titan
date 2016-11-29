#region

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Saturn72.Common.WebApi;
using Saturn72.Core.Services.Localization;
using Saturn72.Extensions;
using Titan.Common.Domain.Execution;
using Titan.Server.Services.Clients;
using Titan.Server.Services.Execution;
using Titan.Server.Services.Testing;
using Titan.WebApi.Models.Execution;

#endregion

namespace Titan.WebApi.Controllers
{
    [RoutePrefix("api/exec")]
    public class TestExecutionController : Saturn72ApiControllerBase
    {
        private readonly IAutomationClientService _autoClientService;
        private readonly IExecutionService _exeService;
        private readonly ILocaleService _localeService;
        private readonly ITestService _testService;

        public TestExecutionController(ITestService testService, IAutomationClientService autoClientService,
            IExecutionService exeService, ILocaleService localeService)
        {
            _localeService = localeService;
            _testService = testService;
            _autoClientService = autoClientService;
            _exeService = exeService;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(HttpRequestMessage httpRequest, TestExecutionRequestModel request)
        {
            if (request.IsNull() || (request.TestId == default(long)) ||
                (request.AutomationClientId == default(long)))
                return BadRequest(_localeService.GetLocaleResource("TestExecution.Post.NullRequest"));

            var reqTestId = request.TestId;
            var test = await _testService.GetTestByIdAsync(reqTestId);
            if (test.IsNull())
                return BadRequest(_localeService.GetLocaleResource("TestExecution.Post.TestNotExist"));

            var reqClientId = request.AutomationClientId;
            var autoClient = await _autoClientService.GetClientByIdAsync(reqClientId);
            if (autoClient.IsNull())
                return BadRequest(_localeService.GetLocaleResource("TestExecution.Post.AutoClientNotExist"));
            var testExeRequest = new TestExecutionRequestDomainModel
            {
                TestId = reqTestId,
                ClientId = reqClientId
            };

            var res = await _exeService.CreateExecutionRequest(testExeRequest);
            if(res == default(long))
                return BadRequest(_localeService.GetLocaleResource("TestExecution.Post.RequestSubmitFail"));

            return Ok(res);
        }
    }
}