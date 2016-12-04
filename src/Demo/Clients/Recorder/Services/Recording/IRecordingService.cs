using Common.Testing;

namespace Recorder.Services.Recording
{
    public interface IRecordingService
    {
        void LaunchNotepad();
        void WriteText(string content);
        void SaveDocument();
        TestModel CurrentTest { get;}
    }
}