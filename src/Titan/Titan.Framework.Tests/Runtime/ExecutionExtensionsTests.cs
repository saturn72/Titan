using System.Linq;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Runtime;
using Titan.Framework.Validation;

namespace Titan.Framework.Tests.Runtime
{
    [Category("unit_test")]
    public class ExecutionExtensionsTests
    {
        [Test]
        public void ExecutionExtension_ExecutionStepHasError()
        {
            var hasErrorStep = ExecutionResultStep.Build(ExecutionStatus.Failed, "has error");
            hasErrorStep.HasError().ShouldBeTrue();

            var infoStep = ExecutionResultStep.Build(ExecutionStatus.Info, "has info");
            infoStep.HasError().ShouldBeFalse();

            var passStep = ExecutionResultStep.Build(ExecutionStatus.Passed, "passed");
            passStep.HasError().ShouldBeFalse();
        }

        [Test]
        public void ExecutionExtension_AddExecutionResultStep()
        {
            var er1 = new ExecutionResult("ss", null);
            er1.Add(ExecutionResultStep.Build(ExecutionStatus.Passed, "message"));
            er1.ExecutionResultSteps.Any().ShouldBeTrue();
        }

        [Test]
        public void ExecutionExtension_AddExecutionResultStepCollection()
        {
            var er1 = new ExecutionResult("ss", null);
            er1.Add(new[]
            {
                ExecutionResultStep.Build(ExecutionStatus.Passed, "message"),
                ExecutionResultStep.Build(ExecutionStatus.Passed, "message"),
                ExecutionResultStep.Build(ExecutionStatus.Passed, "message"),
            });

            er1.ExecutionResultSteps.Count.ShouldBe(3);
        }


        [Test]
        public void ExecutionExtension_AddValidationStep()
        {
            var er1 = new ExecutionResult("ss", null);
            er1.Add(ValidationStep.BuildValidationResponseStep(false, "message"));
            er1.ValidationSteps.Any().ShouldBeTrue();
        }


        [Test]
        public void ExecutionExtension_AddValidationStepCollection()
        {
            var er1 = new ExecutionResult("ss", null);
            er1.Add(new[]
            {
                ValidationStep.BuildValidationResponseStep(false, "message"),
                ValidationStep.BuildValidationResponseStep(false, "message"),
                ValidationStep.BuildValidationResponseStep(false, "message")
            });
            er1.ValidationSteps.Count.ShouldBe(3);
        }


        [Test]
        public void ExecutionExtension_ValidationSummary()
        {
            string msg1 = "message1",
                msg2 = "message2",
                msg3 = "message3";

            var er = new ExecutionResult("ss", null);
            er.Add(new[]
            {
                ValidationStep.BuildValidationResponseStep(true, msg1),
                ValidationStep.BuildValidationResponseStep(false, msg2),
                ValidationStep.BuildValidationResponseStep(false, msg3)
            });
            var vs1 = er.ValidationSummary();
            vs1.Count().ShouldBe(3);
            vs1.Contains(msg1).ShouldBeTrue();
            vs1.Contains(msg2).ShouldBeTrue();
            vs1.Contains(msg3).ShouldBeTrue();

            var vs2 = er.ValidationSummary(true);
            vs2.Count().ShouldBe(1);
            vs2.Contains(msg1).ShouldBeTrue();

            var vs3 = er.ValidationSummary(false);
            vs3.Count().ShouldBe(2);
            vs3.Contains(msg2).ShouldBeTrue();
            vs3.Contains(msg3).ShouldBeTrue();
        }

        [Test]
        public void ExecutionExtension_executionSummary()
        {
            string msg1 = "message1",
                msg2 = "message2",
                msg3 = "message3";

            var er = new ExecutionResult("ss", null);
            er.Add(new[]
            {
                ExecutionResultStep.Build(ExecutionStatus.Passed, msg1),
                ExecutionResultStep.Build(ExecutionStatus.Failed, msg2),
                ExecutionResultStep.Build(ExecutionStatus.Failed, msg3)
            });
            var es1 = er.ExecutionSummary();
            es1.Count().ShouldBe(3);
            es1.Contains(msg1).ShouldBeTrue();
            es1.Contains(msg2).ShouldBeTrue();
            es1.Contains(msg3).ShouldBeTrue();

            var es2 = er.ExecutionSummary(ExecutionStatus.Passed);
            es2.Count().ShouldBe(1);
            es2.Contains(msg1).ShouldBeTrue();

            var es3 = er.ExecutionSummary(ExecutionStatus.Failed);
            es3.Count().ShouldBe(2);
            es3.Contains(msg2).ShouldBeTrue();
            es3.Contains(msg3).ShouldBeTrue();
        }

    }
}