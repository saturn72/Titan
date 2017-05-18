using NUnit.Framework;
using Titan.Framework.Runtime;
using Shouldly;

namespace Titan.Framework.Tests.Runtime
{
    [Category("unit_test")]
    public class ExecutionResultStepTests
    {
        [Test]
        public void ExecutionResultStep_BuildExecutionresultStep()
        {
            const string msg1 = "failed message";
            var step1 = ExecutionResultStep.Build(ExecutionStatus.Failed, msg1);
            step1.Status.ShouldBe(ExecutionStatus.Failed);
            step1.Message.ShouldBe(msg1);

            const string msg2 = "passed message";
            var step2 = ExecutionResultStep.Build(ExecutionStatus.Passed, msg2);
            step2.Status.ShouldBe(ExecutionStatus.Passed);
            step2.Message.ShouldBe(msg2);

        }
    }
}
