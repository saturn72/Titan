using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Saturn72.Core.Services.Localization;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Domain.Clients;
using Titan.Common.Domain.Execution;
using Titan.Common.Domain.Testing;
using Titan.Server.Services.Clients;
using Titan.Server.Services.Execution;
using Titan.Server.Services.Testing;
using Titan.WebApi.Controllers;
using Titan.WebApi.Models.Execution;

namespace Titan.WebApi.Tests.Controllers
{
    public class TestExecutionControllerTests
    {
        [Test]
        public void Post_ReturnsBadRequest()
        {
            const string err = "Cannot parse submitted request";

            var lclService = new Mock<ILocaleService>();
            lclService.Setup(l => l.GetLocaleResource(It.IsAny<string>(), It.IsAny<int>(), "", false))
                .Returns(err);

            var ctrl = new TestExecutionController(null, null, null, lclService.Object);
            //OnNullExecutionRequest
            var res = ctrl.Post(null, null).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);

            var teReq = new TestExecutionRequestModel();
            //On empty test Id
            res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);

            teReq.TestId = default(long);
            res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);

            //On empty automation client Id
            teReq.TestId = 123;

            res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);

            teReq.AutomationClientId = default(long);
            res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);
        }

        [Test]
        public void Post_ReturnsBadRequest_OnNotExistTestId()
        {
            const string err = "Cannot find automation test";

            var testSrv = new Mock<ITestService>();
            testSrv.Setup(t => t.GetTestByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult((TestDomainModel) null));

            var lclSrv = new Mock<ILocaleService>();
            lclSrv.Setup(l => l.GetLocaleResource(It.IsAny<string>(), It.IsAny<int>(), "", false))
                .Returns(err);

            var ctrl = new TestExecutionController(testSrv.Object, null, null, lclSrv.Object);
            var teReq = new TestExecutionRequestModel
            {
                TestId = 123
            };
            var res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);
        }

        [Test]
        public void Post_ReturnsBadRequest_OnNotExistClientId()
        {
            const string err = "Cannot find automation client";

            var testSrv = new Mock<ITestService>();
            testSrv.Setup(t => t.GetTestByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(new TestDomainModel()));

            var clientSrv = new Mock<IAutomationClientService>();
            clientSrv.Setup(t => t.GetClientByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult((AutomationClientDomainModel) null));


            var lclSrv = new Mock<ILocaleService>();
            lclSrv.Setup(l => l.GetLocaleResource(It.IsAny<string>(), It.IsAny<int>(), "", false))
                .Returns(err);

            var ctrl = new TestExecutionController(testSrv.Object, clientSrv.Object, null, lclSrv.Object);
            var teReq = new TestExecutionRequestModel
            {
                TestId = 123,
                AutomationClientId = 123
            };
            var res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
            res.Message.ShouldEqual(err);
        }

        [Test]
        public void Post_ReturnsBadRequest_OnFailToSubmitsRequestToExecutionService()
        {
            const string err = "Unknown error. Fail to submit request for execution. Please inspect log.";

            var testSrv = new Mock<ITestService>();
            testSrv.Setup(t => t.GetTestByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(new TestDomainModel()));

            var clientSrv = new Mock<IAutomationClientService>();
            clientSrv.Setup(t => t.GetClientByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(new AutomationClientDomainModel()));

            var lclSrv = new Mock<ILocaleService>();
            lclSrv.Setup(l => l.GetLocaleResource(It.IsAny<string>(), It.IsAny<int>(), "", false))
                .Returns(err);

            var exeService = new Mock<IExecutionService>();
            exeService.Setup(e => e.CreateExecutionRequest(It.IsAny<TestExecutionRequestDomainModel>()))
                .Returns(Task.FromResult((long) 0));

            var ctrl = new TestExecutionController(testSrv.Object, clientSrv.Object, exeService.Object, lclSrv.Object);
            var teReq = new TestExecutionRequestModel
            {
                TestId = 123,
                AutomationClientId = 123
            };

            var res = ctrl.Post(null, teReq).Result as BadRequestErrorMessageResult;
            res.ShouldNotBeNull();
        }

        [Test]
        public void Post_ReturnsBadRequest_SubmitsrequestToService()
        {
            const long testExecutionRequestId = 123;

            var testSrv = new Mock<ITestService>();
            testSrv.Setup(t => t.GetTestByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(new TestDomainModel()));

            var clientSrv = new Mock<IAutomationClientService>();
            clientSrv.Setup(t => t.GetClientByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(new AutomationClientDomainModel()));

            var exeService = new Mock<IExecutionService>();
            exeService.Setup(e => e.CreateExecutionRequest(It.IsAny<TestExecutionRequestDomainModel>()))
                .Returns(Task.FromResult(testExecutionRequestId));

            var ctrl = new TestExecutionController(testSrv.Object, clientSrv.Object, exeService.Object, null);
            var teReq = new TestExecutionRequestModel
            {
                TestId = 123,
                AutomationClientId = 123
            };

            var res = ctrl.Post(null, teReq).Result as OkNegotiatedContentResult<long>;
            res.ShouldNotBeNull();
            res.Content.ShouldEqual(testExecutionRequestId);
        }
    }
}