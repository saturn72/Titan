
namespace Calculator.WebApi.Host.Domain
{
    public class Expression
    {
        public long Id { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public Operator Operator { get; set; }
        public int Result { get; set; }
    }
}
