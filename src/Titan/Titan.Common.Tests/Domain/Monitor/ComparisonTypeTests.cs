using NUnit.Framework;
using Shouldly;
using Titan.Common.Domain.Monitor;

namespace Titan.Common.Tests.Domain.Monitor
{
    public class ComparisonTypeTests
    {
        [Test]
        public void ComparisonType_AllTypes()
        {
            var ct = ComparisonType.Equality;
            ct.Name.ShouldBe("equality");
            ct.Code.ShouldBe("B03C6897-97A3-4F5A-B16A-51DD6A9FF39D");
        }
    }
}