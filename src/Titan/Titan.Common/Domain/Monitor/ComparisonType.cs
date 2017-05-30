namespace Titan.Common.Domain.Monitor
{
    public sealed class ComparisonType
    {
        private ComparisonType(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Code { get; }
        public string Name { get;  }

        public static ComparisonType Equality = new ComparisonType("equality", "B03C6897-97A3-4F5A-B16A-51DD6A9FF39D");

        public static ComparisonType ExpectedIsSubsetOfActual = new ComparisonType("expected-is-subset-of-actual",
            "DBA9D119-0BAF-4AB8-BD98-0DE38683ED2C");
    }
}
