using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Shouldly;
using Titan.Framework.CommandAndQuery;
using Titan.Framework.Runtime;
using Titan.Framework.SystemTests.TestStepOptions;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests
{
    public class ValidateRuntimeEvents : TestBase
    {
        [Test]
        public void SimpleTest()
        {
            var tl1 = AppEngine.Current.Resolve<TestLogic>();
            tl1.SimpleFlow();
        }

        [TearDown]
        public void Teardown()
        {
            //Assert;
            var allProps = typeof(EventListener).GetProperties(BindingFlags.Public | BindingFlags.Static);
            var onAppDisposalEvents = new[]
            {
                "OnApplicationStopStartEventHandler",
                "OnApplicationStopFinishEventHandler",
                "OnTestSuiteExecutionEndEventHandler", "OnTestSuiteDisposedEventHandler"
            };

            foreach (var pi in allProps)
            {
                //test suite ended and test suite disposal events are triggered as last operation befor folding the app
                var value = (bool) pi.GetValue(null, null);
                if (onAppDisposalEvents.Contains(pi.Name))
                    value.ShouldBeFalse(pi.Name);
                else
                    value.ShouldBeTrue(pi.Name);
            }
        }
    }

    public class TestLogic
    {
        private readonly TestSteps _testSteps;

        public TestLogic(TestSteps testSteps)
        {
            _testSteps = testSteps;
        }

        public virtual IEnumerable<ExecutionResult> SimpleFlow()
        {
            return new List<ExecutionResult>
            {
                _testSteps.TestStep1(new TestStep1Options {Value = "test-step1-value"}),

                _testSteps.TestStep2(new TestStep2Options {Value = "test-step2-value"})
            };
        }
    }

    public class TestSteps
    {
        private readonly IDummyCommander1 _commander1;
        private readonly IDummyCommander2 _commander2;

        public TestSteps(IDummyCommander1 commander1, IDummyCommander2 commander2)
        {
            _commander1 = commander1;
            _commander2 = commander2;
        }

        public virtual ExecutionResult TestStep1(TestStep1Options options)
        {
            _commander1.Do1();
            return new ExecutionResult("Test step1", options);
        }

        public virtual ExecutionResult TestStep2(TestStep2Options options)
        {
            _commander2.Do2();
            return new ExecutionResult("Test step2", options);
        }
    }

    public interface IDummyCommander1:ICommander
    {
        void Do1();
    }

    public class DummyCommander : IDummyCommander1, IDummyCommander2
    {
        public void Do1()
        {
        }

        public void Do2()
        {
        }
    }

    public interface IDummyCommander2:ICommander
    {
        void Do2();
    }
}