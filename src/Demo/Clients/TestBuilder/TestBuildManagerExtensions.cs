using Common.Testing;

namespace TestBuilder
{
    public static class TestBuildManagerExtensions
    {
        public static void AddStep(this ITestBuildManager builder, string stepName, TestStepParameterModel first,
            params TestStepParameterModel[] parameters)
        {
            var col = new TestStepParameterCollection {first};
            foreach (var p in parameters)
                col.Add(p);

            builder.AddStep(stepName, col);
        }

        public static void AddStep(this ITestBuildManager builder, string stepName)
        {
            builder.AddStep(stepName, new TestStepParameterCollection());
        }
    }
}