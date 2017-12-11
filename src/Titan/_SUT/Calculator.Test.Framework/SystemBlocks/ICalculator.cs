using Calculator.Test.Framework.SystemBlockOptions;
using Titan.Framework.Runtime;

namespace Calculator.Test.Framework.SystemBlocks
{
    public interface ICalculator
    {
        ExecutionResult Add(AddOptions options);
    }
}