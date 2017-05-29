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
    }
}
