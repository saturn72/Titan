namespace Titan.Framework.Runtime
{
    public sealed class ExecutionStatus
    {
        public static readonly ExecutionStatus Initialized = new ExecutionStatus("Initialized",
           "3709C8AD-06BB-433B-80C1-F9340FE7E950");

        public static readonly ExecutionStatus Disposed = new ExecutionStatus("Disposed",
           "5DFA3501-1C03-4D69-9C24-6B99CD972092");

        public static readonly ExecutionStatus Unknown = new ExecutionStatus("Unknown",
           "CAB34FC5-1B21-4FA4-BF86-B602730E6852");

        public static readonly ExecutionStatus Started = new ExecutionStatus("Started",
           "8AE0ACD2-0F92-4099-A43D-8790299E88E3");

        public static readonly ExecutionStatus Ended = new ExecutionStatus("Ended",
           "C0E80FF4-A022-4D82-A55E-AFD8B5126B9E");


        public static readonly ExecutionStatus Passed = new ExecutionStatus("Passed",
            "1FCEE656-3B59-4F9C-BCFB-2E6B93EC9B79");

        public static readonly ExecutionStatus Failed = new ExecutionStatus("Failed",
            "8F064488-5402-4DAB-AD02-6CFF5188E28B");

        public static readonly ExecutionStatus Info = new ExecutionStatus("Info", "8AB19822-9E30-4A07-8DF6-3593A1B0BF92");

        private readonly string _code;
        private readonly string _name;

        private ExecutionStatus(string name, string code)
        {
            _name = name;
            _code = code;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Code
        {
            get { return _code; }
        }
    }
}