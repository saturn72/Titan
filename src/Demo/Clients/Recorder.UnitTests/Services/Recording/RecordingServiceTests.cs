using System.Linq;
using Common.Testing;
using Moq;
using NUnit.Framework;
using Recorder.Services.Recording;
using Saturn72.UnitTesting.Framework;
using TestBuilder;

namespace Recorder.UnitTests.Services.Recording
{
    public class RecordingServiceTests
    {
        [Test]
        public void RecordingService_LaunchNotepad_AddsTestStep()
        {
            string testName = null;

            var builder = new Mock<ITestBuildManager>();
            builder.Setup(b => b.AddStep(It.IsAny<string>(), It.IsAny<TestStepParameterCollection>()))
                .Callback<string, TestStepParameterCollection>((c, p) => testName = c);

            var rs = new RecordingService(builder.Object);
            rs.LaunchNotepad();
            testName.ShouldEqual("Launch");
        }

        [Test]
        public void RecordingService_WriteText_AddsTestStep()
        {
            string expectedContent = null,
                testName = null,
                sentContent = "this is beautiful sentence with\nnew lines";

            var builder = new Mock<ITestBuildManager>();
            builder.Setup(b => b.AddStep(It.IsAny<string>(), It.IsAny<TestStepParameterCollection>()))
                .Callback<string, TestStepParameterCollection>((tname, prms) =>
                {
                    testName = tname;
                    expectedContent = prms.First().Value.ToString();
                });

            IRecordingService rs = new RecordingService(builder.Object);
            rs.WriteText(sentContent);
            testName.ShouldEqual("WriteContent");
            expectedContent.ShouldEqual(sentContent);
        }


        [Test]
        public void RecordingService_SaveDocument()
        {
            var builder = new Mock<ITestBuildManager>();
            string testName = null;
            TestStepParameterCollection expectedContent = null;

            builder.Setup(b => b.AddStep(It.IsAny<string>(), It.IsAny<TestStepParameterCollection>()))
                .Callback<string, TestStepParameterCollection>((tname, prms) =>
                {
                    testName = tname;
                    expectedContent = prms;
                });

            IRecordingService rs = new RecordingService(builder.Object);
            rs.SaveDocument();

            testName.ShouldEqual("SaveDocument");
            expectedContent.Count.ShouldEqual(0);
        }

        [Test]
        public void RecordingService_GetCurrentTest()
        {
            var builder = new Mock<ITestBuildManager>();

            var expectedresult = new TestModel {Name = "This is name"};
            builder.Setup(b => b.CurrentTest)
                .Returns(expectedresult);

            IRecordingService rs = new RecordingService(builder.Object);
            rs.CurrentTest.PropertyValuesAreEquals(expectedresult);
        }

    }
}