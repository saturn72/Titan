namespace Titan.Framework.Runtime
{
    public class ExecutionResultStep
    {
        private readonly string _message;
        private readonly ExecutionStatus _status;

        private ExecutionResultStep(ExecutionStatus status, string message)
        {
            _status = status;
            _message = message;
        }

        public ExecutionStatus Status
        {
            get { return _status; }
        }

        public string Message
        {
            get { return _message; }
        }

        public static ExecutionResultStep Build(ExecutionStatus status, string message)
        {
            return new ExecutionResultStep(status, message);
        }
    }
}