using Common.Testing;

namespace TestBuilder
{
    public interface ITestBuildManager
    {
        TestModel CurrentTest { get; }
        void AddStep(string stepName, TestStepParameterCollection parameters);
    }
}