using System.Collections.Generic;
using Calculator.Test.Framework.SystemBlockOptions;
using Calculator.Test.Framework.SystemBlocks;
using Titan.Framework.Runtime;

namespace Calculator.Test.Framework.Testing
{
    public class CalculatorTestLogic
    {
        private readonly ICalculator _calculator;

        public CalculatorTestLogic(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public virtual IEnumerable<ExecutionResult> Add(AddOptions options)
        {
            return new[]
            {
                _calculator.Add(options)
            };
        }
    }
}