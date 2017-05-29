using System;
using Calculator.Test.Framework.SystemBlockOptions;
using Calculator.Test.Framework.SystemBlocks;
using Titan.Framework.Runtime;

namespace Calculator.Test.Framework.Selenium.Module
{
    public class SeleniumCalculator : ICalculator
    {
        private readonly ISeleniumCommander _commander;

        public SeleniumCalculator(ISeleniumCommander commander)
        {
            _commander = commander;
        }

        public virtual ExecutionResult Add(AddOptions options)
        {
            var result = new ExecutionResult("perform add operation", options);
            var stepResult = _commander.GoToIndexPage();
            result.ExecutionResultSteps.Add(
                ExecutionResultStep.Build(stepResult ? ExecutionStatus.Passed : ExecutionStatus.Failed,
                    "Go to index page " + (stepResult ? "PASS" : "FAILED")));
            if (!stepResult)
                return result;

            stepResult = _commander.SetElementText("x", options.X);
            result.ExecutionResultSteps.Add(
                ExecutionResultStep.Build(stepResult ? ExecutionStatus.Passed : ExecutionStatus.Failed,
                    "Set X inbox value: " + (stepResult ? "PASS" : "FAILED")));
            if (!stepResult)
                return result;


            stepResult = _commander.SetElementText("y", options.X);
            result.ExecutionResultSteps.Add(
                ExecutionResultStep.Build(stepResult ? ExecutionStatus.Passed : ExecutionStatus.Failed,
                    "Set Y inbox value: " + (stepResult ? "PASS" : "FAILED")));

            stepResult = _commander.Click("add");
            result.ExecutionResultSteps.Add(
                ExecutionResultStep.Build(stepResult ? ExecutionStatus.Passed : ExecutionStatus.Failed,
                    "Click Add button: " + (stepResult ? "PASS" : "FAILED")));

            return result;
        }
    }
}