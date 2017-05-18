using NUnit.Framework;
using Titan.Framework.Runtime;
using Shouldly;
using Titan.Framework.Validation;

namespace Titan.Framework.Tests.Runtime
{
    [Category("unit_test")]
    public class ExecutionResultTests
    {
        [Test]
        public void ExecutionResult_Errors()
        {
            var er1 = new ExecutionResult("ss", null);
            er1.ExecutionResultSteps.Add(ExecutionResultStep.Build(ExecutionStatus.Passed, "message"));
            er1.HasErrors.ShouldBeFalse();
            er1.HasExecutionErrors.ShouldBeFalse();
            er1.HasValidationErrors.ShouldBeFalse();

            er1.ExecutionResultSteps.Add(ExecutionResultStep.Build(ExecutionStatus.Failed, "message"));
            er1.HasErrors.ShouldBeTrue();
            er1.HasExecutionErrors.ShouldBeTrue();
            er1.HasValidationErrors.ShouldBeFalse();

            var er2 = new ExecutionResult("ss", null);
            er2.ValidationSteps.Add(ValidationStep.BuildValidationResponseStep(false, "message"));
            er2.HasErrors.ShouldBeTrue();
            er2.HasExecutionErrors.ShouldBeFalse();
            er2.HasValidationErrors.ShouldBeTrue();
        }
    }
}
