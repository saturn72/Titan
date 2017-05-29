using System.Collections.Generic;
using Calculator.Test.Framework.SystemBlockOptions;
using Calculator.Test.Framework.SystemBlocks;
using Titan.Framework.Runtime;

namespace Calculator.Test.Framework.Testing
{
    public class CalculatorTestLogic
    {
        private readonly ICalculator _calculatorCommander;

        public CalculatorTestLogic(ICalculator calculatorCommander)
        {
            _calculatorCommander = calculatorCommander;
        }

        public virtual IEnumerable<ExecutionResult> Add(AddOptions options)
        {
            return new[]
            {
                _calculatorCommander.Add(options)
            };
        }
    }
}