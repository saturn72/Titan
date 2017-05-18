using System.Collections.Generic;
using System.Linq;
using Saturn72.Extensions;
using Titan.Framework.Validation;

namespace Titan.Framework.Runtime
{
    public static class ExecutionResultExtensions
    {
        public static bool HasError(this ExecutionResultStep step)
        {
            return step.Status == ExecutionStatus.Failed;
        }

        public static void Add(this ExecutionResult executionResult, ExecutionResultStep step)
        {
            executionResult.ExecutionResultSteps.Add(step);
        }

        public static void Add(this ExecutionResult executionResult, IEnumerable<ExecutionResultStep> steps)
        {
            steps.ForEachItem(step => executionResult.ExecutionResultSteps.Add(step));
        }

        public static void Add(this ExecutionResult executionResult, ValidationStep step)
        {
            executionResult.ValidationSteps.Add(step);
        }

        public static void Add(this ExecutionResult executionResult, IEnumerable<ValidationStep> steps)
        {
            steps.ForEachItem(step => executionResult.ValidationSteps.Add(step));
        }

        public static IEnumerable<string> ValidationSummary(this ExecutionResult executionResult,
            bool? validationResult = null)
        {
            var query = validationResult.HasValue
                ? executionResult.ValidationSteps.Where(v => v.Result == validationResult)
                : executionResult.ValidationSteps;

            return query.Select(v => v.Message).ToArray();
        }

        public static IEnumerable<string> ExecutionSummary(this ExecutionResult executionResult,
            ExecutionStatus status = null)
        {
            var query = status.NotNull()
                ? executionResult.ExecutionResultSteps.Where(v => v.Status == status)
                : executionResult.ExecutionResultSteps;

            return query.Select(v => v.Message).ToArray();
        }
    }
}