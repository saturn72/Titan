using System;
using System.Collections.Generic;
using NUnit.Framework;
using Titan.Framework.Runtime;
using Titan.Framework.SystemTests.TestStepOptions;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests
{
    public class ValidateRuntimeEvents
    {
        [Test]
        public void SimpleTest()
        {
            var tl = new TestLogic();
            tl.SimpleFlow();

            //need to ise autofac module as this component is responsible on activating the interceptors
            throw new NotImplementedException();
        }
    }

    public class TestLogic : TestBase
    {
        public virtual IEnumerable<ExecutionResult> SimpleFlow()
        {
            var testSteps = new TestSteps();

            return new List<ExecutionResult>
            {
                testSteps.TestStep1(new TestStep1Options {Value = "test-step1-value"}),

                testSteps.TestStep2(new TestStep2Options {Value = "test-step2-value"}),

            };
        }
    }

    public class TestSteps
    {
        public virtual ExecutionResult TestStep1(TestStep1Options options)
        {
            return new ExecutionResult("Test step1", options);
        }

        public virtual ExecutionResult TestStep2(TestStep2Options options)
        {
            return new ExecutionResult("Test step2", options);
        }

    }
}