namespace Common.Testing
{
    public class TestStepModel
    {
        private TestStepParameterCollection _parameters;
        public string Name { get; set; }

        public TestStepParameterCollection Parameters
        {
            get { return _parameters ?? (_parameters = new TestStepParameterCollection()); }
            set { _parameters = value; }
        }
    }
}