namespace Common.Testing
{
    public class TestStepParameterModel
    {
        public TestStepParameterModel(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public object Value { get; }
    }
}