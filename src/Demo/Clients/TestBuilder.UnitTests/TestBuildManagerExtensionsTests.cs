using System.Collections.Generic;
using System.Linq;
using Common.Testing;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace TestBuilder.UnitTests
{
    public class TestBuildManagerExtensionsTests
    {
        [Test]
        public void TestBuildManager_AddTestStep_NameOnly()
        {
            var tb = new TestBuildManager();
            //Step without Parameters
            var testStepName1 = "TestStwpName1";

            tb.AddStep(testStepName1);

            var testStep = tb.CurrentTest.TestSteps.First();
            testStep.Name.ShouldEqual(testStepName1);
            testStep.Parameters.Count.ShouldEqual(0);
        }

        [Test]
        public void TestBuildManager_AddTestStep_AddsStep_ByParameters()
        {
            var tb = new TestBuildManager();
            //Step without Parameters
            var testStepName1 = "TestStwpName1";
            var testStepParams = new List<TestStepParameterModel>();
            var maxParams = 10;
            for (var i = 0; i < maxParams; i++)
                testStepParams.Add(new TestStepParameterModel(i.ToString(), i));


            //single param
            var firstTestStepParams = testStepParams.First();
            tb.AddStep(testStepName1, firstTestStepParams);

            var testStep = tb.CurrentTest.TestSteps.First();
            testStep.Name.ShouldEqual(testStepName1);
            testStep.Parameters.Count.ShouldEqual(1);
            testStep.Parameters.First().PropertyValuesAreEquals(firstTestStepParams);


            //multiple params
            var testStepName2 = "TestStwpName1";
            tb.AddStep(testStepName1, firstTestStepParams, testStepParams.Skip(1).ToArray());

            testStep = tb.CurrentTest.TestSteps.Last();
            testStep.Name.ShouldEqual(testStepName2);
            testStep.Parameters.Count.ShouldEqual(maxParams);

            for (var i = 0; i < maxParams; i++)
                testStep.Parameters.ElementAt(i).PropertyValuesAreEquals(testStepParams.ElementAt(i));
        }
    }
}