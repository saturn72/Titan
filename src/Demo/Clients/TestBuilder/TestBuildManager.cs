using Common.Testing;
using Saturn72.Extensions;

namespace TestBuilder
{
    public class TestBuildManager : ITestBuildManager
    {
        public TestModel CurrentTest { get; private set; }

        public void AddStep(string stepName, TestStepParameterCollection parameters)
        {
            Guard.HasValue(stepName, "Missing argument value " + nameof(stepName));
            AddTestStep(stepName, parameters, GetOrCreateTest());
        }

        #region Utilities

        protected void AddTestStep(string stepName, TestStepParameterCollection parameters, TestModel testModel)
        {
            testModel.TestSteps.Add(new TestStepModel
            {
                Name = stepName,
                Parameters = parameters
            });
        }

        protected TestModel GetOrCreateTest()
        {
            return CurrentTest ?? (CurrentTest = new TestModel());
        }

        #endregion
    }
}