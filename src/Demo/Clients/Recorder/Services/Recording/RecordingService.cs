using Common.Testing;
using TestBuilder;

namespace Recorder.Services.Recording
{
    public class RecordingService : IRecordingService
    {
        private readonly ITestBuildManager _testBuildManager;

        public RecordingService(ITestBuildManager testBuildManager)
        {
            _testBuildManager = testBuildManager;
        }

        public void LaunchNotepad()
        {
            _testBuildManager.AddStep("Launch");
        }

        public void WriteText(string content)
        {
            _testBuildManager.AddStep("WriteContent", new TestStepParameterModel("content", content));
        }

        public void SaveDocument()
        {
            _testBuildManager.AddStep("SaveDocument");
        }

        public TestModel CurrentTest
        {
            get { return _testBuildManager.CurrentTest; }
        }
    }
}