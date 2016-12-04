using System;
using System.Linq;
using Common.Testing;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace TestBuilder.UnitTests
{
    public class TestBuilderManagerTests
    {
        [Test]
        public void TestBuildeManager_AddsTestStep_ThrowsOnEmptyTestName()
        {
            var tb = new TestBuildManager();
            typeof(ArgumentException).ShouldBeThrownBy(() => tb.AddStep("", null));
            typeof(NullReferenceException).ShouldBeThrownBy(() => tb.AddStep(null, null));
        }


        [Test]
        public void TestBuildManager_AddTestStep_AddsStepBycollection()
        {
            var tb = new TestBuildManager();
            //Step without Parameters
            var testStepName1 = "TestStwpName1";

            tb.AddStep(testStepName1, null);
            tb.CurrentTest.TestSteps.Count.ShouldEqual(1);

            var testStep = tb.CurrentTest.TestSteps.First();
            testStep.Name.ShouldEqual(testStepName1);
            testStep.Parameters.Count().ShouldEqual(0);


            //step with parameters
            var testStepName2 = "TestStwpName2";
            var parameters = new TestStepParameterCollection();

            var maxParams = 11;
            for (var i = 0; i < maxParams; i++)
                parameters.Add(new TestStepParameterModel(i.ToString(), i));

            tb.AddStep(testStepName2, parameters);
            tb.CurrentTest.TestSteps.Count.ShouldEqual(2);

            testStep = tb.CurrentTest.TestSteps.Last();
            testStep.Name.ShouldEqual(testStepName2);
            testStep.Parameters.Count().ShouldEqual(maxParams);

            for (var i = 0; i < maxParams; i++)
                testStep.Parameters.ElementAt(i).PropertyValuesAreEquals(parameters.ElementAt(i));
        }
    }
}