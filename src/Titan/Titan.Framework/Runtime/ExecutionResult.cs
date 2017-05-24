using System.Collections.Generic;
using System.Linq;
using Titan.Framework.Testing;
using Titan.Framework.Validation;

namespace Titan.Framework.Runtime
{
    public class ExecutionResult
    {
        private readonly string _description;

        private readonly TestStepOptionsBase _testStepOptions;
        private ICollection<ExecutionResultStep> _executionResultStep;
        private ICollection<ValidationStep> _validationSteps;

        public ExecutionResult(string description, TestStepOptionsBase testStepOptions)
        {
            _description = description;
            _testStepOptions = testStepOptions;
        }

        public bool HasErrors
        {
            get { return HasExecutionErrors || HasValidationErrors; }
        }

        public bool HasValidationErrors
        {
            get { return ValidationSteps.Any(vs => !vs.Result); }
        }

        public bool HasExecutionErrors
        {
            get { return ExecutionResultSteps.Any(ers => ers.Status == ExecutionStatus.Failed); }
        }

        public string Description
        {
            get { return _description; }
        }

        public TestStepOptionsBase TestStepOptions
        {
            get { return _testStepOptions; }
        }

        public virtual ICollection<ExecutionResultStep> ExecutionResultSteps
        {
            get { return _executionResultStep ?? (_executionResultStep = new List<ExecutionResultStep>()); }
        }

        public ICollection<ValidationStep> ValidationSteps
        {
            get { return _validationSteps ?? (_validationSteps = new List<ValidationStep>()); }
            set { _validationSteps = value; }
        }
    }
}