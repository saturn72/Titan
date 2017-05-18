using System.Collections.Generic;
using System.Linq;
using Titan.Framework.Pump;
using Titan.Framework.Validation;

namespace Titan.Framework.Runtime
{
    public class ExecutionResult
    {
        private readonly string _description;

        private readonly PumpOptionsBase _pumpOptions;
        private ICollection<ExecutionResultStep> _executionResultStep;
        private ICollection<ValidationStep> _validationSteps;

        public ExecutionResult(string description, PumpOptionsBase pumpOptions)
        {
            _description = description;
            _pumpOptions = pumpOptions;
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

        public PumpOptionsBase PumpOptions
        {
            get { return _pumpOptions; }
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